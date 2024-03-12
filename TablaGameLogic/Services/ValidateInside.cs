namespace TablaGameLogic.Services
{
     using TablaGameLogic.Core.Contracts;
     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;

     public class ValidateInside : ValidateBase,IValidateMove
     {
          public override bool MoveIsCorrect(IMoveParameters motion,IBoard board, 
               IPlayer player)
          {
               base.SetBoard( board );
               base.SetColor(player.MyPoolsColor);
               base.SetColumns( board.ColumnSet );
               base.SetMotionParams( motion );

               if ( !ChipsOnTheBar() )
               {
                    return false;
               }

               //if ( !base.HasMoves())
               //{
               //     return false;
               //}

               if ( !base.ColumnIsPartOfTheBoard(this.MotionParams.ColumnNumber) )
               {
                    return false;
               }

               if ( !ChipStatusIsOnTheBar() )
               {
                    return false;
               }

               if ( !base.BaseColumnIsOpen() )
               {
                    return false;
               }

               return true;
          }

          private bool ChipStatusIsOnTheBar()
          {
               if ( this.MotionParams.Chip.State != PoolState.OnTheBar )
               {
                    return false;
               }

               return true;
          }
     }
}
