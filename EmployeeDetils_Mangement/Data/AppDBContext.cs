using Microsoft.EntityFrameworkCore;

namespace EmployeeDetils_Mangement.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
     
    }
 
}
