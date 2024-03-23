namespace TablaModels.Components.Interfaces
{
     using TablaModels.Enums;

     public interface IPlayer
     {
          string Name { get; }

          PoolColor MyPoolsColor { get; set; }

          PlayerState State { get; set; }

          IMoveChips Move { get; set; }

          void ArrangingTheCheckers(IBoard board, IArrangeChips arrangeChips);

          int RollADice();
     }
}
