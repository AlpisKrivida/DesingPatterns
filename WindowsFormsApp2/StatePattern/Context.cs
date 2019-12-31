using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Lab1_1.Facade;
using Lab1_1.MementoPattern;
using Lab1_1.Streategy;

namespace WindowsFormsApp2.StatePattern
{
    class Context
    {
        private int turnLimit = 4;
        private int n = 0;
        private State state;
        private GameManager gameManager;
        private bool succesfulMove;


        private Caretaker _playerSnapshot = new Caretaker();

        public Context(GameManager gm)
        {
            gameManager = gm;
            state = new LoginState(gm);
        }

        public void ChangeState(State state)
        {
            this.state = state;
            state.RenderElements();
        }

        public void NextState(string direction)
        {
            Debug.WriteLine(state);
            switch (state)
            {
                case LoginState _:
                    state.RenderElements();
                    state = new PlayState(gameManager);
                    state.RenderElements();
                    break;
                case PlayState _:
                    state.RenderElements();
                    if (turnLimit > 0)
                    {
                        //memento -----------------------------------------------
                        _playerSnapshot.Memento = gameManager.player.SaveMemento();


                        ((Teleport)gameManager.teleport).SetStartingPosition(gameManager.player.currentX, gameManager.player.currentY);

                        if (n < gameManager.player.NumberOfActions)
                        {
                            n++;
                            if(!(gameManager.player.getAlgorithm() is Teleport))
                                gameManager.MovePlayer(direction, ref succesfulMove);
                            if (succesfulMove) { n--; }
                        }
                        else {
                            if (turnLimit-1 == 0){
                                state = new StopState(gameManager);
                                state.RenderElements();
                            }
                            else{
                                n = 0;
                                state = new UndoState(gameManager);
                            }
                        }
                    }
                    break;
                case ChooseState _:
                    state.RenderElements();
                    state = new PlayState(gameManager);
                    state.RenderElements();
                    break;
                case UndoState _:

                    //memento-------------------------------------------------
                    gameManager.player.RestoreMemento(_playerSnapshot.Memento);


                    state.RenderElements();
                    if (state.IsIterationFinished()) {
                        turnLimit--;
                        state = new ChooseState(gameManager);
                    }
                    else{
                        state = new PlayState(gameManager);
                    }
                    break;
                case StopState _:
                    break;              
                default:
                    throw new ArgumentException(
                    message: "sate is not a recognized sate",
                    paramName: nameof(state));
            }
        }

        public State GetState()
        {
            return state;
        }

    }
}
