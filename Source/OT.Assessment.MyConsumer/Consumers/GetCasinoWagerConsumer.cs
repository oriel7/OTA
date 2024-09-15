using MassTransit;
using OT.Assessment.Core.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var response = new List<CasinoWagerDTO>();

            await context.RespondAsync(response);
        }
    }
}
