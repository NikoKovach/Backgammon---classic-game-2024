namespace TablaGameLogic.Core
{
     using System;
     using System.Linq;
     using System.Collections.Generic;
     using System.Reflection;

     using TablaGameLogic.Core.Contracts;
     using TablaGameLogic.Factory;
     using TablaGameLogic.Services.Contracts;

     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums; 

     using static TablaGameLogic.Utilities.Messages.ExceptionMessages;
     using static TablaGameLogic.Utilities.Messages.OutputMessages;
     using static TablaGameLogic.Services.GeneralServices;
     using TablaModels.ComponentModels.Components.Players;

     public class Controller : IController
     {        
          private static IBoard  defaultTablaBoard       = new BoardFactory().Create();

          private readonly IBoard   tablaBoard;
          private IList<IPlayer>    players;
          private IMotionValidation motionValidate;

          public Controller() :this(defaultTablaBoard)
          {}

          public Controller(IBoard board)
          {          
               this.tablaBoard = board ?? throw new ArgumentNullException(string.Format        (InvalidGameBoard,nameof(this.tablaBoard)));

          }

          public IBoard TablaBoard 
          { 
              get { return this.tablaBoard; }
          } 

          public IList<IPlayer> Players 
          {
              get { return this.players; }
          }         

          public IPlayer CurrentPlayer { get; set; }

          public int CurrentPlayerMovesNumber 
          {
               get 
               {
                   return this.TablaBoard.ValueOfDiceAndCountOfMoves.Values.Sum();
               } 
          }

          public IMotionValidation MotionValidate 
          {
               get { return this.motionValidate; }

               private set 
               {
                    if (value == null)
                    {
                        throw new ArgumentNullException(string.Format       (InvalidMoveConfirmationParameter, nameof(this.motionValidate)));
                    }

                    this.motionValidate = value;
               } 
          }

          public void CreatePlayers( string firstPlayerName, string secondPlayerName)
          {
               List<IPlayer> someDefaultPlayers = new List<IPlayer>
               {
                    new PlayerFactory().CreatePlayer( firstPlayerName,this.TablaBoard.ColumnSet ),
                    new PlayerFactory().CreatePlayer( secondPlayerName,this.TablaBoard.ColumnSet)
               };

               this.players = someDefaultPlayers;
          }

          public string PlayersChooseAColor(int color)
          {
               DeterminingTheColorOfThePlayers( color);

               string message = string.Format(PlayersChoseTheirColor, this.Players[0].Name,  this.Players[0].MyPoolsColor, this.Players[1].Name, this.Players  [1].MyPoolsColor);

               return message;
          }

          public string ArrangingTheCheckersToPlay() 
          {
               for (int i = 0; i < this.Players.Count; i++)
               {
                   this.players[i].ArrangingTheCheckers(this.tablaBoard);
               }

               string message = PlayersArrangedHisCheckers;

               return message;
          }

          public bool RollDice()
          {
               for (int i = 0; i < this.TablaBoard.DiceSet.Count; i++)
               {
                   this.TablaBoard.DiceSet[i+1].ValueOfOneDice = this.CurrentPlayer.RollADice();
               }

               SetValueOfDiceAndCountOfMoves();

               if (this.MotionValidate.CurrentPlayerHasNoMoves())
               {
                   this.ChangeCurrentPlayer();
                   return false;
               }

               return true;
          }

          public string InitialInfoCurrentPlayerMoves() // Къде му е мястото
          {
               string currentPlayerName = this.CurrentPlayer.Name;

               string currentPlayerCheckersColor = this.CurrentPlayer.MyPoolsColor.ToString();

               int diceOneValue = this.TablaBoard.DiceSet[1].ValueOfOneDice;
               int diceTwoValue = this.TablaBoard.DiceSet[2].ValueOfOneDice;

               return string.Format(CurrentPlayerWithTwoDiceRollResult, currentPlayerName,   currentPlayerCheckersColor, diceOneValue, diceTwoValue);
          }

          public string CurrentPlayerMakesMove(string[] moveWithParameters)
          {          
               string typeOfMove = moveWithParameters[0][0].ToString().ToUpper() +         moveWithParameters[0].Remove(0, 1);

               if (typeOfMove.StartsWith("In") || typeOfMove.StartsWith("Out"))
               {
                   typeOfMove += "side";
               }

               List<int> moveParams = moveWithParameters.Skip(1).Select(int.Parse).ToList();

               if (typeOfMove.Equals("Move"))
               {
                   int numberOfMoves = CalculateTheNumberOfMoves(moveParams[1],       this.TablaBoard.ValueOfDiceAndCountOfMoves);

                   moveParams.Add(numberOfMoves);
               }

               bool motionIsValid = InvokeMotionValidationMethod(typeOfMove,     moveParams.ToArray   ());

               if (motionIsValid == false) 
               {
                   return InvalidMove;
               }

               InvokeTheMoveMethodOfPlayer(typeOfMove, moveParams.ToArray());

               ChangeValueOfDiceAndCountOfMoves(typeOfMove, moveParams,     this.CurrentPlayer.MyPoolsColor, this.TablaBoard);

               GameHasAWinner();

               if (this.MotionValidate.HasNoOtherMoves())
               {
                   string resultText = string.Format(NoOtherMoves, this.CurrentPlayer.Name);

                   return resultText;
               }

               return MoveIsMade;
          }

          public void CurrentPlayerFirstSet()
          {
               int numberOne = this.TablaBoard.DiceSet[1].ValueOfOneDice;
               int numberTwo = this.TablaBoard.DiceSet[2].ValueOfOneDice;

               this.CurrentPlayer = numberOne > numberTwo ? this.Players[0] : this.Players[1];

               CreateAdditionalServices();

               SetValueOfDiceAndCountOfMoves();
          }

          public void ChangeCurrentPlayer()
          {
               PoolColor currentPlayerColor = this.CurrentPlayer.MyPoolsColor;

               this.CurrentPlayer = 
                  currentPlayerColor == PoolColor.Black ? this.Players[0] : this.Players[1];
          }

          public void ClearBoardFromCheckers()
          {
               foreach (var column in this.TablaBoard.ColumnSet)
               {
                    while (column.Value.PoolStack.Count != 0)
                    {
                        column.Value.PoolStack.Pop();
                    }
               }

               ChangeAllCheckersStateToStarting(this.TablaBoard.BlackPoolsSet);

               ChangeAllCheckersStateToStarting(this.TablaBoard.WhitePoolsSet);
          }
//****************************************************************************
          private void GameHasAWinner()
          {
               IEnumerable<IPool> currentPlayerCheckersSet = 
                    (this.CurrentPlayer.MyPoolsColor == PoolColor.White)
                    ? this.TablaBoard.WhitePoolsSet : this.TablaBoard.BlackPoolsSet;

               if (!currentPlayerCheckersSet.Any(x => x.State != PoolState.OutOfGame))
               {
                   this.CurrentPlayer.State = PlayerState.Winner;
               }
          }

          private void SetValueOfDiceAndCountOfMoves()
          {
               int numberOne = this.TablaBoard.DiceSet[1].ValueOfOneDice;
               int numberTwo = this.TablaBoard.DiceSet[2].ValueOfOneDice;

               this.TablaBoard.ValueOfDiceAndCountOfMoves.Clear();

               if (numberOne != numberTwo)
               {
                   this.TablaBoard.ValueOfDiceAndCountOfMoves[numberOne] = 1;
                   this.TablaBoard.ValueOfDiceAndCountOfMoves[numberTwo] = 1;
               }

               if (numberOne == numberTwo)
               {
                   this.TablaBoard.ValueOfDiceAndCountOfMoves[numberOne] = 4;
               }
          }

          private bool InvokeMotionValidationMethod(string typeOfMove, int[] moveParams)
          {
               string motionValidateName = typeOfMove + "Validate";

               Type motionValidateType = Type.GetType($"TablaGameLogic.Services.      {motionValidateName}");

               IMotionValidation instanceOfMotionValidatе = (IMotionValidation)       Activator.CreateInstance(motionValidateType, new object[]      { this.TablaBoard,this.CurrentPlayer});

               return instanceOfMotionValidatе.IsValidMove(moveParams);
          }

          private void InvokeTheMoveMethodOfPlayer(string moveType,int[] moveParams)
          {
               int[] realParams = moveParams.SkipLast(1).ToArray();

               MethodInfo moveMethodType = this.CurrentPlayer.Move.GetType().GetMethod     (moveType);

               object[] parametersArray = new object[realParams.Length];

               realParams.CopyTo(parametersArray, 0);

               moveMethodType.Invoke(this.CurrentPlayer.Move, parametersArray);
          }

          private  void DeterminingTheColorOfThePlayers( int colorOfPools)
          {
               if ( colorOfPools == (int)PoolColor.White )
               {
                    this.players[0].MyPoolsColor = PoolColor.White;
                    this.players[1].MyPoolsColor = PoolColor.Black;
               }
               else if ( colorOfPools == (int)PoolColor.Black )
               {
                    this.players[0].MyPoolsColor = PoolColor.Black;
                    this.players[1].MyPoolsColor = PoolColor.White;
               }
          }     

          private void ChangeAllCheckersStateToStarting(List<IPool> checkersCollection) 
          {
              foreach (var item in checkersCollection)
              {
                  item.State = PoolState.Starting;
              }
          }

          private void CreateAdditionalServices()
          {
              this.MotionValidate = new MotionValidateFactory().Create(this.TablaBoard,     this.CurrentPlayer);
          }
          
     }
}
