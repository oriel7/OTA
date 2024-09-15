using OT.Assessment.Core.Domain.Models;
using OT.Assessment.Core.Services.Services;

namespace OT.Assessment.Infrastructure.Service.Services
{
    public class CasinoWagerService : ICasinoWagerService
    {
        public async Task<IEnumerable<CasinoWager>> GetCasinoWagersAsync(int playerId)
        {
            await Task.CompletedTask;

            throw new NotImplementedException();
        }

        public async Task CreateCasinoWagerAsync(CasinoWager wager)
        {
            await Task.CompletedTask;

            throw new NotImplementedException();
        }
    }
}
