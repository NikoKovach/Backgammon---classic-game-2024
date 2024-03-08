namespace TablaModels.ComponentModels.Components.Interfaces
{
     using TablaModels.ComponentModels.Enums;

     public interface IPlayer
     {
          string Name { get; }

          PoolColor MyPoolsColor { get; set; }

          public PlayerState State { get; set; }

          public IMoveChecker Move { get; set; }

          void ArrangingTheCheckers(IBoard board,IArrangeChips arrangeChips);

          int RollADice();
    }
}
