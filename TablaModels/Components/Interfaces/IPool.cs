namespace TablaModels.Components.Interfaces
{
     using TablaModels.Enums;

     public interface IPool
     {
          int IdentityNumber { get; }

          int OuterPoolDiameter { get; }

          int InnerPoolDiameter { get; }

          PoolColor PoolColor { get; }

          PoolState State { get; set; }
     }
}
