using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public class Teleport : Algorithm
    {
        private int startingXPosition = 0;
        private int startingYPosition = 0;
        public override void Action(Player player, string command, Map map, bool undo)
        {
            player.Power = 1;
            player.SetPreviousCoordinates(player.currentX, player.currentY);

            if (undo == true)
            {
                map.GetUnit(player.currentX, player.currentY).ResetSymbol();
                map.GetUnit(player.currentX, player.currentY).ResetOwner();
            }

            string[] numbers = command.Split(' ');
            int XPosition = 0;
            int YPosition = 0;

            bool number1Success = Int32.TryParse(numbers[0], out XPosition);
            bool number2Success = Int32.TryParse(numbers[1], out YPosition);

            player.currentX = XPosition;
            player.currentY = YPosition;

            if (player.currentX > -1 && player.currentX < map.GetXSize() && player.currentY > -1 && player.currentY < map.GetYSize())
            {

                if (undo == false)
                {
                    map.GetUnit(player.currentX, player.currentY).TakeUnit(player);
                    player.Money = player.Money + player.MoneyMultiplier;
                    map.GetUnit(player.GetPreviousX(), player.GetPreviousY()).ResetSymbol();
                }
                else
                {
                    map.GetUnit(player.currentX, player.currentY).ResetSymbol();
                    map.GetUnit(player.currentX, player.currentY).ResetOwner();
                    player.Money = player.Money - player.MoneyMultiplier;
                }


            }
            
        }
        public void SetStartingPosition(int x, int y)
        {
            startingXPosition = x;
            startingYPosition = y;
        }

        public int GetStartingX ()
        {
            return startingXPosition;
        }
        public int GetStartingY()
        {
            return startingYPosition;
        }
    }
}
