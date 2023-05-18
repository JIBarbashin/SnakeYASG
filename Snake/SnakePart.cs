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
        private int size = 10;

        public SnakePart(SnakeHead snake)
        {
            PartImage.Fill = Brushes.Green;
            PartImage.Width = size;
            PartImage.Height = size;
        }

        public void TransCoordToGrid()
        {
            Grid.SetColumn(PartImage, X);
            Grid.SetRow(PartImage, Y);
        }

        public void Draw(Grid world)
        {
            if (world.Children.Contains(PartImage))
                world.Children.Remove(PartImage);

            world.Children.Add(PartImage);
        }
    }
}
