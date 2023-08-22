namespace Tabla.Factories
{
    using Tabla.Components.Items;
    using Tabla.Enums;
    using Tabla.Factories.Contracts;
    using Tabla.Model.Interfaces;

    public class PoolFactory : IPoolFactory
    {

        public IPool CreatePool(Color color, int idNumber)
        {
            IPool pool = new Pool(color,idNumber);
            return pool;
        }
    }
}
