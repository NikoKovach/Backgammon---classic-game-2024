namespace TablaGameLogic.Services.Contracts
{
    using TablaModels.Components.Interfaces;

    public interface IHasMoves
     {
          bool HasMoves( IBoard board, IPlayer player );
     }
}
