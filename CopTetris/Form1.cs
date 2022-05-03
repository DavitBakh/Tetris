namespace CopTetris
{
    public partial class Form1 : Form
    {
        int speed = 800;
        int widht = 600;
        int heigh = 300;
        int sizeOfSides = 30;
        int[,] map = new int[20, 10];
        bool isStoped = false;
        int columnCount = 10;
        int rowCount = 20;
        Shape shape;
        public Form1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(500, 642);

            shape = new Shape(4, 0);

            timer.Interval = speed;

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Start();
            Merge();
        }


        private bool CanMoveDown()
        {
            if (isStoped)
                return false;
            for (int i = shape.Y; i < shape.Y + shape.MatrixSize; i++)
            {
                for (int j = shape.X; j < shape.X + shape.MatrixSize; j++)
                {
                    //Check move down
                    if (shape.Y + shape.MatrixSize < rowCount
                        && shape.Matrix[i - shape.Y, j - shape.X] != 0
                        && map[i + 1, j] != 0)
                    {
                        isStoped = true;
                        return false;
                    }
                }
            }
            if (shape.Y + shape.MatrixSize == rowCount)
            {
                isStoped = true;
                return false;
            }
            return true;
        }
        private bool CanMoveRight()
        {
            if (isStoped)
                return false;
            for (int i = shape.Y; i < shape.Y + shape.MatrixSize; i++)
            {
                for (int j = shape.X; j < shape.X + shape.MatrixSize; j++)
                {
                    if (shape.Matrix[i - shape.Y, j - shape.X] != 0 && (j >= columnCount - 1 || map[i, j + 1] != 0))
                        return false;
                }
            }
            return true;
        }
        public bool CanMoveLeft()
        {
            if (isStoped)
                return false;
            for (int i = shape.Y; i < shape.Y + shape.MatrixSize; i++)
            {
                for (int j = shape.X; j < shape.X + shape.MatrixSize; j++)
                {                   
                    if (shape.Matrix[i - shape.Y, j - shape.X] != 0 && (j - 1 < 0 || map[i, j - 1] != 0))
                        return false;
                }
            }
            return true;
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            GenerateGrid(e.Graphics);
            DrawMap(e.Graphics);
        }

        private void ResetMap()
        {
            for (int i = shape.Y; i < shape.Y + shape.MatrixSize; i++)
            {
                for (int j = shape.X; j < shape.X + shape.MatrixSize; j++)
                {
                    if (shape.Matrix[i - shape.Y, j - shape.X] == 1)
                    {
                        map[i, j] = 0;
                    }
                }
            }
        }

        private void Merge()
        {
            for (int i = shape.Y; i < shape.Y + shape.MatrixSize; i++)
            {
                for (int j = shape.X; j < shape.X + shape.MatrixSize; j++)
                {
                    if (i >= 0 && j >= 0 && i < rowCount && j < columnCount)
                    {
                        if (shape.Matrix[i - shape.Y, j - shape.X] != 0)
                            map[i, j] = shape.Matrix[i - shape.Y, j - shape.X];

                    }
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (isStoped)
            {
                GenerateNewShape();
                Merge();
                isStoped = false;
            }

            ResetMap();
            if (CanMoveDown())
                shape.MoveDown();
            Merge();
            this.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    ResetMap();
                    if (CanMoveRight())
                        shape.MoveRight();
                    Merge();
                    this.Invalidate();
                    break;

                case Keys.Left:
                    ResetMap();
                    if (CanMoveLeft())
                        shape.MoveLeft();
                    Merge();
                    this.Invalidate();
                    break;

                case Keys.Down:
                    ResetMap();
                    if (CanMoveDown())
                        shape.MoveDown();
                    Merge();
                    this.Invalidate();
                    break;

                case Keys.Space:
                    ResetMap();
                    while (CanMoveDown())
                    {
                        shape.Y++;
                    }
                    Merge();
                    this.Invalidate();
                    break;
                case Keys.Up:
                    ResetMap();
                    shape.Rotate();
                    Merge();
                    this.Invalidate();
                    break;
            }
        }

        private void GenerateNewShape()
        {
            shape = new Shape(4, 0);
        }
        private void GenerateGrid(Graphics g)
        {
            for (int i = 0; i <= rowCount; i++)
            {
                g.DrawLine(Pens.Black, new Point(0, i * sizeOfSides), new Point(heigh, i * sizeOfSides));
            }
            for (int i = 0; i <= columnCount; i++)
            {
                g.DrawLine(Pens.Black, new Point(i * sizeOfSides, 0), new Point(i * sizeOfSides, widht));

            }
        }

        private void DrawMap(Graphics g)
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if (map[i, j] != 0)
                        g.FillRectangle(Brushes.Red, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                    //switch (map[i,j])
                    //{
                    //    case 1:
                    //        g.FillRectangle(Brushes.Red, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                    //        break;
                    //    case 2:
                    //        g.FillRectangle(Brushes.Yellow, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                    //        break;
                    //    case 3:
                    //        g.FillRectangle(Brushes.Blue, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                    //        break;
                    //    case 4:
                    //        g.FillRectangle(Brushes.DarkGreen, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                    //        break;
                    //    case 5:
                    //        g.FillRectangle(Brushes.Orange, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                    //        break;
                    //}
                }
            }
        }


    }
}