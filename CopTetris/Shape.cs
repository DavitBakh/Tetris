using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopTetris
{
    internal class Shape
    {
        public Shape(int x, int y)
        {
            X = x;
            Y = y;
            IsMove = true;

            Matrix = new int[3, 3]
            {
                {1,0,0},
                {1,0,0},
                {1,1,0},

            };
            MatrixSize = (int)Math.Sqrt(Matrix.Length);
        }
        public int X { get; set; }
        public int Y { get; set; }

        public int MatrixSize { get; }
        public bool IsMove { get; set; }
        public int[,] Matrix { get; set; }


        public void MoveDown()
        {
            if (Y < 20 - MatrixSize)
                Y++;
        }
        public void MoveRight()
        {
            if (X <= 10 - MatrixSize)
                X++;
        }
        public void MoveLeft()
        {
            if (X > 0)
                X--;
        }
    }
}
