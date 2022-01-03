using System.Net;

[Route("api/v1/[controller]")]
[ApiController]
public class WaterController : ControllerBase
{

    private readonly ILogger<WaterController> _logger;
    private readonly WaterRepository _waterRepository;

    public WaterController(ILogger<WaterController> logger, WaterRepository waterRepository)
    {
        _logger = logger;
        _waterRepository = waterRepository;
    }

    [HttpPost]
    [ProducesResponseType(typeof(WaterEvent), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> SaveEventAsync(WaterEvent waterEvent)
    {
        await _waterRepository.SaveWaterEventAsync(waterEvent);

        return Ok();

    }

    [HttpGet]
    [ProducesResponseType(typeof(WaterStatus), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<WaterStatus>> GetLatestAsync()
    {
        return Ok(await _waterRepository.GetStatusAsync("1"));
    }
}