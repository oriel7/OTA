﻿using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Core.Domain.Constants;
using OT.Assessment.Core.Domain.Models;
using OT.Assessment.Core.Services.Services;
namespace OT.Assessment.App.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;
        private readonly ICasinoWagerService _casinoWagerService;
        private readonly IPlayerService _playerService;
        private readonly IBus _bus;


        public PlayerController(ICasinoWagerService casinoWagerService, IPlayerService playerService,
            IBus bus, ILogger<PlayerController> logger)
        {
            _casinoWagerService = casinoWagerService;
            _playerService = playerService; 
            _bus = bus; 
            _logger = logger;
        }

        //POST api/player/casinowager

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CasinoWager(CasinoWager casinoWager)
        {
            if (casinoWager == null)
            {
                return BadRequest("Casino wager data is missing");
            }

            _logger.LogInformation("Request received to create a wager");

            var endPoint = await _bus.GetSendEndpoint(new Uri(RabbitMqConstants.RabbitMqCreateCasinoWagerQueueUri));
            await endPoint.Send(casinoWager);

            return Ok("Casino wager created successfully");
        }

        //GET api/player/{playerId}/wagers
        [HttpGet]
        [Route("{playerId:int}/wagers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CasinoWager>>> Wagers(int playerId)
        {
            if (playerId < 1)
            {
                return BadRequest("Player Id must be greater than 0");
            }

            _logger.LogInformation(message: $"Request received to get wagers for player with Id: {playerId}");

            return Ok(await _casinoWagerService.GetCasinoWagersAsync(playerId));
        }

        //GET api/player/topSpenders?count=10        
        [HttpGet]
        [Route("topSpenders/{count:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Player>>> TopSpenders(int count = 10)
        {
            if (count < 1)
            {
                return BadRequest("Count must be greater than 0");
            }

            _logger.LogInformation(message: $"Request received to get top {count} highest spenders");

            return Ok(await _playerService.GetTopSpendersAsync(count));
        }
    }
}
