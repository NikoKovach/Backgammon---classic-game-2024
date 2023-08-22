
namespace Tabla.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Tabla.PlayerFolder.Contracts;
    using Tabla.Repositories.Contracts;
    using Tabla.ServicesFolder;

    public class PlayerRepository : IPlayerRepository
    {
        private readonly IList<IPlayer> playersList;
        
        public PlayerRepository()
        {
            this.playersList = new List<IPlayer>();
        }
        
        public IList<IPlayer> Players
        {
            get 
            { 
                return new List< IPlayer>(this.playersList); 
                //return this.playerDictionary; 
            }
        }

        public void AddPlayer(IPlayer person)
        {
            GlobalValidateClass.NullArgumentValidate(person);
            if (this.Players.Count < 2 && !this.Players.Any(y => y.Name == person.Name))
            {
                this.playersList.Add(person);
            }

           
        }
    }
}
