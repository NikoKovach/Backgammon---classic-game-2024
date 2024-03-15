namespace TablaGameLogic.Services
{
     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;

     public class MoveCombinations : IMoveCombinations
     {
          public int InsideMoveCount { get; set; }

          public int MotionMoveCount { get; set; }

          public int OutsideMoveCount { get; set; }

          public bool HasAnyMove(IBoard board,IPlayer player)
          {
               return default;
          }
     }
}
