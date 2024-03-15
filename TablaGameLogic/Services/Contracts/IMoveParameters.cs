namespace TablaGameLogic.Services.Contracts
{
     using System.Collections.Generic;

     public interface IMoveParameters
     {
          int ColumnNumber { get; set; }

          string MoveMethodName { get; set; }

          int chipNumberOrPlaceToMove { get; set; }

          IList<int> UseDiceMotionCount { get; set; }
     }
}