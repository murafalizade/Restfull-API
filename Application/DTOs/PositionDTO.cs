namespace Application.DTOs;

public record PositionDTO
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public double Salary { get; set; }
};