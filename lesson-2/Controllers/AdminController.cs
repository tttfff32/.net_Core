using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using  lesson_2.Models;
using lesson_2.Services;


namespace lesson_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        public AdminController() { }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<string> Login([FromBody] User User)
        {
            // User?.FindFirst;
            var dt = DateTime.Now;

            if (User.Username != "Wray"
            || User.Password != $"W{dt.Year}#{dt.Day}!")
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim("type", "Admin"),
            };

            var token = TokenService.GetToken(claims);

            return new OkObjectResult(TokenService.WriteToken(token));
        }


    //     [HttpPost]
    //     [Route("[action]")]
    //     [Authorize(Policy = "Admin")]
    //     public IActionResult GenerateBadge([FromBody] myUser user)
    //     {
    //         var claims = new List<Claim>
    //         {
    //             new Claim("type", "classUser"),
    //         };

    //         var token = TokenService.GetToken(claims);

    //         return new OkObjectResult(TokenService.WriteToken(token));
    //     }
     }



}
