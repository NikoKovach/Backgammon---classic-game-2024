namespace TablaGameLogic.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TablaGameLogic.Services.Contracts;
    using TablaModels.ComponentModels.Components.Interfaces;
    using TablaModels.ComponentModels.Enums;

    public class MoveValidate:MotionValidate,IMotionValidation
    {
        public MoveValidate(IBoard gameBoard, IPlayer currentPlayer) : base(gameBoard, currentPlayer)
        { }

        public override bool IsValidMove(params int[] moveParams)
        {
            // two parameters -> <<column number>> <<places to move>>

            if (!CheckPametersCountForMove(moveParams))
            {
                return false;
            }

            int startColumn = moveParams[0] ;
            int positions = moveParams[1];
            int numberOfMotions = moveParams[2];

            int targetColumn = (this.CurrentColor == PoolColor.Black)? (startColumn + positions) : (startColumn - positions);

            //Check that position is valid => dice1.value = dice2.value            
            if (!PositionsAreValid(positions, numberOfMotions))
            {
                return false;
            }

            //we check whether the target column is part of board
            if (!base.CheckForValidTargetColumn(targetColumn))
            {               
                return false;
            }

            //There are checkers with state 'OnTheBar'
            if (!base.HaveCheckersWithStateOnTheBar())
            {
                return false;
            }

            //Check whether the start columt is valid
            if (!base.StartingColumnIsValid(startColumn))
            {
                return false;
            }

            //we check whether the target column is free or not for pushing checker
            if (!base.TheTargetColumnIsOpen(targetColumn))
            {
                return false;
            }

            //Checking for a free path to the target column
            if (!ThereIsFreeWayToTargetColumn(numberOfMotions,targetColumn))
            {
                return false;
            }

            return true;
        }

        private bool PositionsAreValid(int positions, int numberOfMotions)
        {
            Dictionary<int,int> kvpDicesCountOfMoves = this.Board.ValueOfDiceAndCountOfMoves
                                                      .Where(x => x.Value > 0)
                                                      .ToDictionary(x => x.Key, x => x.Value);

            if (numberOfMotions == 1 && !kvpDicesCountOfMoves.ContainsKey(positions))
            {
                return false;
            }

            int KeysSum = kvpDicesCountOfMoves.Select(x => x.Key).Sum();

            if (numberOfMotions == 2 && kvpDicesCountOfMoves.Count == 2 && positions != KeysSum)
            {
                return false;
            }

            if (numberOfMotions > 1 && (kvpDicesCountOfMoves.Count == 1) && (positions % kvpDicesCountOfMoves.First().Key != 0))
            {
                return false;
            }

            return true;
        }

        protected bool CheckPametersCountForMove(int[] moveParams)
        {
            if (moveParams.Length != 3)
            {
                return false;
            }

            return true;
        }

        private bool ThereIsFreeWayToTargetColumn(int numberOfMotions,int targetColumn)
        {
            //Checking for a free path to the target column
            // for 1 motiion
            if (numberOfMotions == 1)
            {
                if (!base.TheTargetColumnIsOpen(targetColumn))
                {
                    return false;
                }
            }

            // for 2 motiion
            if (numberOfMotions == 2)
            {
                if (!ForTwoMoves(targetColumn))
                {
                    return false;
                }
            }

            // for > 2 motiion
            if (numberOfMotions > 2)
            {
                if (!ForMoreThenTwoMoves(numberOfMotions, targetColumn))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ForTwoMoves(int targetColumn)
        {
            List<bool> validMovesList = new List<bool>();

            foreach (var dice in this.Board.DiceSet.Values)
            {
                int previousColumn = (this.CurrentColor == PoolColor.Black) ? (targetColumn - dice.ValueOfOneDice) : (targetColumn + dice.ValueOfOneDice);

                validMovesList.Add(TheTargetColumnIsOpen(previousColumn));
            }

            if (!validMovesList.Exists(x => x == true))
            {
                return false;
            }

            return true;
        }

        private bool ForMoreThenTwoMoves(int numberOfMotions, int targetColumn)
        {
            List<bool> validMovesList = new List<bool>();
            int diceNumber = this.Board.DiceSet[1].ValueOfOneDice;

            int previousColumn = (this.CurrentColor == PoolColor.Black) ? (targetColumn - diceNumber) : (targetColumn + diceNumber);

            for (int i = 1; i < numberOfMotions; i++)
            {
                previousColumn = (this.CurrentColor == PoolColor.Black) ? (previousColumn - diceNumber) : (previousColumn + diceNumber);

                validMovesList.Add(TheTargetColumnIsOpen(previousColumn));
            }          

            if (validMovesList.Exists(x => x == false))
            {
                return false;
            }

            return true;
        }

        
    }
}
