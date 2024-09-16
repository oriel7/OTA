using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Core.Domain.Constants;
using OT.Assessment.Core.Domain.DTO;
using OT.Assessment.Core.Services.Services;
namespace OT.Assessment.App.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;
        private readonly IBus _bus;
        private readonly IPlayerCasinoWagerService _playerCasinoWagerService;


        public PlayerController(IBus bus, IPlayerCasinoWagerService playerCasinoWagerService,
            ILogger<PlayerController> logger)
        {
            _bus = bus;
            _playerCasinoWagerService = playerCasinoWagerService;
            _logger = logger;
        }

        //POST api/player/casinowager

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CasinoWager(CasinoWagerDTO casinoWager)
        {
            if (casinoWager == null)
            {
                return BadRequest("Casino wager data is missing");
            }

            //if (string.IsNullOrEmpty(casinoWager.WagerId))
            //{
            //    return BadRequest("Casino wager Id is missing");
            //}

            if (string.IsNullOrEmpty(casinoWager.GameName))
            {
                return BadRequest("Casino wager Id is missing");
            }

            if (string.IsNullOrEmpty(casinoWager.Provider))
            {
                return BadRequest("Casino wager Id is missing");
            }

            //if (string.IsNullOrEmpty(casinoWager.AccountId))
            //{
            //    return BadRequest("Casino wager Id is missing");
            //}

            _logger.LogInformation("Request received to create a wager");

            var endPoint = await _bus.GetSendEndpoint(new Uri(RabbitMqConstants.CreateCasinoWagerQueueUri));
            await endPoint.Send(casinoWager);

            return Ok("Casino wager created successfully");
        }

        //GET api/player/{playerId}/wagers
        [HttpGet]
        [Route("{playerId:Guid}/wagers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CasinoWagerResponseDTO>>> Wagers(Guid playerId)
        {
            //if (string.IsNullOrEmpty(playerId))
            //{
            //    return BadRequest("Player Id is missing");
            //}

            _logger.LogInformation(message: $"Request received to get wagers for player with Id: {playerId}");

            var response = await _playerCasinoWagerService.GetCasinoWagersAsync(playerId);

            return Ok(response);
        }

        //GET api/player/topSpenders?count=10        
        [HttpGet]
        [Route("topSpenders/{count:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> TopSpenders(int count = 10)
        {
            if (count < 1)
            {
                return BadRequest("Count must be greater than 0");
            }

            _logger.LogInformation(message: $"Request received to get top {count} highest spenders");

            var response = await _playerCasinoWagerService.GetTopSpendersAsync(count);

            return Ok(response);
        }
    }
}
