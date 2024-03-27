namespace TablaModels.Test.TestsModels
{
     using NUnit.Framework;
     using System.Collections.Generic;
     using TablaModels.Components;
     using TablaModels.Components.Interfaces;
     using TablaModels.Components.Players;
     using TablaModels.Enums;

     [TestFixture]
     public class MoveChipsClassicGameTest
     {
          private IDictionary<int, IColumn> columnSet;

          [SetUp]
          protected void SetUp()
          {
               columnSet = new Dictionary<int, IColumn>();
          }

          [TestCaseSource(nameof(InsideMethodParams))]
          public void InsideMethodShouldPutsAChipIntoPlay(int colNumber, IPool pool,
               IDictionary<int, IColumn> columns,IPool beatenChip)
          {
               //Arrange
               columnSet = columns;
               var moveChipsObj = new MoveChipsClassicGame();

               //Act
               moveChipsObj.Inside( colNumber, pool, columnSet );

               //Assert
               Assert.That(pool.State == PoolState.InGame);
               Assert.AreEqual( 1, columnSet[ colNumber ].PoolStack.Count );
          }

          [TestCaseSource(nameof(InsideMethodParams))]
          public void InsideMethodShouldHitsTheEnemyChip(int colNumber, IPool pool,
               IDictionary<int, IColumn> columns,IPool beatenChip)
          {
               columnSet = columns;
               columnSet[ colNumber ].PoolStack.Push( beatenChip );

               var moveChipsObj = new MoveChipsClassicGame();

               //Act
               moveChipsObj.Inside( colNumber, pool, columnSet );

               //Assert
               Assert.That(pool.State == PoolState.InGame);
               Assert.That(beatenChip.State == PoolState.OnTheBar);
               Assert.AreEqual( 1, columnSet[ colNumber ].PoolStack.Count );     
          }

          [TestCaseSource(nameof(OutsideMethodParams))]
          public void OutsideMethodShouldMoveAChipOutOgGame(int colNumber,
               IDictionary<int, IColumn> columns,IPool chipGoOut)
          {
               //Arrange
               columnSet = columns;
               columnSet[ colNumber ].PoolStack.Push( chipGoOut );
               var moveChipsObj = new MoveChipsClassicGame();

               //Act
               moveChipsObj.Outside( colNumber, columnSet );

               //Assert
               Assert.That(chipGoOut.State == PoolState.OutOfGame);
               Assert.AreEqual( 0, columnSet[ colNumber ].PoolStack.Count );
          }

          [TestCaseSource(nameof(MoveMethodParams))]
          public void MoveMethodShouldMoveAChipAndHitTheEnemyChip
               (int colNumber,int positions,IDictionary<int, IColumn> columns,
               int targetCol,IPool chipToMove,IPool beatenChip)
          {
               //Arrange             
               columnSet = new Dictionary<int, IColumn>(columns.AsReadOnly());

               int idNumber = chipToMove.IdentityNumber;
               columnSet[ colNumber ].PoolStack.Push( chipToMove );
               columnSet[ targetCol ].PoolStack.Push( beatenChip );
               var moveChipsObj = new MoveChipsClassicGame();

               //Act
               //int columnNumber, int positions,IDictionary<int, IColumn> columns
               moveChipsObj.Move( colNumber, positions, columnSet );

               //Assert
               Assert.That(beatenChip.State == PoolState.OnTheBar);
               Assert.AreEqual( idNumber, columnSet[ targetCol ].PoolStack
                                          .Peek().IdentityNumber);
               Assert.AreEqual( 0, columnSet[ colNumber ].PoolStack.Count );
               Assert.AreEqual( 1, columnSet[ targetCol ].PoolStack.Count );
          }
//***********************************************************************
          private static object[] InsideMethodParams()
          {
               IPool blackChipToIn = new Pool( 1, PoolColor.Black, PoolState.OnTheBar );
               IPool beatenWhiteChip = new Pool( 2, PoolColor.White, PoolState.AtHome );

               IPool whiteChipToIn = new Pool( 1, PoolColor.White, PoolState.OnTheBar );
               IPool beatenBlackChip = new Pool( 2, PoolColor.Black, PoolState.AtHome );

               Dictionary<int, IColumn> columns1To6 = new Dictionary<int, IColumn>();
               Dictionary<int, IColumn> columns19To24 = new Dictionary<int, IColumn>();

               for ( int i = 1; i <= 6; i++ )
               {
                    columns1To6[ i ]      = new Column( i );
                    columns19To24[25 - i] = new Column( 25 - i );
               }

               //int colNumber, IPool pool, IDictionary<int, IColumn> columns,
               //IPool beatenChip
               object[] methodParams = new object[] 
               {
                    new object[]{ 1,blackChipToIn,columns1To6,beatenWhiteChip},
                    new object[]{ 2,blackChipToIn,columns1To6,beatenWhiteChip},
                    new object[]{ 3,blackChipToIn,columns1To6,beatenWhiteChip},
                    new object[]{ 4,blackChipToIn,columns1To6,beatenWhiteChip},
                    new object[]{ 5,blackChipToIn,columns1To6,beatenWhiteChip},
                    new object[]{ 6,blackChipToIn,columns1To6,beatenWhiteChip},

                    //colNumbers  - white chip In { 19, 20, 21, 22, 23, 24 };
                    new object[]{ 19,whiteChipToIn,columns19To24,beatenBlackChip},
                    new object[]{ 20,whiteChipToIn,columns19To24,beatenBlackChip},
                    new object[]{ 21,whiteChipToIn,columns19To24,beatenBlackChip},
                    new object[]{ 22,whiteChipToIn,columns19To24,beatenBlackChip},
                    new object[]{ 23,whiteChipToIn,columns19To24,beatenBlackChip},
                    new object[]{ 24,whiteChipToIn,columns19To24,beatenBlackChip},
               };

               return methodParams;
          }

          private static object[] OutsideMethodParams()
          {
               IPool blackChipGoOut = new Pool( 1, PoolColor.Black, PoolState.AtHome );

               IPool whiteChipGoOut = new Pool( 2, PoolColor.White, PoolState.AtHome );

               Dictionary<int, IColumn> columns = new Dictionary<int, IColumn>();

               for ( int i = 1; i <= 24; i++ )
               {
                    columns[ i ] = new Column( i );
               }

               //int columnNumber,IDictionary<int, IColumn> columns
               object[] methodParams = new object[] 
               {
                    //white columns
                    new object[]{ 6,columns,whiteChipGoOut},
                    new object[]{ 5,columns,whiteChipGoOut},
                    new object[]{ 2,columns,whiteChipGoOut},
                    new object[]{ 1,columns,whiteChipGoOut},

                    // black columns{ 19, 22, 23, 24 };
                    new object[]{ 19,columns,blackChipGoOut},
                    new object[]{ 22,columns,blackChipGoOut},
                    new object[]{ 23,columns,blackChipGoOut},
                    new object[]{ 24,columns,blackChipGoOut},
               };

               return methodParams;
          }

          private static object[] MoveMethodParams()
          {
               IPool blackChipMove   = new Pool( 1, PoolColor.Black, PoolState.InGame );
               IPool beatenWhiteChip = new Pool( 2, PoolColor.White, PoolState.InGame );

               IPool whiteChipMove   = new Pool( 3, PoolColor.White, PoolState.InGame );
               IPool beatenBlackChip = new Pool( 4, PoolColor.Black, PoolState.InGame );

               Dictionary<int, IColumn> columns = new Dictionary<int, IColumn>();

               for ( int i = 1; i <= 24; i++ )
               {
                    columns[ i ] = new Column( i );
               }

               //int columnNumber, int positions,IDictionary<int, IColumn> columns
               //int targetCol , IPool chipToMove , IPool beatenChip
               object[] methodParams = new object[] 
               {
                    //black columns
                    new object[]{ 16, 1,columns,17,blackChipMove,beatenWhiteChip},
                    new object[]{ 14, 6,columns,20,blackChipMove,beatenWhiteChip},
                    new object[]{ 12,10,columns,22,blackChipMove,beatenWhiteChip},
                    new object[]{  4,20,columns,24,blackChipMove,beatenWhiteChip},

                    // white columns{ };
                    new object[]{  8, 4,columns, 4,whiteChipMove,beatenBlackChip},
                    new object[]{ 10, 3,columns, 7,whiteChipMove,beatenBlackChip},
                    new object[]{ 16,10,columns, 6,whiteChipMove,beatenBlackChip},
                    new object[]{ 23, 5,columns,18,whiteChipMove,beatenBlackChip},
               };

               return methodParams;
          }
     }
}

