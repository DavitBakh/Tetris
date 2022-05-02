namespace CopTetris
{
    public partial class Form1 : Form
    {
        int speed = 1000;
        int widht = 600;
        int heigh = 300;
        int sizeOfSides = 30;
        int[,] map = new int[20, 10];
        bool isStoped = false;

        Shape shape = new Shape(4, 0);
        public Form1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(500, 642);

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
                    if (shape.Y + shape.MatrixSize < 20
                        && shape.Matrix[i - shape.Y, j - shape.X] != 0
                        && map[i + 1, j] != 0)
                    {
                        isStoped = true;
                        return false;
                    }
                }
            }
            if (shape.Y + shape.MatrixSize == 20)
            {
                isStoped=true;
                return false;
            }
            return true;
        }
        private bool CanMoveToTheSides()
        {
            if (isStoped)
                return false;
            for (int i = shape.Y; i < shape.Y + shape.MatrixSize; i++)
            {
                for (int j = shape.X; j < shape.X + shape.MatrixSize; j++)
                {
                    if (shape.X + shape.MatrixSize < 10 && shape.Matrix[i - shape.Y, j - shape.X] != 0 && map[i, j + 1] != 0)
                        return false;
                    if (shape.X > 0 && shape.Matrix[i - shape.Y, j - shape.X] != 0 && map[i, j - 1] != 0)
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
                    if (i >= 0 && j >= 0 && i < 20 && j < 10)
                    {
                        if (shape.Matrix[i - shape.Y, j - shape.X] != 0)
                            map[i, j] = shape.Matrix[i - shape.Y, j - shape.X];

                    }
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
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
                    if (CanMoveToTheSides())
                        shape.MoveRight();
                    Merge();
                    this.Invalidate();
                    break;
                case Keys.Left:
                    ResetMap();
                    if (CanMoveToTheSides())
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
                    shape.Y = 20 - shape.MatrixSize;
                    Merge();
                    this.Invalidate();
                    break;
            }
        }

        private void GenerateGrid(Graphics g)
        {
            //for (int i = 0; i <= widht / sizeOfSides; i++)
            //{
            //    this.Controls.Add(new PictureBox()
            //    {
            //        BackColor = Color.Black,
            //        Location = new System.Drawing.Point(0, i * sizeOfSides),
            //        Size = new System.Drawing.Size(heigh, 1)
            //    });
            //}
            //for (int i = 0; i <= heigh / sizeOfSides; i++)
            //{
            //    this.Controls.Add(new PictureBox()
            //    {
            //        BackColor = Color.Black,
            //        Size = new System.Drawing.Size(1, widht),
            //        Location = new System.Drawing.Point(i * sizeOfSides, 0),
            //    });
            //}
            for (int i = 0; i <= 20; i++)
            {
                g.DrawLine(Pens.Black, new Point(0, i * sizeOfSides), new Point(heigh, i * sizeOfSides));
            }
            for (int i = 0; i <= 10; i++)
            {
                g.DrawLine(Pens.Black, new Point(i * sizeOfSides, 0), new Point(i * sizeOfSides, widht));

            }
        }

        private void DrawMap(Graphics g)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (map[i, j] == 1)
                        g.FillRectangle(Brushes.Red, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                }
            }
        }


    }
}