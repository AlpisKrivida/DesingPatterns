using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public class Standart : Algorithm
    {
        public override void Action(Player player, string command, Map map, bool undo)
        {
            player.SetPreviousCoordinates(player.currentX, player.currentY);

            if (undo == true)
            {
                map.GetUnit(player.currentX, player.currentY).ResetSymbol();
                map.GetUnit(player.currentX, player.currentY).ResetOwner();
            }

            player.Power = 1;
            switch (command)
            {
                case "U":
                    if (player.currentY - player.Power > -1)
                        player.currentY -= player.Power;
                    
                    break;
                case "R":
                    if (player.currentX + player.Power < map.GetXSize())
                        player.currentX += player.Power;
                    
                    break;
                case "D":
                    if (player.currentY + player.Power < map.GetYSize())
                        player.currentY += player.Power;
                   
                    break;
                case "L":
                    if (player.currentX - player.Power > -1)
                        player.currentX -= player.Power;
                   
                    break;
                default:
                    map.GetUnit(player.currentX, player.currentY).ResetSymbol();
                    player.currentX = 0;
                    player.currentY = 0;
                    break;
            }

            if (undo == false)
            {
                player.Money = player.Money + player.MoneyMultiplier;
                map.GetUnit(player.GetPreviousX(), player.GetPreviousY()).ResetSymbol();
                map.GetUnit(player.currentX, player.currentY).TakeUnit(player);
            }
            else
            {
                player.Money = player.Money - player.MoneyMultiplier;
            }
            
            

        }
    }
}
