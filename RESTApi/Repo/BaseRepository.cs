using System.Diagnostics;
using RESTApi.Context;
using RESTApi.Models;

namespace RESTApi.Repo;

public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseModel
{
    private ApplicationContext Context { get; set; }

    public BaseRepository(ApplicationContext context)
    {
        Context = context;
    }

    public List<TDbModel> GetAll()
    {
        return Context.Set<TDbModel>().ToList();
    }

    public TDbModel? Get(Guid id)
    {
        return Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
    }

    public TDbModel Create(TDbModel model)
    {
        Context.Set<TDbModel>().Add(model);
        Context.SaveChanges();
        return model;
    }

    public TDbModel Update(TDbModel model)
    { 
        var toUpdate = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == model.Id); 
        if (toUpdate != null) 
        { 
            toUpdate = model; 
        } 
        Context?.Update(toUpdate);
        Context?.SaveChanges();
        Debug.Assert(toUpdate != null, nameof(toUpdate) + " != null");
        return toUpdate;
    }

    public void Delete(Guid id)
    {
        var toDelete = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
        if (toDelete != null) Context.Set<TDbModel>().Remove(toDelete);
        Context.SaveChanges();
    }
}