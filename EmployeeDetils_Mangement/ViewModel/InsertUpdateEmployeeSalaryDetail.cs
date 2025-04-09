using System.ComponentModel.DataAnnotations;

namespace EmployeeDetils_Mangement.ViewModel
{
    public class InsertUpdateEmployeeSalaryDetail
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Employee Id is required")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        public int Salary { get; set; }

    }
}
