namespace TablaGameLogic.Factory.Contracts
{
     using System.Collections.Generic;
     using TablaModels.Components.Interfaces;

     public interface IBoardFactory
     {
          IBoard Create();

          IBoard Create(IDictionary<int,IColumn> columns,
                        IDictionary<int, IDice> diceSet,
                        IList<IPool> whitePools,IList<IPool> blackPools);
     }
}
