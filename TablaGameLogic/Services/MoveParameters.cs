namespace TablaGameLogic.Services
{
     using System.Collections.Generic;
     using TablaGameLogic.Services.Contracts;

     public class MoveParameters : IMoveParameters
     {
          public MoveParameters()
          {
              UseDiceMotionCount = new List<int>();
          }

          public string MoveMethodName { get; set; }

          public int ColumnNumber { get; set; }

          public int chipNumberOrPlaceToMove { get; set; }

          public IList<int> UseDiceMotionCount { get; set; }
     }
}
