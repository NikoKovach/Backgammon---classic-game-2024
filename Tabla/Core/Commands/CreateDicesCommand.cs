namespace Tabla.Core.Commands
{
    using System;

    using Tabla.Core.Commands.Contracts;
    using Tabla.Factories;
    using Tabla.Repositories.Contracts;
    using Tabla.Model.Interfaces;
    using Tabla.Repositories;
    using Tabla.ServicesFolder;
    using Tabla.Factories.Contracts;

    public class CreateDicesCommand : ICommand
    {
        private const string namePrefix = "Dice ";

        private IDiceFactory diceFactory;
        private IDiceRepository diceRepository;

        public CreateDicesCommand(DiceFactory diceFactory, IDiceRepository diceRepository)
        {
            this.DiceFactory = diceFactory;
            this.DiceRepository = diceRepository;
        }

        public IDiceFactory DiceFactory 
        {
            get
            {
                return this.diceFactory;
            }
            set
            {
                GlobalValidateClass.NullArgumentValidate(value);
                this.diceFactory = value;
            }
        }

        public IDiceRepository DiceRepository
        {
            get { return this.diceRepository; }
            set
            {
                GlobalValidateClass.NullArgumentValidate(value);
                this.diceRepository = value;
            }
        }

        public void Execute()
        {
            try
            {
                Random generateSeedRandom = new Random();
                for (int i = 1; i <= TableGlobalConstants.DiceNumber; i++)
                {
                    string diceName = namePrefix + i;
                    int seed = generateSeedRandom.Next();
                    IDice dice = this.DiceFactory.CreateDice(diceName, seed);
                    this.DiceRepository.AddDice(dice);
                }

            }
            catch (Exception cmdEx)
            {
                throw new InvalidOperationException(cmdEx.Message);
            }
        }
    }
}
