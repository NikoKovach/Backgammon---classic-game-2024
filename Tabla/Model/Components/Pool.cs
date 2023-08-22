namespace Tabla.Components.Items
{
    using System;

    using Tabla.Enums;
    using Tabla.Model.Interfaces;
    using Tabla.ServicesFolder;

    public class Pool : IPool
    {
        private Color color;
        private PoolState state;
        private int identityNumber;

        public Pool(Color color,int idNumber)
        {
            this.State = PoolState.Inside;
            this.Color = color;
            this.IdentityNumber = idNumber;
        }

        public int IdentityNumber
        {
            get { return this.identityNumber; }
            private set
            {
                if (value < TableGlobalConstants.MinPoolsNumber || value > TableGlobalConstants.MaxPoolsNumber)
                {
                    throw new ArgumentException("Use number in the range 1 - 15");
                }
                this.identityNumber = value;
            }

        }

        public Color Color
        {
            get
            {
                return this.color;
            }
            private set
            {
                if (value != Color.White && value != Color.Black)
                {
                    throw new ArgumentException("Pool color can be only black or white!");
                }
                this.color = value;
            }
        }

        public PoolState State
        {
            get
            {
                return this.state;
            }
            private set
            {
                if (value != PoolState.Inside && value != PoolState.Outside)
                {
                    throw new ArgumentException("The Pool position can be only inside or outside!");
                }
                this.state = value;
            }
        }

    }
}
