namespace TablaGameLogic.Services
{
    using System.Linq;
    using TablaGameLogic.Services.Contracts;
    using TablaModels.Components.Interfaces;
    using TablaModels.Enums;

    public class ValidateInside : ValidateBase,IValidateMove
     {
          public override bool MoveIsCorrect(IMoveParameters motion,IBoard board, 
               IPlayer player)
          {
               base.SetBoard( board );
               base.SetColor(player.MyPoolsColor);
               base.SetColumns( board.ColumnSet );
               base.SetMotionParams( motion );

               if ( !ChipsOnTheBar() ) return false;

               if ( !base.ColumnIsPartOfTheBoard(this.MotionParams.ColumnNumber) ) 
                    return false;

               if ( !ChipStatusIsOnTheBar() ) return false;

               if ( !base.BaseColumnIsOpen() ) return false;

               return true;
          }

          private bool ChipStatusIsOnTheBar()
          {
               int chipNumber = this.MotionParams.chipNumberOrPlaceToMove;

               IPool chip = this.Board.BlackPoolsSet
                            .FirstOrDefault( x => x.IdentityNumber == chipNumber );

               if ( chip == null )
               {
                    return false;
               }

               if ( chip.State != PoolState.OnTheBar )
               {
                    return false;
               }

               return true;
          }
     }
}
