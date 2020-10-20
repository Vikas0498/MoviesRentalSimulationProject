using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticate1Api
{
    public class AuthProvider : IAuthRepo
    {
        private IConfiguration _config;
        IRepository<Authenticate1> _userdetails;
        public AuthProvider(IConfiguration config, IRepository<Authenticate1> userdetails)
        {
            _config = config;
            _userdetails = userdetails;
        }
        public Authenticate1 AuthenticateUser(Authenticate1 userdetail)
        {

            if (userdetail.Username == "Vikas" && userdetail.Pass == 1234 && userdetail.Id == 1)
            {

                var user = new Authenticate1 { Username = userdetail.Username, Pass = userdetail.Pass, Id = userdetail.Id };
                return user;
            }
            return null;
        }

        public string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
