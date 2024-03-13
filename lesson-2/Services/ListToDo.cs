using System.Collections;
using System.Collections.Immutable;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using lesson_2.Models;
using lesson_2.Interfaces;
using System.IO;

namespace lesson_2.Services;

public class ListToDo :IListToDo
{
    public string JsonData { get; set; }
    public List<myTask> Tasks { get; set; }


    public ListToDo()
    {
        JsonData = System.IO.File.ReadAllText("E:/שיעורי בית תשפד/זילברברג/.net_Core/lesson-2/Data/Task.json");
        Tasks = JsonSerializer.Deserialize<List<myTask>>(JsonData);
    }


    public List<myTask> Get() => Tasks;


    public myTask GetById(int id)
    {
        myTask myTask = Tasks.FirstOrDefault(task => task.Id == id);
        return myTask;
    }

    public bool DeleteTaskById(int id)
    {
        int taskToDelete = Tasks.FindIndex(task => task.Id == id);
        if (taskToDelete > -1)
        {
            Tasks.RemoveAt(taskToDelete);
            string updatedJsonData = JsonSerializer.Serialize(Tasks);
            System.IO.File.WriteAllText("E:/שיעורי בית תשפד/זילברברג/.net_Core/lesson-2/Data/Task.json", updatedJsonData);
            return true;
        }
        return false;
    }

    public bool AddTask([FromBody] myTask newTask)
    {
        if (newTask == null)
        {
            return false;
        }
        Tasks.Add(newTask);
        string updatedJsonData = JsonSerializer.Serialize(Tasks);
        System.IO.File.WriteAllText("E:/שיעורי בית תשפד/זילברברג/.net_Core/lesson-2/Data/Task.json", updatedJsonData);
        return true;
    }


    public bool UpdateTask(int id, [FromBody] myTask updatedTask)
    {
        if (updatedTask == null || id <= 0)
        {
            return false;
        }

        int taskIndex = Tasks.FindIndex(t => t.Id == id);

        if (taskIndex == -1)
        {
            return false;
        }
        Tasks[taskIndex] = updatedTask;
        string updatedJsonData = JsonSerializer.Serialize(Tasks);
        System.IO.File.WriteAllText("E:/שיעורי בית תשפד/זילברברג/.net_Core/lesson-2/Data/Task.json", updatedJsonData);

        return true;
    }

}
