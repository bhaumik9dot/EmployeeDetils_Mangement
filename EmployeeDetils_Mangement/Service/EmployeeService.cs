using EmployeeDetils_Mangement.Data;
using EmployeeDetils_Mangement.Helper;
using EmployeeDetils_Mangement.Model;
using EmployeeDetils_Mangement.Repository;
using EmployeeDetils_Mangement.ViewModel;
using Microsoft.Data.SqlClient;
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


                #region :: Using For Strored Procedure ::
                //var idParam = new SqlParameter("@Id", (object?)employeeDetail.Id ?? DBNull.Value);
                //var fnameParam = new SqlParameter("@FirstName", employeeDetail.FirstName ?? (object)DBNull.Value);
                //var lnameParam = new SqlParameter("@LastName", employeeDetail.LastName ?? (object)DBNull.Value);
                //var emailParam = new SqlParameter("@Email", employeeDetail.Email ?? (object)DBNull.Value);
                //var phoneParam = new SqlParameter("@Phone", employeeDetail.Phone ?? (object)DBNull.Value);

                //await _dbContext.Database.ExecuteSqlRawAsync(
                //    "EXEC InsertUpdateEmployeeDetail @Id, @FirstName, @LastName, @Email, @Phone",
                //    idParam, fnameParam, lnameParam, emailParam, phoneParam);
                #endregion

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

                #region :: Using For Strored Procedure ::
                //var result = await _dbContext.EmployeeDetails.FromSqlRaw("EXEC GetAllEmployeeDetail").AsNoTracking().ToListAsync();
                //var employeeDetails1 = result.Select(e => new EmployeeDetailVM
                //{
                //    Id = e.Id,
                //    FirstName = e.FirstName,
                //    LastName = e.LastName,
                //    Email = e.Email,
                //    Phone = e.Phone,
                //    CreatedDate = e.CreatedDate
                //}).ToList();
                #endregion

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
                                             join s in _dbContext.EmployeeSalary on e.Id equals s.EmployeeId into salaryGroup
                                             from sg in salaryGroup.DefaultIfEmpty()
                                             group sg by new { e.Id, e.FirstName, e.LastName, e.Email, e.Phone, e.CreatedDate } into g
                                             select new EmployeeDetailVM
                                             {
                                                 Id = g.Key.Id,
                                                 FirstName = g.Key.FirstName,
                                                 LastName = g.Key.LastName,
                                                 Email = g.Key.Email,
                                                 Phone = g.Key.Phone,
                                                 CreatedDate = g.Key.CreatedDate,
                                                 TotalSalary = g.Sum(x => x.Salary)
                                             }).FirstOrDefaultAsync();


                #region :: Using For Strored Procedure ::
                //var parameter = new SqlParameter("@Id", Id);
                //var result = await _dbContext.EmployeeDetails.FromSqlRaw("EXEC GetAllEmployeeDetailById @Id", parameter).AsNoTracking().ToListAsync();

                //var res = result.FirstOrDefault(); 

                #endregion

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
                #region :: Using For Strored Procedure ::

                //var parameter = new SqlParameter("@Id", Id);

                //int result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteEmployeeDetailById @Id", parameter);

                //if (result == 0)
                //{
                //    return new BaseResponse
                //    {
                //        Success = false,
                //        Message = "Employee details not found or already deleted."
                //    };
                //}

                //return new BaseResponse
                //{
                //    Success = true,
                //    Message = "Employee details deleted successfully."
                //};
                #endregion

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
