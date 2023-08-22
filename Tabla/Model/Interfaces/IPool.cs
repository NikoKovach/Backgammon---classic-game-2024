namespace Tabla.Model.Interfaces
{
    using Tabla.Enums;

    public interface IPool
    {
        int IdentityNumber { get; }

        Color Color { get; }

        PoolState State { get; }
    }
}
