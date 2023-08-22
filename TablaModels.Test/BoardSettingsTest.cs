namespace TablaModels.Test
{
    using System;

    using NUnit.Framework;
    using TablaModels.ComponentModels;

    [TestFixture]
    public class BoardSettingsTest
    {

        [SetUp]
        public void Setup()
        {
        }

        [TestCase(12)]
        [TestCase(540)]
        [TestCase(270)]
        [TestCase(534)]
        [TestCase(18)]
        public void SetSideBoardWidthWithFiveAllowableValuesUsingCtor(int boardWidthValue)
        {
            BoardSettings settings = new BoardSettings(boardWidthValue);

            Assert.AreEqual(boardWidthValue, settings.SideBoardWidth);
        }

        [TestCase(11)]
        [TestCase(541)]
        [TestCase(0)]
        [TestCase(-12)]
        [TestCase(-540)]
        public void PropSideBoardWidthThrowArgumentOutOfRangeExceptionWhenWePassItAnInvalidValue(int boardWidthValue)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new BoardSettings(boardWidthValue));
        }

        [TestCase(13)]
        [TestCase(539)]
        [TestCase(20)]
        [TestCase(533)]
        [TestCase(271)]
        public void PropSideBoardWidthThrowArgumentExceptionWhenWePassItAValueThatIsNotDivisibleBySixWithoutARemainder(int boardWidthValue)
        {
            Assert.Throws<ArgumentException>(() => new BoardSettings(boardWidthValue));
        }

        [TestCase(12)]
        [TestCase(540)]
        [TestCase(270)]
        [TestCase(534)]
        [TestCase(18)]
        public void PropSideBoardHeightGetterReturnCorectValue(int boardWidthValue)
        {
            BoardSettings gameSettings = new BoardSettings(boardWidthValue);

            int expectedBoardHeight = (int)Math.Ceiling(boardWidthValue * TableGlobalConstants.BoardHeightIncreaseFactor);

            Assert.AreEqual(expectedBoardHeight, gameSettings.SideBoardHeight);
        }

        [TestCase(12)]
        [TestCase(540)]
        [TestCase(270)]
        [TestCase(534)]
        [TestCase(18)]
        public void PropColumnBaseGetterReturnCorectValue(int boardWidthValue)
        {
            BoardSettings gameSettings = new BoardSettings(boardWidthValue);

            int expectedColumnBase = (int)gameSettings.SideBoardWidth / TableGlobalConstants.NumberOfColumnsPerWidth;

            Assert.AreEqual(expectedColumnBase, gameSettings.ColumnBase);
        }

        [TestCase(12)]
        [TestCase(540)]
        [TestCase(270)]
        [TestCase(534)]
        [TestCase(18)]
        public void PropColumnHeightGetterReturnCorectValue(int boardWidthValue)
        {
            BoardSettings gameSettings = new BoardSettings(boardWidthValue);

            int expectedColumnHeight = (int)(gameSettings.SideBoardHeight * TableGlobalConstants.ColumnHeightToBoardHeightRatio);

            Assert.AreEqual(expectedColumnHeight, gameSettings.ColumnHeight);
        }

        [TestCase(12)]
        [TestCase(540)]
        [TestCase(270)]
        [TestCase(534)]
        [TestCase(18)]
        public void PropDiceSideGetterReturnCorectValue(int boardWidthValue)
        {
            BoardSettings gameSettings = new BoardSettings(boardWidthValue);

            int expectedDiceSide = (int)(gameSettings.SideBoardWidth * TableGlobalConstants.DiceSideToBoardWidthRatio);

            Assert.AreEqual(expectedDiceSide, gameSettings.DiceSide);
        }

        [TestCase(12)]
        [TestCase(540)]
        [TestCase(270)]
        [TestCase(534)]
        [TestCase(18)]
        public void PropOuterPoolDiameterGetterReturnCorectValue(int boardWidthValue)
        {
            BoardSettings gameSettings = new BoardSettings(boardWidthValue);

            int expectedOuterPoolDiameter = (int)((gameSettings.SideBoardWidth / TableGlobalConstants.NumberOfColumnsPerWidth) * 0.98);

            Assert.AreEqual(expectedOuterPoolDiameter, gameSettings.OuterPoolDiameter);
        }

        [TestCase(12)]
        [TestCase(540)]
        [TestCase(270)]
        [TestCase(534)]
        [TestCase(18)]
        public void PropInnerPoolDiameterGetterReturnCorectValue(int boardWidthValue)
        {
            BoardSettings gameSettings = new BoardSettings(boardWidthValue);

            int expectedInnerPoolDiameter = (int)(gameSettings.OuterPoolDiameter * TableGlobalConstants.InnerDiameterToOuterDiameterRatio);

            Assert.AreEqual(expectedInnerPoolDiameter, gameSettings.InnerPoolDiameter);
        }
    }
}
