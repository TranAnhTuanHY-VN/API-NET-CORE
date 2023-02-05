using System;

namespace Api_QLKhachSan_N2.Entities
{
    public class KhachHang
    {
        /// <summary>
        /// ID khách hàng
        /// </summary>
        public Guid? KHID { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string? MaKH { get; set; }

        /// <summary>
        /// Họ tên của khách hàng
        /// </summary>
        public string? HoTen { get; set; }

        /// <summary>
        /// Địa chỉ của khách hàng
        /// </summary>
        public string DiaChi { get; set; }

        /// <summary>
        /// Số điện thoại của khách hàng
        /// </summary>
        public string? DienThoai { get; set; }

        /// <summary>
        /// Số chứng minh thư hoặc căn cước công dân của khách hàng
        /// </summary>
        public string? CMT { get; set; }

        public KhachHang()
        {

        }
    }
}
