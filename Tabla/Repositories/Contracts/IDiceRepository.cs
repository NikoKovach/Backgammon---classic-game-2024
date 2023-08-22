namespace Tabla.Repositories.Contracts
{
    using System.Collections.Generic;

    using Tabla.Model.Interfaces;

    public interface IDiceRepository
    {
        Dictionary<string, IDice> DiceDictionary { get; }

        void AddDice(IDice dice);
    }
}
