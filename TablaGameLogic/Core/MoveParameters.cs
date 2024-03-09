using System.Collections;
using System.Collections.Generic;
using TablaGameLogic.Core.Contracts;
using TablaModels.ComponentModels.Components.Interfaces;

namespace TablaGameLogic.Core
{
    public class MoveParameters : IMoveParameters
     {
          public string MoveMethodName { get; set; }

          public int ColumnNumber { get; set; }

          public IPool Chip { get; set; }

          public int chipNumberOrPlaceToMove { get; set; }

          public IList<int> UseDiceMotionCount { get; set; }
     }
}
