using Microsoft.Extensions.Options;
using Workshop.Api.Bll;
using Workshop.Api.Bll.Services;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Repositories;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<PriceCalculatorService>(_configuration.GetSection("PriceCalculatorOptions"));
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName);
        });
        services.AddScoped<IPriceCalculatorService, PriceCalculatorService>();
        services.AddSingleton<IStorageRepository, StorageRepository>();
        services.AddSingleton<IAnalyticsService, AnalyticsService>();
    }
    
    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseRouting();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}