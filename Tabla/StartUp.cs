namespace Tabla
{
    using System;

    using Tabla.Core.Engine;
    using Tabla.InputProvider.Contract;
    using Tabla.InputProvider;
    using Tabla.Model.Components;
    using Tabla.Model.Interfaces;

    public class StartUp
    {

        public static void Main()
        {
            //PlayTabla();
            IInputProvider inputProvider = new InProvider();

            EngineTwoPlayers entryPoint = new EngineTwoPlayers(inputProvider);
            entryPoint.InitiliazeTabla();
            entryPoint.Run();
            
        }

        private static void PlayTabla()
        {

            //Random random1 = new Random();
            //Random random2 = new Random(1);
            //for (int i = 0; i < 30; i++)
            //{
            //    //dice.Roll();
            //    Console.WriteLine(random1.Next(1,7) + "," + random2.Next(1,7));
            //}

        }

        //##########################################################
        //private static IServiceProvider ConfigureServices()
        //{
            //var serviceCollection = new ServiceCollection();
            //serviceCollection.AddTransient<DiceFactory>();
            //serviceCollection.AddTransient<PoolFactory>();
            //serviceCollection.AddTransient<ColumnFactory>();
            //serviceCollection.AddTransient< PlayerFactory>();

            //serviceCollection.AddSingleton<IDiceRepository, DiceRepository>();
            //serviceCollection.AddSingleton<IPoolRepository, PoolRepository>();
            //serviceCollection.AddSingleton<IColumnRepository, ColumnsRepository>();
            //serviceCollection.AddSingleton<IPlayerRepository, PlayerRepository>();

            //return serviceCollection.BuildServiceProvider();
        //}

    }
}