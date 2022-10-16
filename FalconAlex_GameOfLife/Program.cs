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
        }
    }
}

//OPTIONALS

/* TODO: Game Colors. The user should be able to select individual colors for the grid, the background and living cells through a modal dialog box.*/
/* TODO: Universe boundary behavior. The user should choose how the game is going to treat the edges of the universe. The two basic options would be toroidal (the edges wrap around to the other side) or finite (cells outside the universe are considered dead.)*/
/* TODO: Context sensitive menu. Implement a ContextMenuStrip that allows the user to change various options in the application.*/
/* TODO: Heads up display. A heads up display that indicates current generation, cell count, boundary type, universe size and any other information you wish to display. The user should be able to toggle this display on and off through a View menu and a context sensitive menu (if one is implemented as an advanced feature.)*/
/* TODO: Settings. When universe size, timer interval and color options are changed by the user they should persist even after the program has been closed and then opened again. Also, the user should have two menu items Reset and Reload. Reload will revert back to the last saved settings and Reset will return the applications default settings for these values.*/


