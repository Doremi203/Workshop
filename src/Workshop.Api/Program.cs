namespace Workshop.Api;

public class Program
{
    public static void Main()
    {
        var builder = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(hostBuilder => hostBuilder.UseStartup<Startup>());
        
        builder.Build().Run();
    }
}