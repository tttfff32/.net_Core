using System.Collections;
using System.Collections.Immutable;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using lesson_2.Models;


namespace lesson_2.Interfaces;

public interface IListToDo
{

     List<myTask> Get() ;
     myTask GetById(int id);
     bool DeleteTaskById(int id);
     bool AddTask([FromBody] myTask newTask);
     bool UpdateTask(int id, myTask updatedTask);

}
