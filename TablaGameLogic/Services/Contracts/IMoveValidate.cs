using TablaModels.ComponentModels.Components.Interfaces;

namespace TablaGameLogic.Services.Contracts
{
     public interface IMoveValidate
     {
          bool MoveIsValid( string moveType, int[] parameters, IBoard board, IPlayer CurrentPlayer );
     }
}
