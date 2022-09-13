using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyProject.Entities.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(b => b.Id);
    builder.Property(b => b.Id).ValueGeneratedOnAdd();
    builder.Property(b => b.Name).HasColumnName("nvarchar(56)").IsRequired();
    builder.Property(b => b.Email).HasColumnName("nvarchar(100)").IsRequired();
    builder.Property(b => b.Password).HasColumnName("nvarchar(30)").IsRequired();
  }
}