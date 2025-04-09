using EmployeeDetils_Mangement.Helper;
using EmployeeDetils_Mangement.ViewModel;

namespace EmployeeDetils_Mangement.Repository
{
    public interface IEmployeeSalaryDetailRepository
    {
        Task<BaseResponse> AddUpdateEmployeeSalaryDetail(InsertUpdateEmployeeSalaryDetail employeeSalaryDetail);

    }
}
