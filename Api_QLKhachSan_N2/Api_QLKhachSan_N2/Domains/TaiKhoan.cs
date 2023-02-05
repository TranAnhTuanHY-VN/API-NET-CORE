using System;
using System.ComponentModel.DataAnnotations;

namespace Api_QLKhachSan_N2.Domains
{
    public class TaiKhoan
    {
        [Key]
        public Guid? TKID { get; set; } = Guid.NewGuid();
        public string? MaTK { get; set; }
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public string? VaiTro { get; set; }
    }
}
