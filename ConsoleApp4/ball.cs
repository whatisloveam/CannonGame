using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp4
{
    class Ball
    {
        public Vector position;
        public Vector velocity;
        public double r;
        public double damping = 0.3;
        public PictureBox ballPicture;

        public Ball(float x, float y, float r)
        {
            ballPicture = new PictureBox();
            position = new Vector(x, y);
            velocity = new Vector(0.5, 0);
            this.r = r;
        }

        public void Move()
        {
            velocity.Add(Constants.gravity);
            position.Add(velocity);
        }

        public void CheckWallCollision()
        {
            if (position.X > Constants.WindowWidth - 2*r)
            {
                position.X = Constants.WindowWidth - 2*r;
                velocity.X *= -damping;
            }
            else if (position.X < r)
            {
                position.X = r;
                velocity.X *= -damping;
            }
        }
    }
}