using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.DTOS;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
  IMapper _mapper;

  IUserRepository _userRepository;

  public UserEFController(IConfiguration config, IUserRepository repository)
  {
    Console.WriteLine(config.GetConnectionString("DefaultConnection"));
    _mapper = new Mapper(new MapperConfiguration(
      c =>
      {
        c.CreateMap<UserDTO, User>();
        c.CreateMap<UserSalaryDTO, UserSalary>();
      }
    ));
    _userRepository = repository;
  }

  // User
  [HttpGet("GetUsers")]
  public IEnumerable<User> GetUsers()
  {
    return _userRepository.GetUsers();
  }

  [HttpGet("GetUsers/{id}")]
  public User GetSingleUser(int id)
  {
    return _userRepository.GetSingleUser(id);
  }

  [HttpDelete("DeleteUser/{id}")]
  public IActionResult DeleteUser(int id)
  {

    User? userDB = _userRepository.GetSingleUser(id);

    if (userDB != null)
    {

      _userRepository.RemoveEntity(userDB);

      if (_userRepository.SaveChanges())
      {
        return Ok();
      }

      throw new Exception("Failed to Delete User");
    }

    throw new Exception("Failed to Add User");
  }

  [HttpPut("AddUser")]
  public IActionResult AddUser(UserDTO user)
  {

    User userDB = _mapper.Map<User>(user);

    _userRepository.AddEntity(userDB);

    if (_userRepository.SaveChanges())
    {
      return Ok();
    }

    throw new Exception("Failed to Add User");
  }

  [HttpPut("EditUser")]
  public IActionResult EditUser(User user)
  {

    User? userDB = _userRepository.GetSingleUser(user.UserId);

    if (userDB != null)
    {
      userDB.Active = user.Active;
      userDB.FirstName = user.FirstName;
      userDB.LastName = user.LastName;
      userDB.Email = user.Email;
      userDB.Gender = user.Gender;


      if (_userRepository.SaveChanges())
      {
        return Ok();
      }

      throw new Exception("Failed to Update User");
    }

    throw new Exception("Failed to Get User");
  }

  // UserSalary

  [HttpGet("GetSalaries")]
  public IEnumerable<UserSalary> GetSalaries()
  {
    return _userRepository.GetUserSalaries();
  }

  [HttpGet("GetSalary/{id}")]
  public UserSalary GetSalary(int id)
  {
    return _userRepository.GetSalary(id);
  }

  [HttpDelete("DeleteSalary/{id}")]
  public IActionResult DeleteSalary(int id)
  {
    UserSalary salary = _userRepository.GetSalary(id);

    if (salary != null)
    {
      _userRepository.RemoveEntity(salary);

      if (_userRepository.SaveChanges())
      {
        return Ok();
      }

      throw new Exception("Couldn't remove salary");
    }

    throw new Exception("Couldn't find salary");
  }

  [HttpPut("EditSalary")]
  public IActionResult EditSalary(UserSalary salary)
  {
    UserSalary salaryDB = _userRepository.GetSalary(salary.UserId);

    if (salaryDB != null)
    {

      _mapper.Map(salary, salaryDB);

      if (_userRepository.SaveChanges())
      {
        return Ok();
      }

      throw new Exception("Couldn't update salary");
    }

    throw new Exception("Couldn't find salary");
  }

  [HttpPut("AddSalary")]
  public IActionResult AddSalary(UserSalary salary)
  {
    _userRepository.AddEntity(salary);
    if (_userRepository.SaveChanges())
    {
      return Ok();
    }

    throw new Exception("Couldn't add salary");
  }
}
