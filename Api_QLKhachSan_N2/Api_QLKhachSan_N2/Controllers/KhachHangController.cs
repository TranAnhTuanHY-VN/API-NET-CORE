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
    public class KhachHangController : ControllerBase
    {
        public KhachHangController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Api lấy thông tin tất cả khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        [HttpGet]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(List<KhachHang>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult getAllKhachHang([FromQuery] int? pagenumber, [FromQuery] int? rowsofpage, [FromQuery] string? search)
        {
            try
            {
                // Kết nối DB
                var appSetting = Configuration.GetSection("AppSetting");
                var connectionString = appSetting.GetValue<string>("ConnectionString");
                SqlConnection myConnection = new SqlConnection(connectionString);
                myConnection.Open();

                // Chuẩn bị proc
                var getProcedure = "proc_getPagingKhachHang";

                // Chuẩn bị biến paging
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@search", search);
                parameters.Add("@skip", pagenumber * rowsofpage);
                parameters.Add("@take", rowsofpage);

                // Thực thi proc
                var result = myConnection.QueryMultiple(getProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý trả về của DB
                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, result.Read<KhachHang>());
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Lỗi db!");
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Api tạo mới khách hàng
        /// </summary>
        /// <param name="khachang"></param>
        /// <returns>Thông tin của khách hàng mới</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(KhachHang))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult createKhachHang([FromBody] KhachHang khachang)
        {
            try
            {
                // Kết nối DB
                var appSetting = Configuration.GetSection("AppSetting");
                var connectionString = appSetting.GetValue<string>("ConnectionString");
                SqlConnection myConnection = new SqlConnection(connectionString);
                myConnection.Open();

                // Chuẩn bị proc
                var insertProcedure = "proc_insertKhachHang";

                // Chuẩn bị tham số cho proc
                khachang.KHID = new Guid();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MaKH", khachang.MaKH);
                parameters.Add("@HoTen", khachang.HoTen);
                parameters.Add("@DiaChi", khachang.DiaChi);
                parameters.Add("@DienThoai", khachang.DienThoai);
                parameters.Add("@CMT", khachang.CMT);

                // Thực thi proc
                var result = myConnection.Query(insertProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý trả về của DB
                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, khachang);
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Lỗi db!");
            }
            catch (Exception)
            {
                return BadRequest("Lỗi server!");
            }
        }

        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(KhachHang))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult updateKhachHang([FromBody] KhachHang kh)
        {
            try
            {
                // Kết nối DB
                var appSetting = Configuration.GetSection("AppSetting");
                string connectionString = appSetting.GetValue<string>("ConnectionString");
                SqlConnection myConnection = new SqlConnection(connectionString);
                myConnection.Open();

                // Chuẩn bị tên proc
                var updateProcedure = "proc_updateKhachHanh";

                // Chuẩn bị param cho proc
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@KHID", kh.KHID);
                parameters.Add("@MaKH", kh.MaKH);
                parameters.Add("@HoTen", kh.HoTen);
                parameters.Add("@DiaChi", kh.DiaChi);
                parameters.Add("@DienThoai",kh.DienThoai);
                parameters.Add("@CMT", kh.CMT);

                // Thực thi proc
                var result = myConnection.Query(updateProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý giá trị trả về từ db
                if(result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, kh);
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Lỗi db!");
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi server!");
            }
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guid?))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult deleteKhachHang([FromRoute] Guid? id)
        {
            try
            {
                // Kết nối DB
                var appSetting = Configuration.GetSection("AppSetting");
                string connectionString = appSetting.GetValue<string>("ConnectionString");
                SqlConnection myConnection = new SqlConnection(connectionString);
                myConnection.Open();

                // Chuẩn bị tên proc
                var deleteProcedure = "proc_deleteKhachHang";

                // Chuẩn bị param cho proc
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@khid", id);

                // Thực thi proc
                var result = myConnection.Query(deleteProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý giá trị trả về từ db
                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, id);
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Lỗi db!");
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi server!");
            }
        }
    }
}
