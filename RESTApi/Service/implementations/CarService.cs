using RESTApi.Models;
using RESTApi.Repo;
using RESTApi.Service.interfaces;

namespace RESTApi.Service.implementations;

public class CarService : ICarService
{
    private readonly IBaseRepository<Car> _baseRepository;

    public CarService(IBaseRepository<Car> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public List<Car> GetAllCars()
    {
        return _baseRepository.GetAll();
    }
}