namespace SnakeYASG
{
    public class World
    {
        public int Width { get; set; } = 20;
        public int Height { get; set; } = 20;
        public bool Borders { get; set; } = false;

        private SnakeHead snake;
        private Apple apple;

        private char[,] map;
        public char[,] Map { get { return map; } }

        public SnakeHead Snake
        {
            get { return snake; }
        }

        public Apple Apple
        {
            get { return apple; }
        }

        public World()
        {

        }

        public void Create()
        {
            Clear();
            snake = new SnakeHead(Width / 2, Height / 2);
            apple = new Apple();
            map = new char[Height, Width];

            SpawnApple();
        }

        public void Draw()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    map[y, x] = ' ';
                }
            }

            map[apple.Position.Y, apple.Position.X] = apple.Visual;
            map[snake.Position.Y, snake.Position.X] = snake.Visual;
            for (int i = 0; i < snake.SnakeParts.Count; i++)
            {
                map[snake.SnakeParts[i].Position.Y, snake.SnakeParts[i].Position.X] = snake.SnakeParts[i].Visual;
            }
        }

        public void Clear()
        {
            //snake = null;
            //apple = null;
        }

        public void SpawnApple()
        {
            Random random = new();
            Vector2I newPos = new();

            while (true)
            {
                newPos.X = 1 + random.Next(Width - 1);
                newPos.Y = 1 + random.Next(Height - 1);

                if (!snake.IsColliding(newPos.X, newPos.Y))
                    break;
            }

            apple.Position = newPos;
        }

        public void EatApple()
        {
            apple.IncreaseApple();
            SpawnApple();
            snake.IncreaseTail();
        }

        public void CollideSnake()
        { 
            if (snake.IsColliding(apple.Position.X, apple.Position.Y))
                EatApple();

            Vector2I pos;
            if (!Borders)
            {
                for (int i = 0; i < snake.SnakeParts.Count; i++)
                {
                    pos = snake.SnakeParts[i].Position;
                    if (pos.Y < 0) pos.Y = Height - 1;
                    if (pos.X < 0) pos.X = Width - 1;
                    if (pos.Y > Height - 1) pos.Y = 0;
                    if (pos.X > Width - 1) pos.X = 0;
                    snake.SnakeParts[i].Position = pos;
                }

                pos = snake.Position;
                if (pos.Y < 0) pos.Y = Height - 1;
                if (pos.X < 0) pos.X = Width - 1;
                if (pos.Y > Height - 1) pos.Y = 0;
                if (pos.X > Width - 1) pos.X = 0;
                snake.Position = pos;
            }
            else
            {
                pos = snake.Position;
                if (pos.Y < 0 | pos.X < 0 | pos.Y > Height - 1 | pos.X > Width - 1)
                    snake.Death();
            }
        }
    }
}
