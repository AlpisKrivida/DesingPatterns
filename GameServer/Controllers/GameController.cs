using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab1_1.Observer;
using Lab1_1.AbstractFactory;
using GameServer.Models;
using Lab1_1;
using Newtonsoft.Json;

namespace GameServer.Controllers
{
    [Route("api/gamecontroller")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly PlayerContext _context;
        private readonly GameState gs;

        public GameController(PlayerContext context)
        {
            _context = context;
            gs = new GameState();

            gs.Attach(new Tree(0, 0));
            gs.Attach(new GoldMine(0, 0));
            gs.Attach(new ActionTower(0, 0));
            gs.Attach(new Wonder(0, 0));

            //Generate map
            if (_context.Map.ToList().Count < Constants.mapLenghtX * Constants.mapLenghtY)
            {
                Map.GetInstance.GenerateGrid(Constants.mapLenghtX, Constants.mapLenghtY);
                _context.State.Add(new State { StateGame = "Updating" });
                for (int y = 0; y < Map.GetInstance.GetYSize(); y++)
                {
                    for (int x = 0; x < Map.GetInstance.GetXSize(); x++)
                    {
                        Unit u = Map.GetInstance.GetUnit(x, y);
                        _context.Map.Add(JsonConvert.DeserializeObject<MapUnit>(JsonConvert.SerializeObject(u)));
                    }
                }
            }
            _context.SaveChanges();
        }

        [HttpGet("{id}", Name = "GetMap")]
        public ActionResult<List<MapUnit>> GetMap(long id)
        {
            var val = _context.PG_ID.ToList().Where(x => x.Id == id).FirstOrDefault();

            if (val == null)
            {
                PlayerGet t = new PlayerGet { Id = id };
                _context.PG_ID.Add(t);
                _context.SaveChanges();
            }

            List<PlayerGet> tl = _context.PG_ID.ToList();

            if (tl.Count() == Constants.playerCount)
            {
                _context.PP_ID.RemoveRange(_context.PP_ID);
                State s = _context.State.First();
                s.StateGame = "Updating";
                _context.SaveChanges();
            }

            return _context.Map.ToList();
        }


        [HttpDelete]
        public IActionResult ResetGame()
        {
            _context.Map.RemoveRange(_context.Map);
            _context.Players.RemoveRange(_context.Players);
            _context.PP_ID.RemoveRange(_context.PP_ID);
            _context.PG_ID.RemoveRange(_context.PG_ID);
            _context.State.RemoveRange(_context.State);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("/allg", Name = "GetAllGet")]
        public ActionResult<IEnumerable<PlayerGet>> GetAllg()
        {
            return _context.PG_ID.ToList();
        }

        [HttpPost("{id}", Name = "UpdateMap")]
        public ActionResult<State> UpdatedMap(long id, [FromBody] List<Unit> map)
        {
            Unit playerMapUnit;
            List<MapUnit> serverMap;

            var val = _context.PP_ID.ToList().Where(x => x.Id == id).FirstOrDefault();
            var playerc = _context.Players.Find(id);

            if (val == null)
            {
                serverMap = _context.Map.ToList();
                PlayerPost t = new PlayerPost { Id = id };
                _context.PP_ID.Add(t);

                //Merge maps
                Map.GetInstance.ConvertListToArray(map);
                for (int y = 0; y < Map.GetInstance.GetYSize(); y++)
                {
                    for (int x = 0; x < Map.GetInstance.GetXSize(); x++)
                    {
                        playerMapUnit = Map.GetInstance.GetUnit(x, y);
                        MapUnit serverMapUnit = serverMap.ElementAt(y * Map.GetInstance.GetXSize() + x);

                        if (serverMapUnit.symbol == '0' && playerMapUnit.GetSymbol() == '0' && serverMapUnit.color == (ConsoleColor)15 && playerMapUnit.color != (ConsoleColor)15)
                        {
                            //serverMapUnit.isNew = false;
                            serverMapUnit.isActive = playerMapUnit.isActive;
                            serverMapUnit.color = playerMapUnit.GetColor();
                            serverMapUnit.symbol = playerMapUnit.symbol;
                            _context.Map.Update(serverMapUnit);
                        }
                        else if (playerMapUnit.GetSymbol() == '*')
                        {
                            if (playerMapUnit.GetColor() != playerc.color)
                            {
                                //serverMapUnit.isNew = false;
                                serverMapUnit.color = playerMapUnit.GetColor();
                                serverMapUnit.symbol = '0';
                            }
                            else
                            {
                                //serverMapUnit.isNew = false;
                                serverMapUnit.color = playerMapUnit.GetColor();
                                serverMapUnit.symbol = playerMapUnit.symbol;
                            }
                            _context.Map.Update(serverMapUnit);
                        }
                        else if (serverMapUnit.symbol == '0' && playerMapUnit.GetSymbol() == '0' && serverMapUnit.color != (ConsoleColor)15 && playerMapUnit.color != (ConsoleColor)15 && serverMapUnit.color != playerMapUnit.color)
                        {
                            if (playerMapUnit.isActive && serverMapUnit.isActive == false)
                            {
                                serverMapUnit.isActive = playerMapUnit.isActive;
                                serverMapUnit.color = playerMapUnit.color;
                                _context.Map.Update(serverMapUnit);
                            }else if (playerMapUnit.isActive == false && serverMapUnit.isActive == true)
                            {
                            }
                            else
                            {
                                serverMapUnit.isActive = false;
                                serverMapUnit.color = (ConsoleColor)15;
                                _context.Map.Update(serverMapUnit);
                            }
                        }
                    }
                }
                _context.SaveChanges();
            }

            else if (_context.State.First().StateGame == "Updated" && _context.PP_ID.ToList().Count() == Constants.playerCount)
            {
                return _context.State.First();
            }
            else
            {
                return _context.State.First();
            }

            if (_context.PP_ID.ToList().Count() == Constants.playerCount)
            {
                _context.PG_ID.RemoveRange(_context.PG_ID);
                List<Unit> area = new List<Unit>();
                List<Unit> unitmap = new List<Unit>();

                foreach (MapUnit mu in _context.Map.ToList())
                {
                    unitmap.Add(JsonConvert.DeserializeObject<Unit>(JsonConvert.SerializeObject(mu)));
                }
                Map.GetInstance.ConvertListToArray(unitmap);

                //Update map
                for (int y = 0; y < Map.GetInstance.GetYSize(); y++)
                {
                    for (int x = 0; x < Map.GetInstance.GetXSize(); x++)
                    {
                        if (Map.GetInstance.GetUnit(x, y).GetSymbol() != '0' && Map.GetInstance.GetUnit(x, y).GetSymbol() != '*')
                        {
                            if(x - 1 < 0 && y - 1 < 0 ||
                                x + 1 >= Map.GetInstance.GetXSize() && y - 1 < 0 ||
                                x - 1 < 0 && y + 1 >= Map.GetInstance.GetYSize() ||
                                x + 1 >= Map.GetInstance.GetXSize() && y + 1 >= Map.GetInstance.GetYSize())
                            {

                            }
                            else if (x - 1 < 0)
                            {
                                foreach ((int, int) t in Constants.cordsMinusX)
                                {
                                    playerMapUnit = Map.GetInstance.GetUnit(x + t.Item1, y + t.Item2);
                                    if (playerMapUnit.symbol == '0' || playerMapUnit.symbol == '*')
                                    {
                                        area.Add(playerMapUnit);
                                    }
                                }
                            }
                            else if (x + 1 >= Map.GetInstance.GetXSize())
                            {
                                foreach ((int, int) t in Constants.cordsPlusX)
                                {
                                    playerMapUnit = Map.GetInstance.GetUnit(x + t.Item1, y + t.Item2);
                                    if (playerMapUnit.symbol == '0' || playerMapUnit.symbol == '*')
                                    {
                                        area.Add(playerMapUnit);
                                    }
                                }
                            }
                            else if (y - 1 < 0)
                            {
                                foreach ((int, int) t in Constants.cordsMinusY)
                                {
                                    playerMapUnit = Map.GetInstance.GetUnit(x + t.Item1, y + t.Item2);
                                    if (playerMapUnit.symbol == '0' || playerMapUnit.symbol == '*')
                                    {
                                        area.Add(playerMapUnit);
                                    }
                                }
                            }
                            else if (y + 1 >= Map.GetInstance.GetYSize())
                            {
                                foreach ((int, int) t in Constants.cordsPlusY)
                                {
                                    playerMapUnit = Map.GetInstance.GetUnit(x + t.Item1, y + t.Item2);
                                    if (playerMapUnit.symbol == '0' || playerMapUnit.symbol == '*')
                                    {
                                        area.Add(playerMapUnit);
                                    }
                                }
                            }
                            else
                            {
                                foreach ((int, int) t in Constants.cords)
                                {
                                    playerMapUnit = Map.GetInstance.GetUnit(x + t.Item1, y + t.Item2);
                                    if (playerMapUnit.symbol == '0' || playerMapUnit.symbol == '*')
                                    {
                                        area.Add(playerMapUnit);
                                    }
                                }
                            }

                            if (area.FirstOrDefault() != null)
                            {
                                gs.Notify(Map.GetInstance, (x, y), area);
                                playerMapUnit = Map.GetInstance.GetUnit(x, y);
                                if (playerMapUnit != null && playerMapUnit.color != (ConsoleColor)15)
                                {
                                    //Update player
                                    Models.Player p = _context.Players.Where(player => player.color == playerMapUnit.color).FirstOrDefault();
                                    if (p != null && playerMapUnit.owner != null)
                                    {
                                        if (playerMapUnit.owner.NumberOfActions != 0)
                                            p.NumberOfActions += playerMapUnit.owner.NumberOfActions;
                                        if (playerMapUnit.owner.MoneyMultiplier != 0)
                                            p.MoneyMultiplier += playerMapUnit.owner.MoneyMultiplier;
                                        _context.Players.Update(p);
                                        _context.SaveChanges();
                                    }
                                    if (playerMapUnit.GetSymbol() == 'L')
                                    {
                                        _context.State.First().Winner = playerMapUnit.color;
                                        _context.SaveChanges();
                                    }
                                }
                            }
                            area.Clear();
                        }
                    }
                }

                serverMap = _context.Map.ToList();
                List<Unit> ul = Map.GetInstance.ConvertArrayToList();
                for (int x = 0; x < serverMap.Count; x++)
                {
                    serverMap[x].color = ul[x].color;
                    serverMap[x].symbol = ul[x].symbol;
                    serverMap[x].isActive = false;
                    _context.Map.Update(serverMap[x]);
                }

                State s = _context.State.First();
                s.StateGame = "Updated";
                _context.SaveChanges();

                return _context.State.First();
            }
            else
            {
                return _context.State.First();
            }
        }
    }
}
