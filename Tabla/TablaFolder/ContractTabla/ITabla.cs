namespace Tabla.TablaFolder.ContractTabla
{
    using Tabla.Repositories;
    using Tabla.Repositories.Contracts;

    public interface ITabla
    {
        void CreateTable();

        void SetPoolsOnTabla();

        void SetPoolsToPlayer();

        IDiceRepository DicesRepository { get; }

        IPoolRepository PoolsRepository  { get; }

        IColumnRepository ColsRepository { get; }

        IPlayerRepository PlayersRepository { get; }
    }
}
