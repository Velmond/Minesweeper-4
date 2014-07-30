namespace Minesweeper.Contracts
{
    using Minesweeper.Saving;

    public interface IGameField
    {
        Position[,] Field { get; }

        int Revealed { get; }

        void SetNewField();

        void RevealField();

        void RevealPosition(int row, int col);

        GameFieldMemento Save();

        void RestoreFromSave(GameFieldMemento memento);
    }
}
