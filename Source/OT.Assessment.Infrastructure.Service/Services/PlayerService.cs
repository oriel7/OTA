using OT.Assessment.Core.Domain.Models;
using OT.Assessment.Core.Services.Services;

namespace OT.Assessment.Infrastructure.Service.Services
{
    public class PlayerService : IPlayerService
    {
        public async Task<IEnumerable<Player>> GetTopSpendersAsync(int count)
        {
            await Task.CompletedTask;

            throw new NotImplementedException();
        }
    }
}
