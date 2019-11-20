using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

namespace ConsoleApp4
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new MainForm());
        }
    }
}