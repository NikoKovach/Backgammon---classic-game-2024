using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using TablaEngine.Engine.Contracts;
using TablaEngine.Engine;

using TablaGameLogic.Core;
using TablaGameLogic.Core.Contracts;

using TablaModels.ComponentModels;
using TablaModels.ComponentModels.Components.Interfaces;
using TablaModels.ComponentModels.Enums;

namespace ConsoleTabla
{
    public class Program
    {
        static void Main(string[] args)
        {
            CreateEngine();

            //Test1();
            //GameHasAWinner();
        }

        private static void Test1()
        {
            //Engine engine = new Engine();

            //engine.ChoiceOfColorByThePlayers();

            //engine.PlayersArrangeTheirCheckers();

            //IPlayer player = engine.controller.Players[1];

            //engine.controller.CurrentPlayer = player;

            //engine.controller.CreateAdditionalServices();

            //engine.controller.TablaBoard.DiceSet[1].ValueOfOneDice = 2;
            //engine.controller.TablaBoard.DiceSet[2].ValueOfOneDice = 2;

            //engine.controller.SetValueOfDiceAndCountOfMoves();

            //engine.CurrentPlayerMoves();
        }

        private static void CreateEngine()
        {
            IEngine engine = new ClassicConsoleEngine();

            try
            {
                engine.Run();
            }
            catch (ArgumentNullException argNullEx)
            {
                Console.WriteLine(argNullEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void GameHasAWinner()
        {
            IEngine engine = new ClassicConsoleEngine();

            //engine.controller.Players[0].MyPoolsColor = PoolColor.White;
            //engine.controller.Players[1].MyPoolsColor = PoolColor.Black;

            ////engine.controller.CurrentPlayer = engine.controller.Players[1];// black

            //engine.controller.CurrentPlayer = engine.controller.Players[0];//white


            //ArrangeTheWhiteCheckers(engine);

            //ArrangeTheBlackCheckers(engine);

            //engine.MainGameMethod();

        }

        private static void ArrangeTheBlackCheckers(IEngine engine)
        {
            //for (int i = 0; i < engine.controller.TablaBoard.BlackPoolsSet.Count - 1; i++)
            //{
            //    engine.controller.TablaBoard.BlackPoolsSet[i].State = PoolState.OutOfGame;
            //}

            //IPool blackPool = engine.controller.TablaBoard.BlackPoolsSet[14];

            //blackPool.State = PoolState.AtHome;
            //engine.controller.TablaBoard.ColumnSet[23].PoolStack.Push(blackPool);
        }

        private static void ArrangeTheWhiteCheckers(IEngine engine)
        {
            //for (int i = 0; i < engine.controller.TablaBoard.WhitePoolsSet.Count - 1; i++)
            //{
            //    engine.controller.TablaBoard.WhitePoolsSet[i].State = PoolState.OutOfGame;
            //}

            //IPool whitePool = engine.controller.TablaBoard.WhitePoolsSet[14];

            //whitePool.State = PoolState.AtHome;
            //engine.controller.TablaBoard.ColumnSet[2].PoolStack.Push(whitePool);
        }

        private static void CreateController()
        {
            Controller controller = new Controller();

            string text = controller.PlayersChooseAColor(2);
            Console.WriteLine(text);

            Console.WriteLine(controller.ArrangingTheCheckersToPlay());

            
        }

        private static void PrintBoardInfo(IBoard board)
        {
            Console.WriteLine(new String('-',30));

            for (int i = 0; i < board.DiceSet.Count; i++)
            {
                var diceName = board.DiceSet[i+1].Name;
                var diceValue = board.DiceSet[i + 1].ValueOfOneDice;

                Console.WriteLine($"{diceName} has value : {diceValue}");
            }

            Console.WriteLine(new String('-', 30));

            for (int i = 0; i < board.ColumnSet.Count; i++)
            {
                var columnName = board.ColumnSet[i + 1].IdentityNumber;
                var columnColor = board.ColumnSet[i + 1].Color;

                Console.WriteLine($"Column with Id : {columnName} has color : {columnColor}");
            }

            Console.WriteLine(new String('-', 30));

            for (int i = 0; i < board.WhitePoolsSet.Count; i++)
            {
                var poolId = board.WhitePoolsSet[i].IdentityNumber;
                var poolColor = board.WhitePoolsSet[i].PoolColor;
                var poolStater = board.WhitePoolsSet[i].State;

                Console.WriteLine($"Pool with Id : {poolId} has color : {poolColor} and current status : {poolStater}.");
            }

            Console.WriteLine(new String('-', 30));

            for (int i = 0; i < board.BlackPoolsSet.Count; i++)
            {
                var poolId = board.BlackPoolsSet[i].IdentityNumber;
                var poolColor = board.BlackPoolsSet[i].PoolColor;
                var poolStater = board.BlackPoolsSet[i].State;

                Console.WriteLine($"Pool with Id : {poolId} has color : {poolColor} and current status : {poolStater}.");
            }
        }
    }
}

/*
    //IPlayer somePl = engine.controller.Players[0];
                //MethodInfo[] arMethods = somePl.Move.GetType().GetMethods();
                //foreach (var item in arMethods)
                //{
                //    Console.WriteLine(item.Name + " <<>> " + item.GetType().FullName);
                //}

                //engine.ChoiceOfColorByThePlayers();

                //engine.PlayersArrangeTheirCheckers();

                //IPool pool = engine.controller.TablaBoard.ColumnSet[1].PoolStack.Pop();
                //pool.State = PoolState.OnTheBar;

                //IPlayer player = engine.controller.Players.Find(x=>x.MyPoolsColor == PoolColor.Black);

                //engine.controller.TablaBoard.DiceSet[1].ValueOfOneDice = 6;
                //engine.controller.TablaBoard.DiceSet[2].ValueOfOneDice = 6;

                //var boolValue = engine.controller.ConfirmationOfTheMoves.CurrentPlayerHasNoMoves(player, engine.controller.TablaBoard);

                //List<int> dices = engine.controller.TablaBoard.DiceSet
                //    .Select(x=>x.Value.ValueOfOneDice)
                //    .Distinct()
                //    .ToList();

                //List<IColumn> cols =  engine.controller.TablaBoard.ColumnSet
                //    .Select(x=>x.Value)
                //    .ToList();

                //List<IColumn> newColsCollection = new List<IColumn>();

                //dices.ForEach(x=> 
                //    {
                //        newColsCollection.Add(cols.Find(w=>w.IdentityNumber == x));
                //    });

                //newColsCollection.All(x=>x.PoolStack.Count > 1 && x.PoolStack.Peek().PoolColor == PoolColor.White); 
*/
