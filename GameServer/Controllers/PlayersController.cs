using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly PlayerContext _context;

        public PlayersController(PlayerContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Player> GetPlayers()
        {
            return _context.Players;
        }

        // GET: api/Players/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetPlayer([FromRoute] long id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var player = await _context.Players.FindAsync(id);

        //    if (player == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(player);
        //}

        //GET api/player/5
        [HttpGet("{id}", Name = "GetPlayer")]
        public ActionResult<Player> GetById(long id)
        {
            Player p = _context.Players.Find(id);
            if (p == null)
            {
                return NotFound("player not found");
            }
            return p;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Player p)
        {
            var pp = _context.Players.Find(id);
            if (pp == null)
            {
                return NotFound();
            }

            pp.NumberOfActions = p.NumberOfActions;
            pp.MoneyMultiplier = p.MoneyMultiplier;

            _context.Players.Update(pp);
            _context.SaveChanges();

            return Ok();
        }

        // POST api/player
        [HttpPost]
        //public string Create(Player player)
        public ActionResult<Player> Create([FromBody] Player player)
        {
            if (!PlayerWithNameExists(player.Name))
            {
                _context.Players.Add(player);
                _context.SaveChanges();
                PlayerInfo(_context.Players.Last());
                player.NumberOfActions = Constants.numberOfActions;
                player.Money = Constants.moneyMulti;
                _context.SaveChanges();

                return CreatedAtRoute("GetPlayer", new { id = player.Id }, player);
            }
            else
                return null;
        }
        private void PlayerInfo(Player player)
        {
            switch ((player.Id%Constants.playerCount))
            {
                case 1:
                    player.currentX = 0;
                    player.currentY = 0;
                    player.color = ConsoleColor.Yellow;
                    break;
                case 2:
                    player.currentX = Constants.mapLenghtX-1;
                    player.currentY = 0;
                    player.color = ConsoleColor.Green;
                    break;
                case 3:
                    player.currentX = 0;
                    player.currentY = Constants.mapLenghtY-1;
                    player.color = ConsoleColor.Red;
                    break;
                case 0:
                    player.currentX = Constants.mapLenghtX-1;
                    player.currentY = Constants.mapLenghtY-1;
                    player.color = ConsoleColor.Blue;
                    break;
                default:
                    break;
                    
            }
        }
        private bool PlayerWithNameExists(string name)
        {
            return _context.Players.Any(e => e.Name == name);
        }


        // DELETE api/values/5
        [HttpDelete]
        public IActionResult ResetPlayers()
        {
            //var todo = _context.Players.Find(id);
            //if (todo == null)
            //{
            //    return NotFound();
            //}

            _context.Players.RemoveRange(_context.Players);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
