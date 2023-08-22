namespace Tabla.ServicesFolder
{
    using System.Collections.Generic;
    using System.Linq;

    using Tabla.Enums;
    using Tabla.GameLogic.Contracts;
    using Tabla.Model.Interfaces;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.TablaFolder.ContractTabla;

    public static class GameLogicSupportClass
    {
        public static int CountOfPoolsInTargetColumn(ITabla tabla, int colIndex)
        {
            int count = tabla.ColsRepository
                .Columns[colIndex]
                .PoolStack
                .Count;
            return count;

        }

        public static string ColorOfTargetColumn(ITabla tabla, int colIndex)
        {
            string columnColor = tabla.ColsRepository
                                     .Columns[colIndex]
                                     .GetPool()
                                     .Color
                                     .ToString();
            return columnColor;
        }

        public static void ActionWithPool(IPool movedPool, int countOfPoolsInColumn,string colorOfTargetColumn, int targetColumnIndex, ITabla tabla,IPlayer currentPlayer )
        {
            if (countOfPoolsInColumn == 0 ||
                (countOfPoolsInColumn > 0
                && colorOfTargetColumn == currentPlayer.Color.ToString()))
            {
                tabla.ColsRepository.Columns[targetColumnIndex].AddPool(movedPool);
            }
            else if (countOfPoolsInColumn == 1
                && colorOfTargetColumn != currentPlayer.Color.ToString())
            {
                IPool bitenPool = tabla.ColsRepository
                    .Columns[targetColumnIndex]
                    .RemovePool();

                int currentPlayerIndex = tabla
                    .PlayersRepository
                    .Players
                    .IndexOf(currentPlayer);

                int otherPlayerIndex = GetPlayerOnTheMove(currentPlayerIndex,
                                        tabla.PlayersRepository.Players);

                tabla.PlayersRepository.Players[otherPlayerIndex].AddToBitenList(bitenPool);
                
                tabla.ColsRepository.Columns[targetColumnIndex].AddPool(movedPool);
            }

        }

        private  static int GetPlayerOnTheMove(int indexLastPlayed, IList<IPlayer> players)
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

        public static int FindRightColumnWithPools(IPlayer player, int diceNumber, ITabla tabla)
        {
            int targetColumnIndex = int.MinValue;

            if (player.Color == Color.White)
            {
                int takeColsNumber = 6 - TableGlobalConstants.ColumnNumber - diceNumber;
                targetColumnIndex = TableGlobalConstants.ColumnNumber - diceNumber;
                tabla.ColsRepository.Columns
                    .Skip(TableGlobalConstants.ColumnNumber - 7)
                    .Take(takeColsNumber);
            }
            else
            {
                targetColumnIndex = diceNumber - 1;
            }

            return 1;
        }

        public static IList<int> DiceNumbersToList(IDiceNumbers numbers)
        {
            List<int> diceNumbersList = new List<int>();

            diceNumbersList.Add(numbers.NumberOne);
            diceNumbersList.Add(numbers.NumberTwo);

            return diceNumbersList;
        }

        public static IList<IColumn> PlayerColumnsList(IPlayer player,  ITabla tabla)
        {
            IList<IColumn> columnsList = tabla
                                        .ColsRepository
                                        .Columns
                                        .Where(x=>x.PoolStack.Count > 0 
                                            && x.GetPool().Color == player.Color)
                                        .ToList();

            return columnsList;
        }

        public static IList<IColumn> PlayerColumnsListOut(IPlayer player, ITabla tabla)
        {
            IList<IColumn> columnsList = new List<IColumn>();

            if (player.Color == Color.White)
            {
                columnsList = tabla.ColsRepository
                                        .Columns
                                        .Skip(18)
                                        .ToList();
            }
            else if (player.Color == Color.Black)
            {
                columnsList = tabla.ColsRepository
                                       .Columns
                                       .Take(6)
                                       .ToList();
            }

            if (player.Color == Color.Black)
            {
                return columnsList.Reverse().ToList();
            }

            return columnsList;
        }

    }
}
