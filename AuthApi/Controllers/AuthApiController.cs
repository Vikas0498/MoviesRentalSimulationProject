using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private IConfiguration _config;
        IRepository<Auth> _userdetails;
        public AuthApiController(IConfiguration config, IRepository<Auth> userdetails)
        {
            _config = config;
            _userdetails = userdetails;
        }


        /* [Authorize]
         [HttpGet("{id}")]
         public string  Get(int id)
         {
             return "abc";
         }
        */
        [HttpPost]
        // [HttpGet]
        public IActionResult Login(Auth userdetail)
        {
            /*user.Id = 1;
            user.UserName = "Anuj";
            user.Password = "1234";*/
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(userdetail);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken();
                response = Ok(new { token = tokenString });
            }

            // return response;
            /* var tokenString = GenerateJSONWebToken();
             response = Ok(new { token = tokenString });
             // return Ok(GenerateJSONWebToken());*/
            return response;
        }
        private string GenerateJSONWebToken()
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

        private Auth AuthenticateUser(Auth userdetail)
        {
           /* Auth user = null;
            if (userdetail.Name == "Anuj" && userdetail.Pass == "1234" && userdetail.Id == 1)
            {
                user = new Auth { Name = userdetail.Name, Pass = userdetail.Pass, Id = userdetail.Id };
            }*/
            var userdetailslist = _userdetails.GetAll();
            foreach (var i in userdetailslist)
            {
                if (i.Username == userdetail.Username && i.Pass == userdetail.Pass)
                    return userdetail;
            }
            return null;

        }
    }
}
