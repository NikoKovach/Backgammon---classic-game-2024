namespace TablaModels.ComponentModels.Components.Interfaces
{
    public interface IDice
    {
        string Name { get; set; }

        int ValueOfOneDice { get; set; }

        int DiceSide { get; set; }
    }
}
