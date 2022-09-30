using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FalconAlex_GameOfLife
{
   
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            RandomUniverse();
            while (true)
            {
                UpdateUniverse();
            }
        }
        static Cell[,] universe = new Cell[100, 100];
        static Cell[,] scratch = new Cell[100, 100];
        public static int CountNeighbors(int x, int y) //Count neighbors in cardinal directions and diags
        {
            int result = 0;
            for (int i = -1; i <=1; i++) //Loop for y axis
            {
                for (int j = -1; j <=1; j++) //Loop for x axis
                {
                    if (universe[(x+j), (y + i)].isAlive && universe[(x  + j), (y + i)] != universe[x,y]) //Count the cells around (but not including) the given cell's coordinates
                    {
                        result++;
                    }
                }
            }
            return result;
        }
        public static bool UpdateState(int x, int y)//Logic for updating the state of the cell
        {
            bool result;
            int count = CountNeighbors(x,y); //Store the count of neighbors for legibility
            if (universe[x,y].isAlive)                          //Logic for living cells
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
            for (int x = 0; x <= 100; x++)
            {
                for (int y = 0; y <= 100; y++)
                {
                    scratch[x, y].isAlive = UpdateState(x, y);
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
            for (int x = 0; x <= 100; x++)
            {
                for (int y = 0; y <= 100; y++)
                {
                    Random rand = new Random();
                    int r = rand.Next(3);
                    if(r == 0)
                    {
                        universe[x, y].isAlive = true;
                    }
                }
            }
        }
    }
    public class Cell
    {
        public bool isAlive = false; //State of the cell  
    }
}
