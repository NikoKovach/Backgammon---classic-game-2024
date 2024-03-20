namespace TablaModels.ComponentModels.Components
{
     using System;
     using System.Collections.Generic;

     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;
     using static TablaModels.ModelsUtilities.Messages.ExceptionMessages;

     public class Column : IColumn
     {
          private int identityNumber;
          private Stack<IPool> poolStack;

          public Column(int idNumber, string colColor) 
              : this(idNumber,colColor,new BoardSettings().ColumnBase, new BoardSettings  ().ColumnHeight)
          {
          }

          public Column(int idNumber,string colColor,int colBase,int colHeight)
          {
              this.IdentityNumber = idNumber;

              SetColumnColor(colColor);

              this.ColumnBase = colBase;

              this.ColumnHeight = colHeight;

              this.poolStack = new Stack<IPool>();
          }
        
          public int IdentityNumber
          {
               get => this.identityNumber; 

               set 
               {
                   if (value < TableGlobalConstants.MinColumnNumber 
                         || value > TableGlobalConstants.MaxColumnNumber)
                   {
                       throw new ArgumentException(InvalidColumnId);
                   }

                   this.identityNumber = value;
               }
          }

          public int ColumnBase { get; set; }

          public int ColumnHeight { get; set; }

          public ColumnColor Color { get; private set; }

          public Stack<IPool> PoolStack
          {
              get => this.poolStack;
          } 
          
          public void SetColumnColor(string color)
          {
               ColumnColor colColor;
               bool parseIsCorect = 
                    Enum.TryParse<ColumnColor>( color, true, out colColor );

               if ( !parseIsCorect )
               {
                    throw new ArgumentException(InvalidColumnColor) ;
               }

               this.Color = colColor ;    
          }
     }
}
