namespace TablaGameLogic.Services
{
    using TablaGameLogic.Services.Contracts;
    using TablaModels.ComponentModels.Components.Interfaces;

    public class OutsideValidate : MotionValidate, IMotionValidation
    {
        public OutsideValidate(IBoard gameBoard, IPlayer currentPlayer) : base(gameBoard, currentPlayer)
        { }

        public override bool MoveIsValid( int[] moveParams)
        {
            //One parameter -> <<column number = dice number>>

            if (!base.CheckPametersCountForInsideAndOutsideMove(moveParams))
            {
                return false;
            }

            int targetColNumber = moveParams[0];

            //we check whether the target column is part of board
            if (!base.CheckForValidTargetColumn(targetColNumber))
            {
                return false;
            }

            //we check whether the target column is free or not for Poping checker
            if (!TargetColumnIsOpen(targetColNumber))
            {
                return false;
            }

            //There are checkers with state 'OnTheBar'
            if (!base.HaveCheckersWithStateOnTheBar())
            {
                return false;
            }

            //There are checkers which are not in home field - > with status 'InGame'
            if (!base.ThereAreCheckersOutsideFromHomeField())
            {
                return false;
            }

            return base.MoveIsValid(moveParams);
        }

        protected bool TargetColumnIsOpen(int targetColNumber)
        {
            int countOfCheckersOnColumn = base.CountOfCheckersOnTheColumn(targetColNumber);

            if (countOfCheckersOnColumn < 1)
            {  
                return false;
            }

            if (countOfCheckersOnColumn >= 1 && !base.IsDifferentChecherColor(targetColNumber))
            {
                return false;
            }

            return true;
        }
      
    }
}
