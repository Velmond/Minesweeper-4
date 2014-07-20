namespace Minesweeper.Saving.Contracts
{
    public interface IGameFieldSave
    {
        /// <summary>
        /// Gets or sets the memento that contains a saved game field
        /// </summary>
        GameFieldMemento SavedField { get; set; }
    }
}
