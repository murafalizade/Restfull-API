namespace Domain.Entities;

public abstract class AppUser:BaseEntity
{
    public string UserName { get; set; }
    public string Role { get; set; }
}