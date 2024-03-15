namespace TablaGameLogic.Services
{
     using System.Collections.Generic;
     using System.Linq;
     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;

     public class ValidateBase : IValidateMove
     {
          protected PoolColor Color { get; set; }

          protected IDictionary<int, IColumn> Columns { get; set; }

          protected IBoard Board { get; set; }

          public IMoveParameters MotionParams { get; set; }

          protected void SetBoard( IBoard board ) => this.Board = board;

          protected void SetColor(PoolColor color) => this.Color = color;

          protected void SetColumns(IDictionary<int, IColumn> columns) => 
               this.Columns = columns;

          protected void SetMotionParams(IMoveParameters motion) => this.MotionParams = motion;

//********************************************************************************
          public virtual bool MoveIsCorrect(IMoveParameters motion,IBoard board, IPlayer player)
          {
               return true;
          }

          //TODO :
          //protected bool HasMoves()
          //{
          //     //ToDO
          //     return true;
          //}

          protected bool ColumnIsPartOfTheBoard(int colNumber)
          {
               return Columns.Any( x => x.Key == colNumber );
          }

          protected bool ChipsOnTheBar()
          {
               var chipsSet = (Color == PoolColor.White) ?
                    Board.WhitePoolsSet : Board.BlackPoolsSet;

               return chipsSet.Any( x => x.State == PoolState.OnTheBar );
          }

          protected virtual bool BaseColumnIsOpen()
          {
               if ( !ColumnIsNotLock( MotionParams.ColumnNumber ) )
               {
                    return false;
               }

               return true;
          }

          protected virtual bool ColumnIsNotLock(int colNumber)
          {
               int chipsCount = (this.Columns[ colNumber ].PoolStack.Count > 1) ? 
                                this.Columns[ colNumber ].PoolStack.Count       : default;

               if ( chipsCount > 1 )
               {
                    PoolColor chipsColor = this.Columns[ colNumber ]
                                          .PoolStack
                                          .Peek().PoolColor;

                    if ( chipsColor != this.Color )
                    {
                         return false;
                    }  
               }

               return true;
          }

//****************************************************************************

          //"Please enter your move type in following format :"         + NewRow + 
          //"1.For 'Inside'  - ( 1 ) (int columnNumber) (IPool chip)   ;" + NewRow + 
          //"2.For 'Outside' - ( 2 ) (int columnNumber)                 ;" + NewRow + 
          //"3.For 'Move'    - ( 3 ) (int columnNumber) (int places to move);";         
     }
}