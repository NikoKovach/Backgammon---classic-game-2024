namespace TablaModels.Components
{
    using System;
    using System.Collections.Generic;
    using TablaModels.Components.Interfaces;
    using TablaModels.Enums;
    using TablaModels.ModelsUtilities;
    using static TablaModels.ModelsUtilities.Messages.ExceptionMessages;

    public class Column : IColumn
    {
        private int identityNumber;
        private Stack<IPool> poolStack;

        public Column(int idNumber, string colColor)
            : this(idNumber, colColor, new BoardSettings().ColumnBase, new BoardSettings().ColumnHeight)
        {
        }

        public Column(int idNumber, string colColor, int colBase, int colHeight)
        {
            IdentityNumber = idNumber;

            SetColumnColor(colColor);

            ColumnBase = colBase;

            ColumnHeight = colHeight;

            poolStack = new Stack<IPool>();
        }

        public int IdentityNumber
        {
            get => identityNumber;

            set
            {
                if (value < TableGlobalConstants.MinColumnNumber
                      || value > TableGlobalConstants.MaxColumnNumber)
                {
                    throw new ArgumentException(InvalidColumnId);
                }

                identityNumber = value;
            }
        }

        public int ColumnBase { get; set; }

        public int ColumnHeight { get; set; }

        public ColumnColor Color { get; private set; }

        public Stack<IPool> PoolStack
        {
            get => poolStack;
        }

        public void SetColumnColor(string color)
        {
            ColumnColor colColor;
            bool parseIsCorect =
                 Enum.TryParse(color, true, out colColor);

            if (!parseIsCorect)
            {
                throw new ArgumentException(InvalidColumnColor);
            }

            Color = colColor;
        }
    }
}
