namespace Application.Wrappers;

public class Response<T>
{
    public T Message { get; set; }
    public bool Succeeded { get; set; } = false;
}