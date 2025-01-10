using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;

namespace Snake
{
    public class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; } = 10;

        protected Ellipse visual;

        public void TransCoordToGrid()
        {
            Grid.SetColumn(visual, X);
            Grid.SetRow(visual, Y);
        }

        public void SetCoord()
        {
            Grid.SetColumn(visual, X);
            Grid.SetRow(visual, Y);
        }

        public void Draw(Grid world)
        {
            if (!world.Children.Contains(visual))
                world.Children.Add(visual);
        }
    }
}
