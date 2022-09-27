namespace CopTetris
{
    public partial class Form1 : Form
    {
        int speed = 600;
        int widht = 600;
        int heigh = 300;
        int sizeOfSides = 30;
        int[,] map = new int[20, 10];
        bool isStoped = false;
        int columnCount = 10;
        int rowCount = 20;
        int lines = 0;
        int score = 0;
        Shape shape;
        Shape nextShape;
        
        public Form1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(500, 642);

            

            timer.Interval = speed;

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            shape = new Shape(4, 0);
            nextShape = new Shape(11,4);
            timer.Start();
            Merge();
        }


        private bool CanMoveDown(Shape shape)
        {

            if (shape.IsStoped)
                return false;
            for (int i = shape.Y; i < shape.Y + shape.MatrixSize; i++)
            {
                for (int j = shape.X; j < shape.X + shape.MatrixSize; j++)
                {
                    if (shape.Matrix[i - shape.Y, j - shape.X] != 0 && (i >= rowCount - 1 || map[i + 1, j] != 0))
                    {
                        shape.IsStoped = true;
                        return false;
                    }
                }
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
            DrawShapeGhost(e.Graphics);
            GenerateGrid(e.Graphics);
            DrawMap(e.Graphics);
            DrawNextShape(e.Graphics);
            

            //e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, sizeOfSides, sizeOfSides));
        }

        private void ResetMap()
        {
            for (int i = shape.Y; i < shape.Y + shape.MatrixSize; i++)
            {
                for (int j = shape.X; j < shape.X + shape.MatrixSize; j++)
                {
                    if (shape.Matrix[i - shape.Y, j - shape.X] != 0)
                    {
                        map[i, j] = 0;
                    }
                }
            }
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
                    if (CanMoveDown(shape))
                        shape.MoveDown();
                    Merge();
                    this.Invalidate();
                    break;

                case Keys.Space:
                    ResetMap();
                    while (CanMoveDown(shape))
                    {
                        shape.MoveDown();
                    }
                    Merge();
                    this.Invalidate();
                    break;
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
            CheckEnd();
            CheckLines();

            if (shape.IsStoped)
            {
                shape.IsStoped = false;
                GenerateNewShape();

                ResetMap();
                Merge();

                Invalidate();

                return;
            }


            ResetMap();
            if (CanMoveDown(shape))
            {
               
                shape.MoveDown();
            }
            else CheckEnd();


            Merge();

            this.Invalidate();
        }


        private void CheckLines()
        {
            //if (!isStoped)
            //    return;
            int currentLines = 0;
            for (int i = 0; i < rowCount; i++)
            {
                bool b = true;
                for (int j = 0; j < columnCount; j++)
                {
                    if (map[i, j] == 0)
                        b = false;
                }
                if (b)
                {
                    currentLines++;
                    lines++;
                    for (int j = i; j > 0; j--)
                    {
                        for (int k = 0; k < columnCount; k++)
                        {
                            map[j, k] = map[j - 1, k];
                        }
                    }
                }
            }
            for (int i = 0; i < currentLines; i++)
            {
                score += 10 * (i + currentLines);
            }

        }

        private void GenerateNewShape()
        {
            shape = nextShape;
            shape.X = 4;
            shape.Y = 0;
            nextShape = new Shape(11, 4);
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

                    switch (map[i, j])
                    {
                        case 1:
                            g.FillRectangle(Brushes.Red, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 2:
                            g.FillRectangle(Brushes.HotPink, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 3:
                            g.FillRectangle(Brushes.LightBlue, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 4:
                            g.FillRectangle(Brushes.GreenYellow, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 5:
                            g.FillRectangle(Brushes.LightGreen, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 6:
                            g.FillRectangle(Brushes.Orange, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 7:
                            g.FillRectangle(Brushes.OrangeRed, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                    }
            }
            lbScore.Text = $"Score: {score}";
            lbLines.Text = $"Lines: {lines}";
        }
        
        private void DrawNextShape(Graphics g)
        {
            for (int i = nextShape.Y; i < nextShape.Y + nextShape.MatrixSize; i++)
            {
                for (int j = nextShape.X; j < nextShape.X + nextShape.MatrixSize; j++)
                {
                    if (nextShape.Matrix[i - nextShape.Y, j - nextShape.X] == 0)
                        continue;
                    switch (nextShape.Matrix[i - nextShape.Y, j - nextShape.X])
                    {
                        case 1:
                            
                                g.FillRectangle(Brushes.Red, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 2:
                            g.FillRectangle(Brushes.HotPink, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 3:
                            g.FillRectangle(Brushes.LightBlue, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 4:
                            g.FillRectangle(Brushes.GreenYellow, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 5:
                            g.FillRectangle(Brushes.LightGreen, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 6:
                            g.FillRectangle(Brushes.Orange, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 7:
                            g.FillRectangle(Brushes.OrangeRed, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                    }
                }
            }
        }

        private void DrawShapeGhost(Graphics g)
        {
            Shape shapeGhoast = new Shape(shape.X, shape.Y+2) { Matrix = shape.Matrix };
            shapeGhoast.IsMove = shape.IsMove;         
            
            do 
            {
                shapeGhoast.MoveDown() ;
            } while (CanMoveDown(shapeGhoast)) ;

            for (int i = shapeGhoast.Y; i < shapeGhoast.Y + shapeGhoast.MatrixSize; i++)
            {
                for (int j = shapeGhoast.X; j < shapeGhoast.X + shapeGhoast.MatrixSize; j++)
                {
                    if (shapeGhoast.Matrix[i - shapeGhoast.Y, j - shapeGhoast.X] == 0 || i >= rowCount)
                        continue;
                    switch (shapeGhoast.Matrix[i - shapeGhoast.Y, j - shapeGhoast.X])
                    {
                        //case 1:

                        //    g.DrawRectangle(Pens.Red, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                        //    break;
                        //case 2:
                        //    g.DrawRectangle(Pens.HotPink, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                        //    break;
                        //case 3:
                        //    g.DrawRectangle(Pens.LightBlue, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                        //    break;
                        //case 4:
                        //    g.DrawRectangle(Pens.GreenYellow, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                        //    break;
                        //case 5:
                        //    g.DrawRectangle(Pens.LightGreen, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                        //    break;
                        //case 6:
                        //    g.DrawRectangle(Pens.Orange, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                        //    break;
                        //case 7:
                        //    g.DrawRectangle(Pens.OrangeRed, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                        //    break;
                        case 1:

                            g.FillRectangle(Brushes.Red, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 2:
                            g.FillRectangle(Brushes.HotPink, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 3:
                            g.FillRectangle(Brushes.LightBlue, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 4:
                            g.FillRectangle(Brushes.GreenYellow, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 5:
                            g.FillRectangle(Brushes.LightGreen, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 6:
                            g.FillRectangle(Brushes.Orange, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                        case 7:
                            g.FillRectangle(Brushes.OrangeRed, new Rectangle(j * sizeOfSides, i * sizeOfSides, sizeOfSides, sizeOfSides));
                            break;
                    }
                }
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (!IsIntersects())
                    {
                        ResetMap();
                        shape.Rotate();
                        Merge();
                        this.Invalidate();
                    }

                    break;
            }
        }

        private bool IsIntersects()
        {
            for (int i = shape.Y; i < shape.Y + shape.MatrixSize; i++)
            {
                for (int j = shape.X; j < shape.X + shape.MatrixSize; j++)
                {
                    if (j >= 0 && j <= 7)
                    {
                        if (i >= 20 || (map[i, j] != 0 && shape.Matrix[i - shape.Y, j - shape.X] == 0))
                            return true;
                    }
                }
            }
            return false;
        }

        private void CheckEnd()
        {
            if (isStoped && shape.Y == 0)
            {
                timer.Stop();
              DialogResult = MessageBox.Show("You lose");
                if(DialogResult == DialogResult.OK)
                {
                    this.Close();
                }
            }
                

        }
    }


}
