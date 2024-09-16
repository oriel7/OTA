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
    }
}
