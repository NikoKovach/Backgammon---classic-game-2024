namespace Tabla.InputProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Tabla.GameLogic;
    using Tabla.GameLogic.Contracts;
    using Tabla.InputProvider.Contract;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.Repositories.Contracts;
    using Tabla.ServicesFolder;
    using Tabla.TablaFolder.ContractTabla;

    public class InProvider : IInputProvider
    {
        public void SameDiceNumbers(ITabla tabla)
        {
            string playerOne = tabla.PlayersRepository.Players[0].Name;
            string playerTwo = tabla.PlayersRepository.Players[1].Name;

            Console.Clear();
            Console.Write("Both players threw the same numbers !!!");
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        public int EveryOneRollDice(int index,ITabla tabla)
        {
            var diceList = tabla.DicesRepository.DiceDictionary.ToList();
            string playerName = tabla.PlayersRepository.Players[index].Name;
            string playerColor = tabla.PlayersRepository.Players[index].Color.ToString();

            Console.Clear();
            Console.Write("Player {0}({1} pools) roll a dice :", playerName, playerColor);
            string rollCommand = Console.ReadLine();

            while (true)
            {
                if (rollCommand.ToLower() == "roll")
                {
                    int number = diceList[index].Value.Roll();
                    return number;
                }

                Console.Write("Player {0}({1} pools) enter write command (roll): ", playerName,playerColor);
                rollCommand = Console.ReadLine();
            }
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        public IDiceNumbers RollDicesCommand(IPlayer player,IDiceRepository dices)
        {
            Console.Clear();
            Console.Write("Player {0}({1} pools) roll the dices: ",player.Name,player.Color);
            string rollCommand = Console.ReadLine();

            while (true)
            {
                if (rollCommand.ToLower() == "roll")
                {
                    DiceNumbers dicesNumbers = new DiceNumbers(dices);
                    return dicesNumbers;
                }

                Console.Write("Player {0}({1} pools) enter write command (roll): ", player.Name,player.Color);
                rollCommand = Console.ReadLine();
            }
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        public IList<int> CanMoveChipInputCommand(IPlayer player, IDiceNumbers numbers, IColumnRepository collumnsRepository)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.Write("Player {0}({1} pools)  : choose a column number and a dice number to make a move: ", player.Name,player.Color);
                    IList<int> moveList = Console.ReadLine()
                            .Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => int.Parse(x))
                            .ToList();
                    int columnIndex = moveList[0] - 1;
                    int timesToMove = moveList[1];

                    GlobalValidateClass.ColumnNumberValidate(columnIndex, player,collumnsRepository);
                    GlobalValidateClass.TimesToMoveValidate(numbers, timesToMove);

                    return moveList;
                }
                catch (IndexOutOfRangeException ore)
                {

                    Console.WriteLine(ore.Message);
                    continue;
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        public void GameEnded(IPlayer winner)
        {
            Console.Clear();
            Console.WriteLine("Player {0}({1} pools)  is winner !!!", winner.Name,winner.Color);
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        public IList<int> HaveBeatenChipInputCommand(IPlayer player, IDiceNumbers numbers, IColumnRepository colsRepository)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.Write("Player {0}({1} pools) select a column number to place your pool : ",player.Name,player.Color);

                    IList<int> colNumberList = Console.ReadLine()
                            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => int.Parse(x))
                            .ToList();
                    int columnIndex = colNumberList[0] - 1;

                    GlobalValidateClass.ColumnNumberValidate(columnIndex,
                                                player, colsRepository);

                    return colNumberList;
                }
                catch (IndexOutOfRangeException ore)
                {
                    Console.WriteLine(ore.Message);
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public IList<int> CanMoveOutInputCommand(IPlayer player, IDiceNumbers numbers, IColumnRepository colsRepository)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    //TODO : Validate movement
                    Console.WriteLine("Player {0}({1} pools) : you have two options: ",                                                         player.Name,player.Color);
                    Console.WriteLine("1.move a pool or");
                    Console.WriteLine("2.take out an pool.");
                    Console.Write("Make a choice ( move or take out a pool ): ");
                    string choice = Console.ReadLine();

                    IList<int> colNumberList = null;
                    if (choice.ToLower() == "out")
                    {
                        Console.Write("Player {0}({1} pools) select a column from which to remove a pool : ", player.Name,player.Color);
                        colNumberList = Console.ReadLine()
                           .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(x => int.Parse(x))
                           .ToList();
                        int columnIndex = colNumberList[0] - 1;
                        GlobalValidateClass.ColumnNumberValidate(columnIndex,
                                                player, colsRepository);
                        return colNumberList;
                    }
                    else if (choice.ToLower() == "move")
                    {
                        colNumberList = CanMoveChipInputCommand(player, numbers, colsRepository);
                        return colNumberList;
                    }
                }
                catch (IndexOutOfRangeException ore)
                {
                    Console.WriteLine(ore.Message);
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }

        public void CheckMovementCommand(IPlayer player)
        {
            Console.Clear();
            Console.WriteLine("Player {0}({1} pools) : You have no possible move !!!",                                                  player.Name,player.Color);
        }
    }
}
