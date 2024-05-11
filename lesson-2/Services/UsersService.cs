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

public class UsersService :IUsers
{
    public string JsonData { get; set; }
    public List<User> Users { get; set; }
public string JsonUrl="E:/שיעורי בית תשפד/זילברברג/.net_Core/lesson-2/Data/Users.json";

    public UsersService()
    {
        JsonData =File.ReadAllText(JsonUrl);
        Users = JsonSerializer.Deserialize<List<User>>(JsonData);
    }


    public List<User> Get() => Users;


    public User GetById(int id)
    {
        User user = Users.FirstOrDefault(u => u.Id == id);
        return user;
    }

    public bool DeleteUserById(int id)
    {
        int UserToDelete = Users.FindIndex(u => u.Id == id);
        if (UserToDelete > -1)
        {
            Users.RemoveAt(UserToDelete);
            string updatedJsonData = JsonSerializer.Serialize(Users);
            System.IO.File.WriteAllText(JsonUrl, updatedJsonData);
            return true;
        }
        return false;
    }

    public bool AddUser([FromBody] User newUser)
    {
        if (newUser == null)
        {
            return false;
        }
        //  newUser.Id = Users.Count() + 1;
        Users.Add(newUser);
        string updatedJsonData = JsonSerializer.Serialize(Users);
        System.IO.File.WriteAllText(JsonUrl, updatedJsonData);
        return true;
    }


    public bool UpdateUser(int id,[FromBody] User updateduser)
    {
        if (updateduser == null || id <= 0)
        {
            return false;
        }
        int UserIndex = Users.FindIndex(u => u.Id == id);
        if (UserIndex == -1)
        {
            return false;
        }
        Users[UserIndex] = updateduser;
        string updatedJsonData = JsonSerializer.Serialize(Users);
        System.IO.File.WriteAllText(JsonUrl, updatedJsonData);

        return true;
    }

}
