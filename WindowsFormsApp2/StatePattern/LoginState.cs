using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp2;
using Lab1_1.Facade;
using Lab1_1;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WindowsFormsApp2.StatePattern
{
    class LoginState : State
    {
        private GameManager gameManager;
        private Uri url;

        public LoginState(GameManager gm): base()
        {
            gameManager = gm;
        }

        public override async void RenderElements()
        {   
            string playerName = tb1.Text;
            string faction = tb2.Text;
            tb1.Enabled = false;
            tb2.Enabled = false;
            bt1.Enabled = false;
            tb1.Text = "";

            gameManager.player.SetName(playerName);
            if (Constants.online)
            {
                url = await gameManager.CreatePlayerAsync(gameManager.player);
            }
            gameManager.CreatePlayerWithFaction(faction, gameManager.standart);

            if (Constants.online)
            {
                Player p = await gameManager.GetPlayer(url.PathAndQuery);
                gameManager.player.id = p.id;
                gameManager.player.currentX = p.currentX;
                gameManager.player.currentY = p.currentY;
                gameManager.player.color = p.color;
                await gameManager.UpdatePlayerAsync(gameManager.player);
                gameManager.GetMap().GetUnit(gameManager.player.currentX, gameManager.player.currentY).TakeUnit(gameManager.player);
                while (!await gameManager.LobbyIsFull())
                {
                    Debug.WriteLine(gameManager.LobbyIsFull());
                    if (await gameManager.LobbyIsFull())
                        Debug.Write("\r{0}", "lobby is full");
                }
                Constants.form.RenderMap();
                Constants.IsButtonActive = true;
            }
            else
            {
                gameManager.CreatePlayerWithFaction(faction, gameManager.standart);
                gameManager.TakeUnit();
                gameManager.player.currentX = 0;
                gameManager.player.currentY = 0;
                Constants.IsButtonActive = true;
            }
        }
    }
}
