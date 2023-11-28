using System.Threading.Channels;

namespace Trout8A.Services
{
    public class UCIProducer
    {
        private readonly IHostApplicationLifetime hostAppLifetime;
        private readonly ILogger<UCIProducer> log;

        public UCIProducer(ILogger<UCIProducer> logger, IHostApplicationLifetime HostLifetime)
        {
            hostAppLifetime = HostLifetime;     
            log = logger;
            log.LogInformation("In UCIProducer");
        }

        public async Task BeginProducing(ChannelWriter<string> channelWriter)
        {
            log.LogInformation($"BeginProducing starting.");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Enter a command:");
                string? command = Console.ReadLine();
                if (!string.IsNullOrEmpty(command))
                {
                    switch (command.ToLower())
                    {
                        case "exit":
                            exit = true;
                            hostAppLifetime.StopApplication();
                            break;
                        default:
                            log.LogInformation($"Processing command: {command}");
                            await channelWriter.WriteAsync(command);
                            break;
                    }
                }
}
        }
    }    
}