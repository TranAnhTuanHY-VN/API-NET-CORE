using System;

namespace Api_QLKhachSan_N2.Entities
{
    public class TaiKhoan
    {
        public Guid? TKID { get; set; }
        public string? MaTK { get; set; }
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public string? VaiTro { get; set; }
        public TaiKhoan()
        {

        }
    }
}
