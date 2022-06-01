namespace RESTApi.Models;

public sealed class Document : BaseModel
{
    public Guid WorkerId { get; set; }
    public Car Car { get; set; }
    public Worker Worker { get; set; }
}
