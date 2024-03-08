namespace TablaModels.ComponentModels.Components.Players
{
     using System;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;
     using static TablaModels.ModelsUtilities.Messages.ExceptionMessages;

     public class PlayerClassicGame : IPlayer
     {        
          public PlayerClassicGame(string name)
          {
               this.Name = name ;

               this.State = PlayerState.NormalState;
          }

          public PlayerClassicGame(string name,IMoveChecker moveChecker)
             :this(name)
          {
               this.Move = moveChecker;
          }

          public string Name{ get; set; }

          public PoolColor MyPoolsColor { get ; set ; }

          public PlayerState State { get ; set ; }

          public IMoveChecker Move { get; set; }

          public virtual void ArrangingTheCheckers(IBoard board,IArrangeChips arrangeChips)
          {
               if (board == null )
               {
                   throw new ArgumentNullException(string.Format(InvalidBoardParameter,nameof       (board)));
               }
               
               if (arrangeChips == null )
               {
                   throw new ArgumentNullException(string.Format(InvalidBoardParameter,nameof       (arrangeChips)));
               }

               if (this.MyPoolsColor == PoolColor.White)
               {
                    arrangeChips.ArrangeWhiteChips( board.ColumnSet, board.WhitePoolsSet );
               }
               else 
               {
                    arrangeChips.ArrangeWhiteChips( board.ColumnSet, board.WhitePoolsSet );
               };            
          }

          public int RollADice()
          {
              Random rnd = new Random();

              int number = rnd.Next
                  (TableGlobalConstants.MinDiceValue, TableGlobalConstants.MaxDiceValue + 1);

              return number;
          }      
     }
}
