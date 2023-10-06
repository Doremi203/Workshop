using Workshop.Bll.Separated;
using Workshop.Bll.Services.Interfaces;

namespace Workshop.Api.HostedServices;

public class GoodsSyncHostedService : IHostedService
{
    private readonly IGoodsRepository _goodsRepository;

    private readonly IServiceProvider _serviceProvider;
    //private readonly IGoodsService _goodsService;

    public GoodsSyncHostedService(
        IGoodsRepository goodsRepository,
        IServiceProvider serviceProvider
        //IGoodsService goodsService
        )
    {
        _goodsRepository = goodsRepository;
        _serviceProvider = serviceProvider;
        //_goodsService = goodsService;
    }
    
    private async void ExecuteAsync()
    {
        while (true)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var goodsService = scope.ServiceProvider.GetRequiredService<IGoodsService>();
                // TODO: Get GoodEntities and call AddOrUpdate for each of them
                var goods = goodsService.GetGoods().ToList();
                foreach (var good in goods) 
                    _goodsRepository.AddOrUpdate(good);
            }
            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        ExecuteAsync();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) 
        => Task.CompletedTask;
}