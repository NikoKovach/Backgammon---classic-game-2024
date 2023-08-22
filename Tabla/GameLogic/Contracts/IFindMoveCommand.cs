namespace Tabla.GameLogic.Contracts
{
    using Tabla.Enums;

    public interface IFindMoveCommand
    {
        void Execute(GameCondition playerCondition);
    }
}
