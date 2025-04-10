using System.ComponentModel.DataAnnotations;

namespace EmployeeDetils_Mangement.ViewModel
{
    public class InsertUpdateEmployeeDetail
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required (ErrorMessage ="Phone no is required")]
        public string? Phone { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}
