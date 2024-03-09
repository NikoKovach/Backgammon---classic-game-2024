namespace TablaGameLogic.Services.Contracts
{
     using TablaGameLogic.Core.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;
    using TablaModels.ComponentModels.Enums;

     public interface IMotionValidation
     {
          bool MoveIsValid(IMoveParameters motion,IBoard gameBoard, IPlayer currentPlayer);

          //bool CurrentPlayerHasNoMoves();

          //bool HasNoOtherMoves();     
     }
}

          //IBoard Board { get; }

          //public IPlayer Player { get; }

          //public PoolColor CurrentColor { get; }