using System.ComponentModel.DataAnnotations;

namespace EmployeeDetils_Mangement.Model
{
    public class EmployeeSalary
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int Salary { get; set; }
    }
}
