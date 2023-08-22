namespace TablaGameLogic.Factory.Contracts
{
    using TablaGameLogic.Services.Contracts;
    using TablaModels.ComponentModels.Components.Interfaces;

    public interface IMotionValidateFactory
    {
        IMotionValidation Create(IBoard board,IPlayer currentPlayer);
    }
}
