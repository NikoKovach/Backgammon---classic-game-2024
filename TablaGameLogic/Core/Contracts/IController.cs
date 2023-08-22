namespace TablaGameLogic.Core.Contracts
{
    using System.Collections.Generic;

    using TablaGameLogic.Services.Contracts;
    using TablaModels.ComponentModels.Components.Interfaces;

    public interface IController
    {
        IBoard TablaBoard { get; }

        IList<IPlayer> Players { get; }

        IPlayer CurrentPlayer { get; set; }

        int CurrentPlayerMovesNumber { get; }

        IMotionValidation MotionValidate { get;  }

        string PlayersChooseAColor(int color);

        string ArrangingTheCheckersToPlay();

        string InitialInfoCurrentPlayerMoves();

        bool RollDice();

        string CurrentPlayerMakesMove(string[] moveWithParameters);

        void CurrentPlayerFirstSet();

        void ChangeCurrentPlayer();

        void ClearBoardFromCheckers();

    }
}
