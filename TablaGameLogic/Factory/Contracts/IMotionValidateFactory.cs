namespace TablaGameLogic.Factory.Contracts
{
    using TablaGameLogic.Services.Contracts;
    using TablaModels.Components.Interfaces;

    public interface IMotionValidateFactory
    {
        IValidateMove Create(IBoard board,IPlayer currentPlayer);
    }
}
