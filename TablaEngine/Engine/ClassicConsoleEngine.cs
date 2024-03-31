namespace TablaEngine.Engine
{
     using System;

     using TablaEngine.Engine.Contracts;
     using TablaGameLogic.Core;
     using TablaEngine.IO;
     using TablaEngine.IO.Contracts;

     using TablaGameLogic.Core.Contracts;
     using TablaModels.Enums;

     using static TablaGameLogic.Utilities.Messages.OutputMessages;

     public class ClassicConsoleEngine : ConsoleEngine,IConsoleEngine,IEngine
     {
          public ClassicConsoleEngine() : 
               base(new Controller(), new Writer(),new Reader())
          { }

          public ClassicConsoleEngine
               (IController someController, IWriter outerWriter, IReader outerReader) :
               base(someController,outerWriter,outerReader)
          { }

          public override void Run()
          {
               try
               {
                    this.RegistrationOfPlayers();
                    this.ChoiceOfColorByThePlayers();
                    this.PlayersArrangeTheirCheckers();
                    this.WhoWillMakeTheFirstMove();

                    while (true)
                    {
                        MainGameMethod(); 
                    }
               }
               catch (ArgumentException argEx)
               {
                   this.Writer.WriteLine("\n\r" + argEx.Message  + 
                                         "\n\r"+  argEx.StackTrace);
               }          
               catch (Exception ex)
               {
                   this.Writer.WriteLine("\n\r" + ex.Message + 
                                         "\n\r" + ex.StackTrace);
               }       
          }

          public void MainGameMethod()
          {
               GeneralLoopCurrentPlayerMoves();

               this.Controller.ChangeCurrentPlayer();

               string currentPlayerName = this.Controller.CurrentPlayer.Name;

               Console.Clear();

               this.PressKeyForRolling(currentPlayerName, AskPlayerToRollDiceAndMakeMove);

               this.Controller.RollDice();
          }

          private void GeneralLoopCurrentPlayerMoves()
          {
               Console.Clear();

               if ( !this.Controller.HasOtherMoves())
               {
                    this.Writer.WriteLine( "No Moves !!!" );

                    return;
               }

               this.Writer.WriteLine(this.Controller.InitialInfoCurrentPlayerMoves());
               this.Writer.WriteLine(MovesType);
               this.Writer.WriteLine(EnterTheWayOfMoveAndItsParameters);

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
                    message = this.Controller
                             .CurrentPlayerMakesMove( this.Reader.ReadLine() );

                    this.Writer.WriteLine(message);

                    if ( !this.Controller.HasOtherMoves())
                    {
                         this.Controller.TablaBoard.DiceValueAndMovesCount.Clear();
                         break;
                    }

                    string playerName = this.Controller.CurrentPlayer.Name;
                    this.Writer.WriteLine(string.Format(NextMoveMessage,playerName));
               }
          }
     }
}
