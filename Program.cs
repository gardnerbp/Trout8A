using Trout8A.Services;

namespace Trout8A
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            builder.Services.AddSingleton<UCIProducer>();
            builder.Services.AddSingleton<UCIConsumer>();
            
            //builder.Services.Configure<HostOptions>(x =>
            //{
            //    x.ServicesStartConcurrently = true;
            //    x.ServicesStopConcurrently = true;
            //});

            var host = builder.Build();
            host.Run();
        }
    }
}