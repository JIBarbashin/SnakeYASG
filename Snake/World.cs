using System.Windows.Controls;

namespace Snake
{
    internal class World
    {
        public double Speed { get; set; } = 5;
        public double MaxSpeed { get; set; } = 7.5;
        public int Width { get; set; } = 16;
        public int Height { get; set; } = 16;

        private SnakeHead snake;
        private Apple apple;

        private Grid world;
        private bool[,] map;

        public Grid GetWorld
        {
            get { return world; }
        }

        public SnakeHead GetSnake
        {
            get { return snake; }
        }

        public Apple GetApple
        {
            get { return apple; }
        }

        public World(Grid world, bool isPlayer)
        {
            this.world = world;
        }

        public void Create()
        {
            snake = new SnakeHead(this);
            apple = new Apple(this);

            for (int i = 0; i < Width; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                world.ColumnDefinitions.Add(col);
            }
            for (int i = 0; i < Height; i++)
            {
                RowDefinition row = new RowDefinition();
                world.RowDefinitions.Add(row);
            }
            map = new bool[Height, Width];
        }

        public void Clear()
        {
            world.Children.Clear();
            world.RowDefinitions.Clear();
            world.ColumnDefinitions.Clear();

            snake = null;
            apple = null;
        }

        public bool IsFreeCell(int x, int y)
        {
            if (map[y, x])
                return true;
            else
                return false;
        }
    }
}
