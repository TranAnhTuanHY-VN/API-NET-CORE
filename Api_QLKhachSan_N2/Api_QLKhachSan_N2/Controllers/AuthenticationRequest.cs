using System;

namespace Api_QLKhachSan_N2.Controllers
{
    [Serializable]
    public class AuthenticationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
