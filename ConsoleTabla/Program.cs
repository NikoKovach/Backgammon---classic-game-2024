using System;

using TablaEngine.Engine.Contracts;
using TablaEngine.Engine;
using TablaGameLogic.Core;
using TablaModels.ComponentModels.Components.Interfaces;
using TablaConsoleGame;

namespace ConsoleTabla
{
    public class Program
    {
        static void Main(string[] args)
        {
            //CreateEngine();

            Test1();
        }

          private static void CreateEngine()
          {
               IConsoleEngine engine = new ClassicConsoleEngine();

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
                   Console.WriteLine(ex.InnerException.Message);
               }

          }

          private static void Test1()
          {
               var scheme = new GameScheme();
               scheme.SchemaBasic(3,1);
               scheme.Controler.CurrentPlayerFirstSet();
               scheme.Engine.MainGameMethod();
               //string input = "1 24 15";
               //string message = scheme.Engine.Controller.CurrentPlayerMakesMove(input);
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
