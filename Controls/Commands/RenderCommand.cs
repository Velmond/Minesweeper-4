namespace Minesweeper.Controls.Commands
{
    using Minesweeper.Controls.Contracts;
    using Minesweeper.Rendering.Contracts;

    public abstract class RenderCommand : IRenderCommand
    {
        private IRenderer renderer;

        public RenderCommand(IRenderer renderer)
        {
            this.Renderer = renderer;
        }

        public IRenderer Renderer
        {
            get { return this.renderer; }
            set { this.renderer = value; }
        }

        public abstract void Execute();
    }
}
