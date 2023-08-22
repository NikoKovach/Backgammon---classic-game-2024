

namespace Tabla.GameLogic
{
    using System.Linq;
    using Tabla.GameLogic.Contracts;
    using Tabla.Repositories.Contracts;

    public class DiceNumbers : IDiceNumbers
    {
        public DiceNumbers(IDiceRepository dices)
        {
            InitializeNumbers(dices);
        }

        public int NumberOne { get; set; }

        public int NumberTwo { get; set; }

        private void InitializeNumbers(IDiceRepository dices)
        {
            this.NumberOne = dices.DiceDictionary.First().Value.Roll();
            this.NumberTwo = dices.DiceDictionary.Last().Value.Roll();
        }

    }
}