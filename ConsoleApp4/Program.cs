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
            var ball = new Ball(15, 0, 5);
            ball.velocity = new Vector(-5, 0);
            var t = ball.CheckGroundCollision(new Ground(0, 0, 20, 20));
            ball.velocity = ball.velocity;
            Application.Run(new MainForm());
        }
    }
}