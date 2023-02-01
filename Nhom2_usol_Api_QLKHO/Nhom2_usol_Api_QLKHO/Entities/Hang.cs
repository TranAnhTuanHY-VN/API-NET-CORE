using System;

namespace Nhom2_usol_Api_QLKHO.Entities
{
    public class Hang
    {
        public Guid? HID { get; set; }
        public string? MaHang { get; set; }
        public Guid? DMID { get; set; }
        public Guid? NCCID { get; set; }
        public string? TenHang { get; set; }
        public string? MauSac { get; set; }
        public int SoLuong { get; set; }
        public Double DonGia { get; set; }

        public Hang()
        {

        }
    }
}
