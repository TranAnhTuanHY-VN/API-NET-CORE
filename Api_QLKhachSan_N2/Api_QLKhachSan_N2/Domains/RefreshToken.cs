using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_QLKhachSan_N2.Domains
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string Token { get; set; }
        public virtual RefreshToken ReplacedBy { get; set; }
        public string ReplacedById { get; set; }
        public DateTime? RevokedDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsValid => !RevokedDate.HasValue && ExpireDate < DateTime.UtcNow;
    }
}
