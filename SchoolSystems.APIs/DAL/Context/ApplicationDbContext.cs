using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolSystems.APIs.DAL;
using SchoolSystems.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystems.DAL.Context
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
       
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
        }
    }
}
