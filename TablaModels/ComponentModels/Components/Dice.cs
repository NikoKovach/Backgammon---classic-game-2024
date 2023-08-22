namespace TablaModels.ComponentModels.Components
{
    using System;

    using TablaModels.ComponentModels.Components.Interfaces;
    using static TablaModels.ModelsUtilities.Messages.ExceptionMessages;

    public class Dice :IDice
    {
        private string name;
        private int valueOfOneDice;
        private int diceSide;

        public Dice() :this("Dice1", TableGlobalConstants.MinDiceValue, new BoardSettings().DiceSide)
        {
        }

        public Dice(string name,int cubeNumber,int sideOfDice)
        {
            this.Name = name;

            this.ValueOfOneDice = cubeNumber;

            this.DiceSide = sideOfDice;
        }

        public string Name
        {
            get => this.name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(InvalDieName);
                }

                this.name = value;
            }
        }

        public int ValueOfOneDice
        {
            get => this.valueOfOneDice;

            set
            {
                if (value < TableGlobalConstants.MinDiceValue  || value > TableGlobalConstants.MaxDiceValue)
                {
                    throw new ArgumentException(InvalidDieValue);
                }

                this.valueOfOneDice = value;
            }
        }

        public int DiceSide 
        { 
            get=>this.diceSide;

            set
            {
                if (value <= 0 )
                {
                    throw new ArgumentException(InvalidDieSide);
                }

                this.diceSide = value;
            } 
        }

    }
}
