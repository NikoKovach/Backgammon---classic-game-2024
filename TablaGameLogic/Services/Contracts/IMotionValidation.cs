namespace TablaGameLogic.Services.Contracts
{
    using TablaModels.ComponentModels.Components.Interfaces;
    using TablaModels.ComponentModels.Enums;

    public interface IMotionValidation
    {
        IBoard Board { get; }

        public IPlayer Player { get; }

        public PoolColor CurrentColor { get; }

        bool CurrentPlayerHasNoMoves();

        bool HasNoOtherMoves();

        bool IsValidMove(int[] moveParams);
    }
}
