namespace _Scripts.Tiles
{
    public struct Index
    {
        public int Row { get; }
        public int Column { get; }

        public Index(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}