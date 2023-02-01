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
    public class DanhMucController : ControllerBase
    {
        /// <summary>
        /// Api lấy thông tin tất cả danh mục
        /// </summary>
        /// <returns>Danh sách danh mục</returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(DanhMuc))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllDanhMuc()
        {
            try
            {
                // kết nối db
                var connectionString = "Server=LOIDV-IT-PC1\\SQLEXPRESS;Database=QLKHO;Trusted_Connection=true";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Chuẩn bị query 
                String getQuery = "SELECT * FROM DANHMUC;";

                //Thực thi query
                var result = connection.Query<DanhMuc>(getQuery);
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
        /// Api xóa danh muc theo mã danh mục
        /// </summary>
        /// <param name="madm"></param>
        /// <returns>Mã danh mục của danh mục bị xóa</returns>
        [HttpDelete("{madm}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult deleteDanhMucByMaDM([FromRoute] string? madm)
        {
            try
            {
                // kết nối db
                var connectionString = "Server=LOIDV-IT-PC1\\SQLEXPRESS;Database=QLKHO;Trusted_Connection=true";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Chuẩn bị query 
                String deleteQuery = "DELETE FROM DANHMUC WHERE MaDM = @madm;";

                // Chuẩn bị param
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@madm", madm);

                //Thực thi query
                var result = connection.Execute(deleteQuery, parameters);
                connection.Close();
                //Trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, madm);
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
        /// <param name="danhmuc"></param>
        /// <returns>mã danh mục đc tạo</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult createDanhMuc([FromBody] DanhMuc danhmuc)
        {
            try
            {
                // kết nối db
                var connectionString = "Server=LOIDV-IT-PC1\\SQLEXPRESS;Database=QLKHO;Trusted_Connection=true";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Chuẩn bị query 
                String createQuery = "INSERT INTO DANHMUC(MaDM, TenDM)" +
                                        "VALUES(@madm, @tendm);";

                // Chuẩn bị param
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@madm", danhmuc.MaDM);
                parameters.Add("@tendm", danhmuc.TenDM);

                //Thực thi query
                var result = connection.Execute(createQuery, parameters);
                connection.Close();
                //Trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, danhmuc.MaDM);
                }
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpPut("{madm}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult updateDanhMuc([FromRoute] string? madm, [FromBody] DanhMuc danhmuc)
        {
            try
            {
                // kết nối db
                var connectionString = "Server=LOIDV-IT-PC1\\SQLEXPRESS;Database=QLKHO;Trusted_Connection=true";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Chuẩn bị query 
                String updateQuery = "UPDATE DANHMUC SET TenDM = @tendm WHERE MaDM = @madm;";

                // Chuẩn bị param
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@madm", madm);
                parameters.Add("@tendm", danhmuc.TenDM);

                //Thực thi query
                var result = connection.Execute(updateQuery, parameters);
                connection.Close();
                //Trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, madm);
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
