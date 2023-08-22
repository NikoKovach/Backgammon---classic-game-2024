namespace Tabla.Factories
{
    using Tabla.Factories.Contracts;
    using Tabla.Model.Components;
    using Tabla.Model.Interfaces;

    public class DiceFactory : IDiceFactory
    {
        public DiceFactory()
        {
        }

        public IDice CreateDice(string name,int number)
        {
            IDice dice = new Dice(name,number);

            return dice;
        }
    }
}
