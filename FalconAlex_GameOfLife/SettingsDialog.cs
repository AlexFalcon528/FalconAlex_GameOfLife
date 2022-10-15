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
        public int xSize = 100;
        public int ySize = 100;
        public SettingsDialog()
        {
            InitializeComponent();
            TickTime.Text = Form1.tickTime.ToString();
            XSize.Text = Form1.universeX.ToString();
            YSize.Text = Form1.universeY.ToString();
            SquareArray.Checked = true;
            
        }

        private void SettingsDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            bool isSquare = SquareArray.Checked;
            int.TryParse(TickTime.Text, out tickTime);
            int.TryParse(XSize.Text, out xSize);
            if (isSquare)
            {
                ySize = xSize;
            }
            else
            {
                int.TryParse(YSize.Text, out ySize);
            }
            
        }
    }
}
