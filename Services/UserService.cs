using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using MyProject.Repositories;

namespace MyProject.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;
  private readonly ILogger<UserService> _logger;

  public UserService(IUserRepository userRepository, ILogger<UserService> logger)
  {
    _userRepository = userRepository;
    _logger = logger;
  }

  public async ValueTask<Result<User>> CreateUser(string name, string email, string password)
  {
    if (string.IsNullOrEmpty(name))
      return new("Name is invalid");

    if (string.IsNullOrEmpty(email))
      return new("Email is invalid");

    if (string.IsNullOrEmpty(password))
      return new("Password is invalid");

    var entity = new Entities.User(name, email, password);

    try
    {
      var createdUser = await _userRepository.Add(entity);

      if (createdUser == null)
        return new("Can't create user");

      return new(true) { Data = ToModel(createdUser) };
    }
    catch (DbUpdateException dbUpdateException)
    {
      _logger.LogInformation("Error occured:", dbUpdateException);
      return new("Couldn't create user. Contact support.");
    }
    catch (Exception e)
    {
      _logger.LogError($"Error occured at {nameof(UserService)}", e);
      throw new("Couldn't create user. Contact support.", e);
    }
  }

  public async ValueTask<Result<User>> DeleteUser(ulong id)
  {
    try
    {
      var user = await _userRepository.GetById(id);
      if (user is null)
        return new("User is not found");

      var removedUser = await _userRepository.Remove(user);
      if (removedUser == null)
        return new("Can't remove user");

      return new(true) { Data = ToModel(removedUser) };
    }
    catch (Exception e)
    {
      _logger.LogError($"Error occured at {nameof(UserService)}", e);
      throw new("Couldn't create user. Contact support.", e);
    }
  }


  public async ValueTask<Result<List<User>>> GetAllUsers()
  {
    try
    {
      var existingUser = await _userRepository.GetAll();
      if (existingUser is null)
        return new("No users to get");

      return new(true) { Data = existingUser.Select(ToModel).ToList() };
    }
    catch (Exception e)
    {
      _logger.LogError($"Error occured at {nameof(UserService)}", e);
      throw new("Couldn't create user. Contact support.", e);
    }
  }

  public async ValueTask<Result<User>> GetUserById(ulong id)
  {
    try
    {
      var existingUser = await _userRepository.GetById(id);
      if (existingUser is null)
        return new("User not found");

      return new(true) { Data = ToModel(existingUser) };
    }
    catch (Exception e)
    {
      _logger.LogError($"Error occured at {nameof(UserService)}", e);
      throw new("Couldn't create user. Contact support.", e);
    }
  }

  public async ValueTask<bool> ExistsAsync(ulong id)
  {
    var carResult = await GetUserById(id);
    return carResult.IsSuccess;
  }

  public async ValueTask<Result<User>> UpdateUser(ulong id, string name, string email, string password)
  {

    var existingUser = await _userRepository.GetById(id);
    if (existingUser == null)
      return new("User not found");

    existingUser.Name = name;
    existingUser.Email = email;
    existingUser.Password = password;

    try
    {
      var updatedUser = await _userRepository.Update(existingUser);

      return new(true) { Data = ToModel(updatedUser!) };
    }
    catch (DbUpdateException dbUpdateException)
    {
      _logger.LogInformation("Error occured:", dbUpdateException);
      return new("Couldn't create user. Contact support.");
    }
    catch (Exception e)
    {
      _logger.LogError($"Error occured at {nameof(UserService)}", e);
      throw new("Couldn't create user. Contact support.", e);
    }
  }

  private User ToModel(Entities.User entity)
  => new()
  {
    Name = entity.Name,
    Email = entity.Email,
    Password = entity.Password
  };

}