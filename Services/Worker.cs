using System.Threading.Channels;

namespace Trout8A.Services;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> log;
    public readonly UCIProducer uciProducer;
    public readonly UCIConsumer uciConsumer;

    public Worker(ILogger<Worker> logger, UCIProducer producer, UCIConsumer consumer)
    {
        log = logger;
        uciProducer = producer;
        uciConsumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channel = Channel.CreateUnbounded<string>();

        // In this example, the consumer keeps up with the producer
        log.LogInformation("Beginning ");

        Task consumerTask1 = uciConsumer.ConsumeData(channel.Reader);    // begin consuming
        Task producerTask1 = uciProducer.BeginProducing(channel.Writer); // begin producing

        // wait until both producer and consumer have finished processing.
        await producerTask1.ContinueWith(_ => channel.Writer.Complete());
        await consumerTask1;

    }
}
