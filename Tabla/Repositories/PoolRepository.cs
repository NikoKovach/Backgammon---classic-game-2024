namespace Tabla.Repositories
{

    using System.Collections.Generic;
    using System.Linq;

    using Tabla.Repositories.Contracts;
    using Tabla.Enums;
    using Tabla.Model.Interfaces;

    public class PoolRepository : IPoolRepository
    {
        private readonly IList<IPool> poolsForFirstPlayer;

        private readonly IList<IPool> poolsForSecondPlayer;

        public PoolRepository()
        {
            this.poolsForFirstPlayer = new List<IPool>();
            this.poolsForSecondPlayer = new List<IPool>();
        }

        public IList<IPool> PoolsForFirstPlayer
        {
            get
            {
                return new List<IPool>(poolsForFirstPlayer);
            }
        }

        public IList<IPool> PoolsForSecondPlayer
        {
            get
            {
                return new List<IPool>(poolsForSecondPlayer);
            }
        }

        public void AddPool(IPool pool)
        {
            int idPool = pool.IdentityNumber;

            if (pool.Color == Color.White)
            {
                if (!this.PoolsForFirstPlayer.Any(x=>x.IdentityNumber == pool.IdentityNumber))
                {
                    this.poolsForFirstPlayer.Add(pool);
                }
            }
            else if (pool.Color == Color.Black)
            {
                if (!this.PoolsForSecondPlayer.Any(x => x.IdentityNumber == pool.IdentityNumber))
                {
                    this.poolsForSecondPlayer.Add( pool);
                }
            }
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    }
}
