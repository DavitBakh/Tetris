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
            GenerateMatrix();
            MatrixSize = (int)Math.Sqrt(Matrix.Length);
        }
        public int X { get; set; }
        public int Y { get; set; }

        public int MatrixSize { get; }
        public bool IsMove { get; set; }
        public int[,] Matrix { get; set; }

        public void GenerateMatrix()
        {
            Random random = new Random();
            switch (random.Next(1,8))
            {
                case 1:
                    Matrix = new int[3, 3]
                    {
                        {1,0,0 },
                        {1,0,0 },
                        {1,1,0},
                    };
                    break;
                case 2:
                    Matrix = new int[3, 3]
                    {
                        {2,2,2 },
                        {0,2,0 },
                        {0,0,0},
                    };
                    break;
                case 3:
                    Matrix = new int[4, 4]
                    {
                        {0,0,3,0 },
                        {0,0,3,0 },
                        {0,0,3,0},
                        {0,0,3,0 }
                    };
                    break;
                case 4:
                    Matrix = new int[2, 2]
                    {
                        {4,4,},
                        {4,4,},
                        
                    };
                    break;
                case 5:
                    Matrix = new int[3, 3]
                    {
                        {0,5,0 },
                        {0,5,5 },
                        {0,0,5},
                    };
                    break;
                case 6:
                    Matrix = new int[3, 3]
                    {
                        {0,0,6 },
                        {0,0,6 },
                        {0,6,6},
                    };
                    break;
                case 7:
                    Matrix = new int[3, 3]
                    {
                        {0,7,0 },
                        {7,7,0 },
                        {7,0,0},
                    };
                    break;
            }
        }
        public void Rotate()
        {
           
            int[,] tempMatrix = new int[MatrixSize, MatrixSize];
            for (int i = 0; i < MatrixSize; i++)
            {
                int k = 0;
                for (int j = MatrixSize - 1; j >= 0; j--)
                {
                    tempMatrix[i, k] = Matrix[j, i];
                    k++;
                }
            }
            Matrix = tempMatrix;
            int offset = (10 - (X + MatrixSize));
            if (offset < 0)
            {
                for (int i = 0; i < Math.Abs(offset); i++)
                    MoveLeft();
            }

            if (X < 0)
            {
                for (int i = 0; i < Math.Abs(X) + 1; i++)
                    MoveRight();
            }
          
        }
        public void MoveDown()
        {
                Y++;
        }
        public void MoveRight()
        {
           
                X++;
        }
        public void MoveLeft()
        {
           
                X--;
        }
    }
}
