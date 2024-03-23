namespace TablaModels.Components
{
    using System;
    using TablaModels.Components.Interfaces;
    using TablaModels.Enums;
    using TablaModels.ModelsUtilities;
    using static TablaModels.ModelsUtilities.Messages.ExceptionMessages;

    public class Pool : IPool
    {
        private int identityNumber;

        public Pool() : this(PoolColor.White, PoolState.Starting, 1)
        {
        }

        public Pool(PoolColor color, PoolState poolState, int idNumber)
            : this(color, poolState, idNumber, new BoardSettings().OuterPoolDiameter, new BoardSettings().InnerPoolDiameter)
        {
        }

        public Pool(PoolColor color, PoolState poolState, int idNumber, int outerDiameter, int innerDiameter)
        {
            State = poolState;

            PoolColor = color;

            IdentityNumber = idNumber;

            OuterPoolDiameter = outerDiameter;

            InnerPoolDiameter = innerDiameter;
        }

        public int IdentityNumber
        {
            get => identityNumber;

            private set
            {
                if (value < TableGlobalConstants.MinPoolsNumber || value > TableGlobalConstants.MaxPoolsNumber)
                {
                    throw new ArgumentException(InvalidPoolId);
                }

                identityNumber = value;
            }

        }

        public int OuterPoolDiameter { get; set; }

        public int InnerPoolDiameter { get; set; }

        public PoolColor PoolColor { get; set; }

        public PoolState State { get; set; }

    }
}
