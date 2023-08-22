
namespace Tabla.Core.Commands.Contracts
{
    using Tabla.TablaFolder.ContractTabla;

    public interface ITablaCommand
    {
        ITabla Execute();
    }
}
