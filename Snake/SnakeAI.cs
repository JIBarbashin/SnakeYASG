using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Snake
{
    internal class SnakeAI
    {
        public SnakeHead Snake { get; set; }
        private Apple apple;
        private Grid world;

        public void Control()
        {
            world = Snake.GetWorld.GetWorld;
            apple = Snake.GetWorld.GetApple;

            int appleCol = Grid.GetColumn(apple.AppleImage);
            int appleRow = Grid.GetRow(apple.AppleImage);

            if (appleRow == Snake.Y)
            {
                if (appleCol < Snake.X)
                {
                    Snake.Direction = SnakeHead.Directions.LEFT;
                }
                else
                {
                    Snake.Direction = SnakeHead.Directions.RIGHT;
                }
            }
            else
            {

            }
        }

    }
}
