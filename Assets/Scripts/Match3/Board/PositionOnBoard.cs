using System;

namespace Match3.Board
{
    [Serializable]
    public struct PositionOnBoard
    {
        public int x;
        public int y;

        public PositionOnBoard(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}