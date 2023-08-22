namespace Tabla.InputProvider.Contract
{
    using System.Collections.Generic;

    using Tabla.GameLogic.Contracts;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.Repositories.Contracts;
    using Tabla.TablaFolder.ContractTabla;

    public interface IInputProvider
    {
        int EveryOneRollDice(int index, ITabla tabla);

        void SameDiceNumbers(ITabla tabla);
        
        IDiceNumbers RollDicesCommand(IPlayer player, IDiceRepository dices);

        IList<int> CanMoveChipInputCommand(IPlayer player, IDiceNumbers numbers, IColumnRepository colsRepository);

        IList<int> HaveBeatenChipInputCommand(IPlayer player, IDiceNumbers numbers, IColumnRepository colsRepository);

        IList<int> CanMoveOutInputCommand(IPlayer player, IDiceNumbers numbers, IColumnRepository colsRepository);

        void GameEnded(IPlayer winner);

        void CheckMovementCommand(IPlayer player);
    }
}