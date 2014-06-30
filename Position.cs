namespace Minesweeper
{
    public class Position
    {
        private bool isBomb;
        private bool isHidden;
        private char value;

        public Position()
        {
            this.IsBomb = false;
            this.IsHidden = true;
            this.Value = '?';
        }

        public bool IsBomb
        {
            get { return this.isBomb; }
            private set { this.isBomb = value; }
        }

        public bool IsHidden
        {
            get { return this.isHidden; }
            private set { this.isHidden = value; }
        }

        public char Value
        {
            get { return this.value; }
            private set { this.value = value; }
        }

        public void MakeBomb()
        {
            this.IsBomb = true;
        }

        public void Reveal(char hiddenValue)
        {
            this.IsHidden = false;
            this.Value = hiddenValue;
        }
    }
}
