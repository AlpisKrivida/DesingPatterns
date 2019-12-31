using Lab1_1;
using Lab1_1.Facade;
using Lab1_1.Observer;
using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApp2.StatePattern
{
    class StopState : State
    {
        private GameManager gameManager;
        public StopState(GameManager gm)
        {
            gameManager = gm;
            Constants.IsButtonActive = false;
        }

        public override async void RenderElements()
        {
            bt1.Visible = false;
            tb1.Visible = false;
            l1.Visible = true;

            if (Constants.online)
            {
                List<Unit> serverMap;
                l1.Text = "Waiting for other player...";
                GameState gs = await gameManager.UpdateMap(gameManager.player.id, Map.GetInstance.ConvertArrayToList());
                while (gs.StateGame == "Updating")
                {
                    gs = await gameManager.UpdateMap(gameManager.player.id, Map.GetInstance.ConvertArrayToList());
                }
                serverMap = await gameManager.GetPlayerMap(gameManager.player.id);
                Map.GetInstance.ConvertListToArray(serverMap);
                l1.Text = gameManager.GetWinner(gameManager.GetPlayerInst().color);
                Constants.form.RenderMap();
            }
            else
            {
                l1.Text = "Game ended";
            }
        }
    }
}
