namespace TablaGameLogic.Services
{
    using TablaGameLogic.Services.Contracts;
    using TablaModels.ComponentModels.Components.Interfaces;

    public class InsideValidate : MotionValidate,IMotionValidation
    {
        public InsideValidate(IBoard gameBoard, IPlayer currentPlayer) : base(gameBoard, currentPlayer)
        { }

        public override bool IsValidMove(int[] moveParams)
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

            //we check whether the target column is free or not for pushing checker
            if (!base.TheTargetColumnIsOpen(targetColNumber))
            {
                return false;
            }

            return true;
        }

        public override bool HasNoOtherMoves()
        {
            return base.CurrentPlayerHasNoMoves();
        }
    }
}
