namespace TablaGameLogic.Core
{
     using System;
     using System.Linq;
     using System.Collections.Generic;

     using TablaGameLogic.Core.Contracts;
     using TablaGameLogic.Factory;
     using TablaGameLogic.Services;
     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums; 
     using TablaModels.ComponentModels.Components.Players;

     using static TablaGameLogic.Utilities.Messages.ExceptionMessages;
     using static TablaGameLogic.Utilities.Messages.OutputMessages;
     using static TablaGameLogic.Services.CalculateService;
     using static TablaGameLogic.Utilities.Messages.GameConstants;

     public class Controller : IController
     {        
          private static IBoard  defaultTablaBoard = CreateDefaultBoard();
          //private static IMotionValidation defaultMotionValidate = GetMoveValidation();
          private static IMoveService defaultMoveService = GetMoveService();

          private readonly IBoard   tablaBoard;
          private IList<IPlayer>    players;
          //private IMotionValidation motionValidate;
          private IMoveService moveService;

          public Controller() :this(defaultTablaBoard,defaultMoveService)
          { }

          public Controller(IBoard board,IMoveService moveService)
          {          
               this.tablaBoard = board ?? 
                    throw new ArgumentNullException
                    (
                         string.Format(InvalidGameBoard,nameof(board))
                    );


               this.moveService = moveService ??
                    throw new ArgumentNullException
                    (
                        string.Format(InvalidMoveConfirmationParameter,
                        nameof(moveService))
                    );

               //this.motionValidate = motionValidate ?? 
               //     throw new ArgumentNullException
               //     (
               //         string.Format(InvalidMoveConfirmationParameter,
               //         nameof(motionValidate))
               //     );
          }

          public IBoard TablaBoard => this.tablaBoard;

          public IList<IPlayer> Players 
          {
               get => this.players;
               set => this.players = value; 
          } 
    
          public IPlayer CurrentPlayer { get; set; }

          public int CurrentPlayerMovesNumber => this.TablaBoard.DiceValueAndMovesCount.Values.Sum();

          public IMotionValidation MotionValidate { get; set; }

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
                    this.Players[ i ].ArrangingTheCheckers( this.TablaBoard, DefaultArrangePools() );
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

               SetDiceValueAndMovesCount(this.TablaBoard);

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
               try
               {
                    KeyValuePair<string,int[]> moveKvp = this.moveService.ParseMove(moveString);

                    int[] realInvokeMethodParams = 
                         GetlInvokeMethodParams( moveKvp.Key, 
                         moveKvp.Value ,this.CurrentPlayer.MyPoolsColor, this.TablaBoard);

                    if ( moveKvp.Value.Any( x => x.Equals(null) ))
                    {
                         return InvalidMove;
                    }

                    //TODO : Work here 
                    //MoveService class and ValidateService class

                    //bool moveIsValid = new ValidateService().MoveIsValid( moveKvp.Key,  realInvokeMethodParams, this.TablaBoard, this.CurrentPlayer );

                    bool moveIsValid = MotionValidate.MoveIsValid( realInvokeMethodParams );
                    
                    //bool moveIsValid = true;

                    if ( !moveIsValid )
                    {
                         return InvalidMove;
                    }

                    object[] moveParams = 
                         this.moveService.GetInvokeMethodParameters( moveKvp.Key, realInvokeMethodParams, this.TablaBoard, this.CurrentPlayer );

                    this.moveService.InvokeMoveMethod(moveKvp.Key, moveParams,this.CurrentPlayer);

                    ChangeDiceValueAndMoveCount( moveKvp.Key, moveKvp.Value, this.CurrentPlayer.MyPoolsColor, this.TablaBoard );
               
                    GameHasAWinner();

                    return MoveIsMade;
               }
               catch ( Exception ex)
               {
                    return ex.Message;
               }    
          }

          public void CurrentPlayerFirstSet()
          {
               int numberOne = this.TablaBoard.DiceSet[1].ValueOfOneDice;
               int numberTwo = this.TablaBoard.DiceSet[2].ValueOfOneDice;

               this.CurrentPlayer = numberOne > numberTwo ? this.Players[0] : this.Players[1];

               SetDiceValueAndMovesCount( this.TablaBoard );
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
               IEnumerable<IPool> playerChipSet = 
                    (this.CurrentPlayer.MyPoolsColor == PoolColor.White)
                    ? this.TablaBoard.WhitePoolsSet : this.TablaBoard.BlackPoolsSet;

               if (!playerChipSet.Any(x => x.State != PoolState.OutOfGame))
               {
                   this.CurrentPlayer.State = PlayerState.Winner;
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

          private void ChangeAllCheckersStateToStarting(IList<IPool> checkersCollection) 
          {
              foreach (var item in checkersCollection)
              {
                  item.State = PoolState.Starting;
              }
          }

          public IMotionValidation SetUpMoveValidation()
          {
               return new MotionValidate(this.TablaBoard,this.CurrentPlayer);
          }

          private static IBoard CreateDefaultBoard()
          {
               return new BoardFactory().Create();
          }          
          
          private static IMoveService GetMoveService()
          {
               return new MoveServices();
          }

          private IArrangeChips DefaultArrangePools()
          {
               return new ArrangePools();
          }
     }
}

          //SetValueOfDiceAndCountOfMoves();

          //##################################################
          //CreateAdditionalServices();


          //private bool MoveIsValid()
          //{
          //     //TODO :
          //     //public static bool InvokeMotionValidationMethod(string moveType,IBoard board,IPlayer CurrentPlayer)

          //     return true;
          //}

//**************************************************************************
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

          //public void CreatePlayers( string firstPlayerName, string secondPlayerName)
          //{
          //     this.players = new PlayerFactory().CreatePlayers(firstPlayerName,secondPlayerName,this.TablaBoard).ToList();
          //}