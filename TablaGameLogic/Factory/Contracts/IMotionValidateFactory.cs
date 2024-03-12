namespace TablaGameLogic.Factory.Contracts
{
    using TablaGameLogic.Services.Contracts;
    using TablaModels.ComponentModels.Components.Interfaces;

    public interface IMotionValidateFactory
    {
        IValidateMove Create(IBoard board,IPlayer currentPlayer);
    }
}
