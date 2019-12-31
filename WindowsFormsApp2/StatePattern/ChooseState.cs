using Lab1_1.Facade;
using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApp2.StatePattern
{
    class ChooseState : State
    {
        private GameManager gameManager;
        public ChooseState(GameManager gm):base()
        {
            gameManager = gm;

            tb1.Visible = true;
            l1.Visible = true;
            bt1.Visible = true;

            tb1.Enabled = true;
            bt1.Enabled = true;

            l1.Text = "Choose your algorithm";
            tb1.Text = "";
        }

        public override void RenderElements()
        {
            tb1.Enabled = false;
            bt1.Enabled = false;

            string command = tb1.Text;
            if (command.Equals("Tower"))
                gameManager.player.setAlgorithm(gameManager.tower);
            else if (command.Equals("Hopper"))
                gameManager.player.setAlgorithm(gameManager.hopper);
            else if (command.Equals("Teleport"))
                gameManager.player.setAlgorithm(gameManager.teleport);
            else
                gameManager.player.setAlgorithm(gameManager.standart);

            tb1.Text = "";
        }
    }
}
