using Dapper;
using OT.Assessment.Core.Domain.DTO;
using OT.Assessment.Core.Domain.Models;
using OT.Assessment.Core.Repository.Repositories;
using OT.Assessment.Infrastructure.Persistence.Contexts;

namespace OT.Assessment.Infrastructure.Repository.Repositories
{
    public class PlayerCasinoWagerRepository : IPlayerCasinoWagerRepository
    {
        private readonly DapperContext _context;

        public PlayerCasinoWagerRepository(DapperContext context) => _context = context;

        public async Task CreateCasinoWagerAsync(CasinoWagerDTO casinoWager)
        {
            var insertQuery = "INSERT INTO PlayerCasinoWager (WagerId, Game, Provider, AccountId, Amount, CreatedDateTime) " +
                "VALUES (@WagerId, @Game, @Provider, @AccountId, @Amount, @CreatedDateTime)";

            using (var connection = _context.CreateConnection())
            {
                var wager = new PlayerCasinoWager()
                {
                    WagerId = casinoWager.WagerId,
                    Game = casinoWager.GameName,
                    Provider = casinoWager.Provider,
                    AccountId = casinoWager.AccountId,
                    Amount = casinoWager.Amount,
                    CreatedDateTime = casinoWager.CreatedDateTime
                };

                await connection.ExecuteAsync(insertQuery, wager);
            }       
        }

        public async Task<IEnumerable<CasinoWagerResponseDTO>> GetCasinoWagerAsync(Guid playerId)
        {
            var query = "SELECT WagerId, Game, Provider, Amount, CreatedDateTime FROM PlayerCasinoWager WHERE AccountId = @AccountId";

            using (var connection = _context.CreateConnection())
            {
                var wagers = await connection.QueryAsync<PlayerCasinoWager>(query, new { AccountId = playerId });

                return wagers.Select(wager => new CasinoWagerResponseDTO()
                {
                    WagerId = wager.WagerId,
                    GameName = wager.Game,
                    Provider = wager.Provider,
                    Amount = wager.Amount,
                    CreatedDateTime = wager.CreatedDateTime
                }); 
            }
        }
    }
}
