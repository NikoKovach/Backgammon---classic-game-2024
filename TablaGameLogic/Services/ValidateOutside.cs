namespace TablaGameLogic.Services
{
     using System.Linq;
     using TablaGameLogic.Core.Contracts;
     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;

     public class ValidateOutside : ValidateBase, IValidateMove
     {
          public override bool MoveIsCorrect( IMoveParameters motion,IBoard board, 
               IPlayer player)
          {
               base.SetBoard( board );
               base.SetColor(player.MyPoolsColor);
               base.SetColumns( board.ColumnSet );
               base.SetMotionParams( motion );

               if ( ChipsOnTheBar() )
               {
                    return false;
               }

               if ( ChipsInGame() )
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

               if ( !base.BaseColumnIsOpen() )
               {
                    return false;
               }

               return true;
          }

          public bool ChipsInGame()
          {
               var chipsSet = Color == PoolColor.White ?
                    Board.WhitePoolsSet : Board.BlackPoolsSet;

               return chipsSet.Any( x => x.State == PoolState.InGame );
          }

          //TODO

          /*
           ИЗКАРВАНЕ НА ПУЛОВЕ
           Ако няма пул на позиция отговаряща на зара, 
          играчът трябва да направи легално движение на пул, 
          намиращ се на по-висока позиция. 
          Ако няма пулове на по-висока позиция, 
          играчът е длъжен да извади пул намиращ се на най-високата възможна позиция, 
          по-ниска от стойността на зара. 
          Ако играчът има пул на позиция за изкарване, 
          но има и възможно движение, той не е длъжен да изкарва пула.
           */
      
     }
}
