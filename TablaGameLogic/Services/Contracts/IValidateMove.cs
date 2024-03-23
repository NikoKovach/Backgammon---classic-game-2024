namespace TablaGameLogic.Services.Contracts
{
    using TablaModels.Components.Interfaces;

    public interface IValidateMove
     {
          bool MoveIsCorrect(IMoveParameters motion,IBoard gameBoard, IPlayer currentPlayer);

          string GetMoveType( IBoard gameBoard, IPlayer currentPlayer );
     }
}