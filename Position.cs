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

        public Position(char value, bool isHidden, bool isBomb)
        {
            this.IsBomb = isBomb;
            this.IsHidden = isHidden;
            this.Value = value;
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
            // If position is not hidden it already has a value which should not be able to change
            if (this.IsHidden)
            {
                this.IsHidden = false;
                this.Value = hiddenValue;
            }
        }
    }
}
