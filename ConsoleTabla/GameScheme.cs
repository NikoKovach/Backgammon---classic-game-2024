using System.Collections.Generic;
using TablaEngine.Engine;
using TablaEngine.IO;
using TablaGameLogic.Core;
using TablaGameLogic.Core.Contracts;
using TablaGameLogic.Factory;
using TablaGameLogic.Services;
using TablaModels.Components.Interfaces;
using TablaModels.Enums;

namespace TablaConsoleGame
{
     public class GameScheme
     {
          private IList<IPlayer>  players;
          private IController controler;
          private ClassicConsoleEngine engine;

          public IBoard Board => this.controler.TablaBoard;
          
          public IList<IPlayer> Players  => players;

          public IController Controler  => controler;

          public ClassicConsoleEngine Engine => engine; 

          public void SchemaBasic(int firstDice,int secondDice)
          {
               this.controler = new Controller();
               this.players = CreatePlayers();

               this.controler.TablaBoard.DiceSet[ 1 ].ValueOfOneDice = firstDice;
               this.controler.TablaBoard.DiceSet[ 2 ].ValueOfOneDice = secondDice;

               this.controler.CurrentPlayer = this.Players[ 0 ];

               ServiceCalculate.SetDiceValueAndMovesCount(this.controler.TablaBoard);

               this.engine = new ClassicConsoleEngine(this.Controler,new Writer(),new Reader());
          }
//***************************************************************************

          private IList<IPlayer> CreatePlayers( )
          {
               string firstPlayerName = "AAAAAAAAA";
               string secondPlayerName = "BBBBBBBB";

               this.Controler.Players  = new PlayerFactory().CreatePlayers(firstPlayerName,secondPlayerName);

               var playerList = this.Controler.Players;

               playerList[ 0 ].MyPoolsColor = PoolColor.White;
               playerList[ 1 ].MyPoolsColor = PoolColor.Black;

               foreach ( var item in playerList )
               {
                    //IArrangeChips arrange = new ArrangePoolsScheme();
                    IArrangeChips arrange = new ArrangeOutsideScheme();
                    item.ArrangingTheCheckers( this.controler.TablaBoard,arrange);
               }

               return playerList;
          }

     }
}
