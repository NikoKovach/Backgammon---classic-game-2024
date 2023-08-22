namespace TablaEngine.Engine.Contracts
{
    using TablaEngine.IO.Contracts;
    using TablaEngine.Services.Contracts;
    using TablaGameLogic.Core.Contracts;

    public interface IEngine
    {
        IController Controller { get; }
        IWriter Writer { get; }
        IReader Reader { get; }

        void Run();

        //void Run(IEngineServices services);
    }
}
