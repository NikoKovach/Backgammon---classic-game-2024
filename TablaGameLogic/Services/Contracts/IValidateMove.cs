namespace TablaGameLogic.Services.Contracts
{
    using TablaModels.ComponentModels.Components.Interfaces;

    public interface IValidateMove
     {
          bool MoveIsCorrect(IMoveParameters motion,IBoard gameBoard, IPlayer currentPlayer);
     }
}