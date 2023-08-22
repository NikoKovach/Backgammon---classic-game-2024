namespace TablaGameLogic.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using TablaModels.ComponentModels.Components.Interfaces;
    using TablaModels.ComponentModels.Enums;

    public static class GeneralServices 
    {
        public static int CalculateTheNumberOfMoves(int positions,IDictionary<int,int> valueOfDiceAndCountOfMoves)
        {
            int number = 0;

            var kvpDicesCountOfMoves = valueOfDiceAndCountOfMoves
                .Where(x => x.Value > 0)
                .ToDictionary(x => x.Key, x => x.Value);

            int KeysSum = kvpDicesCountOfMoves.Select(x => x.Key).Sum();

            if (kvpDicesCountOfMoves.Count == 2 && (positions < KeysSum))
            {
                number = 1;
            }

            if (kvpDicesCountOfMoves.Count == 2 && (positions == KeysSum))
            {
                number = 2;
            }

            if (kvpDicesCountOfMoves.Count == 1 && (positions >= kvpDicesCountOfMoves.First().Key) && (positions % kvpDicesCountOfMoves.First().Key == 0))
            {
                number = positions / kvpDicesCountOfMoves.First().Key;
            }

            return number;
        }

        public static void ChangeValueOfDiceAndCountOfMoves(string typeOfMove, IEnumerable<int> moveParams, PoolColor playerColor, IBoard board)
        {
            if (typeOfMove.Equals("Move"))
            {
                ForMoveMotion(moveParams, board);
            }

            if (typeOfMove.Equals("Inside"))
            {
                ForInsideMotion(moveParams, playerColor, board);
            }

            if (typeOfMove.Equals("Outside"))
            {
                ForOutsideMotion(moveParams, playerColor, board);
            }
        }

        private static void ForMoveMotion( IEnumerable<int> moveParams, IBoard board)
        {
            int positions = moveParams.ToArray()[1];
            int numberOfMotions = moveParams.ToArray()[2];

            if (numberOfMotions == 1)
            {
                board.ValueOfDiceAndCountOfMoves[positions] -= 1;
            }

            Dictionary<int, int> kvpDicesCountOfMoves = board.ValueOfDiceAndCountOfMoves
            .Where(x => x.Value > 0)
            .ToDictionary(x => x.Key, x => x.Value);

            int keysSum = kvpDicesCountOfMoves.Select(x => x.Key).Sum();

            int[] keys = kvpDicesCountOfMoves.Select(x => x.Key).ToArray();

            if (numberOfMotions == 2 && kvpDicesCountOfMoves.Count == 2 && positions == keysSum)
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    board.ValueOfDiceAndCountOfMoves[keys[i]] -= 1;
                }
            }

            if (numberOfMotions > 2 && positions > keysSum)
            {
                board.ValueOfDiceAndCountOfMoves[keysSum] -= numberOfMotions;
            }
        }

        private static void ForInsideMotion(IEnumerable<int> moveParams, PoolColor playerColor, IBoard board)
        {
            int column = moveParams.ToArray()[0];
            int positions = (playerColor == PoolColor.Black) ? column : (24 + 1 - column);

            int countOfMotions = board.ValueOfDiceAndCountOfMoves
                .Where(x => x.Key == positions )
                .Select(y=>y.Value)
                .First();

            if (countOfMotions > 0)
            {
                board.ValueOfDiceAndCountOfMoves[positions] -= 1;
            }
        }

        private static void ForOutsideMotion(IEnumerable<int> moveParams, PoolColor playerColor, IBoard board)
        {   
            int column = moveParams.ToArray()[0];

            int positions = (playerColor == PoolColor.Black) ? (24 + 1 - column) : column;

            //TODO:::
            //Белите вадят пул :пример колоната,от която се дърпа пул е №4,а зара е 5
            //колона 5 и 6 са празни
            Dictionary<int, int> kvpDicesCountOfMoves = board.ValueOfDiceAndCountOfMoves
                                                    .Where(x => x.Value > 0)
                                                    .OrderBy(y => y.Key)
                                                    .ToDictionary(x => x.Key, x => x.Value);

            bool diceExists = kvpDicesCountOfMoves.ContainsKey(positions);

            if (diceExists)
            {
                board.ValueOfDiceAndCountOfMoves[positions] -= 1;
                return;
            }

            int diseValue = kvpDicesCountOfMoves.First(x=>x.Key > positions).Key;

            board.ValueOfDiceAndCountOfMoves[positions] -= 1;
        }

    }
}
