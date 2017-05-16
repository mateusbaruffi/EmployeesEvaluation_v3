using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EmployeesEvaluation.WEB.Services;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Repository;
using EmployeesEvaluation.Repository.Repositories;
using EmployeesEvaluation.Repository.Repositories.Impl;
using EmployeesEvaluation.Services;
using EmployeesEvaluation.Services.Impl;
using EmployeesEvaluation.WEB.Dtos;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace EmployeesEvaluation.WEB
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<EmployeesEvaluationContext>(options =>
                options.UseSqlServer(Configuration["Data:Connection:ConnectionString"],
                    b => b.MigrationsAssembly("EmployeesEvaluation.WEB")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<EmployeesEvaluationContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddKendo();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            // Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IDepartmentService, DepartmentService>();

            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IQuestionService, QuestionService>();

            services.AddScoped<ISeasonRepository, SeasonRepository>();
            services.AddTransient<ISeasonService, SeasonService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

            services.AddScoped<IUserRelationRepository, UserRelationRepository>();
            services.AddTransient<IUserRelationService, UserRelationService>();

            services.AddScoped<IEvaluationRepository, EvaluationRepository>();
            services.AddTransient<IEvaluationService, EvaluationService>();

            services.AddScoped<IEvaluationResponseRepository, EvaluationResponseRepository>();
            services.AddScoped<IEvaluationQuestionRepository, EvaluationQuestionRepository>();
            services.AddScoped<IEvaluationAssignedRepository, EvaluationAssignedRepository>();
            services.AddScoped<ILikertAnswerRepository, LikertAnswerRepository>();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ClaimsPrincipal>(
                s => s.GetService<IHttpContextAccessor>().HttpContext.User);

            services.Configure<AuthMessageSenderOptions>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            //loggerFactory.AddDebug(LogLevel.Debug);

            Mapper.Initialize(config =>
            {
                config.CreateMap<ApplicationUser, UserDto>();
                config.CreateMap<UserDto, ApplicationUser>();

                config.CreateMap<Department, DepartmentDto>();
                config.CreateMap<DepartmentDto, Department>();

                config.CreateMap<Season, SeasonDto>();
                config.CreateMap<SeasonDto, Season>();

                config.CreateMap<Question, QuestionDto>();
                config.CreateMap<QuestionDto, Question>();

                config.CreateMap<QuestionType, QuestionTypeDto>();
                config.CreateMap<QuestionTypeDto, QuestionType>();

                config.CreateMap<QuestionAnswer, QuestionAnswerDto>();
                config.CreateMap<QuestionAnswerDto, QuestionAnswer>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.File.FileName));

                config.CreateMap<LikertAnswer, LikertAnswerDto>();
                config.CreateMap<LikertAnswerDto, LikertAnswer>();

                config.CreateMap<Evaluation, EvaluationDto>()
                    .ForMember(dto => dto.Questions, opt => opt.MapFrom(x => x.EvaluationQuestions));

                config.CreateMap<EvaluationDto, Evaluation>();

                config.CreateMap<EvaluationAssigned, EvaluationAssignedDto>();
                config.CreateMap<EvaluationAssignedDto, EvaluationAssigned>();

                config.CreateMap<EvaluationQuestion, QuestionDto>()
                        .ForMember(dto => dto.Id, opt => opt.MapFrom(e => e.Question.Id))
                        .ForMember(dto => dto.Description, opt => opt.MapFrom(e => e.Question.Description))
                        .ForMember(dto => dto.QuestionType, opt => opt.MapFrom(e => e.Question.QuestionType))
                        .ForMember(dto => dto.LikertAnswers, opt => opt.MapFrom(e => e.Question.LikertAnswers));
                

                config.CreateMap<EvaluationQuestion, EvaluationQuestionDto>();
                config.CreateMap<EvaluationQuestionDto, EvaluationQuestion>().ForMember(property => property.Id, options => options.Ignore());

                config.CreateMap<EvaluationResponse, EvaluationResponseDto>();
                config.CreateMap<EvaluationResponseDto, EvaluationResponse>();

            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });


            SeedDatabase.Initialize(app.ApplicationServices);

            // Configure Kendo UI
            app.UseKendo(env);
        }
    }
}
