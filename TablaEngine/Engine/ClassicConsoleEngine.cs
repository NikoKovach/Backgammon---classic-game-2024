namespace TablaEngine.Engine
{
     using System;
     using System.Linq;

     using TablaEngine.Engine.Contracts;
     using TablaGameLogic.Core;
     using TablaModels.ComponentModels.Enums;    
     using TablaEngine.IO;
     using TablaGameLogic.Core.Contracts;
     using TablaEngine.IO.Contracts;
     using static TablaGameLogic.Utilities.Messages.OutputMessages;

     public class ClassicConsoleEngine : ConsoleEngine,IConsoleEngine,IEngine
     {
          /*
               TODO Method()'Undo last move' 
               отмята на последния ход ,
               който не е последен от възможните ходове за дадения играч .
           */

          public ClassicConsoleEngine() : base(new Controller(), new Writer(),new Reader())
          { }

          public ClassicConsoleEngine(IController someController, IWriter outerWriter, IReader outerReader) : base(someController,outerWriter,outerReader)
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

          private void MainGameMethod() // be private
          {
               GeneralLoopCurrentPlayerMoves();

               this.Controller.ChangeCurrentPlayer();

               string currentPlayerName = this.Controller.CurrentPlayer.Name;

               this.PressKeyForRolling(currentPlayerName, AskPlayerToRollDiceAndMakeMove);

               if (!this.Controller.RollDice())
               {
                   this.Writer.WriteLine(string.Format(ThePlayerHaveNoMove,      currentPlayerName));

                   return;
               }
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
                    message = this.Controller.CurrentPlayerMakesMove(this.Reader.ReadLine());

                    this.Writer.WriteLine(message);
               }
          }
    }
}



