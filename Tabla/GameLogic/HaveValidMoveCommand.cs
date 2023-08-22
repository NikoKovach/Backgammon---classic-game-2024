namespace Tabla.GameLogic
{
    using System.Collections.Generic;
    using System.Linq;

    using Tabla.Enums;
    using Tabla.GameLogic.Contracts;
    using Tabla.Model.Interfaces;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.ServicesFolder;
    using Tabla.TablaFolder.ContractTabla;

    public class HaveValidMoveCommand : BaseLogicClass,  IBaseLogic
    {
        public HaveValidMoveCommand(IPlayer player, IList<int> moveParametersList, ITabla tabla)
            :base( player,  moveParametersList, tabla)
        {
            this.TargetColIndex = int.MinValue;
        }

        public int TargetColIndex { get; set; }

        public IList<IColumn> PlayerColsList { get; private set; }

        public IList<int> DiceNumbersList 
        {
            get { return new List<int>(base.MoveParametersList); }
        }
        
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public bool CanMoveChipCheck()
        {
            this.PlayerColsList = null;
            this.PlayerColsList = GameLogicSupportClass.PlayerColumnsList(this.CurrentPlayer, this.Tabla);
            
            foreach (var dice in this.DiceNumbersList)
            {
                List<int> targetIndexList = GetTargetColsIndex(dice);
                foreach (var index in targetIndexList)
                {
                    int poolsCountInTargetCol = GameLogicSupportClass
                        .CountOfPoolsInTargetColumn(this.Tabla, index);

                    if (poolsCountInTargetCol == 0)
                    {
                        return true;
                    }

                    string poolsColorInTargetCol = GameLogicSupportClass
                        .ColorOfTargetColumn(this.Tabla, index);

                    if (poolsColorInTargetCol == this.CurrentPlayer.Color.ToString())
                    {
                        return true;
                    }
                    else if (poolsCountInTargetCol == 1
                        && poolsColorInTargetCol != this.CurrentPlayer.Color.ToString())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private List<int> GetTargetColsIndex(int diceNumber)
        {
            List<int> indexList = new List<int>();
            if (this.CurrentPlayer.Color == Color.White)
            {
                indexList = PlayerColsList
                    .Select(x => x.IdentityNumber + diceNumber - 1)
                    .Where(z => z < TableGlobalConstants.ColumnNumber)
                    .ToList();
            }
            else if (this.CurrentPlayer.Color == Color.Black)
            {
                indexList = PlayerColsList
                   .Select(x => x.IdentityNumber - diceNumber - 1)
                   .Where(z => z >= 0)
                   .ToList();
            }
            return indexList;
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public bool HaveBeatenChipCheck()
        {
            
            this.TargetColIndex = int.MinValue;

            foreach (var dice in this.DiceNumbersList)
            {
                if (this.CurrentPlayer.Color == Color.White)
                {
                    this.TargetColIndex = dice - 1;
                }
                else
                {
                    this.TargetColIndex = TableGlobalConstants.ColumnNumber - 1;
                }

                int poolsCountInTargetCol = GameLogicSupportClass.CountOfPoolsInTargetColumn(this.Tabla, this.TargetColIndex);

                if (poolsCountInTargetCol <= 1)
                {
                    return true;
                }
                else if (poolsCountInTargetCol > 1)
                {
                    string poolsColorInTargetCol = GameLogicSupportClass
                        .ColorOfTargetColumn(this.Tabla, this.TargetColIndex);
                    if (poolsColorInTargetCol == this.CurrentPlayer.Color.ToString())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public bool CanMoveOutCheck()
        {          
            PlayerColsList = null;
            PlayerColsList = GameLogicSupportClass.PlayerColumnsListOut(this.CurrentPlayer, this.Tabla);

            foreach (var dice in this.DiceNumbersList)
            {
                int colIndex = TableGlobalConstants.MaxDiceValue - dice;
                int poolsCountInColumn = PlayerColsList[colIndex].PoolStack.Count;
                int poolsCountFromSixToDice = PlayerColsList
                            .Take(colIndex)
                            .SelectMany(x => x.PoolStack)
                            .Count();

                if (poolsCountInColumn > 0 && poolsCountFromSixToDice > 0)
                {
                    //Can move out a chip or move a chip
                    return true;
                }
                else if (poolsCountInColumn > 0 && poolsCountFromSixToDice == 0)
                {
                    //Can move out a chip
                    return true;
                }
                else if (poolsCountInColumn == 0 && poolsCountFromSixToDice > 0)
                {
                    //Can move a chip
                    return true;
                }
                else if (poolsCountInColumn == 0 && poolsCountFromSixToDice == 0)
                {
                    //Can move out a chip
                    return true;
                }
            }
            return false;
        }
        
    }
}
