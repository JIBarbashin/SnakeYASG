using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake
{
    public class Apple : GameObject
    {
        private Random random = new();

        public Ellipse AppleImage
        {
            get { return visual; }
        }

        public Apple()
        {
            visual = new Ellipse();
            //AppleImage.Source = new BitmapImage(new Uri("apple.png", UriKind.Relative));
            visual.Fill = Brushes.Red;
            visual.Width = Size;
            visual.Height = Size;
            Init();
        }

        public void Init()
        {
            bool spawned = false;

            while (!spawned)
            {
                //X = 1 + random.Next(world.Width - 1);
                //Y = 1 + random.Next(world.Height - 1);

                //if (!world.GetSnake.IsColliding(X, Y))
                    spawned = true;
                //else
                //    spawned = false;
            }
            SetCoord();
        }
    }
}
