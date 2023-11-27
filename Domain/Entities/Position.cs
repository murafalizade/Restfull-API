namespace Domain.Entities;

public class Position:BaseEntity
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public double Salary { get; set; }
}