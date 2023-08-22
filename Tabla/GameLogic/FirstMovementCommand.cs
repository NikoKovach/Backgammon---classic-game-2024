
namespace Tabla.GameLogic
{
    using System.Collections.Generic;
    using System.Linq;

    using Tabla.GameLogic.Contracts;
    using Tabla.InputProvider.Contract;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.ServicesFolder;
    using Tabla.TablaFolder.ContractTabla;

    public class FirstMovementCommand : IFirstMovement
    {
        private ITabla tabla;
        private IInputProvider inputProvider;

        public FirstMovementCommand(ITabla currentTabla,IInputProvider provider)
        {
            this.tabla = currentTabla;
            this.inputProvider = provider;
        }

        public ITabla Tabla 
        {
            get
            { 
                return this.tabla; 
            }
        }

        public IInputProvider InputProvider 
        { 
            get
            {
                return this.inputProvider;
            }
        }

        public int WhoStartFirst()
        {
            //List<int> numbersFirstRoll = new List<int>();
            IDiceNumbers numbersFirstRoll = GetFirstRoll();

            IList<IPlayer> players = this.Tabla.PlayersRepository.Players;
            IPlayer startFirstPlayer = null;

            while (true)
            {
                if (numbersFirstRoll.NumberOne != numbersFirstRoll.NumberTwo)
                {
                    break;
                }

                this.InputProvider.SameDiceNumbers(this.Tabla);

                numbersFirstRoll = GetFirstRoll();
            }

            if (numbersFirstRoll.NumberTwo > numbersFirstRoll.NumberOne)
            {
                startFirstPlayer = players.Last();
            }
            else
            {
                startFirstPlayer = players.First();
            }

            //TODO : Renderer  startFirstPlayer.Name START FIRST !
            
            //TODO : Execute for first time CanMoveChipCommand

            for (int i = 1; i <= TableGlobalConstants.DiceNumber; i++)
            {
                IList<int> moveParametersList = this.InputProvider
                .CanMoveChipInputCommand(startFirstPlayer, numbersFirstRoll,
                                                this.Tabla.ColsRepository);

                CanMoveChipCommand firstMovement = new CanMoveChipCommand
                    (startFirstPlayer, moveParametersList, this.Tabla);
                firstMovement.Move();
                //TODO : Renderer movement
            }
            
            int index = players.IndexOf(startFirstPlayer);
            return index;  
        }

        private IDiceNumbers GetFirstRoll()
        {
            IDiceNumbers rollList = new DiceNumbers(this.Tabla.DicesRepository);

            rollList.NumberOne = this.InputProvider.EveryOneRollDice(0, this.Tabla);
            //TODO : Renderer Dice 1 !
            rollList.NumberTwo = this.InputProvider.EveryOneRollDice(1, this.Tabla);
            //TODO : Renderer Dice 2 !

            return rollList;
        }

    }
}
