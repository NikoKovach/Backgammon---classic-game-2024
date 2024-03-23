namespace TablaEngine.Engine.Contracts
{
    using TablaEngine.IO.Contracts;
    using TablaModels.Components.Interfaces;

    public interface IConsoleEngine : IEngine
     {
          IWriter Writer { get; }

          IReader Reader{ get; }

          void PrintTheWinner( IPlayer currentPlayer );

          void ExitGameOrPlayNewGame();
     }
}
        //IWriter Writer { get; }
        //IReader Reader { get; }
        //void Run(IEngineServices services);