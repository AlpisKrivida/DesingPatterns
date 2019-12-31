using Lab1_1.Facade;
using Lab1_1.Streategy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2.StatePattern
{
    class PlayState : State
    {
        private GameManager gameManager;
        public PlayState(GameManager gm) : base()
        {
            gameManager = gm;
        }

        public override void RenderElements()
        {         
            l2.Visible = false;         
            tb2.Visible = false;

            if (gameManager.player.getAlgorithm() is Teleport)
            {
                Constants.IsButtonActive = false;
                l1.Enabled = true;
                tb1.Enabled = true;
                bt1.Enabled = true;            
                l1.Text = "Enter you next position";
                gameManager.TeleportPlayer(tb1.Text);
            }
            else
            {
                l1.Visible = false;
                tb1.Visible = false;
                bt1.Visible = false;
            }
        }
    }
}
