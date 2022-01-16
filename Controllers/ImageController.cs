using Microsoft.AspNetCore.Mvc;

namespace Kamera.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{

    private readonly ILogger<ImageController> _logger;

    private readonly ImageGenerator _generator;

    public ImageController(ILogger<ImageController> logger, ImageGenerator generator)
    {
        _logger = logger;
        _generator = generator;
    }

    [HttpPost("[action]")]
    public void StartProcessing() => _generator.StartProcessing();

    [HttpPost("[action]")]
    public void StopProcessing() => _generator.StopProcessing();

    [HttpGet("[action]")]
    public ImageGenerationProgress GetProgress() => _generator.GetProgress();

    [HttpGet("[action]")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public IActionResult GetLastImage()
    {
        if (System.IO.File.Exists("image.png"))
            return PhysicalFile(Path.GetFullPath("image.png"), "image/png", false);

        return NotFound();
    }
}