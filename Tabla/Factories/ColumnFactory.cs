namespace Tabla.Factories
{
    using Tabla.Factories.Contracts;
    using Tabla.Model.Components;
    using Tabla.Model.Interfaces;

    public class ColumnFactory : IColumnFactory
    {
        public ColumnFactory()
        {
        }
        public IColumn CreateColumn(int idNumber)
        {
            IColumn column = new Column(idNumber);
            return column;
        }
    }
}
