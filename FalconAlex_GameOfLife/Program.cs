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
/* TODO: Saving the current universe to a text file. The current state and size of the universe should be able to be saved in PlainText file format. The file name should be chosen by the user with a save file dialog box. */
/* TODO: Opening a previously saved universe. A previously saved PlainText file should be able to be read in and assigned to the current universe. Opening should also resize the current universe to match the size of the file being read. */

//OPTIONALS

/* TODO: Importing patterns downloaded from Life Lexicon. PlainText life patterns can be down loaded from the Life Lexicon website and then imported into the current universe. Importing differs from Opening in that the size of he current universe is not changed based on the size of the file being imported. Also, importing will not empty the universe before it read in the file.*/
/* TODO: Game Colors. The user should be able to select individual colors for the grid, the background and living cells through a modal dialog box.*/
/* TODO: Universe boundary behavior. The user should choose how the game is going to treat the edges of the universe. The two basic options would be toroidal (the edges wrap around to the other side) or finite (cells outside the universe are considered dead.)*/
/* TODO: Context sensitive menu. Implement a ContextMenuStrip that allows the user to change various options in the application.*/
/* TODO: Heads up display. A heads up display that indicates current generation, cell count, boundary type, universe size and any other information you wish to display. The user should be able to toggle this display on and off through a View menu and a context sensitive menu (if one is implemented as an advanced feature.)*/
/* TODO: Settings. When universe size, timer interval and color options are changed by the user they should persist even after the program has been closed and then opened again. Also, the user should have two menu items Reset and Reload. Reload will revert back to the last saved settings and Reset will return the applications default settings for these values.*/


