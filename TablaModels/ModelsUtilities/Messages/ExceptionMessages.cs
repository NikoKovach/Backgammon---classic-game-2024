namespace TablaModels.ModelsUtilities.Messages
{
     public static class ExceptionMessages
     {
          public const string InvalidColumnId = "Use number in the range 1 - 24 .";

          public const string InvalDieName = 
               "The name of dice can't be 'Null','Empty string' or " + 
               "can't contains white-space characters !";

          public const string InvalidDieValue = 
               "The value of a die can be in range from 1 to 6 !";

          public const string InvalidDieSide = 
               "The die side can be only positive value !";

          public const string InvalidBoardParameter = 
               "The input parameter {0} cannot be    null !";

          public const string InvalidPoolId = "Use number in the range 1 - 15 .";

          public const string InvalidColumnColor = 
               "The column color must be 'Dark' or 'Light'.";

          public const string BoardWidthArgumentException = 
               "The board width must be in range 12 - 540 units!";

          public const string BoardWidthIsNotMultipleOfSixException =
               "The width must be divisible by 6 without a remainder !";

          public const string BoardArgumentNullException =  
               "The argument {0} can not be null !";
     }
}
