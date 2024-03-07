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
     using static TablaGameLogic.Services.MoveServices;
     using static TablaGameLogic.Utilities.Messages.GameConstants;
     using TablaGameLogic.Services;

     public class Controller : IController
     {        
          private static IBoard  defaultTablaBoard = CreateDefaultBoard();
          private static IMotionValidation defaultMotionValidate = GetMoveValidation();
          private static IMoveService defaultMoveService = GetMoveService();

          private readonly IBoard   tablaBoard;
          private IList<IPlayer>    players;
          private IMotionValidation motionValidate;
          private IMoveService moveService;
          public Controller() :this(defaultTablaBoard,defaultMotionValidate,defaultMoveService)
          { }

          public Controller(IBoard board,IMotionValidation motionValidate,IMoveService moveService)
          {          
               this.tablaBoard = board ?? 
                    throw new ArgumentNullException
                    (
                         string.Format(InvalidGameBoard,nameof(board))
                    );

               this.motionValidate = motionValidate ?? 
                    throw new ArgumentNullException
                    (
                        string.Format(InvalidMoveConfirmationParameter,
                        nameof(motionValidate))
                    );

               this.moveService = moveService ??
                    throw new ArgumentNullException
                    (
                        string.Format(InvalidMoveConfirmationParameter,
                        nameof(moveService))
                    );
          }

          public IBoard TablaBoard => this.tablaBoard;

          public IList<IPlayer> Players => this.players;
    
          public IPlayer CurrentPlayer { get; set; }

          public int CurrentPlayerMovesNumber => this.TablaBoard.ValueOfDiceAndCountOfMoves.Values.Sum();

          public IMotionValidation MotionValidate => this.motionValidate;

          public void CreatePlayers( string firstPlayerName, string secondPlayerName)
          {
               this.players = new PlayerFactory().CreatePlayers(firstPlayerName,secondPlayerName,this.TablaBoard).ToList();
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
               for (int i = 0; i < NumberOfDice; i++)
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

          public string InitialInfoCurrentPlayerMoves() 
          {
               string currentPlayerName = this.CurrentPlayer.Name;

               string currentPlayerCheckersColor = this.CurrentPlayer.MyPoolsColor.ToString();

               int diceOneValue = this.TablaBoard.DiceSet[1].ValueOfOneDice;
               int diceTwoValue = this.TablaBoard.DiceSet[2].ValueOfOneDice;

               return string.Format(CurrentPlayerTwoDiceResult, currentPlayerName,   currentPlayerCheckersColor, diceOneValue, diceTwoValue);
          }

          public string CurrentPlayerMakesMove(string moveString)
          {
               //TODO : Work here 
               //MoveService class and ValidateService class

               KeyValuePair<string,object[]> moveKvp = 
                    this.moveService.ParseMove(moveString,this.TablaBoard,this.CurrentPlayer);

               if ( moveKvp.Value.Any( x => x.Equals(null) ))
               {
                    return InvalidMove;
               }

               bool moveIsValid = new ValidateService().MoveIsValid( moveKvp.Key, moveKvp.Value, this.TablaBoard, this.CurrentPlayer );

               if ( moveIsValid == false)
               {
                    //if  false  - > return съобщение за невалиден ход
                    return InvalidMove;
               }
               
               this.moveService.InvokeMoveMethod(moveKvp.Key, moveKvp.Value,this.CurrentPlayer); 
               
               GameHasAWinner();

               return MoveIsMade;
          }

          public void CurrentPlayerFirstSet()
          {
               int numberOne = this.TablaBoard.DiceSet[1].ValueOfOneDice;
               int numberTwo = this.TablaBoard.DiceSet[2].ValueOfOneDice;

               this.CurrentPlayer = numberOne > numberTwo ? this.Players[0] : this.Players[1];

               SetValueOfDiceAndCountOfMoves();

          //##################################################
               //CreateAdditionalServices();
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

          //private bool MoveIsValid()
          //{
          //     //TODO :
          //     //public static bool InvokeMotionValidationMethod(string moveType,IBoard board,IPlayer CurrentPlayer)

          //     return true;
          //}

          private static IMotionValidation GetMoveValidation()
          {
               return new MotionValidate();
          }

          private static IBoard CreateDefaultBoard()
          {
               return new BoardFactory().Create();
          }          
          
          private static IMoveService GetMoveService()
          {
               return new MoveServices();
          }
     }
}

//Has A Valid Move true or false *******************************************

//bool motionIsValid = true;
//InvokeMotionValidationMethod( moveKvp.Key, paramsList.ToArray() );
//***************************************************************************


//if (this.MotionValidate.HasNoOtherMoves())
//{
//    string resultText = string.Format(NoOtherMoves,this.CurrentPlayer.Name);

//    return resultText;
//}

//ChangeValueOfDiceAndCountOfMoves(moveKvp.Key, moveKvp.Value,     this.CurrentPlayer.MyPoolsColor, this.TablaBoard);


//private bool InvokeMotionValidationMethod(string typeOfMove, int[] moveParams)
//{
//     string motionValidateName = typeOfMove + "Validate";

//     Type motionValidateType = Type.GetType($"TablaGameLogic.Services.     {motionValidateName}");

//     IMotionValidation instanceOfMotionValidatе = (IMotionValidation)      Activator.CreateInstance(motionValidateType, new object[]     { this.TablaBoard,this.CurrentPlayer});

//     return instanceOfMotionValidatе.IsValidMove(moveParams);
//}

//private void InvokeMoveMethod(string moveType,object[] moveParams)
//{
//     MethodInfo moveMethodType = this.CurrentPlayer.Move.GetType().GetMethod   (moveType);

//     moveMethodType.Invoke(this.CurrentPlayer.Move, moveParams);

//     //int[] realParams = moveParams.SkipLast(1).ToArray();
//object[] parametersArray = new object[realParams.Length];

//realParams.CopyTo(parametersArray, 0);
//}