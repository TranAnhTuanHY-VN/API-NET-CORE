using Dapper;
using EmployeeManagement.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách phòng ban
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            try
            {
                SqlConnection connection= new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var result = connection.Query<Department>("Proc_GetAllDepartment", commandType: System.Data.CommandType.StoredProcedure);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
