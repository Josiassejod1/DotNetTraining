using DotnetAPI.Data;
using DotnetAPI.DTOS;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  DataContextDapper _dapper;

  public UserController(IConfiguration config)
  {
    Console.WriteLine(config.GetConnectionString("DefaultConnection"));
    _dapper = new DataContextDapper(config);
  }


  [HttpGet("GetUsers")]
  public IEnumerable<User> GetUsers()
  {
    string sql = @"
      SELECT [UserId],
        [FirstName],
        [LastName],
        [Email],
        [Gender],
        [Active]
        From TutorialAppSchema.Users
    ";
    IEnumerable<User> users = _dapper.LoadData<User>(sql);

    return users;
  }


  [HttpGet("GetUserSalary")]
  public IEnumerable<UserSalary> GetUserSalary()
  {
    string sql = @"
      SELECT [UserId],
        [FirstName],
        [LastName],
        [Email],
        [Gender],
        [Active]
        From TutorialAppSchema.Users
    ";
    IEnumerable<UserSalary> salaries = _dapper.LoadData<UserSalary>(sql);

    return salaries;
  }

  [HttpDelete("DeleteSalary/{id}")]
  public IActionResult DeleteSalary(int id)
  {
    string sql = @"
      DELETE FROM TutorialAppSchema.UserSalary
      WHERE UserId" + id.ToString();

    if (_dapper.ExecuteWithRows<UserSalary>(sql) > 0)
    {
      Ok();
    }

    throw new Exception("Couldn't Delete Salary");
  }

  [HttpPut("EditSalary")]
  public IActionResult EditSalary(UserSalary salary)
  {

    string sql = @"
      UPDATE INTO
        [Salary]
        [AvgSalary]
        TutorialAppSchema.UserSalary
          SET
          [Salary] = '" + salary.Salary +
          "', [AvgSalary] = '" + salary.AvgSalary +
          "' WHERE UserId =  = '" + salary.UserId;

    if (_dapper.ExecuteWithRows<UserSalary>(sql) > 0)
    {
      Ok();
    }

    throw new Exception("Delete Salary");
  }

  [HttpPut("AddSalary")]

  public IActionResult AddSalary(UserSalary salary)
  {

    string sql = @"
      INSERT INTO TutorialAppSchema.UserSalary(
        [Salary]
        [UserId]
        ) VALUES (" +
        "'" + salary.UserId +
       "','" + salary.Salary +
       ")";

    Console.WriteLine(sql);

    if (_dapper.ExecuteWithRows<UserSalary>(sql) > 0)
    {
      Ok();
    }

    throw new Exception("Couldn't Add Salary");
  }




  [HttpGet("GetUsers/{id}")]
  public User GetSingleUser(int id)
  {
    //return new string[] { "user1", "user2", testValue };
    string sql = @"
     SELECT [UserId],
        [FirstName],
        [LastName],
        [Email],
        [Gender],
        [Active]
        From TutorialAppSchema.Users WHERE UserId = " + id.ToString();
    User user = _dapper.LoadDataSingle<User>(sql);

    return user;
  }

  [HttpDelete("DeleteUser/{id}")]
  public IActionResult DeleteUser(int id)
  {
    string sql = @"
     DELETE FROM tutorialAppSchema.Users WHERE UserId = " + id.ToString();


    if (_dapper.Execute(sql))
    {
      return Ok();
    }

    throw new Exception("Failed to Add User");
  }

  [HttpPut("AddUser")]
  public IActionResult AddUser(UserDTO user)
  {

    string sql =
   @"INSERT INTO TutorialAppSchema.Users(
        [FirstName],
        [LastName],
        [Email],
        [Gender],
        [Active]
      ) VALUES (" +
        "'" + user.FirstName +
       "','" + user.LastName +
       "','" + user.Email +
       "','" + user.Gender +
       "','" + user.Active +
       "')";

    if (_dapper.Execute(sql))
    {
      return Ok();
    }

    throw new Exception("Failed to Add User");
  }

  [HttpPut("EditUser")]
  public IActionResult EditUser(User user)
  {

    string sql =
    @"UPDATE INTO TutorialAppSchema.Users
        SET
        [FirstName] = '" + user.FirstName +
        "', [LastName] = '" + user.LastName +
        "', [Email]  = '" + user.Email +
        "',[Gender]  = '" + user.Gender +
        "', [Active] = '" + user.Active +
        "' WHERE Userid =  = '" + user.UserId;

    if (_dapper.Execute(sql))
    {
      return Ok();
    }

    throw new Exception("Failed to Update User");
  }

  [HttpGet("TestConnection")]
  public DateTime TestConnection()
  {
    return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
  }
}
