using OT.Assessment.Core.Domain.DTO;
using OT.Assessment.Core.Repository.Repositories;
using OT.Assessment.Core.Services.Services;

namespace OT.Assessment.Infrastructure.Service.Services
{
    public class PlayerCasinoWagerService : IPlayerCasinoWagerService
    {
        private readonly IPlayerCasinoWagerRepository _playerCasinoWagerRepository;
        public PlayerCasinoWagerService(IPlayerCasinoWagerRepository playerCasinoWagerRepository)
        {
            _playerCasinoWagerRepository = playerCasinoWagerRepository;
        }

        public async Task CreateCasinoWagerAsync(CasinoWagerDTO casinoWager)
        {
            await _playerCasinoWagerRepository.CreateCasinoWagerAsync(casinoWager);
        }

        public async Task<IEnumerable<CasinoWagerResponseDTO>> GetCasinoWagersAsync(Guid playerId)
        {
            return await _playerCasinoWagerRepository.GetCasinoWagerAsync(playerId);
        }

        public async Task<IEnumerable<TopSpenderDTO>> GetTopSpendersAsync(int count)
        {
            return await _playerCasinoWagerRepository.GetTopSpendersAsync(count);
        }
    }
}
