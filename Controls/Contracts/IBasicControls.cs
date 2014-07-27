namespace Minesweeper.Controls.Contracts
{
    using Rendering.Contracts;

    public interface IBasicControls
    {
        void ExecuteCommand (string command);
        void ExitApplicationCommand ();
        void RestartApplicationCommand ();
    }
}
