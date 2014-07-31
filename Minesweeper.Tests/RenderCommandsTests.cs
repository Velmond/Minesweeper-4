namespace Minesweeper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.Controls.Commands;
    using Minesweeper.Field;
    using Minesweeper.Rendering;
    using Minesweeper.Rendering.Contracts;
    using Minesweeper.Scoring;

    [TestClass]
    public class RenderCommandsTests
    {
        private RenderCommand rendererCommand;
        private IRenderer renderer;

        [TestInitialize]
        public void Init()
        {
            this.renderer = new ConsoleRenderer(new ScoreBoard(), new GameField());
            this.rendererCommand = new RenderExitApplicationCommand(this.renderer);
        }

        [TestMethod]
        public void RenderExitApplicationCommandShouldReturnRendererThatWasGivenToItTest()
        {
            Assert.AreEqual(this.renderer, this.rendererCommand.Renderer);
        }

        [TestMethod]
        public void RenderMessageInvalidCommandShouldReturnRendererThatWasGivenToItTest()
        {
            this.rendererCommand = new RenderMessageInvalidCommand(this.renderer);
            Assert.AreEqual(this.renderer, this.rendererCommand.Renderer);
        }

        [TestMethod]
        public void RenderRenderRestoreSaveCommandShouldReturnRendererThatWasGivenToItTest()
        {
            this.rendererCommand = new RenderRestoreSaveCommand(this.renderer);
            Assert.AreEqual(this.renderer, this.rendererCommand.Renderer);
        }

        [TestMethod]
        public void RenderRenderSaveCommandShouldReturnRendererThatWasGivenToItTest()
        {
            this.rendererCommand = new RenderSaveCommand(this.renderer);
            Assert.AreEqual(this.renderer, this.rendererCommand.Renderer);
        }

        [TestMethod]
        public void RenderRenderScoreBoardCommandShouldReturnRendererThatWasGivenToItTest()
        {
            this.rendererCommand = new RenderScoreBoardCommand(this.renderer);
            Assert.AreEqual(this.renderer, this.rendererCommand.Renderer);
        }
    }
}
