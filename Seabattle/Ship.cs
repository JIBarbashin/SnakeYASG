using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seabattle
{
    enum ShipType { SingleDeck = 1, TwoDeck = 2, ThreeDeck = 3, FourDeck = 4 };
    enum ShipOrientation { Vertical, Horizontal };

    class ShipPart
    {
        public int X { get; set; }
        public int Y { get; set; }

        public ShipPart(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void Draw()
        {

        }

        public void Destroy()
        {

        }
    }

    internal class Ship
    {
        private ShipType shipType;
        private ShipOrientation shipOrientation;

        private List<ShipPart> ships = new List<ShipPart>();

        public int StartX { get; set; }
        public int StartY { get; set; }

        public Ship(ShipType type, ShipOrientation orientation, int X, int Y)
        {
            shipType = type;
            shipOrientation = orientation;
            StartX = X;
            StartY = Y;
        }

        private void CreateShip()
        {
            for (int i = 0; i < (int)shipType; i++)
            {
                if (shipOrientation == ShipOrientation.Vertical)
                    ships.Add(new ShipPart(StartX, StartY + i));
                else
                    ships.Add(new ShipPart(StartX + i, StartY));
            }
        }

        private void Destroy(ShipPart part)
        {
            ships.Remove(part);
            if (ships.Count <= 0)
            {

            }
        }
    }
}
