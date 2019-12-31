using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class Wolf : Player
    {
        private int AttackLimit { get; set; }
        public Wolf(string faction) : base(faction)
        {
            NumberOfActions = 9;
            AttackLimit = 1;
        }
        
        public int GetAttackLimit()
        {
            return AttackLimit;
        }
        public void AttackASpecificArea(Player player, string command, Map map)
        {
            int n = 0;
            player.SetPreviousCoordinates(player.currentX, player.currentY);

            switch (command)
            {
                case "D":
                    if (player.currentY + (player.Power * 2) < map.GetYSize())
                    {
                        player.currentY += 2;
                    }
                    TakeSquire(player, map);

                    break;
                case "R":
                    if (player.currentX + (player.Power * 2) < map.GetXSize())
                    {
                        player.currentX += 2;
                    }
                    TakeSquire(player, map);
                    break;
                case "U":
                    if (player.currentY - (player.Power * 2) < map.GetXSize())
                    {
                        player.currentY -= 2;
                    }
                    TakeSquire(player, map);
                    break;
                case "L":
                    if (player.currentX - (player.Power * 2) < map.GetYSize())
                    {
                        player.currentX -= 2;
                    }
                    TakeSquire(player, map);
                    break;
                default:
                    Console.WriteLine("wrong command");
                    n++;
                    break;
            }
            if (n == 0)
            {
                map.GetUnit(player.GetPreviousX(), player.GetPreviousY()).ResetSymbol();
                map.GetUnit(player.currentX, player.currentY).TakeUnit(player);
                AttackLimit = 0;
            }

        }

        public void TakeSquire(Player player, Map map)
        {
            int startX = 0;
            int endX = 0;
            int startY = 0;
            int endY = 0;

            if (player.currentY - 1 == -1)
            {
                startY = player.currentY;
                endY = player.currentY + 1;
            }
            else if (player.currentY + 1 == map.GetYSize())
            {
                startY = player.currentY - 1;
                endY = player.currentY;
            }
            else
            {
                startY = player.currentY - 1;
                endY = player.currentY + 1;
            }

            if (player.currentX - 1 == -1)
            {
                startX = player.currentX;
                endX = player.currentX + 1;
            }
            else if (player.currentX + 1 == map.GetXSize())
            {
                startX = player.currentX - 1;
                endX = player.currentX;
            }
            else
            {
                startX = player.currentX - 1;
                endX = player.currentX + 1;
            }

            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    if (map.GetUnit(i, j).symbol.Equals('0') || map.GetUnit(i, j).symbol.Equals('*'))
                    {
                        map.GetUnit(i, j).TakeUnit(player);
                        map.GetUnit(i, j).ResetSymbol();
                    }
                }
            }
        }
    }
}
