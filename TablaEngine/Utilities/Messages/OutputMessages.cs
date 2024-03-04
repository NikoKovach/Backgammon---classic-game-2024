namespace TablaEngine.Utilities.Messages
{
    public static class OutputMessages
    {
          public const string NewRow = "\r\n";

          public const string PlayerNameRequirement = "The name of player must contains at least 3 characters.";

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

        public const string PlayersRollADie = "Players : please rolls a dice! ";

        public const string OnePlayerRollADice = 
               "Player {0} : please roll a dice !" + NewRow + 
               "For roll press 'r': ";

        public const string OneDiceRollResult = "Player {0} threw {1}.";

        public const string CurrentPlayerWithTwoDiceRollResult = "It's turn the player '{0}' with the '{1}' checkers and rolled dice : {2} and {3} .";
        //"\n\rIt's the turn of player '{0}' with the '{1}' checkers and dice: {2} and {3} .";

        public const string TheDiceOfPlayersAreTheSame = "The Dices have the same values.Players roll your dice again.";

        //public const string PlayersArrangedHisCheckers = "\n\rBoth players arranged his checkers .";

        public const string PlayerStartFirst = "\n\rPlayer '{0}' who plays with {1} checkers start first .";

        //public const string PlayersChoseTheirColor = "{0} select '{1}' and {2} select '{3}' .";

        public const string AskPlayerToRollDiceAndMakeMove = "Player {0} : please , roll both dice and make your moves !\n\rFor rolling press 'r' :";

        public const string RollDiceResultMessage = "Player {0}, you rolled {1} and {2} and you can make {3} moves .";

        public const string TheMoveStartingColumnNumber = "Please,enter the starting column number : ";

        public const string MovesType = "\n\rTypesOfMoves :\n\rIn    - for placing a checker on the board ;\n\rMove  - for moving one checker once across the board ;\n\rOut   - for taking a checker off the board .";

        public const string EnterTheWayOfMoveAndItsParameters = "\n\rPlease enter your move type in following format :\n\r1.For move type 'In'  - <<type of move>> <<column number>> ;\n\r2.For move type 'Out' - <<type of move>> <<column number>> ;\n\r3.For type 'Move'     - <<type of move>> <<column number>> <<places to move>> ;\n\r:";

        public const string TheWinnerIs = "\n\r{0} ,you won the game !!!";

        public const string MessageExitGameOrStartNewGame = "\n\r1 - For exit the game press 'x'.\n\r2 - For playing new game press 'n'.";

        public const string InvalidCommandForExitOrNewGame = "Invalig command ! Please,try again !";

        public const string ThePlayerHaveNoMove = "{0},you have no avaliable motion.";

        public const string NoOtherMoves = "{0},you have no other motion/motions.";

    }
}
