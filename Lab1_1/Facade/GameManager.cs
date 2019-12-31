using Lab1_1.AbstractFactory;
using Lab1_1.Interpreter;
using Lab1_1.Observer;
using Lab1_1.Streategy;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_1.Facade
{
    public class GameManager
    {

        private Lobby lobby;
        public AlgorithmFactory algorithmFactory;
        private ObstacleAbstractFactory mapFactory;
        private ObstacleAbstractFactory shopFactory;
        public Player player;
        public FactionFactory factory;

        public Algorithm standart;
        public Algorithm hopper;
        public Algorithm tower;
        public Algorithm teleport;

        int turnLimit = 4;

        public HttpClient client { get; set; }

        public GameManager() { 
        }
        public GameManager(HttpClient client)
        {
            this.client = client;
            lobby = new Lobby(client);
            algorithmFactory = new AlgorithmFactory();
            player = new Player();
            factory = new FactionFactory();
            standart = algorithmFactory.GetDefault("Standart");
            hopper = algorithmFactory.GetDefault("Hopper");
            tower = algorithmFactory.GetDefault("Tower");
            teleport = algorithmFactory.GetDefault("Teleport");
        }
        public Unit[][] generateGrid(int xSize, int ySize)
        {
            return Map.GetInstance.GenerateGrid(xSize, ySize);
        }
        public void makeMoves(List<Unit> serverMap, Player p, Uri url)
        {
            string command = string.Empty;
            int n = 0;

            while (turnLimit > 0)
            {
                ((Teleport)teleport).SetStartingPosition(player.currentX, player.currentY);
                bool finishedIteration = false;
                while (!finishedIteration)
                {
                    while (n < player.NumberOfActions)
                    {

                        n++;

                        Console.WriteLine("Map looks like:");
                        Console.WriteLine("___________");

                        for (int y = 0; y < GetYSize(); y++)
                        {
                            for (int x = 0; x < GetXSize(); x++)
                            {
                                Console.ForegroundColor = GetColor(x, y);
                                Console.Write(GetSymbol(x, y));
                            }
                            Console.WriteLine();
                        }
                        Console.ResetColor();

                        Console.WriteLine("___________");

                        bool succesfulMove = true;
                        if (n != player.NumberOfActions)
                        {
                            while (succesfulMove)
                            {

                                if (!(player.getAlgorithm() is Teleport))
                                {
                                    
                                    Console.WriteLine("Choose where to go next R,L,U,D?");
                                    command = Console.ReadLine();

                                    MovePlayer(command, ref succesfulMove);

                                }
                                else
                                {
                                    Console.WriteLine("Choose where to go next, type in two numbers with a space between them");
                                    command = Console.ReadLine();

                                    MovePlayerNext(command, ref succesfulMove);

                                }
                            }
                            succesfulMove = true;
                        }

                    }
                    Console.WriteLine("\n\n");
                    Console.WriteLine("Would you like to undo your moves? (Yes/No)");
                    command = Console.ReadLine();

                    Undo(command, ref finishedIteration);
                    n = 0;
                }

                Console.WriteLine("You have " + player.Money + " Money");
                Console.WriteLine("Do you want to move like:");
                Console.WriteLine("Tower - go till the end of the line");
                Console.WriteLine("Hopper - jump over a space");
                Console.WriteLine("Teleport - write two digits and teleport on those coordinates");
                Console.WriteLine("Standart - go one space in what direction you want");
                command = Console.ReadLine();
                if (command.Equals("Tower"))
                    player.setAlgorithm(tower);
                else if (command.Equals("Hopper"))
                    player.setAlgorithm(hopper);
                else if (command.Equals("Teleport"))
                    player.setAlgorithm(teleport);
                else
                    player.setAlgorithm(standart);


                if (player is HardWorker)
                {
                    Console.WriteLine("As a Hard worker you can work harder and get more money per action( double), but have less actions per round( 6 actions)");
                    if (player.MoneyMultiplier == 1)
                        Console.WriteLine("Type 'Work' to do it!");
                    else
                        Console.WriteLine("Type 'Stop' to go back to normal mode");
                    command = Console.ReadLine();

                    if (command.Equals("Work"))
                        ((HardWorker)player).WorkHarder();
                    else if (command.Equals("Stop"))
                        ((HardWorker)player).GetBackToNormal();
                }
                else if (player is Wolf)
                {
                    if (((Wolf)player).GetAttackLimit() != 0)
                    {
                        Console.WriteLine("As a Wolf you can attack an area and capture it");
                        Console.WriteLine("If you want to attack, type a direction: R,L,U,D");
                        command = Console.ReadLine();
                        ((Wolf)player).AttackASpecificArea(player, command, Map.GetInstance);

                    }
                }
                else
                {

                }

                if (player.getAlgorithm() is Tower)
                    ((Tower)player.getAlgorithm()).ResetStartingList();

                //For multi
                GameState gs = GetGameState();
                gs = UpdateMap(player.id, Map.GetInstance.ConvertArrayToList()).Result;
                while (gs.StateGame == "Updating")
                {
                    gs = UpdateMap(player.id, Map.GetInstance.ConvertArrayToList()).Result;
                }
                //Console.WriteLine(JsonConvert.SerializeObject(player, Formatting.Indented));
                serverMap = GetPlayerMap(player.id).Result;
                Map.GetInstance.ConvertListToArray(serverMap);
                p = GetPlayer(url.PathAndQuery).Result;
                player.MoneyMultiplier = p.MoneyMultiplier;
                player.NumberOfActions = p.NumberOfActions;

                //Console.WriteLine(JsonConvert.SerializeObject(player, Formatting.Indented));

                turnLimit--;
            }
        }
        public async Task<GameState> UpdateMap(long id, List<Unit> map)
        {
            return await lobby.UpdateMap(id, map);
        }
        public Map GetMap()
        {
            return Map.GetInstance;
        }
        public GameState GetGameState()
        {
            return new GameState();
        }
        public int GetYSize()
        {
            return Map.GetInstance.GetYSize();
        }
        public int GetXSize()
        {
            return Map.GetInstance.GetXSize();
        }
        public ConsoleColor GetColor(int x, int y)
        {
            return Map.GetInstance.GetUnit(x, y).GetColor();
        }
        public char GetSymbol(int x, int y)
        {
            return Map.GetInstance.GetUnit(x, y).GetSymbol();
        }
        public void TakeUnit()
        {
            Map.GetInstance.GetUnit(0, 0).TakeUnit(player);
        }
        public Player GetPlayerInst()
        {
            return player;
        }
        public void Undo(string command, ref bool finishedIteration)
        {
            if (command.Equals("Yes"))
            {
                player.Undo();
                finishedIteration = false;
            }
            else
            {
                player.ResetCommands();
                finishedIteration = true;
            }
        }
        public void TeleportPlayer(string text)
        {
            if (text.Length != 0)
            {
                bool succesfulMove = true;
                Context _context = new Context(player.currentX, player.currentY, GetXSize());
                _context.Parse(text);
                MovePlayerNext(_context.GetCordinates(), ref succesfulMove);
            }
        }
        public string GetWinner(ConsoleColor playerColor)
        {
            int playerYellow = 0;
            int playerBlue = 0;

            List<Unit> gameMap = Map.GetInstance.ConvertArrayToList();

            foreach(Unit u in gameMap)
            {
                switch (u.color)
                {
                    case ConsoleColor.Yellow:
                        playerYellow += 1;
                        break;
                    case ConsoleColor.Blue:
                        playerBlue += 1;
                        break;
                    default:
                        break;
                }
            }

            if(playerYellow == playerBlue)
            {
                return "Draw";
            }
            else if(playerYellow > playerBlue && ConsoleColor.Yellow == playerColor ||
                playerBlue > playerYellow && ConsoleColor.Blue == playerColor ) {
                return "You won";
            }else
            {
                return "You lost";
            }

            
        }
        public void MovePlayerNext(string command, ref bool succesfulMove)
        {
            string[] numbers = command.Split(' ');
            int XPosition = 0;
            int YPosition = 0;

            bool number1Success = Int32.TryParse(numbers[0], out XPosition);
            bool number2Success = Int32.TryParse(numbers[1], out YPosition);

            if (number1Success && number1Success && XPosition < 20 && YPosition < 20 && XPosition >= 0 && YPosition >= 0 && (Map.GetInstance.GetUnit(XPosition, YPosition).symbol.Equals('0') || Map.GetInstance.GetUnit(XPosition, YPosition).symbol.Equals('*')))
            {
                player.ExecuteMove(command, player);
                succesfulMove = false;
            }
            else
            {
                Console.WriteLine("Your number where wrong or you are teleporting on an obstacle");
            }
        }
        public void MovePlayer(string command, ref bool succesfulMove)
        {
            switch (command)
            {
                case ("D"):
                    if (player.currentY + player.Power < Map.GetInstance.GetYSize() && ( Map.GetInstance.GetUnit(player.currentX, player.currentY + player.Power).symbol.Equals('0') || Map.GetInstance.GetUnit(player.currentX, player.currentY + player.Power).symbol.Equals('*')))
                    {
                        player.ExecuteMove(command, player);
                        succesfulMove = false;
                    }
                    else
                        Console.WriteLine("You are at the edge of the map OR going into an obstacle");
                    break;
                case ("U"):
                    if (player.currentY - player.Power >= 0 && (Map.GetInstance.GetUnit(player.currentX, player.currentY - player.Power).symbol.Equals('0') || Map.GetInstance.GetUnit(player.currentX, player.currentY - player.Power).symbol.Equals('*')))
                    {
                        player.ExecuteMove(command, player);
                        succesfulMove = false;
                    }
                    else
                        Console.WriteLine("You are at the edge of the map OR going into an obstacle");
                    break;
                case ("R"):
                    if (player.currentX + player.Power < Map.GetInstance.GetXSize() && (Map.GetInstance.GetUnit(player.currentX + player.Power, player.currentY ).symbol.Equals('0') || Map.GetInstance.GetUnit(player.currentX + player.Power, player.currentY ).symbol.Equals('*')))
                    {
                        player.ExecuteMove(command, player);
                        succesfulMove = false;
                    }
                    else
                        Console.WriteLine("You are at the edge of the map OR going into an obstacle");
                    break;
                case ("L"):
                    if (player.currentX - player.Power >= 0 && (Map.GetInstance.GetUnit(player.currentX - player.Power, player.currentY ).symbol.Equals('0') || Map.GetInstance.GetUnit(player.currentX - player.Power, player.currentY ).symbol.Equals('*')))
                    {
                        player.ExecuteMove(command, player);
                        succesfulMove = false;
                    }
                    else
                        Console.WriteLine("You are at the edge of the map OR going into an obstacle");
                    break;
            }
        }
        public void CreatePlayerWithFaction(string command, Algorithm standart)
        {
            player = factory.CreatePlayerWithFaction(command);
            player.Creation();
            player.setAlgorithm(standart);
        }
        public void CreateAnObstacle(string command)
        {
            switch (command)
            {
                case "Stone":
                    mapFactory.CreateObstacle(command);
                    break;
                case "Tree":
                    shopFactory.CreateObstacle(command);
                    break;
                case "Gold Mine":
                    mapFactory.CreateSuperObstacle(command);
                    break;
                case "Action Tower":
                    shopFactory.CreateSuperObstacle(command);
                    break;
                case "Wonder":
                    mapFactory.CreateSuperObstacle(command);
                    break;
                default:
                    break;
            }
        }
        public async Task<bool> LobbyIsFull()
        {
            return await lobby.LobbyIsFullAsync();
        }
        public async Task<Uri> CreatePlayerAsync(Player player)
        {
            return await lobby.CreatePlayerAsync(player);
        }
        public async Task<Player> GetPlayer(string path)
        {
            return await lobby.GetPlayerAsync(path);
        }
        public async Task<List<Unit>> GetPlayerMap(long id)
        {
            return await lobby.GetMap(id);
        }
        public async Task<HttpStatusCode> UpdatePlayerAsync(Player player)
        {
            return await lobby.UpdatePlayerAsync(player);
        }
        public Lobby GetLobby()
        {
            return lobby;
        }
    }
}
