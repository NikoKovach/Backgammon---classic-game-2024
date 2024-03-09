namespace TablaGameLogic.Services
{
     using System;
     using System.Collections.Generic;
     using System.Linq;
     using TablaGameLogic.Core.Contracts;
     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;

     public class MotionValidate : IMotionValidation
     {

          public virtual bool MoveIsValid(IMoveParameters motion,IBoard gameBoard, IPlayer currentPlayer)
          {
               return true;
          }

          //public bool CurrentPlayerHasNoMoves()
          //{
            //   bool anyCheckersOnTheBar;

            //   if (this.CurrentColor == PoolColor.White)
            //   {
            //       anyCheckersOnTheBar = this.board.WhitePoolsSet.Any(x => x.State ==   PoolState.OnTheBar);
            //   }
            //   else
            //   {
            //       anyCheckersOnTheBar = this.board.BlackPoolsSet.Any(x => x.State ==   PoolState.OnTheBar);
            //   }

            //   List<int> dices = this.board.DiceValueAndMovesCount
            //                              .Where(x => x.Value > 0)
            //                              .Select(x => x.Key)
            //                              .ToList();

            //   List<IColumn> cols = this.board.ColumnSet
            //       .Select(x => x.Value)
            //       .ToList();

            //   List<IColumn> newColsCollection = new List<IColumn>();

            //   if (this.CurrentColor == PoolColor.White)
            //   {
            //        dices.ForEach(x =>
            //        {
            //            newColsCollection.Add(cols.Find(w => w.IdentityNumber == (cols.Count + 1) -     x));
            //        });
            //   }
            //   else
            //   {
            //        dices.ForEach(x =>
            //        {
            //            newColsCollection.Add(cols.Find(w => w.IdentityNumber == x));
            //        });
            //   }

            //bool colsAreClosed = newColsCollection.All(x => x.PoolStack.Count > 1 && x.PoolStack.Peek().PoolColor != this.player.MyPoolsColor);

            //if (colsAreClosed && anyCheckersOnTheBar)
            //{
            //    return true;
            //}

          //  return false;
          //}

          //public virtual bool HasNoOtherMoves()
          //{
          //  //When player has checkers with status 'OnTheBar'
          //  if (CurrentPlayerHasNoMoves())
          //  {
          //      return true;
          //  }

          //  //When player has no open target colums 'MoveMotion'
          //  if (!ThereAreOpenTargetColumnsForMove())
          //  {
          //      return true;
          //  }

          //  //When player has no valid motion in Home field
          //  //he can not get out checker or he cann't move checker in 'Home field'

          //  if (!HasValidMotionInHomeField()) //There is ERROR return true  а трябваше false
          //  {
          //      return true;
          //  }

          //  return false;
          //}

          //protected bool HasValidMotionInHomeField()
          //{
          //    if (ThereAreCheckersOutsideFromHomeField())
          //    {
          //        if (ThereAreOpenTargetColumnsForMove())
          //        {
          //            return true;
          //        }

          //        if (ThereAreValidColumnToGetOutChecher())
          //        {
          //            return true;
          //        }
          //    }

          //    return false;
          //}

          protected bool ThereAreValidColumnToGetOutChecher()
          {
              //List<IColumn> homeColumns = new List<IColumn>();

              //if (this.CurrentColor == PoolColor.White)
              //{
              //    homeColumns = this.Board.ColumnSet
              //                            .Where(x => x.Key >= 1 && x.Key <= 6)
              //                            .Select(y => y.Value)
              //                            .OrderByDescending(z => z.IdentityNumber)
              //                            .ToList();
              //}
              //else 
              //{
              //    homeColumns = this.Board.ColumnSet
              //                           .Where(x => x.Key >= 19 && x.Key <= 24)
              //                           .Select(y => y.Value)
              //                           .OrderBy(z => z.IdentityNumber)
              //                           .ToList();
              //}

              //List<int> diceCollection = this.board.DiceValueAndMovesCount
              //                           .Where(x => x.Value > 0)
              //                           .Select(y => y.Key)
              //                           .ToList();

              //foreach (var dice in diceCollection)
              //{
              //    int targetColNumber =
              //        (this.CurrentColor == PoolColor.White) ? dice : 24 + 1 - dice;

              //    IColumn targetCol = homeColumns.First(x => x.IdentityNumber ==     targetColNumber);

              //    if (targetCol.PoolStack.Count > 0)
              //    {
              //        return true;
              //    }

              //    if (targetCol.PoolStack.Count == 0)
              //    {
              //        if (ThereAreValidColumnToGetOutChecherSecondVariant(targetColNumber, dice,     homeColumns))
              //        {
              //            return true;
              //        }
              //    }     
              //}

              return false;
          }

          //private bool ThereAreValidColumnToGetOutChecherSecondVariant(int targetCol, int dice,  List<IColumn> homeColumns)
          //{
              //List<IColumn> secondHomeColumns = new List<IColumn>();

              //if (this.CurrentColor == PoolColor.White)
              //{
              //    secondHomeColumns = homeColumns
              //           .Where(x => x.IdentityNumber > targetCol && x.IdentityNumber <= 6)
              //           .OrderByDescending(y=>y.IdentityNumber)
              //           .ToList();
              //}
              //else
              //{
              //    secondHomeColumns = homeColumns
              //            .Where(x => x.IdentityNumber >= 19 && x.IdentityNumber < targetCol)
              //            .OrderBy(y => y.IdentityNumber)
              //            .ToList();
              //}

              //bool conditionOne = secondHomeColumns.All(x=>x.PoolStack.Count == 0 ||   (x.PoolStack.Count > 0 && x.PoolStack.Peek().PoolColor != this.CurrentColor));

              //IColumn hasValidColumnOnTheRight = null;

              //if (this.CurrentColor == PoolColor.White)
              //{
              //    hasValidColumnOnTheRight = homeColumns
              //        .First(x => (x.IdentityNumber < targetCol) 
              //           && (x.PoolStack.Count > 0) 
              //           && (x.PoolStack.Peek().PoolColor == this.CurrentColor));
              //}
              //else 
              //{
              //    hasValidColumnOnTheRight = homeColumns
              //        .First(x => (x.IdentityNumber > targetCol)
              //           && (x.PoolStack.Count > 0)
              //           && (x.PoolStack.Peek().PoolColor == this.CurrentColor));
              //}

              //if (conditionOne && hasValidColumnOnTheRight.PoolStack.Count > 0)
              //{
              //    return true;
              //}

          //    return false;
          //}

          //protected bool CheckForValidTargetColumn(int targetColNumber)
          //{
          //     if (targetColNumber < 1 || targetColNumber > 24)
          //     {
          //         return false;
          //     }

          //     return true;
          //}

          //protected bool ThereAreOpenTargetColumnsForMove()
          //{
               //List<IColumn> colsWithPoolsCurrentPlayer = this.Board.ColumnSet
               //    .Where(x => x.Value.PoolStack.Count > 0 &&
               //           x.Value.PoolStack.Peek().PoolColor == this.CurrentColor)
               //    .Select(y => y.Value).ToList();

               //List<int> dices = this.board.DiceValueAndMovesCount
               //                          .Where(x => x.Value > 0)
               //                          .Select(x => x.Key)
               //                          .ToList();

               //foreach (var item in dices)
               //{
               //    for (int i = 0; i < colsWithPoolsCurrentPlayer.Count; i++)
               //    {
               //        int colId = colsWithPoolsCurrentPlayer[i].IdentityNumber;

               //        int targetColNumber =
               //            (this.CurrentColor == PoolColor.Black) ? colId + item : colId - item;

               //        if (TheTargetColumnIsOpen(targetColNumber))
               //        {
               //            return true;
               //        }
               //    }
               //}

               //return false;
          //}

          //protected bool CheckPametersCountForInsideAndOutsideMove(int[] moveParams)
          //{
          //    if (moveParams.Length > 1)
          //    {
          //        return false;
          //    }

          //    return true;
          //}
       
        // Check whether the start column is valid
        //protected bool StartingColumnIsValid(int columnNumber)
        //{
        //    int countOfCheckersOnColumn = CountOfCheckersOnTheColumn(columnNumber);

        //    if (countOfCheckersOnColumn < 1)
        //    {
        //        return false;
        //    }

        //    if (!this.IsDifferentChecherColor(columnNumber))
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //we check whether the target column is free or not for pushing checker
        //protected bool TheTargetColumnIsOpen(int targetColNumber)
        //{
        //    int countOfCheckersOnColumn = CountOfCheckersOnTheColumn(targetColNumber);

        //    if (countOfCheckersOnColumn > 1 && !IsDifferentChecherColor(targetColNumber))
        //    {
        //            return false;
        //    }
            
        //    return true;
        //}

        //protected bool IsDifferentChecherColor(int targetColNumber)
        //{
        //    PoolColor colorOfLastCheckers = this.board.ColumnSet[targetColNumber].PoolStack.Peek().PoolColor;

        //    if (colorOfLastCheckers != this.CurrentColor)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //There are checkers with state 'OnTheBar'
        //protected bool HaveCheckersWithStateOnTheBar()
        //{
        //    IList<IPool> checkersList = new List<IPool>();

        //    checkersList = (this.CurrentColor == PoolColor.White) ? this.Board.WhitePoolsSet : Board.BlackPoolsSet;

        //    int checkersOnTheBarCount = checkersList.Where(x => x.State == PoolState.OnTheBar).Count();

        //    if (checkersOnTheBarCount > 0)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //protected int CountOfCheckersOnTheColumn(int column)
        //{ 
        //    return this.Board.ColumnSet[column].PoolStack.Count;
        //}

        //protected bool ThereAreCheckersOutsideFromHomeField()
        //{
        //    IList<IPool> checkersList = new List<IPool>();

        //    checkersList = (this.CurrentColor == PoolColor.White) ? this.Board.WhitePoolsSet : Board.BlackPoolsSet;

        //    int checkersInGameCount = checkersList
        //                                .Where(x => x.State == PoolState.InGame)
        //                                .Count();

        //    if (!(checkersInGameCount > 0))
        //    {
        //        return false;
        //    }

        //    return true;
        //}
     }
}

