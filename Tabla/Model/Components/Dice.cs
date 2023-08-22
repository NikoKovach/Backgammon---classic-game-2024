namespace Tabla.Model.Components
{
    using System;
    
    using Tabla.Model.Interfaces;
    using Tabla.ServicesFolder;

    public class Dice : IDice
    {
        private const int MinDiceNumber = 1;
        private const int MaxDiceNumber = 6;
        
        private string name;
        private readonly Random random;

        public Dice(string name,int seedNumber)
        {
            this.Name = name;

            random = new Random(seedNumber);
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value == null || value == string.Empty)
                {
                    throw new ArgumentException("The number can take values between 1 and 6");
                }
                this.name = value;
            }
        }

        public int Roll()
        {
           int  number = this.random.Next(MinDiceNumber, MaxDiceNumber + 1);
           return number;
        }
    }
}
