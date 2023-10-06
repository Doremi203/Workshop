using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using Workshop.Api.Bll;
using Workshop.Api.HostedServices;
using Workshop.Bll.Separated;
using Workshop.Bll.Services;
using Workshop.Bll.Services.Interfaces;
using Workshop.Dal.Dal.Repositories;

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

        services.AddFluentValidation(configuration =>
        {
            configuration.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
            configuration.AutomaticValidationEnabled = true;
        });

        services.AddScoped(x => 
            x.GetRequiredService<IOptionsSnapshot<PriceCalculatorOptions>>().Value);
        
        services.AddScoped<IPriceCalculatorService, PriceCalculatorService>();
        services.AddSingleton<IStorageRepository, StorageRepository>();
        services.AddSingleton<IAnalyticsService, AnalyticsService>();
        services.AddSingleton<IGoodsRepository, GoodsRepository>();
        services.AddScoped<IGoodsService, GoodsService>();
        services.AddHostedService<GoodsSyncHostedService>();
        services.AddHttpContextAccessor();
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