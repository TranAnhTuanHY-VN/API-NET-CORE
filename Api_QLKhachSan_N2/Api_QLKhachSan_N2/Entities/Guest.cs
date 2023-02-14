using System;
using System.ComponentModel.DataAnnotations;

namespace Api_QLKhachSan_N2.Entities
{
    public class Guest
    {
        /// <summary>
        /// ID khách hàng
        /// </summary>
        public Guid? KHID { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [Required(ErrorMessage = "e004")]
        public string? MaKH { get; set; }

        /// <summary>
        /// Họ tên của khách hàng
        /// </summary>
        [Required(ErrorMessage = "e005")]
        public string? HoTen { get; set; }

        /// <summary>
        /// Số chứng minh thư hoặc căn cước công dân của khách hàng
        /// </summary>
        [Required(ErrorMessage = "e006")]
        public string? CMT { get; set; }

        [Required]
        public string? GioiTinh { get; set; }

        /// <summary>
        /// Số điện thoại của khách hàng
        /// </summary>
        [Required(ErrorMessage = "e008")]
        public string? SDT { get; set; }
        public string? GhiChu { get; set; }
        public string? isDelete { get; set; }

        /// <summary>
        /// Địa chỉ của khách hàng
        /// </summary>
        public string DiaChi { get; set; }

        public DateTime? NgaySinh { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }


        public Guest()
        {

        }
    }
}
