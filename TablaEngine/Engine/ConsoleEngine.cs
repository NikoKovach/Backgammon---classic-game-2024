namespace TablaEngine.Engine
{
     using System;
     using System.Threading;
     using TablaEngine.Engine.Contracts;
     using TablaEngine.IO.Contracts;
     using TablaGameLogic.Core.Contracts;
     using TablaModels.ComponentModels;
     using TablaModels.ComponentModels.Components.Interfaces;
     using static TablaGameLogic.Utilities.Messages.ExceptionMessages;
     using static TablaGameLogic.Utilities.Messages.OutputMessages;
     using static TablaGameLogic.Utilities.Messages.GameConstants;

     public abstract class ConsoleEngine : IConsoleEngine,IEngine
     {
          private readonly IController controller;
          private readonly IWriter writer;
          private readonly IReader reader;

          public ConsoleEngine(IController someController, IWriter outerWriter, IReader         OuterReader)
          {
              this.controller = someController ?? throw new ArgumentNullException("");
              this.writer = outerWriter ?? throw new ArgumentNullException("");
              this.reader = OuterReader ?? throw new ArgumentNullException("");
          }

          public IController Controller 
          { 
              get => this.controller;
          }

          public IWriter Writer 
          { 
              get => this.writer;
          }

          public IReader Reader 
          { 
              get => this.reader;
          }

          public abstract void Run();
          
          public void RegistrationOfPlayers()
          {
               string[] playersName = new string[NumberOfPlayers];

               for ( int i = 0; i < NumberOfPlayers; i++ )
               {
                    this.writer.WriteLine( PlayerNameRequirement );
                    this.Writer.Write(string.Format(EnterPlayerName,i + 1));
                    
                    playersName[i] = GetValidPlayerName(i + 1);
                    Console.Clear();
               }
               
               this.controller.CreatePlayers( playersName[0], playersName[1] ); 
          }

          public virtual void ChoiceOfColorByThePlayers()
          {
               this.Writer.Write( string.Format( ChooseOfColor, this.Controller.Players[0].Name ));

               string colorNumber = string.Empty;

               while (true)
               {
                    if (colorNumber.Equals( "1" ) || colorNumber.Equals( "2" ))
                    {
                        break;
                    }

                    if (colorNumber.Equals( "x" ))
                    {
                        Environment.Exit(0);
                    }

                    colorNumber = this.Reader.ReadLine();

                    if (!colorNumber.Equals( "1" ) && !colorNumber.Equals( "2" ) && !colorNumber.Equals( "x" ))
                    {
                        this.Writer.Write(InvalidChooseOfColor);
                    }   
               }

               int color = int.Parse(colorNumber);
               string returnedMessage = this.Controller.PlayersChooseAColor(color);

               this.Writer.WriteLine(returnedMessage);
          } 

          public virtual void PlayersArrangeTheirCheckers() 
          {
               string message = this.Controller.ArrangingTheCheckersToPlay();

               this.Writer.WriteLine(message);

               Thread.Sleep( 5000 );
               Console.Clear();
          }

          public virtual void WhoWillMakeTheFirstMove()
          {
               int[] diceValues = { 0, 0 };

               while (true)
               {
                    EachOtherPlayerRollOneDice(diceValues);

                    if ( diceValues[0] != diceValues[1] )
                    {     
                        break;
                    }
                        
                    this.Writer.WriteLine(TheDiceOfPlayersAreTheSame);
               }

               this.Controller.CurrentPlayerFirstSet();

               this.Writer.WriteLine(string.Format(PlayerStartFirst,
                             this.Controller.CurrentPlayer.Name,
                             this.Controller.CurrentPlayer.MyPoolsColor));

               GameStartsAfterAFewSeconds();
          } 
          
          public virtual void PressKeyForRolling(string playerName,string printMessage)
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

          public virtual void PrintTheWinner(IPlayer currentPlayer)
          {
              this.Writer.WriteLine(string.Format(TheWinnerIs, currentPlayer.Name));
          }

          public virtual void ExitGameOrPlayNewGame()
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
               
               if (exitNewGameText.ToLower() == "n")
               {
                   Console.Clear();

                   this.Controller.ClearBoardFromCheckers();

                   this.Run();
               }
          }

     //******************************************************************

          private void EachOtherPlayerRollOneDice(int[] diceValues)
          {
               for (int i = 0; i < this.Controller.Players.Count; i++)
               {
                    string currentPlayerName = this.Controller.Players[i].Name;

                    PressKeyForRolling(currentPlayerName, OnePlayerRollADice);

                    diceValues[i] = this.Controller.Players[i].RollADice();

                    this.Controller.TablaBoard.DiceSet[i + 1].ValueOfOneDice = diceValues[i];

                    this.Writer.WriteLine(string.Format(RollResultOfOneDice, currentPlayerName, diceValues[i]));
               }
          }

          private string GetValidPlayerName(int number)
          {
               string name = this.reader.ReadLine();

               while ( true )
               {
                    if ( name != null && name != string.Empty &&
                         name.Length >= TableGlobalConstants.MinLenghtPlayerName )
                    {
                         
                         break;
                    }

                    this.writer.WriteLine( InvalidPlayerName );
                    this.Writer.Write(string.Format(EnterPlayerName,number));

                    name = this.reader.ReadLine();
               }

               return name;
          }

          private void GameStartsAfterAFewSeconds()
          {
              this.Writer.WriteLine(string.Format(DelayTheGame,SecondsToDelayTheGame));

              Thread.Sleep(SecondsToDelayTheGame * 1000);
          }
     }
}
