namespace Tabla.TablaFolder
{
    using Tabla.Core.Commands;
    using Tabla.Factories;
    using Tabla.Enums;
    using Tabla.Repositories;
    using Tabla.Repositories.Contracts;
    using Tabla.TablaFolder.ContractTabla;

    public class TablaTwoPlayers : ITabla
    {
        private DiceFactory dicesFactory;
        private PoolFactory poolsFactory;
        private ColumnFactory columnsFactory;
        private PlayerFactory playerFactory;

        private IDiceRepository dicesRepository;
        private IPoolRepository poolsRepository;
        private IColumnRepository colsRepository;
        private IPlayerRepository playersRepository;

        public TablaTwoPlayers()
        {
            InitiliazeFactories();
            InitiliazeRepositories();
        }

        public IDiceRepository DicesRepository
        {
            get { return this.dicesRepository; }
        }

        public IPoolRepository PoolsRepository
        {
            get { return this.poolsRepository; }
        }

        public IColumnRepository ColsRepository
        {
            get { return this.colsRepository; }
        }

        public IPlayerRepository PlayersRepository
        {
            get { return this.playersRepository; }
        }

        public void CreateTable()
        {
            CreateDicesCommand createDices = new CreateDicesCommand(this.dicesFactory, this.DicesRepository);
            CreatePoolsCommand createPools = new CreatePoolsCommand(this.poolsFactory, this.PoolsRepository);
            CreateColumns createColumns = new CreateColumns(this.columnsFactory, this.ColsRepository);
            CreatePlayerCommand createPlayers = new CreatePlayerCommand(this.PlayersRepository, playerFactory);

            createDices.Execute();
            createPools.Execute();
            createColumns.Execute();
            createPlayers.Execute();
        }

        public void SetPoolsOnTabla()
        {
            SetPoolsOnTablaCommand setPools = new SetPoolsOnTablaCommand(this.ColsRepository, this.PoolsRepository);
            setPools.Execute();

            //SetPoolsBeatAddBackCommand setBeatAndGoBackPools = new SetPoolsBeatAddBackCommand(this.ColsRepository, this.PoolsRepository, this.PlayersRepository);
            //setBeatAndGoBackPools.Execute();

            //SetPoolsGoOutCommand setPoolsOut = new SetPoolsGoOutCommand(this.ColsRepository, this.PoolsRepository);

            // setPoolsOut.Execute();

            //SetLastPoolOutCommand setLastPoolsOut = new SetLastPoolOutCommand(this.ColsRepository, this.PoolsRepository,this.PlayersRepository);

            //setLastPoolsOut.Execute();

        }

        public void SetPoolsToPlayer()
        {
            foreach (var player in this.PlayersRepository.Players)
            {
                if (player.Color == Color.White)
                {
                    player.SetMyPools(this.PoolsRepository.PoolsForFirstPlayer);
                    //player.MyPools = this.PoolsRepository.PoolsForFirstPlayer;
                }
                else
                {
                    player.SetMyPools(this.PoolsRepository.PoolsForSecondPlayer);                   
                }
            }
        }

        private void InitiliazeFactories()
        {
            this.dicesFactory = new DiceFactory();
            this.poolsFactory = new PoolFactory();
            this.columnsFactory = new ColumnFactory();
            this.playerFactory = new PlayerFactory();
        }

        private void InitiliazeRepositories()
        {
            this.dicesRepository = new DiceRepository();
            this.poolsRepository = new PoolRepository();
            this.colsRepository = new ColumnsRepository();
            this.playersRepository = new PlayerRepository();
        }
    }
}
