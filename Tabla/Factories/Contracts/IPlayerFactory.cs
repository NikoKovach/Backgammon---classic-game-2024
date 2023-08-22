
namespace Tabla.Factories.Contracts
{
    using Tabla.Enums;
    using Tabla.PlayerFolder.Contracts;

    public interface IPlayerFactory
    {
        IPlayer CreatePlayer(string name,Color color);
    }
}
