using System.Windows.Controls;

namespace Snake
{
    internal class SnakeAI
    {
        public SnakeHead Snake { get; set; }
        private Apple apple;
        private World world;
        private Grid grid;

        public void Init()
        {
            world = Snake.GetWorld;
            grid = world.GetWorld;
            apple = world.GetApple;
        }

        public void Control()
        {
            int appleX = Grid.GetColumn(apple.AppleImage);
            int appleY = Grid.GetRow(apple.AppleImage);

            if (Snake.Direction == SnakeHead.Directions.LEFT | Snake.Direction == SnakeHead.Directions.RIGHT)
            {
                if (Snake.X == appleX)
                {
                    if (Snake.Y > appleY)
                    {
                        if (Snake.Y - appleY < world.Height / 2)
                            Snake.Direction = SnakeHead.Directions.UP;
                        else
                            Snake.Direction = SnakeHead.Directions.DOWN;
                    }
                    else
                    {
                        if (appleY - Snake.Y < world.Height / 2)
                            Snake.Direction = SnakeHead.Directions.DOWN;
                        else
                            Snake.Direction = SnakeHead.Directions.UP;
                    }
                }
            }
            if (Snake.Direction == SnakeHead.Directions.UP | Snake.Direction == SnakeHead.Directions.DOWN)
            {
                if (Snake.Y == appleY)
                {
                    if (Snake.X > appleX)
                    {
                        if (Snake.X - appleX < world.Width / 2)
                            Snake.Direction = SnakeHead.Directions.LEFT;
                        else
                            Snake.Direction = SnakeHead.Directions.RIGHT;
                    }
                    else
                    {
                        if (appleX - Snake.X < world.Width / 2)
                            Snake.Direction = SnakeHead.Directions.RIGHT;
                        else
                            Snake.Direction = SnakeHead.Directions.LEFT;
                    }
                }
            }

            if ((Snake.Direction == SnakeHead.Directions.LEFT & Snake.IsColliding(Snake.X - 1, Snake.Y)) |
                (Snake.Direction == SnakeHead.Directions.RIGHT & Snake.IsColliding(Snake.X + 1, Snake.Y)))
            {
                if (Snake.IsColliding(Snake.X, Snake.Y + 1))
                {
                    Snake.Direction = SnakeHead.Directions.UP;
                }
                else
                {
                    Snake.Direction = SnakeHead.Directions.DOWN;
                }
            }
            else if ((Snake.Direction == SnakeHead.Directions.UP & Snake.IsColliding(Snake.X, Snake.Y - 1)) |
                (Snake.Direction == SnakeHead.Directions.DOWN & Snake.IsColliding(Snake.X, Snake.Y + 1)))
            {
                if (Snake.IsColliding(Snake.X + 1, Snake.Y))
                {
                    Snake.Direction = SnakeHead.Directions.LEFT;
                }
                else
                {
                    Snake.Direction = SnakeHead.Directions.RIGHT;
                }
            }
        }
    }
}
