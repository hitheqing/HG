namespace AStar
{
    public class Node
    {
        public Node(int x, int y, bool b = true)
        {
            X = x;
            Y = y;
            Walkable = b;
        }
        public int X;
        public int Y;
        
        public bool Walkable = true;
        
        public Node Parent;
        public int Fn => Gn + Hn;
        public int Gn;
        public int Hn;

        public bool PosEqual(Node other)
        {
            return X == other.X && Y == other.Y;
        }

        public override string ToString()
        {
            return "x=" + X + "    y=" + Y;
        }
    }
}