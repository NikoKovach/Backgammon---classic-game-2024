namespace TablaGameLogic.Core
{
     using System;
     using System.Linq;
     using System.Collections.Generic;

     using TablaGameLogic.Core.Contracts;
     using TablaGameLogic.Factory;
     using TablaGameLogic.Services;
     using TablaGameLogic.Services.Contracts;
     using TablaGameLogic.Exeptions;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums; 
     using TablaModels.ComponentModels.Components.Players;

     using static TablaGameLogic.Utilities.Messages.ExceptionMessages;
     using static TablaGameLogic.Utilities.Messages.OutputMessages;
     using static TablaGameLogic.Services.ServiceCalculate;
     using static TablaGameLogic.Utilities.Messages.GameConstants;

     public class Controller : IController
     {        
          private static IBoard       defaultTablaBoard  = CreateDefaultBoard();
          private static IMoveService defaultMoveService = GetMoveService();

          private readonly IBoard   tablaBoard;
          private IList<IPlayer>    players;
          private readonly IMoveService moveService;

          public Controller() :this(defaultTablaBoard,defaultMoveService){ }

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
          }

          public IBoard TablaBoard => this.tablaBoard;

          public IList<IPlayer> Players 
          {
               get => this.players;
               set => this.players = value; 
          } 
    
          public IPlayer CurrentPlayer { get; set; }

          public int CurrentPlayerMovesNumber => this.TablaBoard.DiceValueAndMovesCount.Values.Sum();

          public IMoveParameters MoveParams { get; set; }

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

               //if (this.MotionValidate.CurrentPlayerHasNoMoves())
               //{
                    //this.ChangeCurrentPlayer();
                    //return false;
               //}

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
                    this.MoveParams = new MoveParameters();

                    this.moveService.ParseMove(moveString,this.MoveParams);

                    CalculateUseDiceMotionCount( this.MoveParams,this.CurrentPlayer.MyPoolsColor, this.TablaBoard);

                    bool moveIsValid = moveService.MoveIsValid( MoveParams, TablaBoard, CurrentPlayer );

                    //bool moveIsValid = true;

                    if ( !moveIsValid )
                    {
                         return InvalidMove;
                    }

                    object[] invokeMethodParams = this.moveService.GenerateInvokeMethodParameters( this.MoveParams,this.TablaBoard, this.CurrentPlayer );

                    this.moveService.InvokeMoveMethod(this.MoveParams.MoveMethodName,invokeMethodParams,this.CurrentPlayer);

                    ChangeDiceValueAndMoveCount( this.MoveParams, this.TablaBoard );
               
                    GameHasAWinner();

                    this.MoveParams = default;

                    return MoveIsMade;
               }
               catch (  ValidateException validateEx)
               {
                    return validateEx.Message;
               }
               catch ( InvalidOperationException invalidEx)
               {
                    return invalidEx.Message;
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

          private static IBoard CreateDefaultBoard()
          {
               return new BoardFactory().Create();
          }          
          
          private static IMoveService GetMoveService()
          {
               return new ServicesMotion();
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

         //public IValidateService SetUpMoveValidation()
          //{
          //     return new ServiceValidate();
          //}

