using MyProject.Models;

namespace MyProject.Services;

public interface IUserService
{
  ValueTask<Result<User>> GetUserById(ulong id);
  ValueTask<Result<List<User>>> GetAllUsers();
  ValueTask<Result<User>> CreateUser(string name, string email, string password);
  ValueTask<Result<User>> UpdateUser(ulong id, string name, string email, string password);
  ValueTask<Result<User>> DeleteUser(ulong id);
  ValueTask<bool> ExistsAsync(ulong id);
}