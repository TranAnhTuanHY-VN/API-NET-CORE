using System;

namespace Nhom2_usol_Api_QLKHO.Entities
{
    public class DanhMuc
    {
        public Guid DMID { get; set; }
        public string? MaDM { get; set; }
        public string? TenDM { get; set; }

        public DanhMuc() { }
    }
}
