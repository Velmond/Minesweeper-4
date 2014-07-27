namespace Minesweeper.Controls.Contracts
{
    public interface ISaveControls : IBasicControls
    {
        void SaveCommand();
        void RestoreSaveCommand();
        void DisplayScoreBoardCommand();
    }
}
