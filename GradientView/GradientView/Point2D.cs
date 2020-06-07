using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradientView
{
    class Point2D
    {
        private int _x;
        private int _y;

        public Point2D(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public Point2D()
        {
            _x = 0;
            _y = 0;
        }

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }
    }
}
