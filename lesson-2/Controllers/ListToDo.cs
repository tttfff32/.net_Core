using System.Collections;
using System.Collections.Immutable;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using lesson_2.Interfaces;
using lesson_2.Models;
using lesson_2.Services;


namespace lesson_2.Controllers;

[ApiController]
[Route("[controller]")]
public class ListToDoController : ControllerBase
{
    private IListToDo listToDo;


    public ListToDoController( IListToDo listToDo)
    {
        this.listToDo = listToDo;
    }


    [HttpGet(Name = "GetListToDo")]
    public ActionResult<List<myTask>> Get()
    {
        return listToDo.Get();
    }


    [HttpGet("{id}", Name = "GetTaskById")]
    public IActionResult GetById(int id)
    {
        myTask myTask = listToDo.GetById(id);
        if (myTask == null)
            return NotFound();
        return Ok(myTask);
    }

    [HttpDelete("{id}", Name = "DeleteTaskById")]
    public IActionResult DeleteTaskById(int id)
    {
        bool taskToDelete = listToDo.DeleteTaskById(id);
        if (taskToDelete)
        {
            return NoContent();
        }
        return NotFound();
    }

    [HttpPost(Name = "AddTask")]
    public IActionResult AddTask([FromBody] myTask newTask)
    {
        bool addTask = listToDo.AddTask(newTask);
        if(addTask)
            return CreatedAtRoute("GetListToDo", new { id = newTask.Id }, newTask);
        return NotFound("cant add new task ");
    }


    [HttpPut(Name = "UpdateTask")]
    public IActionResult UpdateTask(int id, [FromBody] myTask updatedTask)
    {
        bool update = listToDo.UpdateTask(id,updatedTask);
        if(update)
            return NoContent();
        return NotFound("cant update task");
    }

}
