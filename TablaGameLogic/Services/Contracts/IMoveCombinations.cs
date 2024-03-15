namespace TablaGameLogic.Services.Contracts
{
     using TablaModels.ComponentModels.Components.Interfaces;

     public interface IMoveCombinations
     {
          int InsideMoveCount { get; set; }

          int MotionMoveCount { get; set; }

          int OutsideMoveCount { get; set; }

          bool HasAnyMove(IBoard board,IPlayer player);
     }
}
