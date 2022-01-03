
public class WaterRepository
{
    private const string StoreName = "water-statestore";

    private readonly DaprClient _daprClient;
    private readonly ILogger _logger;

    public WaterRepository(DaprClient daprClient, ILogger<WaterRepository> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    public async Task SaveWaterEventAsync(WaterEvent waterEvent)
    {
        var state = await _daprClient.GetStateEntryAsync<WaterEvent>(StoreName, waterEvent.PumpId);
        state.Value = waterEvent;

        await state.SaveAsync();

        _logger.LogInformation("Water Event persisted successfully.");
    }

    public async Task<WaterStatus> GetStatusAsync(string pumpId)
    {
        var waterEvent = await GetWaterEventAsync(pumpId);
        var waterStatus = new WaterStatus
        {
            PumpId = waterEvent.PumpId,
            Status = waterEvent.Action,
            LastUpdate = waterEvent.EventDateTime
        };
        return waterStatus;
    }

    public Task<WaterEvent> GetWaterEventAsync(string pumpId) =>
        _daprClient.GetStateAsync<WaterEvent>(StoreName, pumpId);
}
