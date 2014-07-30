namespace Minesweeper.Controls.Commands
{
    using Minesweeper.Rendering.Contracts;

    class RenderMessageInvalidCommand : RenderCommand
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
