using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_QLKhachSan_N2.Entities
{
    public class JwtAuthenticationManager
    {
        public JwtAuthenticationManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public JwtAuthResponse Authenticate(string userName, string password)
        {
            string KQ = KiemTra(userName, password);
            if (KQ == null)
            {
                return null;
            }
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(Constants.JWT_TOKEN_VALIDITY_MINS);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(Constants.JWT_SECURITY_KEY);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("username", userName),
                    new Claim(ClaimTypes.PrimaryGroupSid, "User Group 01")
                }),
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return new JwtAuthResponse
            {
                token = token,
                user_name = userName,
                role = KQ,
                expires_in = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };
        }

        public string KiemTra(string userName, string password)
        {
            try
            {
                var appSetting = Configuration.GetSection("AppSetting");
                var connectionString = appSetting.GetValue<string>("ConnectionString");
                SqlConnection myConnection = new SqlConnection(connectionString);
                myConnection.Open();

                var getProcedure = "Proc_GetAllTaiKhoan";

                var results = myConnection.QueryMultiple(getProcedure, commandType: System.Data.CommandType.StoredProcedure);

                var taikhoans = results.Read<TaiKhoan>();
                foreach (var taikhoan in taikhoans)
                {
                    if (taikhoan.TenDangNhap == userName && taikhoan.MatKhau == password)
                    {
                        return taikhoan.Role;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
