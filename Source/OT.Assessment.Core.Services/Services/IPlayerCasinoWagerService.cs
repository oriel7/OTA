using OT.Assessment.Core.Domain.DTO;

namespace OT.Assessment.Core.Services.Services
{
    public interface IPlayerCasinoWagerService
    {
        Task CreateCasinoWagerAsync(CasinoWagerDTO casinoWager);
         Task<IEnumerable<CasinoWagerResponseDTO>> GetCasinoWagersAsync(Guid playerId);
    }
}
