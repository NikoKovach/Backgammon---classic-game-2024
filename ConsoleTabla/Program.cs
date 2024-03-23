using System;

using TablaEngine.Engine.Contracts;
using TablaEngine.Engine;
using TablaGameLogic.Core;
using TablaConsoleGame;
using System.Linq;
using System.Reflection;
using TablaGameLogic.Services;
using TablaModels.Components.Interfaces;

namespace TablaConsoleGame
{
     public class Program
     {
          static void Main(string[] args)
          {
                 //CreateEngine();

                 //Test2();
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

//*****************************************************************
          private static void Test1()
          {
               var scheme = new GameScheme();

               scheme.SchemaBasic(4,4); // Move    = "3 24 4"
               scheme.Engine.MainGameMethod();
          }

          //Inside  = "1 24 15"
          //Outside = "2 4"
          //Move    = "3 24 4";

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

                   Console.WriteLine($"Column with Id : {columnName} has color :   {columnColor}");
               }

               Console.WriteLine(new String('-', 30));

               for (int i = 0; i < board.WhitePoolsSet.Count; i++)
               {
                   var poolId = board.WhitePoolsSet[i].IdentityNumber;
                   var poolColor = board.WhitePoolsSet[i].PoolColor;
                   var poolStater = board.WhitePoolsSet[i].State;

                   Console.WriteLine($"Pool with Id : {poolId} has color : {poolColor} and   current status : {poolStater}.");
               }

               Console.WriteLine(new String('-', 30));

               for (int i = 0; i < board.BlackPoolsSet.Count; i++)
               {
                   var poolId = board.BlackPoolsSet[i].IdentityNumber;
                   var poolColor = board.BlackPoolsSet[i].PoolColor;
                   var poolStater = board.BlackPoolsSet[i].State;

                   Console.WriteLine($"Pool with Id : {poolId} has color : {poolColor} and   current status : {poolStater}.");
               }
          }
     }
}
