
using System.Reflection.PortableExecutable;
using System.Threading.Channels;

namespace Trout8A.Services
{
    public class UCIConsumer //: IHostedService
    {
        private readonly ILogger<UCIConsumer> log;

        public UCIConsumer(ILogger<UCIConsumer> logger)
        {
            log = logger;
            log.LogInformation("In UCIConsumer");
        }

        public async Task ConsumeData(ChannelReader<string> channelReader)
        {
            // Take an item out of the queue and act on it.
            log.LogInformation($"CONSUMER : Starting");

            while (await channelReader.WaitToReadAsync())
            {
                if (channelReader.TryRead(out var timeString))
                {

                    log.LogInformation($"CONSUMER : Consuming {timeString}");
                }
            }

            log.LogInformation($"CONSUMER : Completed");
        }
    }
}