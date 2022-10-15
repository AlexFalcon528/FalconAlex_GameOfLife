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
        static int universeX = 100;
        static int universeY = 100;
        static int tickTime = 100;
        static bool viewGrid = true;
        static bool viewNeighbors = true;
        // The universe array
        static bool[,] universe = new bool[universeX,universeY];
        static bool[,] scratch = new bool[universeX, universeY];
        public static int CountNeighbors(int x, int y) //Count neighbors in cardinal directions and diags
        {
            int result = 0;
            if (universe != null)
            {
                for (int i = -1; i <= 1; i++) //Loop for y axis
                {
                    for (int j = -1; j <= 1; j++) //Loop for x axis
                    {
                        if (x + j > universeX-1 || y + i > universeY-1 || x + j < 0 || y + i < 0)
                        {
                            continue; //if oob, don't count
                        }
                        else
                        {
                            if (universe[(x + j), (y + i)]) //Count active cells around given cell's coordinates
                            {
                                result++;
                            }
                        }
                    }
                }
                if (universe[x, y] == true) //Remove self from neighbors if active
                {
                    result--;
                }
            }
            return result;
        }
        public static void UpdateState(int x, int y)//Logic for updating the state of the cell
        {
            int count = CountNeighbors(x, y); //Store the count of neighbors for legibility
            if (universe != null)
            {
                if (universe[x, y])                          //Logic for living cells
                {
                    if (count == 2 || count == 3) //A living cell with 2 or 3 neighbors lives on
                    {
                        scratch[x, y] = true;
                    }
                    else //In all other cases, the cell dies;
                    {
                        scratch[x, y] = false;
                    }
                }
                else                                      //Logic for dead cells
                {
                    if (count == 3)//A dead cell with 3 neighbors will be born by reproduction
                    {
                        scratch[x, y] = true;
                    }
                    else
                    {
                        scratch[x, y] = false;
                    }
                }
            }
        }
        public static void UpdateScratch() //Update the states of the cells on the scratch pad
        {
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x ++)
                {
                    UpdateState(x, y);
                }
            }
        }
        public static void CleanScratch() //Reset the values of the scratchpad
        {
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    scratch[x,y] = false;
                }
            }
        }
        public static void UpdateUniverse()//Call UpdateScratch and push the updated states to universe
        {
            UpdateScratch();
            universe = scratch.Clone() as bool[,];
            CleanScratch();
        }
        public static void RandomUniverse()
        {
            Random rand = new Random();
            for (int y = 0; y < universeY; y++)//loop for y axis
            {
                for (int x = 0; x < universeX; x++)//loop for x axis
                {

                    if (rand.Next(3) == 0) //set ~33% of cells to true
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
            timer.Interval = tickTime; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer disabled
        }

        // Calculate the next generation of cells
        private void NextGeneration()//Processes called per tick, or upon clicking the next button
        {
            UpdateUniverse();//Update the universe
            graphicsPanel1.Invalidate();//Call to redraw universe
            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            //Update status strip living cells
            toolStripStatusLabelCells.Text = "Living Cells = " + CountCells().ToString();
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
                    int neighbors = CountNeighbors(x, y);
                    Font font = new Font("Arial", 8f);

                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    if (neighbors != 0 && viewNeighbors)
                    {
                        e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Black, cellRect, stringFormat);
                    }
                    // Outline the cell with a pen
                    if (viewGrid)
                    {
                        e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                    }
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)//Exit button
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e) //Pause
        {
            timer.Enabled = false;
        }

        private void toolStripButton2_Click(object sender, EventArgs e) //Erase
        {
            CleanScratch();
            universe = null;
            NextGeneration();
            generations = 0;
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)//Next
        {
            NextGeneration();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)//Random
        {
            RandomUniverse();
            NextGeneration();
            generations = 0;
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)//Play
        {
            timer.Enabled = true;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
        private int CountCells()//Count living cells in the current universe
        {
            int result = 0;
            for (int y = 0; y < universeY; y++)//Loop for y axis
            {
                for (int x = 0; x < universeX; x++)//Loop for x axis
                {
                    if (universe[x, y])//If the cell is alive, add to the counter of living cells
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        private void toolStripMenuViewGrid_CheckStateChanged(object sender, EventArgs e)
        {
            viewGrid = !viewGrid;//invert view grid bool (defaults to true)
            graphicsPanel1.Invalidate();//Call to redraw universe
        }

        private void toolStripMenuViewNeighbors_CheckStateChanged(object sender, EventArgs e)
        {
            viewNeighbors = !viewNeighbors;//invert view neighbors bool (defaults to true)
            graphicsPanel1.Invalidate();//Call to redraw universe
        }

    }
}
