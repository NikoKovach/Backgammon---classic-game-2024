
namespace Tabla.Core.Commands
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Tabla.Core.Commands.Contracts;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.Repositories.Contracts;
    using Tabla.ServicesFolder;
    using Tabla.Enums;
    using Tabla.Factories.Contracts;

    public class CreatePlayerCommand : ICommand
    {
        private const string EnterPlayerName = "Player {0} enter your name : ";
        private const string EnterPlayerNewName = "Player {0} enter new name : ";
        private const string WrongPlayerNameMessage = "Name must contains at least 3 characters.";
       private const string PlayerChooseAColor = "Player {0}  choose a color (0 = white Or 1 = black) :";

        private IPlayerRepository twoPlayersRepository;
        private IPlayerFactory playerFactory;

        public CreatePlayerCommand(IPlayerRepository players,IPlayerFactory factoryOfPlayer)
        {
            this.TwoPlayersRepository = players;
            this.PlayerFactory = factoryOfPlayer;
        }

        public IPlayerRepository TwoPlayersRepository
        {
            get { return this.twoPlayersRepository; }
            set
            {
                GlobalValidateClass.NullArgumentValidate(value);
                this.twoPlayersRepository = value;
            }
        }

        public IPlayerFactory PlayerFactory
        {
            get { return this.playerFactory; }
            set
            {
                GlobalValidateClass.NullArgumentValidate(value);
                this.playerFactory = value;
            }
        }

        public void Execute()
        {
            SetPlayers();
            //SetRealPlayers();
            
        }

        private void SetPlayers()
        {
            string playerOneName = "BOKO";
            string playerTwoName = "Choko";

            this.TwoPlayersRepository.AddPlayer(this.PlayerFactory
                        .CreatePlayer(playerOneName, Color.White));
            this.TwoPlayersRepository.AddPlayer(this.PlayerFactory
                        .CreatePlayer(playerTwoName, Color.Black));
        }

        private void SetRealPlayers()
        {
            try
            {
                string playerName = string.Empty;
                Color currentColor;
                int color = -1;


                for (int i = 1; i <= TableGlobalConstants.NumberOfPlayersStandartTabla; i++)
                {
                    Console.Clear();
                    ConsoleHelpers.CenterCursorOnConsole(EnterPlayerName.Length);
                    Console.Write(EnterPlayerName, i);
                    playerName = Console.ReadLine();
                    Console.Clear();

                    while (true)
                    {
                        if (playerName.Length >= 3)
                        {
                            break;
                        }
                        //Console.Clear();
                        ConsoleHelpers.CenterCursorOnConsole(WrongPlayerNameMessage.Length);
                        Console.WriteLine(WrongPlayerNameMessage);
                        Console.WriteLine();//Console.Clear();
                        ConsoleHelpers.CenterCursorOnConsole(EnterPlayerNewName.Length);
                        Console.Write(EnterPlayerNewName, i);
                        playerName = Console.ReadLine();
                    }

                    if (i == 1)
                    {
                        Console.Clear();
                        ConsoleHelpers.CenterCursorOnConsole(PlayerChooseAColor.Length);
                        Console.Write(PlayerChooseAColor, i);
                        //Console.Write(" (0 = white Or 1 = black) : ");
                        color = int.Parse(Console.ReadLine());
                    }
                    else
                    {
                        if (color == 0)
                        {
                            color = 1;
                        }
                        else { color = 0; }
                    }
                    currentColor = (Color)color;

                    IPlayer currentPlayer = this.PlayerFactory
                        .CreatePlayer(playerName, currentColor);

                    this.TwoPlayersRepository.AddPlayer(currentPlayer);

                    //if (this.TwoPlayersRepository.Players.Any(y => y.Name == currentPlayer.Name))
                    //{

                    //}
                }             
            }
            catch (Exception ioe)
            {
                Console.WriteLine(ioe.Message);
                //return false;
            };
        }

    }
}
