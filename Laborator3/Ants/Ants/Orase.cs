using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ants
{
    class Orase
    {
        public int coordonata_X = 0;
        public int coordonata_Y = 0;

        public Orase(int x,int y)
        {
            this.coordonata_X = x;
            this.coordonata_Y = y;
        }

        public int getX()
        {
            return coordonata_X;
        }

        public int getY()
        {
            return coordonata_Y;
        }

        public void setX(int X)
        {
            coordonata_X = X;
        }

        public void setY(int Y)
        {
            coordonata_Y = Y;
        }
    }
}
