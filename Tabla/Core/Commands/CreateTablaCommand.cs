namespace Tabla.Core.Commands
{
    using Tabla.Core.Commands.Contracts;
    using Tabla.TablaFolder;
    using Tabla.TablaFolder.ContractTabla;

    class CreateTablaCommand : ITablaCommand
    {
        public ITabla Execute()
        {
            TablaTwoPlayers tablaForTwo = new TablaTwoPlayers();
            tablaForTwo.CreateTable();
            tablaForTwo.SetPoolsOnTabla();
            tablaForTwo.SetPoolsToPlayer();

            return tablaForTwo;
        }
    }
}
