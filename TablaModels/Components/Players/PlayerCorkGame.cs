namespace TablaModels.Components.Players
{
     using System;
     using TablaModels.Components.Interfaces;
     using TablaModels.Enums;
     using static TablaModels.ModelsUtilities.Messages.ExceptionMessages;

     public class PlayerCorkGame : PlayerClassicGame, IPlayer
     {
          public PlayerCorkGame(string name)
             : base(name)
          {
          }

          public override void ArrangingTheCheckers(IBoard board, 
                                                   IArrangeChips arrangeChips)
          {
               if (board == null)
               {
                   throw new ArgumentNullException(string.Format(InvalidBoardParameter,    nameof    (board)));
               }

               if (arrangeChips == null)
               {
                   throw new ArgumentNullException(string.Format(InvalidBoardParameter,    nameof    (arrangeChips)));
               }

               if (MyPoolsColor == PoolColor.White)
               {
                    arrangeChips.ArrangeWhiteChips( board.ColumnSet, board.WhitePoolsSet );
               }
               else
               {
                    arrangeChips.ArrangeBlackChips( board.ColumnSet, board.BlackPoolsSet );
               };
          }

     }
}
