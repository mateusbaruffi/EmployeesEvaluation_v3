using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EmployeesEvaluation.Core.Models
{
    public class EntityBase : IEntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}