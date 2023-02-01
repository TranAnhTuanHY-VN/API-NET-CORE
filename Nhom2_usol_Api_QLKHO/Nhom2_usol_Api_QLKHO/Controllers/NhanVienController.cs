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
    public class NhanVienController : ControllerBase
    {
        /// <summary>
        /// Api lấy thông tin tất cả nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(NhanVien))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllNhanVien()
        {
            try
            {
                // kết nối db
                var connectionString = "Server=LOIDV-IT-PC1\\SQLEXPRESS;Database=QLKHO;Trusted_Connection=true";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Chuẩn bị query 
                String getQuery = "SELECT * FROM NHANVIEN;";

                //Thực thi query
                var result = connection.Query<NhanVien>(getQuery);
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
        /// Api xóa nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="manv"></param>
        /// <returns>Mã nhân viên của nhân viên bị xóa</returns>
        [HttpDelete("{manv}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult deleteNhanVienByMaNV([FromRoute] string? manv)
        {
            try
            {
                // kết nối db
                var connectionString = "Server=LOIDV-IT-PC1\\SQLEXPRESS;Database=QLKHO;Trusted_Connection=true";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Chuẩn bị query 
                String deleteQuery = "DELETE FROM NHANVIEN WHERE MaNV = @manv;";

                // Chuẩn bị param
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@manv", manv);

                //Thực thi query
                var result = connection.Execute(deleteQuery, parameters);
                connection.Close();
                //Trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, manv);
                }
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Api tạo danh mục mới
        /// </summary>
        /// <param name="nhanvien"></param>
        /// <returns>mã danh mục đc tạo</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult createNhanVien([FromBody] NhanVien nhanvien)
        {
            try
            {
                // kết nối db
                var connectionString = "Server=LOIDV-IT-PC1\\SQLEXPRESS;Database=QLKHO;Trusted_Connection=true";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Chuẩn bị query 
                String createQuery = "INSERT INTO NHANVIEN(MaNV, TenNV, DiaChi, ChucVu)" +
                                        "VALUES(@manv, @tennv, @diachi, @chucvu);";

                // Chuẩn bị param
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@manv", nhanvien.MaNV);
                parameters.Add("@tennv", nhanvien.TenNV);
                parameters.Add("@diachi", nhanvien.DiaChi);
                parameters.Add("@chucvu", nhanvien.ChucVu);

                //Thực thi query
                var result = connection.Execute(createQuery, parameters);
                connection.Close();
                //Trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, nhanvien.MaNV);
                }
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Api cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="manv"></param>
        /// <param name="nhanvien"></param>
        /// <returns>mã nhân viên của nhân viên được cập nhật</returns>
        [HttpPut("{manv}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult updateNhanVien([FromRoute] string? manv, [FromBody] NhanVien nhanvien)
        {
            try
            {
                // kết nối db
                var connectionString = "Server=LOIDV-IT-PC1\\SQLEXPRESS;Database=QLKHO;Trusted_Connection=true";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Chuẩn bị query 
                String updateQuery = "UPDATE NHANVIEN SET TenNV = @tennv, DiaChi = @diachi, ChucVu = @chucvu WHERE MaNV = @manv;";

                // Chuẩn bị param
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@manv", manv);
                parameters.Add("@tennv", nhanvien.TenNV);
                parameters.Add("@diachi", nhanvien.DiaChi);
                parameters.Add("@chucvu", nhanvien.ChucVu);

                //Thực thi query
                var result = connection.Execute(updateQuery, parameters);
                connection.Close();
                //Trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, manv);
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
