﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FalconAlex_GameOfLife
{
    public partial class Form1 : Form
    {
        public static int universeX;
        public static int universeY;
        public static int tickTime;
        static bool viewGrid = true;
        static bool viewNeighbors = true;
        static bool viewHUD = true;
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
            for (int y = 0; y < universeY; y++)
            {
                for (int x = 0; x < universeX; x ++)
                {
                    UpdateState(x, y);
                }
            }
        }
        public static void CleanScratch() //Reset the values of the scratchpad
        {
            for (int y = 0; y < universeY; y++)
            {
                for (int x = 0; x < universeX; x++)
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
            //Read in settings
            universeX = Properties.Settings.Default.XSize;
            universeY = Properties.Settings.Default.YSize;
            tickTime = Properties.Settings.Default.TickTime;
            InitializeComponent();
            // Setup the timer
            timer.Interval = tickTime; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer disabled
            universe = new bool[universeX, universeY];
            scratch = new bool[universeX, universeY];
            UpdateHUD();
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
            HUDGen.Text = "Gen: " + generations.ToString();
            //Update status strip living cells
            toolStripStatusLabelCells.Text = "Living Cells = " + CountCells().ToString();
            HUDCells.Text = "Cells: " + CountCells().ToString();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            timer.Interval = tickTime;
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
            timer.Interval = tickTime;
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

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsDialog dlg = new SettingsDialog();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                tickTime = dlg.tickTime;
                if(universeX != dlg.x||universeY != dlg.y)
                {
                    universeX = dlg.x;
                    universeY = dlg.y;
                    universe = null; //Delete the old arrays and make new ones with the new size options
                    universe = new bool[universeX, universeY];
                    scratch = null;
                    scratch = new bool[universeX, universeY];
                    
                }
                UpdateHUD();
                graphicsPanel1.Invalidate();
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.TickTime = tickTime;//Update Settings.Settings
            Properties.Settings.Default.XSize = universeX;
            Properties.Settings.Default.YSize = universeY;
            Properties.Settings.Default.Save();//Save new settings
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2; dlg.DefaultExt = "cells";


            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamWriter writer = new StreamWriter(dlg.FileName);

                for (int y = 0; y < universeY; y++)
                {
                    // Create a string to represent the current row.
                    String currentRow = string.Empty;
                    for (int x = 0; x < universeX; x++)
                    {
                        if (universe[x, y])
                        {
                            currentRow += 'O'; //Use capital O to represent living cells
                        }
                        else
                        {
                            currentRow += '.';//Period to represent dead cells
                        }
                    }
                    writer.WriteLine(currentRow);
                }

                writer.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamReader reader = new StreamReader(dlg.FileName);
                int maxWidth = 0;
                int maxHeight = 0;
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();
                    if(row.First() != '!')
                    {
                        maxHeight++;
                    }
                    if(maxWidth != row.Length)
                    {
                        maxWidth = row.Length;
                    }
                }

                // Resize the current universe and scratchPad
                // to the width and height of the file calculated above.
                universeX = maxWidth;
                universeY = maxHeight;
                universe = null;
                scratch = null;
                universe = new bool[universeX, universeY];
                scratch = new bool[universeX, universeY];
                // Reset the file pointer back to the beginning of the file.
                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                int yPos = 0;
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();
                    if (row.First() != '!')
                    {
                        for (int xPos = 0; xPos < row.Length; xPos++)
                        {
                            if(row[xPos] == 'O')
                            {
                                universe[xPos, yPos] = true;
                            }
                            else
                            {
                                universe[xPos, yPos] = false;
                            }
                        }
                        yPos++;
                    }

                }
                reader.Close();
                graphicsPanel1.Invalidate();
                UpdateHUD();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) //this is titled new but is actually import. I realized too late and changing it now would break everything
        {
            OpenFileDialog dlg = new OpenFileDialog(); //Open a dialog to select a file
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;

            if (DialogResult.OK == dlg.ShowDialog()) //If confirmed with ok button
            {
                StreamReader reader = new StreamReader(dlg.FileName);
                int maxWidth = 0;
                int maxHeight = 0;
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();
                    if (row.First() != '!')
                    {
                        maxHeight++;//count height
                    }
                    if (maxWidth < row.Length)
                    {
                        maxWidth = row.Length;//take longest row
                    }
                }

                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                int yPos = 0;
                while (!reader.EndOfStream && yPos<universeY)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine(); //read file again for alive/dead cells
                    if (row.First() != '!')
                    {
                        for (int xPos = 0; xPos < row.Length; xPos++)
                        {
                            if (xPos < universeX)
                            {
                                if (row[xPos] == 'O')
                                {
                                    universe[xPos, yPos] = true;//transscribe data to universe
                                }
                                else
                                {
                                    universe[xPos, yPos] = false;
                                }
                            }
                        }
                        yPos++;
                    }

                }
                reader.Close();//close file, redraw universe, and update hub
                graphicsPanel1.Invalidate();
                UpdateHUD();
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Reload to settings from when the app was previously closed
            universeX = Properties.Settings.Default.XSize;
            universeY = Properties.Settings.Default.YSize;
            tickTime = Properties.Settings.Default.TickTime;
            universe = null; //Delete the old arrays and make new ones with the new size options
            universe = new bool[universeX, universeY];
            scratch = null;
            scratch = new bool[universeX, universeY];
            graphicsPanel1.Invalidate();
            UpdateHUD();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Reset to default settings
            universeX = 100;
            universeY = 100;
            tickTime = 100;
            universe = null; //Delete the old arrays and make new ones with the new size options
            universe = new bool[universeX, universeY];
            scratch = null;
            scratch = new bool[universeX, universeY];
            graphicsPanel1.Invalidate();
            UpdateHUD();
        }
        private void UpdateHUD()//Call this when changing settings to update the HUD
        {
            if (viewHUD&&HUDSize !=null && HUDTickTime !=null)
            {
                HUDSize.Text = universeX.ToString() + ", " + universeY.ToString();
                HUDTickTime.Text = tickTime.ToString() + "ms";
            }
        }


        private void HUDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewHUD = !viewHUD;//invert view HUD bool (defaults to true)
            if (viewHUD)
            {
                HUDPanel.Visible = true;
            }
            else
            {
                HUDPanel.Visible = false;
            }
        }
    }
}
