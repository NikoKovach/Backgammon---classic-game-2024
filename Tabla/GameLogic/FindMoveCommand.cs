namespace Tabla.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Tabla.Enums;
    using Tabla.GameLogic.Contracts;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.TablaFolder.ContractTabla;

    public class FindMoveCommand : BaseLogicClass, IFindMoveCommand, IBaseLogic
    {
        private const string Suffix = "Command";
        private const string CurrentNameSpace = "Tabla.GameLogic.";

        public FindMoveCommand(IPlayer player, IList<int> moveParametersList, ITabla tabla)
            :base( player,  moveParametersList, tabla)
        {
        }

        public void Execute(GameCondition playerCondition)
        {
            string commandName = playerCondition.ToString() + Suffix;
            Type commandType = Assembly.GetExecutingAssembly().GetType(CurrentNameSpace + commandName);
            ICommandGameLogic moveCommandInstance = (ICommandGameLogic)Activator.CreateInstance(commandType, new object[] { this.CurrentPlayer, this.MoveParametersList, this.Tabla });

            moveCommandInstance.Move();
        }
    }
}
