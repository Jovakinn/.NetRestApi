using Microsoft.AspNetCore.Mvc;
using RESTApi.Models;
using RESTApi.Repo;
using RESTApi.Service;
using RESTApi.Service.interfaces;

namespace RESTApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MainController : ControllerBase
{
    private IRepairService RepairService { get; set; }
    private IBaseRepository<Document> Documents { get; set; }

    public MainController(IRepairService repairService, IBaseRepository<Document> documents)
    {
        RepairService = repairService;
        Documents = documents;
    }

    [HttpGet]
    public JsonResult Get()
    {
        return new JsonResult(Documents.GetAll());
    }

    [HttpPost]
    public JsonResult Post()
    {
        RepairService.Work();
        return new JsonResult("Work was successfully completed");
    }
    
    [HttpPut]
    public JsonResult Put(Document doc)
    {
        var success = true;
        var document = Documents.Get(doc.Id);
        try
        {
            if (document != null)
            {
                document = Documents.Update(doc);
            }
            else
            {
                success = false;
            }
        }
        catch (Exception)
        {
            success = false;
        }

        return success ? new JsonResult($"Update successful {document.Id}") : new JsonResult("Update was not successful");
    }

    [HttpDelete]
    public JsonResult Delete(Guid id)
    {
        var success = true;
        var document = Documents.Get(id);

        try
        {
            if (document != null)
            {
                Documents.Delete(document.Id);
            }
            else
            {
                success = false;
            }
        }
        catch (Exception)
        {
            success = false;
        }

        return success ? new JsonResult("Delete successful") : new JsonResult("Delete was not successful");
    }
}