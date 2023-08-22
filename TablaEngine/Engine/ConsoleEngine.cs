namespace TablaEngine.Engine
{
    using System;

    using TablaEngine.Engine.Contracts;
    using TablaEngine.IO.Contracts;
    using TablaGameLogic.Core.Contracts;

    public abstract class ConsoleEngine : IEngine
    {
        private IController controller;
        private IWriter writer;
        private IReader reader;

        public ConsoleEngine(IController someController, IWriter outerWriter, IReader OuterReader)
        {
            this.Controller = someController;
            this.Writer = outerWriter;
            this.Reader = OuterReader;
        }

        public IController Controller 
        { 
            get => this.controller;

            set 
            {
                if (value == null)
                {
                    throw new ArgumentNullException(""); //TODO : Exception string
                }

                this.controller = value;
            }
        }

        public IWriter Writer 
        { 
            get => this.writer;

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("");//TODO : Exception string
                }

                this.writer = value;
            }
        }

        public IReader Reader 
        { 
            get => this.reader;

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("");//TODO : Exception string
                }

                this.reader = value;
            }
        }

        public abstract void Run();
        
    }
}
