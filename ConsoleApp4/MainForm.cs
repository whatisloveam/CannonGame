using System;
using System.Drawing;
using System.Windows.Forms;

namespace ConsoleApp4
{
    public class MainForm : Form
    {
        private Timer timer1;
        private PictureBox pictureBox1;
        private System.ComponentModel.IContainer components;

        Ball ball;        
        Ground[] ground = new Ground[Constants.GroundSegments];

        public MainForm()
        {
            InitializeComponent();

            Constants.WindowWidth = this.Width;
            Constants.WindowHeight = this.Height;
            
            ball = new Ball(100, 100, 5);

            var peakHeights = new double[Constants.GroundSegments + 1];
            Random random = new Random();
            for (int i = 0; i < peakHeights.Length; i++)
            {
                peakHeights[i] = random.Next(Constants.WindowHeight - 100, Constants.WindowHeight - 80);
            }

            float segs = Constants.GroundSegments;
            for (int i = 0; i < Constants.GroundSegments; i++)
            {
                ground[i] = new Ground(Constants.WindowWidth / segs * i, peakHeights[i], 
                                       Constants.WindowWidth / segs * (i + 1), peakHeights[i + 1]);
            }

        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(105, 97);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(10, 10);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(735, 396);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MainForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }


        void CheckGroundCollision(Ground groundSegment)
        {
            var deltaX = ball.position.X - groundSegment.x;
            var deltaY = ball.position.Y - groundSegment.y;

            double cosine = Math.Cos(groundSegment.rot);
            double sine = Math.Sin(groundSegment.rot);

            var groundXTemp = cosine * deltaX + sine * deltaY;
            var groundYTemp = cosine * deltaY - sine * deltaX;
            var velocityXTemp = cosine * ball.velocity.X + sine * ball.velocity.Y;
            var velocityYTemp = cosine * ball.velocity.Y - sine * ball.velocity.X;

            if (groundYTemp > -ball.r &&
              ball.position.X > groundSegment.x1 &&
              ball.position.X < groundSegment.x2)
            {
                groundYTemp = -ball.r;
                velocityYTemp *= -1.0;
                velocityYTemp *= ball.damping;
            }

            deltaX = cosine * groundXTemp - sine * groundYTemp;
            deltaY = cosine * groundYTemp + sine * groundXTemp;
            ball.velocity.X = cosine * velocityXTemp - sine * velocityYTemp;
            ball.velocity.Y = cosine * velocityYTemp + sine * velocityXTemp;
            ball.position.X = groundSegment.x + deltaX;
            ball.position.Y = groundSegment.y + deltaY;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ball.Move();
            pictureBox1.Location = new Point((int)(ball.position.X - ball.r), (int)(ball.position.Y - ball.r));
            ball.CheckWallCollision();

            for (int i = 0; i < Constants.GroundSegments; i++)
            {
                CheckGroundCollision(ground[i]);
            }

        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < Constants.GroundSegments; i++)
            {
                /*e.Graphics.FillPolygon(Brushes.Black, new Point[]
                {
                    new Point((int)ground[i].x1, (int)ground[i].y1),
                    new Point((int)ground[i].x2, (int)ground[i].y2),
                    new Point((int)ground[i].x1, Constants.WindowHeight),
                    new Point((int)ground[i].x1, Constants.WindowHeight)
                });*/
                e.Graphics.DrawLine(Pens.Black, (float)ground[i].x1, (float)ground[i].y1, 
                                                (float)ground[i].x2, (float)ground[i].y2);
            }
        }
    }
}