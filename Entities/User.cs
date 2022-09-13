namespace MyProject.Entities;

public class User
{

  public ulong Id { get; set; }
  public string? Name { get; set; }
  public string? Email { get; set; }
  public string? Password { get; set; }

  [Obsolete]
  public User() {}
  
  public User(string? name, string? email, string? password)
  {
    Name = name;
    Email = email;
    Password = password;
  }
}