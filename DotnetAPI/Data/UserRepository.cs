


using DotnetAPI.Models;

namespace DotnetAPI.Data
{


  public class UserRepository : IUserRepository
  {

    DataContextEF _entityFramework;

    public UserRepository(IConfiguration config)
    {
      _entityFramework = new DataContextEF(config);
    }

    public bool SaveChanges()
    {
      return _entityFramework.SaveChanges() > 0;
    }

    public void AddEntity<T>(T entity)
    {
      if (entity != null)
      {
        _entityFramework.Add(entity);
      }
    }

    public void RemoveEntity<T>(T entity)
    {
      if (entity != null)
      {
        _entityFramework.Remove(entity);
      }
    }

    public IEnumerable<User> GetUsers()
    {
      IEnumerable<User> users = _entityFramework.Users.ToList<User>();

      return users;

    }

    public User GetSingleUser(int id)
    {
      User? user = _entityFramework.Users.Where((user) => user.UserId == id).FirstOrDefault<User>();

      if (user != null)
      {
        return user;
      }

      throw new Exception("Failed to Get User");
    }

    public IEnumerable<UserSalary> GetUserSalaries()
    {
      return _entityFramework.UserSalary.ToList<UserSalary>();
    }

    public UserSalary GetSalary(int id)
    {
      UserSalary? salary = _entityFramework.UserSalary.Where((salary) => salary.UserId == id).FirstOrDefault();

      if (salary != null)
      {
        return salary;
      }

      throw new Exception("Couldn't find salary");
    }
  }

}
