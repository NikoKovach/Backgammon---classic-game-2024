namespace TablaGameLogic.Factory.Contracts
{
    using TablaGameLogic.Services.Contracts;
    using TablaModels.ComponentModels.Components.Interfaces;

    public interface IServicesFactory
    {
        IGeneralServices CreateServices(IBoard gameBoard);

    }
}
