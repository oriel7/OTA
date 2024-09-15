using MassTransit;
using OT.Assessment.Core.Domain.DTO;

namespace OT.Assessment.MyConsumer.Consumers
{
    public class CreateCasinoWagerConsumer : IConsumer<CasinoWagerDTO>
    {
        private readonly ILogger<CreateCasinoWagerConsumer> _logger;
        public CreateCasinoWagerConsumer(ILogger<CreateCasinoWagerConsumer> logger) 
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CasinoWagerDTO> context)
        {
            _logger.LogInformation("Casino wager message consumed");

            var casinoWager = context.Message;

            await Task.Delay(1000);
        }
    }
}
