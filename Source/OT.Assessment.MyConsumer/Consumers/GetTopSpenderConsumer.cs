using MassTransit;
using OT.Assessment.Core.Domain.DTO;

namespace OT.Assessment.MyConsumer.Consumers
{
    public class GetTopSpenderConsumer : IConsumer<TopSpenderData>
    {
        private readonly ILogger<GetTopSpenderConsumer> _logger;

        public GetTopSpenderConsumer(ILogger<GetTopSpenderConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<TopSpenderData> context)
        {
            var data = context.Message;

            var response = new List<PlayerDTO>();

            await context.RespondAsync(response);
        }
    }
}
