using EmployeeDetils_Mangement.Data;
using EmployeeDetils_Mangement.Helper;
using EmployeeDetils_Mangement.Model;
using EmployeeDetils_Mangement.Repository;
using EmployeeDetils_Mangement.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDetils_Mangement.Service
{
    public class EmployeeSalaryDetailService : IEmployeeSalaryDetailRepository
    {
        private readonly AppDBContext _dbContext;
        public EmployeeSalaryDetailService(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<BaseResponse> AddUpdateEmployeeSalaryDetail(InsertUpdateEmployeeSalaryDetail employeeSalaryDetail)
        {
            try
            {
                var employeeSalary = await _dbContext.EmployeeSalary.Where(x => x.Id == employeeSalaryDetail.Id).FirstOrDefaultAsync();
                if (employeeSalary != null)
                {
                    employeeSalary.EmployeeId = employeeSalaryDetail.EmployeeId;
                    employeeSalary.Salary = employeeSalaryDetail.Salary;
                    _dbContext.EmployeeSalary.Update(employeeSalary);
                }
                else
                {
                    EmployeeSalary salaryDetail = new EmployeeSalary
                    {
                        EmployeeId = employeeSalaryDetail.EmployeeId,
                        Salary = employeeSalaryDetail.Salary,
                    };
                    await _dbContext.EmployeeSalary.AddAsync(salaryDetail);
                }
                await _dbContext.SaveChangesAsync();
                return new BaseResponse { Success = true, Message = "Employee Salary Detail added/updated successfully." };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Success = false, Message = ex.Message };
            }
        }
    }
}
