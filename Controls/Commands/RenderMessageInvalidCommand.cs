namespace Minesweeper.Controls.Commands
{
    using Minesweeper.Rendering.Contracts;

    public class RenderMessageInvalidCommand : RenderCommand
    {
        public RenderMessageInvalidCommand(IRenderer renderer)
            : base(renderer)
        {
        }

        public override void Execute()
        {
            this.Renderer.RenderMessageInvalidCommand();
        }
    }
}
