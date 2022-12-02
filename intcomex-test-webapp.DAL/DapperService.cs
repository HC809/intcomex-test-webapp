using Dapper;
using intcomex_test_webapp.DAL.Models;
using intcomex_test_webapp.DAL.Queries;
using MySqlConnector;

namespace intcomex_test_webapp.DAL
{
    public interface IDapperSqlService
    {
        Task<List<DepartmentsTotalSalaries>> GetDepartmentsTotalSalaries();
        Task<List<EmployeeTotalBorn>> GetEmployeesTotalBorn();
    }

    public class DapperSqlService : IDapperSqlService
    {
        protected string connString = "server=35.223.30.36;port=3306;database=intcomexdb;user=admin522;password=admin522";

        public async Task<List<DepartmentsTotalSalaries>> GetDepartmentsTotalSalaries()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    var departmentSalariesList = await connection.QueryAsync<DepartmentsTotalSalaries>(SqlQueries.DepartmentSalaries);

                    return departmentSalariesList.ToList();
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<EmployeeTotalBorn>> GetEmployeesTotalBorn()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    var employees = await connection.QueryAsync<EmployeeTotalBorn>(SqlQueries.EmployeesTotalBorn);

                    return employees.ToList();
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
