using EmployeesEvaluation.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesEvaluation.WEB.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
        public int Limit { get; set; }

        [Required]
        public QuestionTypeDto QuestionType { get; set; }
        //public ApplicationUser Ownership { get; set; }

        [Display(Name = "Ownership")]
        [Required]
        public string OwnershipId { get; set; }

        // public ICollection<EvaluationQuestion> EvaluationQuestions { get; set; }
        public List<LikertAnswerDto> LikertAnswers { get; set; }

        public List<SelectListItem> HR_DM_Managers { get; set; }
    }
}
