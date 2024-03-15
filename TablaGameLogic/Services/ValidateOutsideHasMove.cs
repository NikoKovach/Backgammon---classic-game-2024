using TablaGameLogic.Services.Contracts;
using TablaModels.ComponentModels.Components.Interfaces;

namespace TablaGameLogic.Services
{
     public class ValidateOutsideHasMove : ValidateOutside, IHasMoves
     {
          public bool HasMoves( IBoard board, IPlayer player )
          {
               base.SetBoard( board );
               base.SetColor( player.MyPoolsColor );
               base.SetColumns( board.ColumnSet );

               //TODO ::::::


               return true;
          }
     }
}
