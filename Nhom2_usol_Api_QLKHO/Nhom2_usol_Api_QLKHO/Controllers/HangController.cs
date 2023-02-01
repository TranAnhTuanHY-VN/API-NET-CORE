using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Nhom2_usol_Api_QLKHO.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace Nhom2_usol_Api_QLKHO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangController : ControllerBase
    {
        /// <summary>
        /// Api lấy thông tin tất cả mặt hàng
        /// </summary>
        /// <returns>Danh sách mặt hàng</returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Hang))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllHang()
        {
            try
            {
                // kết nối db
                var connectionString = "Server=LOIDV-IT-PC1\\SQLEXPRESS;Database=QLKHO;Trusted_Connection=true";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Chuẩn bị query 
                String getQuery = "SELECT * FROM HANG;";

                //Thực thi query
                var result = connection.Query<Hang>(getQuery);
                connection.Close();
                //Trả về
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Api xóa hàng theo mã Hàng
        /// </summary>
        /// <param name="mahang"></param>
        /// <returns>Mã hàng của hàng bị xóa</returns>
        [HttpDelete("{mahang}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult deleteHangByMaHang([FromRoute] string? mahang)
        {
            try
            {
                // kết nối db
                var connectionString = "Server=LOIDV-IT-PC1\\SQLEXPRESS;Database=QLKHO;Trusted_Connection=true";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Chuẩn bị query 
                String deleteQuery = "DELETE FROM HANG WHERE MaHang = @mahang;";

                // Chuẩn bị param
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@mahang", mahang);

                //Thực thi query
                var result = connection.Execute(deleteQuery, parameters);
                connection.Close();
                //Trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, mahang);
                }
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Api tạo mặt hàng mới
        /// </summary>
        /// <param name="hang"></param>
        /// <returns>mã hàng đc tạo</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult createDanhMuc([FromBody] Hang hang)
        {
            try
            {
                // kết nối db
                var connectionString = "Server=LOIDV-IT-PC1\\SQLEXPRESS;Database=QLKHO;Trusted_Connection=true";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Chuẩn bị query 
                String createQuery = "INSERT INTO HANG(MaHang, DMID, NCCID, TenHang, MauSac, SoLuong, DonGia)" +
                                        "VALUES(@mahang, @dmid, @nccid, @tenhang, @mausac, @soluong, @dongia);";

                // Chuẩn bị param
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@mahang", hang.MaHang);
                parameters.Add("@dmid", hang.DMID);
                parameters.Add("@nccid", hang.NCCID);
                parameters.Add("@tenhang", hang.TenHang);
                parameters.Add("@mausac", hang.MauSac);
                parameters.Add("@soluong", hang.SoLuong);
                parameters.Add("@dongia", hang.DonGia);

                //Thực thi query
                var result = connection.Execute(createQuery, parameters);
                connection.Close();
                //Trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, hang.MaHang);
                }
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpPut("{mahang}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult updateHang([FromRoute] string? mahang, [FromBody] Hang hang)
        {
            try
            {
                // kết nối db
                var connectionString = "Server=LOIDV-IT-PC1\\SQLEXPRESS;Database=QLKHO;Trusted_Connection=true";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Chuẩn bị query 
                String updateQuery = "UPDATE HANG SET DMID = @dmid, NCCID = @nccid, TenHang = @tenhang, MauSac = @mausac, SoLuong = @soluong, DonGia = @dongia WHERE MaHang = @mahang;";

                // Chuẩn bị param
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@mahang", mahang);
                parameters.Add("@dmid", hang.DMID);
                parameters.Add("@nccid", hang.NCCID);
                parameters.Add("@tenhang", hang.TenHang);
                parameters.Add("@mausac", hang.MauSac);
                parameters.Add("@soluong", hang.SoLuong);
                parameters.Add("@dongia", hang.DonGia);

                //Thực thi query
                var result = connection.Execute(updateQuery, parameters);
                connection.Close();
                //Trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, mahang);
                }
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}
