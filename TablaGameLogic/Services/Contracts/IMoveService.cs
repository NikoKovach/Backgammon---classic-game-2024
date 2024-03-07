using System;
using System.Collections.Generic;
using System.Text;
using TablaModels.ComponentModels.Components.Interfaces;

namespace TablaGameLogic.Services.Contracts
{
     public interface IMoveService
     {
          KeyValuePair<string, object[]> ParseMove( string moveString,
               IBoard board, IPlayer player );

          void InvokeMoveMethod( string moveType, object[] moveParams, IPlayer CurrentPlayer );
     }
}
