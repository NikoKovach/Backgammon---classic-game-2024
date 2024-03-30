namespace TablaModels.Test.TestsTablaGameLogic
{
     using NUnit.Framework;
     using System.Collections.Generic;
     using TablaGameLogic.Core;
     using TablaGameLogic.Core.Contracts;
     using TablaGameLogic.Factory;
     using TablaModels.Components.Interfaces;
     using TablaModels.Enums;

     [TestFixture]
     public class ControllerTest
     {
          private IController controller;

          [TestCase((int)PoolColor.White)]
          public void PlayersChooseAColorMethodReturnCorrectString(int color)
          {
               SetControllerDefaultValue();
               ArrangeFor_PlayersChooseAColorMethod();

               this.controller.PlayersChooseAColor( color );
          }

//***************************************************************

          private void ArrangeFor_PlayersChooseAColorMethod()
          {
               this.controller = new Controller();

               string firstName = "White Jack";
               string secondName = "Black Jack";

               this.controller.Players = new PlayerFactory()
                    .CreatePlayers(firstName,secondName);
          }

          private void SetControllerDefaultValue()
          {
               this.controller = default;
          }
     }
}
