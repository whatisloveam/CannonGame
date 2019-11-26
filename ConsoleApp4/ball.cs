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
            velocity = new Vector(0, 0);
            this.r = r;
        }

        public void Move()
        {
            velocity.Add(Constants.gravity);
            position.Add(velocity);
        }

        public bool CheckWallCollision()
        {
            if (position.X > Constants.WindowWidth - 2*r)
            {
                position.X = Constants.WindowWidth - 2*r;
                velocity.X *= -damping;
                return true;
            }
            else if (position.X < r)
            {
                position.X = r;
                velocity.X *= -damping;
                return true;
            }
            return false;
        }

        public bool CheckGroundCollision(Ground groundSegment)
        {
            var deltaX = position.X - groundSegment.x;
            var deltaY = position.Y - groundSegment.y;

            var cosine = Math.Cos(groundSegment.rot);
            var sine = Math.Sin(groundSegment.rot);

            var groundXTemp = cosine * deltaX + sine * deltaY;
            var groundYTemp = cosine * deltaY - sine * deltaX;
            var velocityXTemp = cosine * velocity.X + sine * velocity.Y;
            var velocityYTemp = cosine * velocity.Y - sine * velocity.X;
            var flag = false;
            if (groundYTemp > -r &&
              position.X > groundSegment.x1 &&
              position.X < groundSegment.x2)
            {
                groundYTemp = -r;
                velocityYTemp *= -1.0;
                velocityYTemp *= damping;
                flag = true;
            }

            deltaX = cosine * groundXTemp - sine * groundYTemp;
            deltaY = cosine * groundYTemp + sine * groundXTemp;
            velocity.X = cosine * velocityXTemp - sine * velocityYTemp;
            velocity.Y = cosine * velocityYTemp + sine * velocityXTemp;
            position.X = groundSegment.x + deltaX;
            position.Y = groundSegment.y + deltaY;
            
            return flag;
        }
    }
}