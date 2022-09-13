using MyProject.Entities;

namespace MyProject.Repositories;

public interface IUserRepository
{
  ValueTask<User?> Add(User user);
  ValueTask<IQueryable<User>> GetAll();
  ValueTask<User?> GetById(ulong id);
  ValueTask<User?> Update(User user);
  ValueTask<User?> Remove(User user);
}