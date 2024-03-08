using System.Collections.Generic;

namespace TablaModels.ComponentModels.Components.Interfaces
{
    public interface IMoveChecker
    {
        void Inside(int columnNumber, IPool pool);

        void Outside(int columnNumber);

        void Move(int columnNumber, int positions);
    }
}
