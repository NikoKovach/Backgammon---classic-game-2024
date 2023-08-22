
namespace Tabla.GameLogic
{
    using System.Collections.Generic;
    using Tabla.Enums;
    using Tabla.GameLogic.Contracts;
    using Tabla.Model.Interfaces;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.ServicesFolder;
    using Tabla.TablaFolder.ContractTabla;

    public class CanMoveChipCommand : BaseLogicClass, ICommandGameLogic, IBaseLogic
    {
        public CanMoveChipCommand(IPlayer player, IList<int> moveParametersList, ITabla tabla)
            :base( player,  moveParametersList, tabla)
        {
        }
        
        public  void Move()
        {
            int columnIndex = this.MoveParametersList[0]-1;
            int moveTimes = this.MoveParametersList[1];
            IPool movedPool = this.Tabla.ColsRepository.Columns[columnIndex].RemovePool();

            int targetColumnIndex = int.MinValue;

            if (this.CurrentPlayer.Color == Color.White)
            {
                targetColumnIndex = columnIndex + moveTimes;
            }
            else
            {
                targetColumnIndex = columnIndex - moveTimes;
            }

            int countOfPoolsInTargetColumn  = GameLogicSupportClass
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
