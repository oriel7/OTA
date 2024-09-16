using MassTransit;
using OT.Assessment.Core.Domain.DTO;

namespace OT.Assessment.MyConsumer.Consumers
{
    public class GetCasinoWagerConsumer : IConsumer<PlayerData>
    {
        private readonly ILogger<GetCasinoWagerConsumer> _logger;

        public GetCasinoWagerConsumer(ILogger<GetCasinoWagerConsumer> logger)
        {
            _logger = logger;   
        }

        public async Task Consume(ConsumeContext<PlayerData> context)
        {
            var data = context.Message;

            var response = new List<CasinoWagerResponseDTO>();

            await context.RespondAsync(response);
        }
    }
}
