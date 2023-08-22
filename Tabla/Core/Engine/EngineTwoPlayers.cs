namespace Tabla.Core.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Tabla.Core.Engine.Interfaces;
    using Tabla.TablaFolder.ContractTabla;
    using Tabla.Core.Commands;
    using Tabla.Core.Commands.Contracts;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.ServicesFolder;
    using Tabla.InputProvider.Contract;
    using Tabla.GameLogic.Contracts;
    using Tabla.GameLogic;
    using Tabla.Enums;
    using Tabla.Model.Interfaces;

    public class EngineTwoPlayers : IEngine
    {
        private ITabla tablaTwoPlayers;
        private IList<IPlayer> players;
        private IInputProvider input;

        public EngineTwoPlayers(IInputProvider inProvider)
        {
            this.input = inProvider;
        }
        public void InitiliazeTabla()
        {
            ITablaCommand createTablaForTwo = new CreateTablaCommand();
            this.tablaTwoPlayers = createTablaForTwo.Execute();
            this.players = this.tablaTwoPlayers.PlayersRepository.Players;
        }

        public void Run()
        {
            IFirstMovement gameFirstMovement = new FirstMovementCommand
                                              (this.tablaTwoPlayers, this.input);
            int indexLastPlayed = gameFirstMovement.WhoStartFirst();
            //int indexLastPlayed = 0;

            while (true)
            {
                int indexCurrentPlayer = GlobalValidateClass.GetPlayerOnTheMove(indexLastPlayed, this.players);
                
                IPlayer currentPlayer = players[indexCurrentPlayer];

                GameCondition playerCondition = GlobalValidateClass.CheckPlayerCondition(currentPlayer, this.tablaTwoPlayers);

                //Current player roll dices !!!
                IDiceNumbers numbers = this.input.RollDicesCommand(currentPlayer, this.tablaTwoPlayers.DicesRepository);
                //Renderer Dices(Print dice numbers on Console)

                bool hasAMove = CheckMovement(numbers, currentPlayer, playerCondition);
                if (hasAMove == false)
                {
                    this.input.CheckMovementCommand(currentPlayer);
                    indexLastPlayed = indexCurrentPlayer;
                    continue;
                }

                int timesToMove = GlobalValidateClass.MoveTimes(numbers);

                for (int i = 1; i <= timesToMove; i++)
                {
                    if (playerCondition != GameCondition.Winner)
                    {
                        string methodName = playerCondition.ToString() + "InputCommand";
                        MethodInfo inputMethod = input.GetType().GetMethod(methodName);
                        
                        //TODO : Check which dice number is played
                        IList<int> moveParametersList = (IList<int>)inputMethod
                            .Invoke(input, new object[] { currentPlayer, numbers,
                                this.tablaTwoPlayers.ColsRepository });                    
                        FindMoveCommand findExecuteCommand= new FindMoveCommand(currentPlayer, moveParametersList,this.tablaTwoPlayers);
                        findExecuteCommand.Execute(playerCondition);
                    }

                    //Renderer Tabla
                    playerCondition = GlobalValidateClass.CheckPlayerCondition(currentPlayer,this.tablaTwoPlayers);

                    if (playerCondition == GameCondition.Winner)
                    {
                        this.input.GameEnded(currentPlayer);
                        return;
                    }
                }

                indexLastPlayed = indexCurrentPlayer;
            }
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        private bool CheckMovement(IDiceNumbers numbers, IPlayer currentPlayer, GameCondition playerCondition)
        {
            
            IList<int> diceNumbersList = GameLogicSupportClass.DiceNumbersToList(numbers);
            HaveValidMoveCommand chekForValidMove = new HaveValidMoveCommand(currentPlayer, diceNumbersList, this.tablaTwoPlayers);
            string checkMethodName = playerCondition.ToString() + "Check";
            MethodInfo checkMethod = chekForValidMove.GetType().GetMethod(checkMethodName);

            return (bool)checkMethod.Invoke(chekForValidMove, new object[] { });
        }
    }
}


