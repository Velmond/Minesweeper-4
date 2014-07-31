namespace Minesweeper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.Controls.Commands;
    using Minesweeper.Rendering;
    using Minesweeper.Rendering.Contracts;
    using Minesweeper.Scoring;
    using Minesweeper.Field;

    [TestClass]
    public class RenderCommandsTests
    {
        private RenderCommand rendererCommand;
        private IRenderer renderer;

        [TestInitialize]
        public void Init()
        {
            renderer = new Renderer(new ScoreBoard(), new GameField());
            rendererCommand = new RenderExitApplicationCommand(renderer);
        }

        [TestMethod]
        public void RenderExitApplicationCommandShouldReturnRendererThatWasGivenToItTest()
        {
            Assert.AreEqual(renderer, rendererCommand.Renderer);
        }

        [TestMethod]
        public void RenderMessageInvalidCommandShouldReturnRendererThatWasGivenToItTest()
        {
            rendererCommand = new RenderMessageInvalidCommand(renderer);
            Assert.AreEqual(renderer, rendererCommand.Renderer);
        }

        [TestMethod]
        public void RenderRenderRestoreSaveCommandShouldReturnRendererThatWasGivenToItTest()
        {
            rendererCommand = new RenderRestoreSaveCommand(renderer);
            Assert.AreEqual(renderer, rendererCommand.Renderer);
        }

        [TestMethod]
        public void RenderRenderSaveCommandShouldReturnRendererThatWasGivenToItTest()
        {
            rendererCommand = new RenderSaveCommand(renderer);
            Assert.AreEqual(renderer, rendererCommand.Renderer);
        }

        [TestMethod]
        public void RenderRenderScoreBoardCommandShouldReturnRendererThatWasGivenToItTest()
        {
            rendererCommand = new RenderScoreBoardCommand(renderer);
            Assert.AreEqual(renderer, rendererCommand.Renderer);
        }
    }
}
