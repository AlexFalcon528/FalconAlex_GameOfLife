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
    public partial class SettingsDialog : Form
    {
        public int tickTime = 100;
        public int x = 100;
        public int y = 100;
        public SettingsDialog()
        {
            InitializeComponent();
            TickTime.Text = Form1.tickTime.ToString(); //Load current settings to the placeholder texts
            XSize.Text = Form1.universeX.ToString();
            YSize.Text = Form1.universeY.ToString();
            SquareArray.Checked = true; //Squares look better so default to square array
            
        }

        private void SettingsDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            bool isSquare = SquareArray.Checked;
            if (!int.TryParse(TickTime.Text, out tickTime))//Try to read the inputs as ints and output them to their respective variables if possible, else output previous settings
            {
                tickTime = Form1.tickTime;
            }
            if (!int.TryParse(XSize.Text, out x))//Try to read the inputs as ints and output them to their respective variables if possible, else output previous settings
            {
                x = Form1.universeX;
            }
            if (isSquare)
            {
                y = x;
            }
            else
            {
                if (!int.TryParse(YSize.Text, out y))//Try to read the inputs as ints and output them to their respective variables if possible, else output previous settings
                {
                    y = Form1.universeY;
                }
            }
            
        }
    }
}
