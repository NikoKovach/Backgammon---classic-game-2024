using TablaGameLogic.Core.Contracts;
using TablaModels.ComponentModels.Components.Interfaces;

namespace TablaGameLogic.Services.Contracts
{
     public interface IMoveValidate
     {
          bool MoveIsValid( IMoveParameters motion,IBoard board,IPlayer player );
     }
}
