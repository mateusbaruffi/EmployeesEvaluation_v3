using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace EmployeesEvaluation.WEB.Dtos
{
    public class EvaluationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }           
        public string Disclosure { get; set; }
        public SeasonDto Season { get; set; }
        public int SeasonId { get; set; }
        public List<SelectListItem> DepartmentManagers { get; set; }
        public string DepartmentManagerId { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }
        public List<SelectListItem> Seasons { get; set; }
        public List<int> QuestionIds { get; set; }
    }
}


