namespace TablaEngine.Engine
{
    using TablaEngine.IO;
    using TablaGameLogic.Core;

    public class CorkConsoleEngine : ConsoleEngine
    {
        public CorkConsoleEngine() 
               : base(new Controller(), new Writer(), new Reader())
        { }

        public override void Run()
        {
            throw new System.NotImplementedException();
        }
    }
}
