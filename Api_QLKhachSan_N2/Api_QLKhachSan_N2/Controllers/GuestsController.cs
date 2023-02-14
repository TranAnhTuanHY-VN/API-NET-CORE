using Api_QLKhachSan_N2.Entities;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace Api_QLKhachSan_N2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        public GuestsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Api lấy thông tin tất cả khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        [HttpGet]
        /*[Authorize]*/
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(List<Guest>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult FilterGuests([FromQuery] int? pagenumber, [FromQuery] int? rowsofpage, [FromQuery] string? search, [FromQuery] string? sort)
        {
            try
            {
                // Kết nối DB
                var appSetting = Configuration.GetSection("AppSetting");
                var connectionString = appSetting.GetValue<string>("ConnectionString");
                SqlConnection myConnection = new SqlConnection(connectionString);
                myConnection.Open();

                // Chuẩn bị proc
                var getProcedure = "Proc_Guest_GetPaging";

                // Chuẩn bị biến paging
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Search", search);
                parameters.Add("@Sort", sort);
                parameters.Add("@Skip", (pagenumber - 1) * rowsofpage);
                parameters.Add("@Take", rowsofpage);

                // Thực thi proc
                var result = myConnection.QueryMultiple(getProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý trả về của DB
                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, result.Read<Guest>());
                }
                return StatusCode(StatusCodes.Status400BadRequest, "e002");
            }
            catch(Exception ex)
            {
                return BadRequest("e001");
            }
        }

        /// <summary>
        /// Api tạo mới khách hàng
        /// </summary>
        /// <param name="guest"></param>
        /// <returns>Thông tin của khách hàng mới</returns>
        [HttpPost]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guest))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult InsertGuest([FromBody] Guest guest)
        {
            try
            {
                // Kết nối DB
                var appSetting = Configuration.GetSection("AppSetting");
                var connectionString = appSetting.GetValue<string>("ConnectionString");
                SqlConnection myConnection = new SqlConnection(connectionString);
                myConnection.Open();

                // Chuẩn bị proc
                var insertProcedure = "Proc_Guest_Insert";

                // Chuẩn bị tham số cho proc
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MaKH", guest.MaKH);
                parameters.Add("@HoTen", guest.HoTen);
                parameters.Add("@CMT", guest.CMT);
                parameters.Add("@GioiTinh", guest.GioiTinh);
                parameters.Add("@SDT", guest.SDT);
                parameters.Add("@GhiChu", guest.GhiChu);
                parameters.Add("@DiaChi", guest.DiaChi);
                parameters.Add("@NgaySinh", guest.NgaySinh);
                parameters.Add("@CreateBy", guest.CreateBy);
                parameters.Add("@ModifiedBy", guest.ModifiedBy);

                // Thực thi proc
                var result = myConnection.Query(insertProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý trả về của DB
                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, guest.MaKH);
                }
                return StatusCode(StatusCodes.Status400BadRequest, "e002");
            }
            catch (Exception)
            {
                return BadRequest("e001");
            }
        }

        [HttpPut("{khid}")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guest))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateGuest([FromRoute] string? khid, [FromBody] Guest guest)
        {
            try
            {
                // Kết nối DB
                var appSetting = Configuration.GetSection("AppSetting");
                string connectionString = appSetting.GetValue<string>("ConnectionString");
                SqlConnection myConnection = new SqlConnection(connectionString);
                myConnection.Open();

                // Chuẩn bị tên proc
                var updateProcedure = "Proc_Guest_Update";

                // Chuẩn bị param cho proc
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@khid", Guid.Parse(khid));
                parameters.Add("@HoTen", guest.HoTen);
                parameters.Add("@CMT", guest.CMT);
                parameters.Add("@GioiTinh", guest.GioiTinh);
                parameters.Add("@SDT", guest.SDT);
                parameters.Add("@GhiChu", guest.GhiChu);
                parameters.Add("@DiaChi", guest.DiaChi);
                parameters.Add("@NgaySinh", guest.NgaySinh);
                parameters.Add("@ModifiedBy", guest.ModifiedBy);

                // Thực thi proc
                var result = myConnection.Execute(updateProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý giá trị trả về từ db
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, khid);
                }
                return StatusCode(StatusCodes.Status400BadRequest, "e002");
            }
            catch (Exception ex)
            {
                return BadRequest("e001");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guid?))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteGuestByID([FromRoute] string? id)
        {
            try
            {
                // Kết nối DB
                var appSetting = Configuration.GetSection("AppSetting");
                string connectionString = appSetting.GetValue<string>("ConnectionString");
                SqlConnection myConnection = new SqlConnection(connectionString);
                myConnection.Open();

                // Chuẩn bị tên proc
                var deleteProcedure = "Proc_Guest_Delete";

                // Chuẩn bị param cho proc
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@khid", Guid.Parse(id));

                // Thực thi proc
                var result = myConnection.Execute(deleteProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý giá trị trả về từ db
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, id);
                }
                return StatusCode(StatusCodes.Status400BadRequest, "e002");
            }
            catch (Exception ex)
            {
                return BadRequest("e001");
            }
        }
    }
}
