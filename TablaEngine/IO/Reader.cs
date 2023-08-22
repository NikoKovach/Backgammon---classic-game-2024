namespace TablaEngine.IO
{
    using System;
    using TablaEngine.IO.Contracts;

    public class Reader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
