namespace CopTetris
{
    public partial class Form1 : Form
    {
        int speed = 1000;
        int widht = 600;
        int heigh = 300;
        int sizeOfSides = 30;
        int[,] map = new int[20, 10];

        Shape shape = new Shape(4, 0);
        public Form1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(500, 642);

            timer.Interval = speed;
            //map[10,5] = 1;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Start();
            Merge();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            GenerateGrid(e.Graphics);
            DrawMap(e.Graphics);
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
        private void ResetMap()
        {
            for (int i = shape.Y; i < shape.Y + shape.MatrixSize; i++)
            {
                for (int j = shape.X; j < shape.X + shape.MatrixSize; j++)
                {
                    if (i >= 0 && j >= 0 && i < 20 && j < 10)
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
                       
                            map[i, j] = shape.Matrix[i - shape.Y, j - shape.X];

                    }
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ResetMap();
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
                    shape.MoveRight();
                    Merge();
                    this.Invalidate();
                    break;
                case Keys.Left:
                    ResetMap();
                    shape.MoveLeft();
                    Merge();
                    this.Invalidate();
                    break;
                case Keys.Down:
                    ResetMap();
                    shape.MoveDown();
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

    }
}