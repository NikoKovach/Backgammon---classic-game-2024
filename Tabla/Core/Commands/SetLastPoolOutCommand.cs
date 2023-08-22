namespace Tabla.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Tabla.Core.Commands.Contracts;
    using Tabla.Enums;
    using Tabla.Model.Interfaces;
    using Tabla.PlayerFolder;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.Repositories;
    using Tabla.Repositories.Contracts;

    public class SetLastPoolOutCommand : ICommand
    {
        private readonly Dictionary<int, int> whitePoolsPerColumn;
        private readonly Dictionary<int, int> blackPoolsPerColumn;


        public SetLastPoolOutCommand(IColumnRepository cols, IPoolRepository pools, IPlayerRepository players)
        {
            this.Columns = cols;
            this.PoolsStore = pools;
            this.Players = players;

            this.whitePoolsPerColumn = new Dictionary<int, int>()
            { {22,1}, {23,1} };

            this.blackPoolsPerColumn = new Dictionary<int, int>()
            { {2,1},{0,1}  };
        }

        private IColumnRepository Columns { get;  set; }

        private IPoolRepository PoolsStore { get; set; }

        private IPlayerRepository Players { get; set; }

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

                IPlayer whitePlayer= this.Players.Players
                    .First(z=>z.Color == Color.White);
                for (int i = 2; i < whitePools.Count; i++)
                {
                    whitePlayer.AddToOutList(whitePools[i]);
                }

                IPlayer blackPlayer = this.Players.Players
                    .First(z => z.Color == Color.Black);
                for (int i = 2; i < blackPools.Count; i++)
                {
                    blackPlayer.AddToOutList(blackPools[i]);
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
