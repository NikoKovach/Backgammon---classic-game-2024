namespace TablaModels.ModelsUtilities.Messages
{
     public static class ExceptionMessages
     {
          public const string InvalidColumnId = "Use number in the range 1 - 24 .";

          public const string InvalDieName = "The name of die can't be 'Null','Empty string' or   can't contains white-space characters !";

          public const string InvalidDieValue = "The value of a die can be in range from 1 to    6 !";

          public const string InvalidDieSide = "The die side can be only positive value !";

          public const string InvalidBoardParameter = "The input parameter {0} cannot be    null !";

          public const string InvalidPoolId = "Use number in the range 1 - 15 .";

          public const string NullMoveParameter = "{0} can not be null !";
     }
}
