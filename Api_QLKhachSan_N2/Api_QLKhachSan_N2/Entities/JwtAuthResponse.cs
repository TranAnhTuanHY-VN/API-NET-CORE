using System;

namespace Api_QLKhachSan_N2.Entities
{
    [Serializable]
    public class JwtAuthResponse
    {
        public string token { get; set; }
        public string user_name { get; set; }
        public string role { get; set; }
        public int expires_in { get; set; }

    }
}
