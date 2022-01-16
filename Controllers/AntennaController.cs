using Microsoft.AspNetCore.Mvc;

namespace Kamera.Controllers;

[ApiController]
[Route("[controller]")]
public class AntennaController : ControllerBase
{

    private readonly ILogger<AntennaController> _logger;

    public AntennaController(ILogger<AntennaController> logger)
    {
        _logger = logger;
    }

    [HttpPost("[action]")]
    public DataPoint GetPoint([FromHeader] int x, [FromHeader] int y)
    {
        return new DataPoint();
    }
    
    [HttpPost("[action]")]
    public RawDataPoint GetRawPoint([FromHeader] int x, [FromHeader] int y)
    {
        return new RawDataPoint();
    }
    
    [HttpPost("[action]")]
    public DataPoint GetSinglePoint([FromHeader] int x, [FromHeader] int y)
    {
        return new DataPoint();
    }
}
