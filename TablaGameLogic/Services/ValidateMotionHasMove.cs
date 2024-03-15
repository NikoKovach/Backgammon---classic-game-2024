namespace TablaGameLogic.Services
{
     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;

     public class ValidateMotionHasMove : ValidateMotion,IHasMoves
     {
          public  bool HasMoves( IBoard board,
               IPlayer player )
          {
               base.SetBoard( board );
               base.SetColor( player.MyPoolsColor );
               base.SetColumns( board.ColumnSet );

               //TODO ::::::

               return true;
          }
     }
}
