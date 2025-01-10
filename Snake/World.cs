using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Snake
{
    public class World
    {
        public double MaxSpeed { get; set; } = 25;
        public double MinSpeed { get; set; } = 0.5;
        public int Width { get; set; } = 50;
        public int Height { get; set; } = 50;
        public bool Borders { get; set; } = false;
        public bool IsPlayer { get; set; }

        private SnakeHead snake;
        private Apple apple;

        private Grid worldGrid;
        private bool[,] map;

        public Grid WorldGrid
        {
            get { return worldGrid; }
            set {  worldGrid = value; }
        }

        public SnakeHead GetSnake
        {
            get { return snake; }
        }

        public Apple GetApple
        {
            get { return apple; }
        }

        public World(bool IsPlayer)
        {
            this.IsPlayer = IsPlayer;
        }

        public void Create()
        {
            snake = new SnakeHead(Width / 2, Height / 2);
            apple = new Apple();

            for (int i = 0; i < Width; i++)
            {
                ColumnDefinition col = new();
                worldGrid.ColumnDefinitions.Add(col);
            }
            for (int i = 0; i < Height; i++)
            {
                RowDefinition row = new();
                worldGrid.RowDefinitions.Add(row);
            }
            map = new bool[Height, Width];
        }

        public void Clear()
        {
            worldGrid.Children.Clear();
            worldGrid.RowDefinitions.Clear();
            worldGrid.ColumnDefinitions.Clear();

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

        public void CollideSnake()
        {
            for (int i = 0; i < snake.SnakeParts.Count; i++)
            {
                if (snake.X == snake.SnakeParts[i].X & snake.Y == snake.SnakeParts[i].Y)
                    snake.Death(i);
            }
            if (snake.X == GetApple.X & snake.Y == GetApple.Y)
                snake.Eat(ref apple);

            if (!Borders)
            {
                for (int i = 0; i < snake.SnakeParts.Count; i++)
                {
                    if (snake.SnakeParts[i].Y < 0) snake.SnakeParts[i].Y = Height - 1;
                    if (snake.SnakeParts[i].X < 0) snake.SnakeParts[i].X = Width - 1;
                    if (snake.SnakeParts[i].Y > Height - 1) snake.SnakeParts[i].Y = 0;
                    if (snake.SnakeParts[i].X > Width - 1) snake.SnakeParts[i].X = 0;
                }

                if (snake.Y < 0) snake.Y = Height - 1;
                if (snake.X < 0) snake.X = Width - 1;
                if (snake.Y > Height - 1) snake.Y = 0;
                if (snake.X > Width - 1) snake.X = 0;
            }
        }
    }
}
