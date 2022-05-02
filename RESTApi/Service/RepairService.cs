using RESTApi.Models;
using RESTApi.Repo;
using static System.String;

namespace RESTApi.Service;

public class RepairService  : IRepairService
{
    private IBaseRepository<Document> Documents { get; set; } = null!;
    private IBaseRepository<Car> Cars { get; set; } = null!;
    private IBaseRepository<Worker> Workers { get; set; } = null!;

    public void Work()
    {
        var rand = new Random();
        var carId = Guid.NewGuid();
        var workerId = Guid.NewGuid();

        Cars.Create(new Car
        {
            Id = carId,
            Name = Format($"Car{rand.Next()}"),
            Number = Format($"{rand.Next()}")
        });

        Workers.Create(new Worker
        {
            Id = workerId,
            Name = Format($"Worker{rand.Next()}"),
            Position = Format($"Position{rand.Next()}"),
            Telephone = Format($"8916{rand.Next()}{rand.Next()}{rand.Next()}{rand.Next()}{rand.Next()}{rand.Next()}{rand.Next()}")
        });

        var car = Cars.Get(carId);
        var worker = Workers.Get(workerId);

        if (car == null) return;
        if (worker == null) return;
        Documents.Create(new Document
        {
            CarId = car.Id,
            WorkerId = worker.Id,
            Car = car,
            Worker = worker
        });
    }
}