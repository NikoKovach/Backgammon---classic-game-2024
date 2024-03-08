
using System;
using TablaGameLogic.Services.Contracts;
using TablaModels.ComponentModels.Components.Interfaces;

namespace TablaGameLogic.Services
{
     public class ValidateService : IMoveValidate
     {
           //TODO : General Class Move Validation

          /// <summary>
          /// //TODO :
          /// </summary>
          /// <param name="moveType"></param>
          /// <param name="board"></param>
          /// <param name="CurrentPlayer"></param>
          /// <returns></returns>
          
          public bool MoveIsValid(string moveType, int[] parameters,IBoard board,IPlayer CurrentPlayer)
          {
               
               string motionValidateName = moveType + "Validate";
               Type motionValidateType = Type.GetType($"TablaGameLogic.Services.     {motionValidateName}");

               IMotionValidation instanceOfMotionValidatе = (IMotionValidation)      Activator.CreateInstance(motionValidateType, 
                    new object[]{ board,CurrentPlayer});

               return instanceOfMotionValidatе.MoveIsValid(parameters);
          }
     }
}
