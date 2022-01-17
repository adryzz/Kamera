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
        ulong reading = 0;
        for (int i = 0; i < Configuration.ReadingsPerPoint; i++)
        {
            reading += Program.Reader.Read();
        }

        reading /= Configuration.ReadingsPerPoint;
        
        return new DataPoint
        {
            Data = (ushort)reading,
            X = Program.Reader.Column,
            Y = Program.Reader.Row
        };
    }
    
    [HttpPost("[action]")]
    public RawDataPoint GetRawPoint([FromHeader] int x, [FromHeader] int y)
    {
        ushort[] readings = new ushort[Configuration.ReadingsPerPoint];
        for (int i = 0; i < Configuration.ReadingsPerPoint; i++)
        {
            readings[i]= Program.Reader.Read();
        }

        return new RawDataPoint()
        {
            Data = readings,
            X = Program.Reader.Column,
            Y = Program.Reader.Row
        };
    }
    
    [HttpPost("[action]")]
    public DataPoint GetSinglePoint([FromHeader] int x, [FromHeader] int y)
    {
        return new DataPoint
        {
            Data = Program.Reader.Read(),
            X = Program.Reader.Column,
            Y = Program.Reader.Row
        };
    }
}
