using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake
{
    public class SnakePart : GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Ellipse PartImage { get => partImage; }

        private Ellipse partImage = new();

        public SnakePart()
        {
            partImage.Fill = Brushes.Green;
            partImage.Width = Size;
            partImage.Height = Size;
        }

        public void Draw(Grid world)
        {
            if (world.Children.Contains(partImage))
                world.Children.Remove(partImage);

            world.Children.Add(partImage);
        }
    }
}
