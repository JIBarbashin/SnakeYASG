using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeYASG
{
    public class AiController : Controller
    {
        Apple apple;
        int width, height;

        public AiController(SnakeHead snake, Apple apple, int width, int height) : base(snake)
        {
            this.apple = apple;
            this.width = width;
            this.height = height;
        }

        public override void Update()
        {
            if (snake.Direction == SnakeHead.Directions.LEFT | snake.Direction == SnakeHead.Directions.RIGHT)
            {
                if (snake.Position.X == apple.Position.X)
                {
                    if (snake.Position.Y > apple.Position.Y)
                    {
                        if (snake.Position.Y - apple.Position.Y < height / 2)
                            SetCommand(moveUp);
                        else
                            SetCommand(moveDown);
                    }
                    else
                    {
                        if (apple.Position.Y - snake.Position.Y < height / 2)
                            SetCommand(moveDown);
                        else
                            SetCommand(moveUp);
                    }
                }
            }
            if (snake.Direction == SnakeHead.Directions.UP | snake.Direction == SnakeHead.Directions.DOWN)
            {
                if (snake.Position.Y == apple.Position.Y)
                {
                    if (snake.Position.X > apple.Position.X)
                    {
                        if (snake.Position.X - apple.Position.X < width / 2)
                            SetCommand(moveLeft);
                        else
                            SetCommand(moveRight);
                    }
                    else
                    {
                        if (apple.Position.X - snake.Position.X < width / 2)
                            SetCommand(moveRight);
                        else
                            SetCommand(moveLeft);
                    }
                }
            }

            if (snake.Direction == SnakeHead.Directions.LEFT & snake.IsColliding(snake.Position.X - 1, snake.Position.Y) |
                snake.Direction == SnakeHead.Directions.RIGHT & snake.IsColliding(snake.Position.X + 1, snake.Position.Y))
            {
                if (snake.IsColliding(snake.Position.X, snake.Position.Y + 1))
                {
                    SetCommand(moveUp);
                }
                else
                {
                    SetCommand(moveDown);
                }
            }
            if (snake.Direction == SnakeHead.Directions.UP & snake.IsColliding(snake.Position.X, snake.Position.Y - 1) |
                snake.Direction == SnakeHead.Directions.DOWN & snake.IsColliding(snake.Position.X, snake.Position.Y + 1))
            {
                if (snake.IsColliding(snake.Position.X + 1, snake.Position.Y))
                {
                    SetCommand(moveLeft);
                }
                else
                {
                    SetCommand(moveRight);
                }
            }
            ExecuteCommand();
        }
    }
}
