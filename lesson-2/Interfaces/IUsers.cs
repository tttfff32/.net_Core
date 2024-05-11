using System.Collections;
using System.Collections.Immutable;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using lesson_2. Models;

namespace lesson_2.Interfaces;

public interface IUsers
{

     List<User> Get() ;
     User GetById(int id);
     bool DeleteUserById(int id);
     bool AddUser([FromBody] User newuser);
     bool UpdateUser(int id, User updateUser);

}

