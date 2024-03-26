namespace TablaModels.Test.TestsModels
{
     using NUnit.Framework;

     [TestFixture]
     public class MoveChipsClassicGameTest
     {
          [TestCaseSource(nameof(InsideMethodParams))]
          public void InsideMethodShouldPutsAChipIntoPlay(int colNumber, IPool pool,
               IDictionary<int, IColumn> columns,IPool beatenChip)
          {
               var moveChipsObj = new MoveChipsClassicGame();
               moveChipsObj.Inside( colNumber, pool, columns );

               Assert.That(pool.State == PoolState.InGame);
               Assert.AreEqual( 1, columns[ colNumber ].PoolStack.Count );
          }

          [TestCaseSource(nameof(InsideMethodParams))]
          public void InsideMethodShouldHitsTheEnemyChip(int colNumber, IPool pool,
               IDictionary<int, IColumn> columns,IPool beatenChip)
          {
               //IPool beatenChip = new Pool(2,PoolColor.White,PoolState.AtHome);
               columns[ colNumber ].PoolStack.Push( beatenChip );

               var moveChipsObj = new MoveChipsClassicGame();
               moveChipsObj.Inside( colNumber, pool, columns );

               Assert.That(pool.State == PoolState.InGame);
               Assert.That(beatenChip.State == PoolState.OnTheBar);
               Assert.AreEqual( 1, columns[ colNumber ].PoolStack.Count );     
          }

          private static object[] InsideMethodParams()
          {
               IPool blackChipToIn = new Pool(1,PoolColor.Black,PoolState.OnTheBar);
               IPool beatenWhiteChip = new Pool(2,PoolColor.White,PoolState.AtHome);

               IPool whiteChipToIn = new Pool(1,PoolColor.White,PoolState.OnTheBar);
               IPool beatenBlackChip = new Pool(2,PoolColor.Black,PoolState.AtHome);

               List<int> colNumbers1To6 = new List<int>() { 1, 2, 3, 4, 5, 6 };
               List<int> colNumbers19To24 = new List<int>() { 19, 20, 21, 22, 23, 24 };

               Dictionary<int, IColumn> cols1To6BlackIn = new Dictionary<int, IColumn>();
               Dictionary<int, IColumn> cols19To24WhiteIn = new Dictionary<int, IColumn>();

               for ( int i = 1; i <= 6; i++ )
          { 
          
          }

     }
}
