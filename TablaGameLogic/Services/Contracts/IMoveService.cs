using System;
using System.Collections.Generic;
using System.Text;
using TablaGameLogic.Core.Contracts;
using TablaModels.ComponentModels.Components.Interfaces;

namespace TablaGameLogic.Services.Contracts
{
     public interface IMoveService
     {
          void ParseMove( string moveString,IMoveParameters motion);

          public object[] GenerateInvokeMethodParameters( IMoveParameters motion,IBoard board,IPlayer player);

          void InvokeMoveMethod(string methodName, object[] moveParams,IPlayer CurrentPlayer );
     }
}
