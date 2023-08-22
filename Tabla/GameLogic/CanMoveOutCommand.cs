namespace Tabla.GameLogic
{
    using System.Collections.Generic;
    using Tabla.GameLogic.Contracts;
    using Tabla.Model.Interfaces;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.ServicesFolder;
    using Tabla.TablaFolder.ContractTabla;

    public class CanMoveOutCommand: BaseLogicClass, ICommandGameLogic, IBaseLogic
    {
        public CanMoveOutCommand(IPlayer player, IList<int> moveParametersList, ITabla tabla)
            : base(player, moveParametersList, tabla)
        {
        }

        public void Move()
        {         
            if (this.MoveParametersList.Count == 1)
            {
                MoovOut();
            }
            else if (this.MoveParametersList.Count == 2)
            {
                ICommandGameLogic moovPool = new CanMoveChipCommand(this.CurrentPlayer, this.MoveParametersList, this.Tabla);
                moovPool.Move();
            }
        }

        private void MoovOut()
        {
            int targetColumnIndex = this.MoveParametersList[0]-1;

            int poolsCount = GameLogicSupportClass.CountOfPoolsInTargetColumn(this.Tabla,
                                                                targetColumnIndex);

            IPool outPool = null;
            
            if (poolsCount > 0)
            {
                outPool = this.Tabla.ColsRepository.Columns[targetColumnIndex].RemovePool();
                this.CurrentPlayer.AddToOutList(outPool);
            }       
        }
    }
}
