namespace Tabla.Repositories
{
    using System.Collections.Generic;

    using Tabla.Repositories.Contracts;
    using Tabla.Model.Interfaces;

    public class DiceRepository : IDiceRepository
    {
        private readonly Dictionary<string, IDice> diceDictionary;

        public DiceRepository()
        {
            this.diceDictionary = new Dictionary<string, IDice>();
        }

        public Dictionary<string, IDice> DiceDictionary
        {
            get
            {
                return new Dictionary<string, IDice>( this.diceDictionary);
            }
        }

        public void AddDice(IDice diceAdded)
        {
            string diceName = diceAdded.Name;

            if (!this.DiceDictionary.ContainsKey(diceName))
            {
                this.diceDictionary.Add(diceName, diceAdded);
            }
        }
    }
}
