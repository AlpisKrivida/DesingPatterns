using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp2;
using Lab1_1.Facade;
using Lab1_1.Streategy;
using Lab1_1.Observer;
using Lab1_1;
using System.Diagnostics;

namespace WindowsFormsApp2.StatePattern
{
    class UndoState : State
    {
        private GameManager gameManager;
        public UndoState(GameManager gm):base()
        {
            gameManager = gm;
            Constants.IsButtonActive = false;

            tb1.Visible = true;
            l1.Visible = true;
            bt1.Visible = true;

            tb1.Enabled = true;
            bt1.Enabled = true;

            l1.Text = "Do you want to undo?";
            tb1.Text = "";
        }

        public override async void RenderElements()
        {
            tb1.Enabled = false;
            bt1.Enabled = false;

            gameManager.Undo(tb1.Text, ref finishedIteration);
            ((Teleport)gameManager.teleport).SetStartingPosition(gameManager.player.currentX, gameManager.player.currentY);

            if (Constants.online && finishedIteration)
            {
                List<Unit> serverMap;
                Debug.WriteLine(Map.GetInstance.GetUnit(0, 0).isActive);
                GameState gs = await gameManager.UpdateMap(gameManager.player.id, Map.GetInstance.ConvertArrayToList());
                while (gs.StateGame == "Updating")
                {
                    gs = await gameManager.UpdateMap(gameManager.player.id, Map.GetInstance.ConvertArrayToList());
                }
                Constants.IsButtonActive = true;
                serverMap = await gameManager.GetPlayerMap(gameManager.player.id);             
                //
                for (int x = 0; x < serverMap.Count; x++)
                {
                    serverMap[x].isActive = false;
                }
                //
                Map.GetInstance.ConvertListToArray(serverMap);
                Player p = await gameManager.GetLobby().GetPlayerAsync(gameManager.GetLobby().url.PathAndQuery);
                gameManager.player.MoneyMultiplier = p.MoneyMultiplier;
                gameManager.player.NumberOfActions = p.NumberOfActions;

                gameManager.GetMap().GetUnit(gameManager.player.currentX, gameManager.player.currentY).TakeUnit(gameManager.player);
                Constants.form.RenderMap();
            }
            else
            {
                Constants.IsButtonActive = true;
            }
        }
    }
}
