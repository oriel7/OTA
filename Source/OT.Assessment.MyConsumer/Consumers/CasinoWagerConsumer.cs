using MassTransit;
using OT.Assessment.Core.Domain.Models;

namespace OT.Assessment.MyConsumer.Consumers
{
    public class CasinoWagerConsumer : IConsumer<CasinoWager>
    {
        private readonly ILogger<CasinoWagerConsumer> _logger;
        public CasinoWagerConsumer(ILogger<CasinoWagerConsumer> logger) 
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CasinoWager> context)
        {
            await Task.CompletedTask;

            _logger.LogInformation("Casino wager message consumed");

            var casinoWager = context.Message;
        }
    }
}
