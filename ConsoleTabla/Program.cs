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
               RunEngine();
          }

          private static void RunEngine()
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
     }
}
