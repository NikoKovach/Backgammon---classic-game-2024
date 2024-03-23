namespace TablaModels.Components
{
    using System;
    using TablaModels.Components.Interfaces;
    using TablaModels.ModelsUtilities;
    using static TablaModels.ModelsUtilities.Messages.ExceptionMessages;

    public class Dice : IDice
    {
        private string name;
        private int valueOfOneDice;
        private int diceSide;

        public Dice() : this("Dice1", TableGlobalConstants.MinDiceValue, new BoardSettings().DiceSide)
        {
        }

        public Dice(string name, int cubeNumber, int sideOfDice)
        {
            Name = name;

            ValueOfOneDice = cubeNumber;

            DiceSide = sideOfDice;
        }

        public string Name
        {
            get => name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(InvalDieName);
                }

                name = value;
            }
        }

        public int ValueOfOneDice
        {
            get => valueOfOneDice;

            set
            {
                if (value < TableGlobalConstants.MinDiceValue || value > TableGlobalConstants.MaxDiceValue)
                {
                    throw new ArgumentException(InvalidDieValue);
                }

                valueOfOneDice = value;
            }
        }

        public int DiceSide
        {
            get => diceSide;

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(InvalidDieSide);
                }

                diceSide = value;
            }
        }

    }
}
