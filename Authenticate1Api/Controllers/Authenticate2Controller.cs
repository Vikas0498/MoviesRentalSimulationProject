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

namespace Authenticate1Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authenticate2Controller : ControllerBase
    {
        private IAuthRepo _auth;
        private IConfiguration _config;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(Authenticate2Controller));
      //  private IConfiguration _config;
        IRepository<Authenticate1> _userdetails;
        public Authenticate2Controller(IConfiguration config, IRepository<Authenticate1> userdetails, IAuthRepo auth)
        {
            _config = config;
            _userdetails = userdetails;
            _auth = auth;
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
        public IActionResult Login(Authenticate1 userdetail)
        {
            _log4net.Info("Inserting Started");
            /*user.Id = 1;
            user.UserName = "Anuj";
            user.Password = "1234";*/
            IActionResult response = Unauthorized();
           var user = _auth.AuthenticateUser(userdetail);

          if (user != null)
          {
                var tokenString = _auth.GenerateJSONWebToken();
                response = Ok(new { token = tokenString });
          }

            // return response;
            /* var tokenString = GenerateJSONWebToken();
             response = Ok(new { token = tokenString });
             // return Ok(GenerateJSONWebToken());*/
              return response;
          //  return NotFound();
        }
      /*  private string GenerateJSONWebToken()
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

        private Authenticate1 AuthenticateUser(Authenticate1 userdetail)
        {
            
            if (userdetail.Username == "Vikas" && userdetail.Pass == 1234 && userdetail.Id == 1)
            {

                var user = new Authenticate1 { Username = userdetail.Username, Pass = userdetail.Pass,Id = userdetail.Id };
                return user;
            }
           /* var userdetailslist = _userdetails.GetAll();
            foreach (var i in userdetailslist)
            {
                if (i.Username == userdetail.Username && i.Pass == userdetail.Pass)
                    return userdetail;
            }
            return null;

        }*/
    }
}
