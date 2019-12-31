using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public class Tower : Algorithm
    {
        private List<int> StartingPositionsX = new List<int>();
        private List<int> StartingPositionsY = new List<int>();

        public override void Action(Player player, string command, Map map, bool undo)
        {
            if(undo == false)
                AddStartingPosition(player.currentX, player.currentY);

            player.SetPreviousCoordinates(player.currentX, player.currentY);
            int n = 0;
            
            switch (command)
            {
                case "U":
                    if (undo == false)
                    {
                        while (player.currentY >= 0 )
                        {

                            player.Money = player.Money + player.MoneyMultiplier;

                            map.GetUnit(player.currentX, player.currentY).TakeUnit(player);

                            if (n != 0)
                                map.GetUnit(player.currentX, player.currentY + 1).ResetSymbol();

                            if (undo == true)
                            {
                                map.GetUnit(player.currentX, player.currentY).ResetSymbol();
                                map.GetUnit(player.currentX, player.currentY).ResetOwner();
                                player.Money = player.Money - player.MoneyMultiplier;
                            }

                            if (player.currentY >= 0 && player.currentY - 1 >= 0 && Map.GetInstance.GetUnit(player.currentX, player.currentY - 1).symbol.Equals('0'))
                                player.currentY--;
                            else
                                break;

                            n++;
                        }
                    }
                    else
                    {
                        while (player.currentY >= StartingPositionsY[StartingPositionsY.Count - 1])
                        {

                            map.GetUnit(player.currentX, player.currentY).ResetSymbol();
                            map.GetUnit(player.currentX, player.currentY).ResetOwner();
                            player.Money = player.Money - player.MoneyMultiplier;
                           
                            if (player.currentY >= StartingPositionsY[StartingPositionsY.Count - 1])
                                player.currentY--;
                      
                        }
                        player.currentY++;
                        StartingPositionsY.RemoveAt(StartingPositionsY.Count - 1);
                        StartingPositionsX.RemoveAt(StartingPositionsX.Count - 1);
                    }
                    break;
                case "R":
                    if (undo == false)
                    {
                        while (player.currentX < map.GetXSize())
                        {
                            player.Money = player.Money + player.MoneyMultiplier;

                            map.GetUnit(player.currentX, player.currentY).TakeUnit(player);

                            if (n != 0)
                                map.GetUnit(player.currentX - 1, player.currentY).ResetSymbol();


                            if (undo == true)
                            {
                                map.GetUnit(player.currentX, player.currentY).ResetSymbol();
                                map.GetUnit(player.currentX, player.currentY).ResetOwner();
                                player.Money = player.Money - player.MoneyMultiplier;
                            }

                            if (player.currentX <= map.GetXSize() && player.currentX + 1 < map.GetXSize() && Map.GetInstance.GetUnit(player.currentX + 1, player.currentY).symbol.Equals('0'))
                                player.currentX++;
                            else
                                break;

                            n++;
                        }
                    }
                    else
                    {
                        while (player.currentX <= StartingPositionsX[StartingPositionsX.Count - 1])
                        {

                            map.GetUnit(player.currentX, player.currentY).ResetSymbol();
                            map.GetUnit(player.currentX, player.currentY).ResetOwner();
                            player.Money = player.Money - player.MoneyMultiplier;

                            if (player.currentX <= StartingPositionsX[StartingPositionsX.Count - 1])
                                player.currentX++;

                        }
                        player.currentX--;
                        StartingPositionsX.RemoveAt(StartingPositionsX.Count - 1);
                        StartingPositionsY.RemoveAt(StartingPositionsY.Count - 1);
                    }
                    break;
                case "D":
                    if (undo == false)
                    {
                        while (player.currentY < map.GetYSize() )
                        {
                            player.Money = player.Money + player.MoneyMultiplier;

                            map.GetUnit(player.currentX, player.currentY).TakeUnit(player);

                            if (n != 0)
                                map.GetUnit(player.currentX, player.currentY - 1).ResetSymbol();

                            if (undo == true)
                            {
                                map.GetUnit(player.currentX, player.currentY).ResetSymbol();
                                map.GetUnit(player.currentX, player.currentY).ResetOwner();
                                player.Money = player.Money - player.MoneyMultiplier;
                            }


                            if (player.currentY <= map.GetYSize() && player.currentY + 1 < map.GetYSize() && Map.GetInstance.GetUnit(player.currentX, player.currentY + 1).symbol.Equals('0'))
                                player.currentY++;
                            else
                                break;

                            n++;
                        }
                    }
                    else
                    {
                        while (player.currentY <= StartingPositionsY[StartingPositionsY.Count - 1])
                        {

                            map.GetUnit(player.currentX, player.currentY).ResetSymbol();
                            map.GetUnit(player.currentX, player.currentY).ResetOwner();
                            player.Money = player.Money - player.MoneyMultiplier;

                            if (player.currentY <= StartingPositionsY[StartingPositionsY.Count - 1])
                                player.currentY++;

                        }
                        player.currentY--;
                        StartingPositionsY.RemoveAt(StartingPositionsY.Count - 1);
                        StartingPositionsX.RemoveAt(StartingPositionsX.Count - 1);
                    }
                    break;
                case "L":
                    if (undo == false)
                    {
                        while (player.currentX >= 0)
                        {
                            player.Money = player.Money + player.MoneyMultiplier;

                            map.GetUnit(player.currentX, player.currentY).TakeUnit(player);

                            if (n != 0)
                                map.GetUnit(player.currentX + 1, player.currentY).ResetSymbol();

                            if (undo == true)
                            {
                                map.GetUnit(player.currentX, player.currentY).ResetSymbol();
                                map.GetUnit(player.currentX, player.currentY).ResetOwner();
                                player.Money = player.Money - player.MoneyMultiplier;
                            }

                            if (player.currentX >= 0 && player.currentX - 1 >= 0 &&  Map.GetInstance.GetUnit(player.currentX - 1, player.currentY).symbol.Equals('0'))
                                player.currentX--;
                            else
                                break;

                            n++;
                        }
                    }
                    else
                    {
                        while (player.currentX >= StartingPositionsX[StartingPositionsX.Count - 1])
                        {

                            map.GetUnit(player.currentX, player.currentY).ResetSymbol();
                            map.GetUnit(player.currentX, player.currentY).ResetOwner();
                            player.Money = player.Money - player.MoneyMultiplier;

                            if (player.currentX >= StartingPositionsX[StartingPositionsX.Count - 1])
                                player.currentX--;

                        }
                        player.currentX++;
                        StartingPositionsX.RemoveAt(StartingPositionsX.Count - 1);
                        StartingPositionsY.RemoveAt(StartingPositionsY.Count - 1);
                    }
                    break;
                default:
                    map.GetUnit(player.GetPreviousX(), player.GetPreviousY()).ResetSymbol();
                    player.currentX = 0;
                    player.currentY = 0;
                    break;
            }
        }
        public void AddStartingPosition(int x, int y)
        {
            StartingPositionsX.Add(x);
            StartingPositionsY.Add(y);
        }

        public void ResetStartingList()
        {
            StartingPositionsX.Clear();
            StartingPositionsY.Clear();
        }
    }
}
