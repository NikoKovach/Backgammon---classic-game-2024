namespace Tabla.Model.Interfaces
{
    public interface IDice
    {
        string Name { get; }

        int Roll();
    }
}
