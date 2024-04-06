using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystems.DAL.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Grade { get; set; }
        public int? TeacherId { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }
}
