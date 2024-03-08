using System;
using System.Collections.Generic;
using System.Text;
using TablaModels.ComponentModels.Components.Interfaces;

namespace TablaGameLogic.Services.Contracts
{
     public interface IMoveService
     {
          KeyValuePair<string, int[]> ParseMove( string moveString);

          public object[] GetInvokeMethodParameters( string moveMethod, int[] moveParams, IBoard board, IPlayer player );

          void InvokeMoveMethod( string moveType, object[] moveParams, IPlayer CurrentPlayer );
     }
}
