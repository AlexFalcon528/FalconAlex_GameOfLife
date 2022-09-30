using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FalconAlex_GameOfLife
{
    public partial class Form1 : Form
    {
        // The universe array
        static bool[,] universe = new bool[100, 100];
        static bool[,] scratch = new bool[100, 100];
        public static int CountNeighbors(int x, int y) //Count neighbors in cardinal directions and diags
        {
            int result = 0;
            for (int i = -1; i <= 1; i++) //Loop for y axis
            {
                for (int j = -1; j <= 1; j++) //Loop for x axis
                {
                    if (x + j > 99 || y + i > 99 || x+j<0 || y+i<0)
                    {
                        continue;
                    }
                    else
                    { 
                        if (universe[(x + j), (y + i)] && universe[(x + j), (y + i)] != universe[x, y]) //Count the cells around (but not including) the given cell's coordinates
                        {
                            result++;
                        }
                    }
                }
            }
            return result;
        }
        public static bool UpdateState(int x, int y)//Logic for updating the state of the cell
        {
            bool result;
            int count = CountNeighbors(x, y); //Store the count of neighbors for legibility
            if (universe[x, y])                          //Logic for living cells
            {
                if (count < 2) //A living cell with less than 2 neighbors dies by under-population
                {
                    result = false;
                }
                else if (count > 3)//A living cell with more than 3 neighbors dies by over-population
                {
                    result = false;
                }
                else //A living cell with 2 or 3 neighbors lives on
                {
                    result = true;
                }
            }
            else                                      //Logic for dead cells
            {
                if (count == 3)//A dead cell with 3 neighbors will be born by reproduction
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
        public static void UpdateScratch() //Update the states of the cells on the scratch pad
        {
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    scratch[x, y] = UpdateState(x, y);
                }
            }
        }
        public static void UpdateUniverse()//Call UpdateScratch and push the updated states to universe
        {
            UpdateScratch();
            universe = scratch;

        }
        public static void RandomUniverse()
        {
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    Random rand = new Random();
                    int r = rand.Next(3);
                    if (r == 0)
                    {
                        universe[x, y] = true;
                    }
                }
            }
        }
   

// Drawing colors
Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        public Form1()
        {
            InitializeComponent();
            RandomUniverse();
            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            UpdateUniverse();
            graphicsPanel1.Invalidate();
            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);
            Brush neighborBrush = new SolidBrush(Color.Red);
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }
                    int count = CountNeighbors(x, y);
                    if (count != 0)
                    {
                        e.Graphics.DrawString(CountNeighbors(x, y).ToString(), DefaultFont, neighborBrush, new PointF(cellRect.Left, cellRect.Top);
                    }
                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
        }


    }
}
