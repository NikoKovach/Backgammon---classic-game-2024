using System.Collections;
using System.Collections.Generic;
using TablaModels.ComponentModels.Components.Interfaces;

namespace TablaGameLogic.Core.Contracts
{
    public interface IMoveParameters
    {        
          int ColumnNumber { get; set; }
        
          string MoveMethodName { get; set; }

          int chipNumberOrPlaceToMove { get; set; }

          IList<int> UseDiceMotionCount { get; set; }
    }
}