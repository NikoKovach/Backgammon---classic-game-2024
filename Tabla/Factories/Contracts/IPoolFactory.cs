namespace Tabla.Factories.Contracts
{
    using Tabla.Enums;
    using Tabla.Model.Interfaces;

    public interface IPoolFactory
    {
        IPool CreatePool(Color color, int idNumber);
    }
}
