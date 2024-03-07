namespace TablaGameLogic.Utilities.Messages
{
    public static class OutputMessages
    {
          public const string NewRow = "\r\n";

          public const string PlayerNameRequirement = 
               "The name of player must contains at least 3 characters.";

          public const string EnterPlayerName = "Player {0} enter your name : ";

          public const string ChooseOfColor =
               "Player one (with name :{0}) please select a color:" + NewRow +
               "Press 1 : for \"White\"      ;"    + NewRow +
               "Press 2 : for \"Black\"      ;"    + NewRow +
               "Press x : for EXIT THE GAME  ."    + NewRow +
               "Enter your choice : ";

          public const string InvalidChooseOfColor = 
               "You have entered an invalid digit ! Please, try again !" + NewRow +
               "Enter your choice : ";

           public const string PlayersChoseTheirColor = 
               "Player {0} chose the '{1}' color." + NewRow + 
               "Player {2} chose the '{3} color.";

          public const string PlayersArrangedHisCheckers = 
               NewRow + "Both players arranged his checkers." + NewRow;

//***********************************************************************************
          public const string PlayersRollADie = "Players : please rolls a dice!";

          public const string OnePlayerRollADice = 
               "Player {0} : please roll a dice !" + NewRow + 
               "For roll press 'r': ";

          public const string RollResultOfOneDice = "Player {0} threw {1}." + NewRow;

          public const string TheDiceOfPlayersAreTheSame = 
               "The Dices have the same values.Players roll your dice again." + NewRow;

          public const string PlayerStartFirst = 
               "Player '{0}' who plays with {1} checkers start first.";

          public const string DelayTheGame = "The game will start after {0} seconds !";

          public const string CurrentPlayerTwoDiceResult = 
               "It's turn of player {0} with the {1} checkers and rolled dice : {2} and {3}." + NewRow;

          public const string MovesType =
               "TypesOfMoves :" + NewRow +
               "Inside  - for placing a checker on the board ;"       + NewRow +
               "Outside - for taking a checker off the board ;"       + NewRow +
               "Move    - for moving one checker across the board ;"  + NewRow;
               
          public const string EnterTheWayOfMoveAndItsParameters = 
          "Please enter your move type in following format :"         + NewRow + 
          "1.For 'Inside'  - ( 1 ) (column number) (pool number)   ;" + NewRow + 
          "2.For 'Outside' - ( 2 ) (column number)                 ;" + NewRow + 
          "3.For 'Move'    - ( 3 ) (column number) (places to move);";

          public const string AskPlayerToRollDiceAndMakeMove = 
               "Player {0} : please,roll both dice and make your moves !" + NewRow + 
               "For rolling press 'r' :";

          public const string RollDiceResultMessage = 
               "Player {0}, you rolled {1} and {2} and you can make {3} moves .";        

          public const string TheMoveStartingColumnNumber = 
               "Please,enter the starting column number : ";

          public const string ThePlayerHaveNoMove = "{0},you have no avaliable motion.";

          public const string NoOtherMoves = "{0},you have no other motion/motions.";

          public const string MoveIsMade = "The move is made.";

          public const string InvalidMove = "Move is invalid !";

//*****************************************************************************
          public const string TheWinnerIs = "{0},you won the game !!!";

          public const string MessageExitGameOrStartNewGame = 
               "1 - For exit the game press 'x'." + NewRow + 
               "2 - For playing new game press 'n'.";

          public const string InvalidCommandForExitOrNewGame = 
               "Invalig command ! Please,try again !";

         
    }
}
