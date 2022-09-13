using MyProject.Data;
using MyProject.Entities;

namespace MyProject.Repositories;

public class UserRepository : IUserRepository
{
  private readonly AppDbContext _context;

  public UserRepository(AppDbContext context)
  {
    _context = context;
  }

  public async ValueTask<User?> Add(User user)
  {
    var entry = await _context.Set<User>().AddAsync(user);

    await _context.SaveChangesAsync();

    return entry.Entity;
  }

  public ValueTask<IQueryable<User>> GetAll()
    => ValueTask.FromResult(_context.Set<User>().AsQueryable());

  public ValueTask<User?> GetById(ulong id)
    {
      var entry = _context.Set<User>().Find(id);

      return ValueTask.FromResult(entry);
    }

  public async ValueTask<User?> Remove(User user)
  {
    var entry = _context.Set<User>().Remove(user);

    await _context.SaveChangesAsync();

    return entry.Entity;
  }

  public async ValueTask<User?> Update(User user)
  {
    var entry = _context.Set<User>().Update(user);

    await _context.SaveChangesAsync();

    return entry.Entity;
  }
}