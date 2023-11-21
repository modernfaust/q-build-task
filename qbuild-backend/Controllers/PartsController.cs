using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Backend.Controllers;

[Route("[controller]")]
[ApiController]
public class PartsController : ControllerBase
{
    private readonly DataService _dataService;
    //private readonly ILogger<PartsController> _logger;

    public PartsController(DataService dataService)
    {
        _dataService = dataService;
        _dataService.LoadData("db/bom.csv", "db/part.csv");
    }

    [EnableCors("AllowAllOrigins")]
    [HttpGet("billofmaterials")]
    public IActionResult GetAllBillofMaterialItems()
    {
        var items = _dataService.GetAllBillofMaterialItems();
        return Ok(items);
    }

    [EnableCors("AllowAllOrigins")]
    [HttpGet("parent")]
    public IActionResult GetAllParts()
    {
        var items = _dataService.GetAllParts();
        return Ok(items);
    }

    [EnableCors("AllowAllOrigins")]
    [HttpGet("parent/indentedbomview")]
    public IActionResult GetAllIndentedBomView()
    {
        var items = _dataService.GetAllIndentedBomView();
        return Ok(items);
    }

    [EnableCors("AllowAllOrigins")]
    [HttpGet("component/{name}")]
    public IActionResult GetDetailsByComponentName(string name)
    {
        var items = _dataService.GetDetailsByComponentName(name.ToUpper());
        return Ok(items);
    }

    [EnableCors("AllowAllOrigins")]
    [HttpGet("parent/{name}")]
    public IActionResult GetDetailsByParent(string name)
    {
        var items = _dataService.GetDetailsByParent(name.ToUpper());
        return Ok(items);
    }    
    
    [EnableCors("AllowAllOrigins")]
    [HttpGet("parent/{name}/component/")]
    public IActionResult GetComponentsByParent(string name)
    {
        var items = _dataService.GetComponentsByParent(name.ToUpper());
        return Ok(items);
    }

    [EnableCors("AllowAllOrigins")]
    [HttpGet("parent/{name}/component/{componentName}")]
    public IActionResult GetComponentDetailsByParent(string name, string componentName)
    {
        var items = _dataService.GetComponentDetailsByParent(name.ToUpper(), componentName.ToUpper());
        return Ok(items);
    }
}