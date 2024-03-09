namespace TablaGameLogic.Services
{
     using TablaGameLogic.Core.Contracts;
     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;

    public class InsideValidate : MotionValidate,IMotionValidation
    {
        public InsideValidate() 
        { }

        public override bool MoveIsValid(IMoveParameters motion,IBoard gameBoard, IPlayer currentPlayer)
        {
            ////One parameter -> <<column number = dice number>>

            //if (!base.CheckPametersCountForInsideAndOutsideMove(moveParams))
            //{
            //    return false;
            //}

            //int targetColNumber = moveParams[0];

            ////we check whether the target column is part of board
            //if (!base.CheckForValidTargetColumn(targetColNumber))
            //{                
            //    return false;
            //}

            ////we check whether the target column is free or not for pushing checker
            //if (!base.TheTargetColumnIsOpen(targetColNumber))
            //{
            //    return false;
            //}

            return true;
        }

        //public override bool HasNoOtherMoves()
        //{
        //    return base.CurrentPlayerHasNoMoves();
        //}
    }
}
