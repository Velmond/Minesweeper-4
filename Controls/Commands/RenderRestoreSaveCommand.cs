namespace Minesweeper.Controls.Commands
{
    using Minesweeper.Rendering.Contracts;

    public class RenderRestoreSaveCommand : RenderCommand
    {
        public RenderRestoreSaveCommand(IRenderer renderer)
            : base(renderer)
        {
        }

        public override void Execute()
        {
            this.Renderer.RenderGameField();
        }
    }
}
