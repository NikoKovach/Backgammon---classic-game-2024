namespace TablaGameLogic.Factory.Contracts
{
    using TablaGameLogic.Services.Contracts;
    using TablaModels.Components.Interfaces;

    public interface IServicesFactory
    {
        IGeneralServices CreateServices(IBoard gameBoard);

    }
}
