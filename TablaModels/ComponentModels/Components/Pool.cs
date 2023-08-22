namespace TablaModels.ComponentModels.Components
{
    using System;

    using TablaModels.ComponentModels.Components.Interfaces;
    using TablaModels.ComponentModels.Enums;
    using static TablaModels.ModelsUtilities.Messages.ExceptionMessages;

    public class Pool : IPool
    {
        private int identityNumber;

        public Pool() : this(PoolColor.White,PoolState.Starting,1)
        {
        }
        
        public Pool(PoolColor color, PoolState poolState, int idNumber)  
            : this(color,poolState,idNumber,new BoardSettings().OuterPoolDiameter, new BoardSettings().InnerPoolDiameter)
        {
        }

        public Pool(PoolColor color,PoolState poolState,int idNumber,int outerDiameter,int innerDiameter)
        {
            this.State = poolState;

            this.PoolColor = color;

            this.IdentityNumber = idNumber;

            this.OuterPoolDiameter = outerDiameter;

            this.InnerPoolDiameter = innerDiameter;
        }

        public int IdentityNumber
        {
            get => this.identityNumber;

            private set
            {
                if (value < TableGlobalConstants.MinPoolsNumber || value > TableGlobalConstants.MaxPoolsNumber)
                {
                    throw new ArgumentException(InvalidPoolId);
                }

                this.identityNumber = value;
            }

        }

        public int OuterPoolDiameter { get; set; }

        public int InnerPoolDiameter { get; set; }

        public PoolColor PoolColor { get; set; }

        public PoolState State { get ; set; }

    }
}
