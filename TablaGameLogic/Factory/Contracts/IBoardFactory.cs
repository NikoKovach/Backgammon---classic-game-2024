namespace TablaGameLogic.Factory.Contracts
{
    using TablaModels.ComponentModels.Components.Interfaces;

    public interface IBoardFactory
    {
        IBoard Create();
    }
}
