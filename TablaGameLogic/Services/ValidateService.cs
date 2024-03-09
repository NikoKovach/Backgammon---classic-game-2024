
using System;
using TablaGameLogic.Core.Contracts;
using TablaGameLogic.Services.Contracts;
using TablaModels.ComponentModels.Components.Interfaces;

namespace TablaGameLogic.Services
{
     public class ValidateService : IMoveValidate
     {
          //public void MoveIsValid(IMoveParameters motion,IBoard board,IPlayer player)
          //{

          //     //string motionValidateName = moveType + "Validate";
          //     //Type motionValidateType = Type.GetType($"TablaGameLogic.Services.     {motionValidateName}");

          //     //IMotionValidation instanceOfMotionValidatе = (IMotionValidation)      Activator.CreateInstance(motionValidateType, 
          //     //     new object[]{ board,CurrentPlayer});

          //     //return instanceOfMotionValidatе.MoveIsValid(parameters);
          //}
          public bool MoveIsValid( IMoveParameters motion, IBoard board, IPlayer player )
          {
               throw new NotImplementedException();
          }
     }
}
