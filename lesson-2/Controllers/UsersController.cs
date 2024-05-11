using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using  lesson_2.Models;
using lesson_2.Interfaces;


namespace lesson_2.Controllers
{
[ApiController]
[Route("[controller]")]
[Authorize(Policy = "Admin")]

public class UsersController : ControllerBase
{
    private IUsers myUser;


    public UsersController( IUsers user)
    {
        myUser = user;

    }

    [HttpGet(Name = "GetUser")]
    public ActionResult<List<User>> Get()
    {
        return myUser.Get();
    }


    [HttpGet("{id}", Name = "GetUserById")]
    public IActionResult GetById(int id)
    {
        User user = myUser.GetById(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpDelete("{id}", Name = "DeleteUserById")]
    public IActionResult DeleteUserById(int id)
    {
        bool userToDelete = myUser.DeleteUserById(id);
        if (userToDelete)
        {
            return NoContent();
        }
        return NotFound();
    }

    [HttpPost(Name = "AddUser")]
    public IActionResult AddUser([FromBody] User newuser)
    {
        bool AddUser = myUser.AddUser(newuser);
        if(AddUser)
            return CreatedAtRoute("GetUser", new { id = newuser.Id }, newuser);
        return NotFound("cant add new user ");
    }


    [HttpPut("{id}", Name = "UpdateUser")]
    public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
    {
        bool update = myUser.UpdateUser(id,updatedUser);
        if(update)
            return NoContent();
        else
             return NotFound(); 
    }
}

}