namespace Tabla.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Tabla.Core.Commands.Contracts;
    using Tabla.Model.Interfaces;
    using Tabla.Repositories.Contracts;

    public class SetPoolsGoOutCommand : ICommand
    {
        private readonly Dictionary<int, int> whitePoolsPerColumn;
        private readonly Dictionary<int, int> blackPoolsPerColumn;

        public SetPoolsGoOutCommand(IColumnRepository cols, IPoolRepository pools)
        {
            this.Columns = cols;
            this.PoolsStore = pools;

            this.whitePoolsPerColumn = new Dictionary<int, int>()
            { { 18,7}, {21,5}, {22,3} };

            this.blackPoolsPerColumn = new Dictionary<int, int>()
            { {5,6}, {4,4}, {1,5} };
        }

        private IColumnRepository Columns { get;  set; }

        private IPoolRepository PoolsStore { get; set; }

        public void Execute()
        {
            List<IPool> whitePools = new List<IPool>(this.PoolsStore.PoolsForFirstPlayer);
            List<IPool> blackPools = new List<IPool>(this.PoolsStore.PoolsForSecondPlayer);

            try
            {
                int skipedElements = 0;

                foreach (var item in whitePoolsPerColumn)
                {
                    var collection = whitePools.Skip(skipedElements).Take(item.Value).ToList();
                    IColumn col = this.Columns.Columns[item.Key];

                    SetPoolsOnColumn(col, collection);
                    skipedElements = skipedElements + item.Value;
                }

                skipedElements = 0;
                foreach (var blackItem in blackPoolsPerColumn)
                {
                    var collectionBlack = blackPools.Skip(skipedElements).Take(blackItem.Value).ToList();
                    IColumn col = this.Columns.Columns[blackItem.Key];

                    SetPoolsOnColumn(col, collectionBlack);
                    skipedElements = skipedElements + blackItem.Value;
                }
            }
            catch (Exception ioe)
            {
                throw new InvalidOperationException(ioe.Message) ;
            }
            
        }

        private static void SetPoolsOnColumn(IColumn column, List<IPool> pools)
        {
            for (int i = 1; i <= pools.Count; i++)
            {
                column.AddPool(pools[i - 1]);
            }
        }
    }
}
