namespace Minesweeper.Controls.Commands
{
    using Minesweeper.Rendering.Contracts;

    public class RenderSaveCommand : RenderCommand
    {
        public RenderSaveCommand(IRenderer renderer)
            : base(renderer)
        {
        }

        public override void Execute()
        {
            this.Renderer.RenderSaveDone();
        }
    }
}
