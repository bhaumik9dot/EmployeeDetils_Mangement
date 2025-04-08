using EmployeeDetils_Mangement.Helper;
using EmployeeDetils_Mangement.ViewModel;

namespace EmployeeDetils_Mangement.Repository
{
    public interface IEmployeeRepository
    {
        Task<BaseResponse> AddUpdateEmployeeDetail(InsertUpdateEmployeeDetail employeeDetail);
        Task<BaseResponseModel<IEnumerable<EmployeeDetailVM>>> GetEmployeeDetail();
        Task<BaseResponseObject<EmployeeDetailVM>> GetEmployeeDetailById(int Id);
        Task<BaseResponse> DeleteEmployeeDetailById(int Id);
    }
}
