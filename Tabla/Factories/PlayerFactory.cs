namespace Tabla.Factories
{
    using Tabla.Enums;
    using Tabla.Factories.Contracts;
    using Tabla.PlayerFolder;
    using Tabla.PlayerFolder.Contracts;

    public class PlayerFactory : IPlayerFactory
    { 

        public IPlayer CreatePlayer(string name, Color color)
        {
            IPlayer player = new Player(name,color);
            return player;
        }
    }
}
