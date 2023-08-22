
namespace Tabla.GameLogic
{
    using System.Collections.Generic;

    using Tabla.GameLogic.Contracts;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.TablaFolder.ContractTabla;

    public class BaseLogicClass : IBaseLogic
    {
        private readonly IPlayer currentPlayer;
        private readonly IList<int> moveParametersList;
        private readonly ITabla tabla;

        public BaseLogicClass(IPlayer player, IList<int> moveParametersList, ITabla tabla)
        {
            this.currentPlayer = player;
            this.moveParametersList = moveParametersList;
            this.tabla = tabla;
        }

        public IPlayer CurrentPlayer 
        {
            get { return this.currentPlayer; }
        }

        public ITabla Tabla 
        {
            get { return this.tabla; }
        }

        public IList<int> MoveParametersList
        {
            get { return this.moveParametersList; }
        }
    }
}
