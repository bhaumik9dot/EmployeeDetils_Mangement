using EmployeeDetils_Mangement.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDetils_Mangement.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        
       public  DbSet<EmployeeDetail> EmployeeDetails { get; set; }
    }
 
}
