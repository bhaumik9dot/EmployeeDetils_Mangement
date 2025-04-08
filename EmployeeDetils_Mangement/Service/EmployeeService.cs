using EmployeeDetils_Mangement.Data;
using EmployeeDetils_Mangement.Helper;
using EmployeeDetils_Mangement.Model;
using EmployeeDetils_Mangement.Repository;
using EmployeeDetils_Mangement.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EmployeeDetils_Mangement.Service
{
    public class EmployeeService : IEmployeeRepository
    {
        private readonly AppDBContext _dbContext;

        public EmployeeService(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<BaseResponse> AddUpdateEmployeeDetail(InsertUpdateEmployeeDetail employeeDetail)
        {
            try
            {

                var Employee = await _dbContext.EmployeeDetails.Where(x => x.Id == employeeDetail.Id).FirstOrDefaultAsync();
                if (Employee != null)
                {

                    Employee.FirstName = employeeDetail.FirstName;
                    Employee.LastName = employeeDetail.LastName;
                    Employee.Email = employeeDetail.Email;
                    Employee.Phone = employeeDetail.Phone;
                    _dbContext.EmployeeDetails.Update(Employee);
                }
                else
                {
                    EmployeeDetail employee = new EmployeeDetail
                    {
                        FirstName = employeeDetail.FirstName,
                        LastName = employeeDetail.LastName,
                        Email = employeeDetail.Email,
                        Phone = employeeDetail.Phone,
                        CreatedDate = DateTime.Now
                    };
                    await _dbContext.EmployeeDetails.AddAsync(employee);
                }
                await _dbContext.SaveChangesAsync();

                return new BaseResponse
                {
                    Success = false,
                    Message = Employee == null ? "Inserted Recored Successfully" : "updated recored successfully",
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponseModel<IEnumerable<EmployeeDetailVM>>> GetEmployeeDetail()
        {
            try
            {
                var employeeDetails = await (from e in _dbContext.EmployeeDetails
                                             select new EmployeeDetailVM
                                             {
                                                 Id = e.Id,
                                                 FirstName = e.FirstName,
                                                 LastName = e.LastName,
                                                 Email = e.Email,
                                                 Phone = e.Phone,
                                                 CreatedDate = e.CreatedDate
                                             }).ToListAsync();

                return new BaseResponseModel<IEnumerable<EmployeeDetailVM>>
                {
                    Success = true,
                    Message = "Employee details retrieved successfully.",
                    Data = employeeDetails
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponseObject<EmployeeDetailVM>> GetEmployeeDetailById(int Id)
        {
            try
            {
                var employeeDetails = await (from e in _dbContext.EmployeeDetails
                                             where e.Id == Id
                                             select new EmployeeDetailVM
                                             {
                                                 Id = e.Id,
                                                 FirstName = e.FirstName,
                                                 LastName = e.LastName,
                                                 Email = e.Email,
                                                 Phone = e.Phone,
                                                 CreatedDate = e.CreatedDate
                                             }).FirstOrDefaultAsync();

                return new BaseResponseObject<EmployeeDetailVM>
                {
                    Success = employeeDetails != null ? true : false,
                    Message = employeeDetails != null ? "Employee details retrieved successfully." : "Recored not found, pleae try again.",
                    Data = employeeDetails
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<BaseResponse> DeleteEmployeeDetailById(int Id)
        {
            try
            {
                var Employee = await _dbContext.EmployeeDetails.Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (Employee == null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Employee details not found.",
                    };
                }

                _dbContext.EmployeeDetails.Remove(Employee);
                await _dbContext.SaveChangesAsync();

                return new BaseResponse
                {
                    Success = true,
                    Message = "Employee details deleted successfully.",
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
