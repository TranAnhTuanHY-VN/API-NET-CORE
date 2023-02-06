using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_QLKhachSan_N2.Domains
{
    [Table("User")]
    public class User
    {
        [Key]
        public string? TKID { get; set; } = Guid.NewGuid().ToString();
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }

        public virtual IList<RefreshToken> RefreshTokens { get; set; }
    }
}
