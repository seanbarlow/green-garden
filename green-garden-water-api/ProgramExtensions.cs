public static class ProgramExtensions
{
    private const string AppName = "Water API";


    public static void AddCustomMvc(this WebApplicationBuilder builder)
    {
        // TODO DaprClient good enough?
        builder.Services.AddControllers().AddDapr();
    }


    public static void AddCustomApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<WaterRepository, WaterRepository>();
    }
}