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
using System.Security.Cryptography;


namespace lesson_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        public string JsonData { get; set; }
        public List<User> Users { get; set; }
        public string JsonUrl = "E:/שיעורי בית תשפד/זילברברג/.net_Core/lesson-2/Data/Users.json";
        public LoginController()
        {
            JsonData = System.IO.File.ReadAllText(JsonUrl);
            Users = JsonSerializer.Deserialize<List<User>>(JsonData);

        }


        [HttpPost(Name = "Login")]
        public ActionResult<string> Login([FromBody] User User)
        {
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
                if (Users.Find(u => u.Username.Equals(User.Username)) !=null)
                {
                    var claims = new List<Claim>
                     {
                           new Claim("type", "classUser"),
                            new Claim("FLName",User.Username.ToString()),
                      };

                    var token = TokenService.GetToken(claims);
                    return new OkObjectResult(TokenService.WriteToken(token));

                }
                else
                    {
                         return NotFound("user is not exist in the system");
                    }
                   
            }
        }

    }
}
