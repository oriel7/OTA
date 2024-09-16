using OT.Assessment.Core.Domain.DTO;

namespace OT.Assessment.Core.Repository.Repositories
{
    public interface IPlayerCasinoWagerRepository
    {
        Task CreateCasinoWagerAsync(CasinoWagerDTO casinoWager);
        Task<IEnumerable<CasinoWagerResponseDTO>> GetCasinoWagerAsync(Guid playerId);
    }
}
