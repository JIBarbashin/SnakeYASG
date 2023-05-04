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
        private int size = 32;

        public SnakePart(SnakeHead snake)
        {
            PartImage.Fill = Brushes.Green;
            PartImage.Width = size;
            PartImage.Height = size;
        }

        public void Draw(Grid world)
        {
            if (world.Children.Contains(PartImage))
                world.Children.Remove(PartImage);

            Grid.SetColumn(PartImage, X);
            Grid.SetRow(PartImage, Y);

            world.Children.Add(PartImage);
        }
    }
}
