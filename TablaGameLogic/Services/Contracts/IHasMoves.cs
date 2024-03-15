namespace TablaGameLogic.Services.Contracts
{
     using TablaModels.ComponentModels.Components.Interfaces;

     public interface IHasMoves
     {
          bool HasMoves( IBoard board, IPlayer player );
     }
}
