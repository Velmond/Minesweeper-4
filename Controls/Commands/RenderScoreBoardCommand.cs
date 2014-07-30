namespace Minesweeper.Controls.Commands
{
    using Minesweeper.Rendering.Contracts;

    public class RenderScoreBoardCommand : RenderCommand
    {
        public RenderScoreBoardCommand(IRenderer renderer)
            : base(renderer)
        {
        }

        public override void Execute()
        {
            this.Renderer.RenderScoreBoard();
        }
    }
}
