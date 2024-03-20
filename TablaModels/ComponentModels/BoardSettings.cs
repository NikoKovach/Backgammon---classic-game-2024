namespace TablaModels.ComponentModels
{
     using System;

     using static TablaModels.ModelsUtilities.Messages.ExceptionMessages;

     public  class BoardSettings
     {
          private const int OneSideBoardWidth = 270;  
          private int sideBoardWidth;

          public BoardSettings() : this(OneSideBoardWidth)
          {
          }
          
          public BoardSettings(int sBoardWidth)
          {
              this.SideBoardWidth = sBoardWidth;
          }

          public int SideBoardWidth 
          { 
               get => this.sideBoardWidth;

               set
               {
                    if (value < TableGlobalConstants.MinSideBoardWidth || value >          TableGlobalConstants.MaxSideBoardWidth)
                    {
                         throw new ArgumentOutOfRangeException(BoardWidthArgumentException);
                    }

                    if (value % 6 > 0)
                    {
                         throw new ArgumentException
                              (BoardWidthIsNotMultipleOfSixException);
                    }

                    this.sideBoardWidth = value;
               }
          }

          public int SideBoardHeight 
          {
               get => (int)Math.Ceiling(this.SideBoardWidth *  TableGlobalConstants.BoardHeightIncreaseFactor);
          }

          public int ColumnBase 
          { 
               get => (int) this.SideBoardWidth / TableGlobalConstants.NumberOfColumnsPerWidth;    
          }

          public int ColumnHeight 
          { 
               get => (int) (this.SideBoardHeight *  TableGlobalConstants.ColumnHeightToBoardHeightRatio); 
          }

          public int DiceSide 
          { 
               get =>(int) (this.SideBoardWidth *    TableGlobalConstants.DiceSideToBoardWidthRatio);  
          }

          public int OuterPoolDiameter 
          {
               get => (int)((this.SideBoardWidth / TableGlobalConstants.NumberOfColumnsPerWidth)   * 0.98);
          }

          public int InnerPoolDiameter
          {
               get => (int)(this.OuterPoolDiameter *      TableGlobalConstants.InnerDiameterToOuterDiameterRatio);
          }
    }
}

            // maxOneSideBoardWidth = DefaultOneSideBoardWidth *2
            //private const int OneSideBoardHeight = 441; // BoardHeight / BoardWidth = 1.62963