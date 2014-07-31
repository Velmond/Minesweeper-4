namespace Minesweeper.Controls.Commands
{
    using Minesweeper.Rendering.Contracts;

    public class RenderExitApplicationCommand : RenderCommand
    {
        public RenderExitApplicationCommand(IRenderer renderer)
            : base(renderer)
        {
        }

        public override void Execute()
        {
            this.Renderer.RenderApplicationExit();
        }
    }
}
