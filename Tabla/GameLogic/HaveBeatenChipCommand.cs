namespace Tabla.GameLogic
{
    using System.Collections.Generic;
    using Tabla.Enums;
    using Tabla.GameLogic.Contracts;
    using Tabla.Model.Interfaces;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.ServicesFolder;
    using Tabla.TablaFolder.ContractTabla;

    public class HaveBeatenChipCommand : BaseLogicClass, ICommandGameLogic, IBaseLogic
    {
        public HaveBeatenChipCommand(IPlayer player, IList<int> moveParametersList, ITabla tabla)
            : base(player, moveParametersList, tabla)
        {
        }

        public void Move()
        {
            int diceNumber = this.MoveParametersList[0];
            int targetColumnIndex = int.MinValue;
            IPool movedPool = this.CurrentPlayer.RemoveFromBitenList();

            if (this.CurrentPlayer.Color == Color.White)
            {
                targetColumnIndex = diceNumber - 1;
            }
            else
            {
                targetColumnIndex = TableGlobalConstants.ColumnNumber - diceNumber;
            }

            int countOfPoolsInTargetColumn = GameLogicSupportClass
                .CountOfPoolsInTargetColumn(this.Tabla, targetColumnIndex);
                
            string colorOfTargetColumn = string.Empty;

            if (countOfPoolsInTargetColumn > 0)
            {
                colorOfTargetColumn = GameLogicSupportClass
                .ColorOfTargetColumn(this.Tabla, targetColumnIndex);
            }

            GameLogicSupportClass.ActionWithPool(movedPool,
               countOfPoolsInTargetColumn, colorOfTargetColumn,
               targetColumnIndex, this.Tabla, this.CurrentPlayer);
        }
    }

}
