namespace RESTApi.Models;

public class Car : BaseModel
{
    public string Name { get; init; } = null!;
    public string Number { get; init; } = null!;
}