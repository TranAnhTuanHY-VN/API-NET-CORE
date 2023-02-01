using System;

namespace Nhom2_usol_Api_QLKHO.Entities
{
    public class NhanVien
    {
        public Guid NVID { get; set; }
        public string? MaNV { get; set; }
        public string? TenNV { get; set; }
        public string? DiaChi { get; set; }
        public string? ChucVu { get; set; }
        public NhanVien() { }
    }
}
