
namespace Tabla.GameLogic.Contracts
{
    using Tabla.InputProvider.Contract;
    using Tabla.TablaFolder.ContractTabla;

    public interface IFirstMovement
    {
        ITabla Tabla{ get;}

        IInputProvider InputProvider { get; }

        int WhoStartFirst();
           
    }
}
