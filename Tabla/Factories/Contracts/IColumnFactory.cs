namespace Tabla.Factories.Contracts
{
    using Tabla.Model.Interfaces;

    public interface IColumnFactory
    {
        IColumn CreateColumn(int idNumber);
    }
}
