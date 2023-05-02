using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Snake
{
    internal class Apple
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Ellipse AppleImage = new Ellipse();
        private int size = 16;
        public int Score { get; set; }
        private Random random = new Random();

        private World world;

        public Apple(World world)
        {
            this.world = world;
            //AppleImage.Source = new BitmapImage(new Uri("apple.png", UriKind.Relative));
            AppleImage.Fill = Brushes.Red;
            AppleImage.Width = size;
            AppleImage.Height = size;
            Init();
        }

        public void Init()
        {
            X = 1 + random.Next(32);
            Y = 1 + random.Next(32);
            Score = 1;
            Draw();
        }

        public void Draw()
        {
            Grid.SetColumn(AppleImage, X);
            Grid.SetRow(AppleImage, Y);

            if (!world.GetWorld.Children.Contains(AppleImage))
                world.GetWorld.Children.Add(AppleImage);
        }
    }
}
