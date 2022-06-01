using RESTApi.Models;

namespace RESTApi.Service.interfaces;

public interface ICarService
{
    public List<Car> GetAllCars();
}