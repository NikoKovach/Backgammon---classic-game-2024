namespace TablaEngine.Engine
{
    using System;
    using System.Threading;
    using System.Linq;
    using System.Collections.Generic;

    using TablaEngine.Engine.Contracts;
    using TablaEngine.IO.Contracts;

    using TablaGameLogic.Core;
    using TablaGameLogic.Core.Contracts;

    using TablaModels.ComponentModels.Components.Interfaces;
    using TablaModels.ComponentModels.Enums;
    using static TablaGameLogic.Utilities.Messages.OutputMessages;
    using TablaEngine.IO;

    public class ClassicConsoleEngine : ConsoleEngine,IEngine
    {
        public ClassicConsoleEngine() : base(new Controller(), new Writer(),new Reader())
        { }

        public override void Run()
        {
            try
            {
                ChoiceOfColorByThePlayers();

                PlayersArrangeTheirCheckers();

                WhoWillMakeTheFirstMove();

                GeneralLoopCurrentPlayerMoves();

                //TODO Method()'Undo last move' 
                //// отмята на последния ход ,който не е последен от възможните ходове за дадения играч

                while (true)
                {
                    MainGameMethod(); 
                }
            }
            catch (ArgumentException argEx)
            {
                this.Writer.WriteLine("\n\r" + argEx.Message + 
                                      "\n\r"  + argEx.InnerException.Message + 
                                      "\n\r"+  argEx.StackTrace);
            }          
            catch (Exception ex)
            {
                this.Writer.WriteLine("\n\r" + ex.Message + 
                                      "\n\r" + ex.StackTrace);
            }       
        }

        private void ChoiceOfColorByThePlayers() // be private
        {
            int colorNumber = int.MinValue;

            while (true)
            {
                if (colorNumber == 1 || colorNumber == 2 )
                {
                    break;
                }

                if (colorNumber == 0)
                {
                    Environment.Exit(0);
                }

                this.Writer.Write(ChooseOfColor);

                colorNumber = int.Parse(this.Reader.ReadLine());

                if (colorNumber != 2 && colorNumber != 1 && colorNumber != 0)
                {
                    this.Writer.WriteLine(InvalidChooseOfColor);
                    this.Writer.WriteLine(string.Empty);
                }   
            }

            string returnedMessage = this.Controller.PlayersChooseAColor(colorNumber);

            this.Writer.WriteLine(returnedMessage);
        } 

        private void PlayersArrangeTheirCheckers() // be private
        {
            string message = this.Controller.ArrangingTheCheckersToPlay();

            this.Writer.WriteLine(message);
        } 

        private void WhoWillMakeTheFirstMove()
        {
            while (true)
            {
                EachOtherPlayerRollOneDice();

                this.Controller.CurrentPlayerFirstSet();

                if (this.Controller.CurrentPlayerMovesNumber == 2)
                {
                    this.Writer.WriteLine(string.Format(PlayerStartFirst, this.Controller.CurrentPlayer.Name, this.Controller.CurrentPlayer.MyPoolsColor));

                    break;
                }
                else 
                {
                    this.Writer.WriteLine(TheDiceOfPlayersAreTheSame);
                }
            }

            int seconds = 10;
            GameStartsAfterAFewSeconds(seconds);
        } 

        public void MainGameMethod() // be private
        {
            Console.Clear();
            this.Controller.ChangeCurrentPlayer();
            string currentPlayerName = this.Controller.CurrentPlayer.Name;

            PressKeyForRolling(currentPlayerName, AskPlayerToRollDiceAndMakeMove);

            if (!this.Controller.RollDice())
            {
                this.Writer.WriteLine(string.Format(ThePlayerHaveNoMove, currentPlayerName));

                return;
            }

            GeneralLoopCurrentPlayerMoves();
        }

        private void GeneralLoopCurrentPlayerMoves()
        {
            Console.Clear();

            this.Writer.WriteLine(this.Controller.InitialInfoCurrentPlayerMoves());
            this.Writer.WriteLine(MovesType);
            this.Writer.Write(EnterTheWayOfMoveAndItsParameters);

            while (this.Controller.CurrentPlayerMovesNumber > 0)
            {
                if (this.Controller.CurrentPlayer.State == PlayerState.Winner)
                {
                    PrintTheWinner(this.Controller.CurrentPlayer);

                    ExitGameOrPlayNewGame();

                    break;
                }

                ResultFromCurrentPlayerMoves();
            }
        }

        private void ResultFromCurrentPlayerMoves()
        {
            string message = InvalidMove;

            while (message.Equals(InvalidMove))
            {
                string[] moveTypeAndParams = this.Reader
                .ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

                message = this.Controller.CurrentPlayerMakesMove(moveTypeAndParams);

                this.Writer.WriteLine(message);
            }
        }

        private void EachOtherPlayerRollOneDice()
        {
            int[] diceValues = { 0, 0 };

            for (int i = 0; i < this.Controller.Players.Count; i++)
            {
                string currentPlayerName = this.Controller.Players[i].Name;

                PressKeyForRolling(currentPlayerName, OnePlayerRollADice);

                diceValues[i] = this.Controller.Players[i].RollADice();

                this.Controller.TablaBoard.DiceSet[i + 1].ValueOfOneDice = diceValues[i];

                this.Writer.WriteLine(string.Format(OneDiceRollResult, currentPlayerName, diceValues[i]));
            }
        }

        private void PressKeyForRolling(string playerName,string printMessage)
        {
            this.Writer.Write(string.Format(printMessage, playerName));

            string rollCommand = this.Reader.ReadLine().ToLower();

            while (true)
            {
                if (rollCommand == "r")
                {
                    break;
                }

                this.Writer.Write(string.Format(printMessage, playerName));

                rollCommand = this.Reader.ReadLine().ToLower();
            }
        }

        private void GameStartsAfterAFewSeconds(int seconds)
        {
            this.Writer.WriteLine($"\n\rThe game will start after {seconds} seconds !");
            Thread.Sleep(seconds * 1000);
        }

        private void ExitGameOrPlayNewGame()
        {
            string exitNewGameText;

            while (true)
            {
                this.Writer.WriteLine(MessageExitGameOrStartNewGame);

                exitNewGameText = this.Reader.ReadLine();

                if (exitNewGameText.ToLower() == "x" || exitNewGameText.ToLower() == "n")
                {
                    break;
                }
                else
                {                  
                    this.Writer.WriteLine(InvalidCommandForExitOrNewGame);
                }
            }

            if (exitNewGameText.ToLower() == "x")
            {
                Environment.Exit(0);
            }
            else if (exitNewGameText.ToLower() == "n")
            {
                Console.Clear();

                this.Controller.ClearBoardFromCheckers();

                this.Run();
            }
        }

        private void PrintTheWinner(IPlayer currentPlayer)
        {
            this.Writer.WriteLine(string.Format(TheWinnerIs, currentPlayer.Name));
        }

    }
}

//private void CurrentPlayerMoves() //be private
//{
//    GeneralLoopCurrentPlayerMoves();

//    this.controller.ChangeCurrentPlayer();
//}


