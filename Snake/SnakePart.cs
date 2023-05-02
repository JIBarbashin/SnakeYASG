using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake
{
    internal class SnakePart
    {
        public int X { get; set; }
        public int Y { get; set; }
        private Ellipse PartImage = new Ellipse();
        private int size = 16;

        public SnakePart(SnakeHead snake)
        {
            switch (snake.Direction)
            {
                case SnakeHead.Directions.UP:    X = snake.X;  Y = snake.Y + snake.EatenApples + 1; break;
                case SnakeHead.Directions.DOWN:  X = snake.X;  Y = snake.Y - snake.EatenApples + 1; break;
                case SnakeHead.Directions.LEFT:  X = snake.X + snake.EatenApples + 1;  Y = snake.Y; break;
                case SnakeHead.Directions.RIGHT: X = snake.X - snake.EatenApples + 1;  Y = snake.Y; break;
            }

            PartImage.Fill = Brushes.Green;
            PartImage.Width = size;
            PartImage.Height = size;
        }

        public void Draw(Grid world)
        {
            Grid.SetColumn(PartImage, X);
            Grid.SetRow(PartImage, Y);

            if (!world.Children.Contains(PartImage))
                world.Children.Add(PartImage);
        }
    }
}
