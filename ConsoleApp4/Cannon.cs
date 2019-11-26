using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp4
{
    class Cannon
    {
        public PictureBox cannonPicture;
        public PictureBox carriagePicture;
        public double angle;
        public bool IsAlive;
        public Image ImageSource;

        public Cannon()
        {
            IsAlive = true;
            cannonPicture = new PictureBox();
            carriagePicture = new PictureBox();
        }
    }
}
