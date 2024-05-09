using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using lesson_2.Models;
using lesson_2.Services;
using System.Text.Json;
using System.Diagnostics;


namespace lesson_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        public AdminController() {  }


        [HttpPost(Name = "Login")]        
        public ActionResult<string> Login([FromBody] User User)
        {
            var dt = DateTime.Now;

            if (User.Username == "aaa" && User.Password == "123")
            {
                
                var claims = new List<Claim>
            {
                new Claim("type", "Admin"),
                new Claim("type", "classUser")
            };
                var token = TokenService.GetToken(claims);
                return new OkObjectResult(TokenService.WriteToken(token));

            }
            else
            {
                var claims = new List<Claim>
            {
                new Claim("type", "classUser"),
                new Claim("FLName",User.Username.ToString()),
            };

                var token = TokenService.GetToken(claims);

                return new OkObjectResult(TokenService.WriteToken(token));
            }

        }

    }
}
