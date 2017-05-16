using System;
using System.Collections.Generic;

namespace EmployeesEvaluation.Core.Models
{
    public class Season : EntityBase
    {
        public string Name { get; set; }
        public ICollection<Evaluation> Evaluations { get; set; }           
    }
}