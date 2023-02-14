using System;

namespace Api_QLKhachSan_N2.Entities
{
    public class TaiKhoan
    {
        public Guid? TKID { get; set; }
        public string? Hoten { get; set; }
        public string? SDT { get; set; }
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public string? Role { get; set; }
        public string? isDelete { get; set; }
        public TaiKhoan()
        {

        }
    }
}
