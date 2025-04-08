namespace SnakeYASG
{
    public struct Vector2I
    {
        public int X = 0, Y = 0;

        public Vector2I(int x, int y)
        {
            X = x; Y = y;
        }
    }

    public class GameObject
    {
        protected Vector2I position = new Vector2I();
        public Vector2I Position { get { return position; } set { position = value; } }

        protected char visual;
        public char Visual { get { return visual; } }
    }
}
