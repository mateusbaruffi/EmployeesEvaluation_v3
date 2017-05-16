/*
 ** Entity Framework DbContext class 
 */
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EmployeesEvaluation.Core.Models;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EmployeesEvaluation.Repository
{
    public class EmployeesEvaluationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Evaluation> Evaluation { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<EvaluationQuestion> EvaluationQuestion { get; set; }
        public DbSet<EvaluationResponse> EvaluationResponse { get; set; }
        public DbSet<LikertAnswer> LikertAnswer { get; set; }
        public DbSet<Season> Season { get; set; }
        public DbSet<UserRelation> UserRelation { get; set; }

        private IHttpContextAccessor _httpContextAccessor;
        private HttpContext _httpContext { get { return _httpContextAccessor.HttpContext; } }

        public EmployeesEvaluationContext (IHttpContextAccessor contextAccessor, DbContextOptions<EmployeesEvaluationContext> options) : base(options) {
            _httpContextAccessor = contextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Set ON DELETE no Action
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Using fluent API instead data annotations to preserve the domain model
            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.UserType)
                .HasDefaultValue(UserType.EMP);

            modelBuilder.Entity<UserRelation>()
                .HasKey(ur => new { ur.DepartmentManagerId, ur.EmployeeId });

            modelBuilder.Entity<UserRelation>()
                .HasOne(ur => ur.Employee)
                .WithMany(u => u.EmployeesRelated)
                .HasForeignKey(ur => ur.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRelation>()
                .HasOne(ur => ur.DepartmentManager)
                .WithMany(u => u.DepartmentManagersRelated)
                .HasForeignKey(ur => ur.DepartmentManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
                .ToTable("Departments");

            modelBuilder.Entity<Department>()
                .Property(d => d.CreatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Department>()
                .Property(d => d.UpdatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Evaluation>()
                .ToTable("Evaluations").Ignore(e => e.Questions);

            modelBuilder.Entity<Evaluation>()
                .HasOne(e => e.DepartmentManager);

            modelBuilder.Entity<Evaluation>()
                 .HasOne(e => e.Season)
                 .WithMany(s => s.Evaluations);

            modelBuilder.Entity<EvaluationResponse>()
                   .ToTable("EvaluationResponses");

            modelBuilder.Entity<EvaluationResponse>()
                   .HasOne(er => er.Evaluation)
                   .WithMany(e => e.EvaluationResponses)
                   .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EvaluationResponse>()
                   .HasOne(er => er.Employee)
                   .WithMany(au => au.EvaluationResponses);

            modelBuilder.Entity<QuestionAnswer>()
                .HasOne(qa => qa.EvaluationResponse)
                .WithMany(evr => evr.QuestionAnswers)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuestionAnswer>()
                .HasOne(qa => qa.Question);

            modelBuilder.Entity<QuestionAnswer>()
                .HasIndex(qa => qa.LikertAnswerId);

            modelBuilder.Entity<Question>()
                .ToTable("Questions");

            modelBuilder.Entity<Question>()
                .Property(d => d.CreatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Question>()
                .Property(d => d.UpdatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Ownership);

            modelBuilder.Entity<LikertAnswer>()
                .HasOne(la => la.Question)
                .WithMany(q => q.LikertAnswers)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Season>()
                .ToTable("Seasons");

            modelBuilder.Entity<EvaluationQuestion>()
              .ToTable("EvaluationQuestion");

            modelBuilder.Entity<EvaluationQuestion>()
                .HasOne(eq => eq.Evaluation)
                .WithMany(e => e.EvaluationQuestions)
                .HasForeignKey(eq => eq.EvaluationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EvaluationQuestion>()
                .HasOne(eq => eq.Question)
                .WithMany(q => q.EvaluationQuestions)
                .HasForeignKey(eq => eq.QuestionId);

            modelBuilder.Entity<EvaluationAssigned>()
              .ToTable("EvaluationAssigned");

            modelBuilder.Entity<EvaluationAssigned>()
                .HasOne(ea => ea.Evaluation)
                .WithMany(e => e.EvaluationsAssigned)
                .HasForeignKey(ea => ea.EvaluationId);

            modelBuilder.Entity<EvaluationAssigned>()
                .HasOne(ea => ea.Employee)
                .WithMany(au => au.EvaluationsAssigned)
                .HasForeignKey(ea => ea.EmployeeId);


        }

        public override int SaveChanges()
        {
            SetAuditingData();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetAuditingData();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditingData()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));

            if (_httpContext != null)
            {
                if (_httpContext.User != null)
                {
                    var claimsIdentity = (ClaimsIdentity)_httpContext.User.Identity;
                    var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                    var userId = claim.Value;


                    var currentUsername = !string.IsNullOrEmpty(userId)
                        ? userId
                        : "Anonymous";

                    foreach (var entity in entities)
                    {
                        if (entity.State == EntityState.Added)
                        {
                            ((EntityBase)entity.Entity).CreatedAt = DateTime.UtcNow;
                            ((EntityBase)entity.Entity).CreatedBy = currentUsername;
                        }

                        ((EntityBase)entity.Entity).UpdatedAt = DateTime.UtcNow;
                        ((EntityBase)entity.Entity).UpdatedBy = currentUsername;
                    }
                }
               

            }
           
        }


    }
}