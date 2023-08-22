namespace TablaModels.ComponentModels.Components.Interfaces
{
    using TablaModels.ComponentModels.Enums;

    public interface IPool
    {
        int IdentityNumber { get;}

        int OuterPoolDiameter { get;}

        int InnerPoolDiameter { get;}

        PoolColor PoolColor { get;}

        PoolState State { get; set;}
    }
}
