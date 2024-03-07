using System.Collections;
using System.Collections.Immutable;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.IO;

namespace lesson_2.Controllers;

[ApiController]
[Route("[controller]")]
public class ListToDoController : ControllerBase
{
    public string JsonData { get; set; }
    public List<Task> Tasks { get; set; }
    private readonly ILogger<ListToDoController> _logger;


    public ListToDoController(ILogger<ListToDoController> logger)
    {

        JsonData = System.IO.File.ReadAllText("D:/שיעורי בית תשפד/זילברברג/.net_Core/lesson-2/Data/Task.json");
        Tasks = JsonSerializer.Deserialize<List<Task>>(JsonData);

        _logger = logger;

    }


    [HttpGet(Name = "GetListToDo")]
    public List<Task> Get()
    {
        return Tasks;
    }


    [HttpGet("{id}", Name = "GetTaskById")]
    public IActionResult GetById(int id)
    {
        Task myTask = Tasks.FirstOrDefault(task => task.Id == id);
        if (myTask == null)
            return NotFound();
        return Ok(myTask);
    }

    [HttpDelete("{id}", Name = "DeleteTaskById")]
    public IActionResult DeleteTaskById(int id)
    {
        int taskToDelete = Tasks.FindIndex(task => task.Id == id);
        if (taskToDelete > -1)
        {
            Tasks.RemoveAt(taskToDelete);
            string updatedJsonData = JsonSerializer.Serialize(Tasks);
            System.IO.File.WriteAllText("D:/שיעורי בית תשפד/זילברברג/.net_Core/lesson-2/Data/Task.json", updatedJsonData);
            return NoContent();
        }
        return NotFound();
    }

    [HttpPost(Name = "AddTask")]
    public IActionResult AddTask([FromBody] Task newTask)
    {
        if (newTask == null)
        {
            return BadRequest("Invalid task data");
        }
        Tasks.Add(newTask);
        string updatedJsonData = JsonSerializer.Serialize(Tasks);
        System.IO.File.WriteAllText("D:/שיעורי בית תשפד/זילברברג/.net_Core/lesson-2/Data/Task.json", updatedJsonData);

        return CreatedAtRoute("GetListToDo", new { id = newTask.Id }, newTask);
    }


    [HttpPut(Name = "UpdateTask")]
    public IActionResult UpdateTask(int id, [FromBody] Task updatedTask)
    {
        if (updatedTask == null || id <= 0)
        {
            return BadRequest("Invalid task data or ID");
        }

        int taskIndex = Tasks.FindIndex(t => t.Id == id);

        if (taskIndex == -1)
        {
            return NotFound("Task not found");
        }

        Tasks[taskIndex] = updatedTask;
        string updatedJsonData = JsonSerializer.Serialize(Tasks);
        System.IO.File.WriteAllText("D:/שיעורי בית תשפד/זילברברג/.net_Core/lesson-2/Data/Task.json", updatedJsonData);

        return NoContent();
    }

}
