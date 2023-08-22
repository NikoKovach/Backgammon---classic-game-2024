namespace TablaModels.ComponentModels.Components
{
    using System;
    using System.Collections.Generic;

    using TablaModels.ComponentModels.Components.Interfaces;
    using TablaModels.ComponentModels.Enums;
    using static TablaModels.ModelsUtilities.Messages.ExceptionMessages;

    public class Column : IColumn
    {
        private int identityNumber;
        private Stack<IPool> poolStack;

        public Column(int idNumber, ColumnColor color) 
            : this(idNumber,color,new BoardSettings().ColumnBase, new BoardSettings().ColumnHeight)
        {
        }

        public Column(int idNumber,ColumnColor color,int colBase,int colHeight)
        {
            this.IdentityNumber = idNumber;

            this.Color = color;

            this.ColumnBase = colBase;

            this.ColumnHeight = colHeight;

            this.poolStack = new Stack<IPool>();
        }
        
        public int IdentityNumber
        {
            get => this.identityNumber; 

            private set 
            {
                if (value < 1 || value > TableGlobalConstants.ColumnNumber)
                {
                    throw new ArgumentException(InvalidColumnId);
                }

                this.identityNumber = value;
            }
        }

        public int ColumnBase { get; set; }

        public int ColumnHeight { get; set; }

        public ColumnColor Color { get; set; } // dark or light

        public Stack<IPool> PoolStack
        {
            get
            { 
                return this.poolStack;
            }
        }       
    }
}
