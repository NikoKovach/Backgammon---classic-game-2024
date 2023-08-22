namespace Tabla.ServicesFolder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Tabla.Enums;
    using Tabla.GameLogic.Contracts;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.Repositories.Contracts;
    using Tabla.TablaFolder.ContractTabla;

    public static class GlobalValidateClass
    {
        public static void NullArgumentValidate(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(string.Format(TableGlobalConstants.PropertyNullExceptionMassage, obj.GetType().Name));
            }

        }

        public static void ColumnNumberValidate(int index, IPlayer player, IColumnRepository colsRepository)
        {
            if (index < 0 || index > TableGlobalConstants.ColumnNumber -1)
            {
                throw new IndexOutOfRangeException(string.Format(TableGlobalConstants.InvalidColumnNumber));
            }

            int poolsNumber=colsRepository.Columns[index].PoolStack.Count;
            Color columnColor = colsRepository.Columns[index].GetPool().Color;
            if (poolsNumber > 0 && (player.Color != columnColor))
            {
                throw new InvalidOperationException(TableGlobalConstants.InvalidChoiceOfColumn);
            }
        }

        public static void TimesToMoveValidate(IDiceNumbers diceNumbers,int number)
        {
            if (number != diceNumbers.NumberOne && number != diceNumbers.NumberTwo)
            {
                throw new Exception(string.Format(TableGlobalConstants.InvalidNumberToMove,diceNumbers.NumberOne, diceNumbers.NumberTwo));
            }
        }

        public static GameCondition CheckPlayerCondition(IPlayer player,ITabla tabla)
        {
            if (player.BitenPools.Count > 0)
            {
                return  GameCondition.HaveBeatenChip;
            }

            if (player.OutPools.Count == TableGlobalConstants.MaxPoolsNumber)
            {
                return GameCondition.Winner;
            }

            if (player.Color == Color.White)
            {
                int whitePoolsCount = tabla
                                    .ColsRepository
                                    .Columns
                                    .Take(18)
                                    .SelectMany(x => x.PoolStack)
                                    .Where(w => w.Color == Color.White).ToList()
                                    .Count;
                if (whitePoolsCount == 0)
                {
                    return GameCondition.CanMoveOut;
                }
            }
            else if (player.Color == Color.Black)
            {
                int blackPoolsCount = tabla
                                    .ColsRepository
                                    .Columns
                                    .Skip(6)
                                    .SelectMany(x => x.PoolStack)
                                    .Where(w => w.Color == Color.Black)
                                    .ToList().Count;

                if (blackPoolsCount == 0)
                {
                    return GameCondition.CanMoveOut;
                }
            }

            return GameCondition.CanMoveChip;
        }

        public static int MoveTimes(IDiceNumbers diceNumbers)
        {
            if (diceNumbers.NumberOne == diceNumbers.NumberTwo)
            {
                return TableGlobalConstants.MaxMoveTimes;
            }
            return TableGlobalConstants.MinMoveTimes;
        }

        public static int GetPlayerOnTheMove(int indexLastPlayed, IList<IPlayer> players)
        {
            int index = -1;

            if (indexLastPlayed == players.Count - 1)
            {
                index = 0;
            }
            else
            {
                index = indexLastPlayed + 1;
            }

            return index;
        }
    }
}
