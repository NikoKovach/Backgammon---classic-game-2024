namespace TablaModels.ComponentModels.Components.Players
{
     using System;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;

     public abstract class Player : IPlayer
     {        
          public Player(string name)
          {
               this.Name = name ;

               this.State = PlayerState.NormalState;
          }

          public Player(string name,IMoveChecker moveChecker)
             :this(name)
          {
               this.Move = moveChecker;
          }

          public string Name{ get; set; }

          public PoolColor MyPoolsColor { get ; set ; }

          public PlayerState State { get ; set ; }

          public IMoveChecker Move { get; set; }

          public abstract void ArrangingTheCheckers( IBoard board);

          public int RollADice()
          {
              Random rnd = new Random();

              int number = rnd.Next
                  (TableGlobalConstants.MinDiceValue, TableGlobalConstants.MaxDiceValue + 1);

              return number;
          }      
     }
}
