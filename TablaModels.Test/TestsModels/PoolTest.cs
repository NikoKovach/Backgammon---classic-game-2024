namespace TablaModels.Test.TestsModels
{
     using NUnit.Framework;
     using System;

     using TablaModels.Components;
     using TablaModels.Components.Interfaces;
     using TablaModels.Enums;

     [TestFixture]
     public class PoolTest
     {
          [Test]
          public void CreateAPoolObjectWithDefaultCtor()
          {
              IPool pool = new Pool();

              string color = pool.PoolColor.ToString();
              string state = pool.State.ToString();
              int idNumber = pool.IdentityNumber;

              Assert.AreEqual("White", color);
              Assert.AreEqual("Starting", state);
              Assert.AreEqual(1, idNumber);
          }

          [TestCase(PoolColor.Black)]
          [TestCase(PoolColor.White)]
          public void CreateAPoolWithParamPoolColor(PoolColor color)
          {
              IPool pool = new Pool(color, PoolState.Starting, 1);

              Assert.IsNotNull(pool);
              Assert.IsInstanceOf<IPool>(pool);
          }

          [TestCase(PoolState.Starting)]
          [TestCase(PoolState.InGame)]
          [TestCase(PoolState.OnTheBar)]
          [TestCase(PoolState.AtHome)]
          [TestCase(PoolState.OutOfGame)]
          public void CreateAPoolWithParamPoolState(PoolState state)
          {
              IPool pool = new Pool(PoolColor.White, state, 1);

              Assert.IsNotNull(pool);
              Assert.IsInstanceOf<IPool>(pool);
          }

          [TestCase(1)]
          [TestCase(8)]
          [TestCase(15)]
          public void CreateAPoolWithParamIdNumber(int id)
          {
              IPool pool = new Pool(PoolColor.Black, PoolState.Starting, id);

              Assert.IsNotNull(pool);
              Assert.IsInstanceOf<IPool>(pool);
          }

          [TestCase(0)]
          [TestCase(16)]
          [TestCase(-1)]
          [TestCase(-15)]
          public void PropIdentityNumberThrowExceptionWhenWeSetInvalidValue(int id)
          {
              //Pool pool = new Pool(PoolColor.Black, PoolState.Starting, 1);

              Assert.Throws<ArgumentException>(() => new Pool(PoolColor.Black,    PoolState.Starting, id));
          }
     }
}