namespace Tabla.Factories.Contracts
{
    using Tabla.Model.Interfaces;

    public interface IDiceFactory
    {
        IDice CreateDice(string name,int number);
    }
}
