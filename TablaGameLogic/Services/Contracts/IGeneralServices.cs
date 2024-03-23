namespace TablaGameLogic.Services.Contracts
{
    using TablaModels.Components.Interfaces;

    public interface IGeneralServices
    {
        IBoard Board { get ; }

        int CalculateTheNumberOfMoves(int positions);

        int CalculateTheNumberOfMoves(string moveType, int positions);
    }
}
