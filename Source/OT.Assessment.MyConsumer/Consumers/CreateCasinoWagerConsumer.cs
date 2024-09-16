using MassTransit;
using OT.Assessment.Core.Domain.DTO;
using OT.Assessment.Core.Services.Services;

namespace OT.Assessment.MyConsumer.Consumers
{
    public class CreateCasinoWagerConsumer : IConsumer<CasinoWagerDTO>
    {
        private readonly ILogger<CreateCasinoWagerConsumer> _logger;
        private readonly IPlayerCasinoWagerService _playerCasinoWagerService;
        public CreateCasinoWagerConsumer(IPlayerCasinoWagerService playerCasinoWagerService, ILogger<CreateCasinoWagerConsumer> logger) 
        {
            _playerCasinoWagerService = playerCasinoWagerService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CasinoWagerDTO> context)
        {
            _logger.LogInformation("Casino wager message consumed");

            var casinoWager = context.Message;

            await _playerCasinoWagerService.CreateCasinoWagerAsync(casinoWager);

            await Task.Delay(1000);
        }
    }
}
