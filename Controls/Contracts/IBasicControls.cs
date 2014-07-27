namespace Minesweeper.Controls.Contracts
{
    public interface IBasicControls
    {
        void ExecuteCommand(string command);
        void ExitApplicationCommand();
        void RestartApplicationCommand();
    }
}
