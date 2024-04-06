using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystems.DAL.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        
        public virtual ICollection<Student> Students { get; set; }=new HashSet<Student>();

    }
}
