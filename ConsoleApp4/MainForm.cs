using CannonGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ConsoleApp4
{
    public class MainForm : Form
    {
        private Timer timer1;
        private System.ComponentModel.IContainer components;
        private readonly List<Cannon> Cannons;
        private readonly Ball ball;
        private Label label1;
        private Label label2;
        private Button button1;
        readonly Ground[] ground = new Ground[Constants.GroundSegments];
        bool IsMouseCtrl = true;

        double power, angle;

        int playerNum = 0;
        bool switchedMove = true;

        public MainForm()
        {
            InitializeComponent();
            Constants.WindowWidth = this.Width;
            Constants.WindowHeight = this.Height;

            ball = new Ball(100, 100, 5);

            ball.ballPicture.Image = Image.FromFile("../images/ball.png");
            ball.ballPicture.Location = new Point(105, 97);
            ball.ballPicture.Name = "ball1";
            ball.ballPicture.Size = new Size(10, 10);
            ball.ballPicture.SizeMode = PictureBoxSizeMode.Zoom;
            ball.ballPicture.BackColor = Color.Transparent;

            this.Controls.Add(ball.ballPicture);

            Cannons = new List<Cannon>();

            #region Terrain
            var randomizer = new Random();

            var rand1 = randomizer.NextDouble() + 1;
            var rand2 = randomizer.NextDouble() + 2;
            var rand3 = randomizer.NextDouble() + 3;

            var offset = Constants.WindowHeight / 2;
            var peakheight = 130;
            var flatness = 4;

            var peakHeights = new double[Constants.GroundSegments+1];
            var counter = -1;
            for (int i = 0; i < peakHeights.Length; i++)
            {
                double height = peakheight / rand1 * Math.Sin((float)i / flatness * rand1 + rand1);
                height += peakheight / rand2 * Math.Sin((float)i / flatness * rand2 + rand2);
                height += peakheight / rand3 * Math.Sin((float)i / flatness * rand3 + rand3);
                height += offset;
                peakHeights[i] = height;
                if (i % (Constants.GroundSegments / 5) == 0 || counter > 0)
                {
                    counter++;
                    if(counter == 1)
                    {

                    }
                    else if(counter > 1 && counter < 5)
                    {
                        peakHeights[i] = peakHeights[i-1];
                    }
                    else
                    {
                        counter = 0;
                    }
                }                
            }

            var segs = Constants.GroundSegments;
            for (int i = 0; i < Constants.GroundSegments; i++)
            {
                ground[i] = new Ground(Constants.WindowWidth / segs * i, peakHeights[i],
                                       Constants.WindowWidth / segs * (i + 1), peakHeights[i + 1]);
            }

            #endregion

            
            
            for (int i = 0; i < 4; i++)
            {
                var c = new Cannon();

                c.carriagePicture.Image = ImageProcessing.ARGBFilter(Image.FromFile("../images/carriage.png"), Constants.PlayersColors[i]);
                c.carriagePicture.Location = new Point(i * 100, i * 100);
                c.carriagePicture.Name = "carriagePicture" + i;
                c.carriagePicture.Size = new Size(50, 20);
                c.carriagePicture.SizeMode = PictureBoxSizeMode.Zoom;
                c.carriagePicture.BackColor = Color.Transparent;

                c.ImageSource = ImageProcessing.ARGBFilter(Image.FromFile("../images/cannon.png"), Constants.PlayersColors[i]);
                c.cannonPicture.Image = (Bitmap)c.ImageSource.Clone();
                c.cannonPicture.Location = new Point(i*100, 500);
                c.cannonPicture.Name = "cannonPicture" + i;
                c.cannonPicture.Size = new System.Drawing.Size(10, 35);
                c.cannonPicture.SizeMode = PictureBoxSizeMode.Zoom;
                c.cannonPicture.BackColor = Color.Transparent;
                c.cannonPicture.ClientSize = new Size(10, 35);
                
                var x = ground[Constants.GroundSegments / 5 * (i+1)].x;

                c.carriagePicture.Location = new Point((int)x, 
                    (int)ground[Constants.GroundSegments / 5 * (i+1)].y - c.carriagePicture.Size.Height);

                c.cannonPicture.Location = new Point((int)(x + 20),
                    (int)ground[Constants.GroundSegments / 5 * (i + 1)].y - c.cannonPicture.Size.Height);


                this.Controls.Add(c.carriagePicture);
                this.Controls.Add(c.cannonPicture);
                
                Cannons.Add(c);

            }



        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Power";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "angle";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 576);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "mouse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1234, 611);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Click += new System.EventHandler(this.MainForm_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        public static Bitmap RotateImage(Image image, PointF offset, float angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            var rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            var g = Graphics.FromImage(rotatedBmp);

            g.TranslateTransform(offset.X, offset.Y);
            g.RotateTransform(angle);
            g.TranslateTransform(-offset.X, -offset.Y);
            g.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
        }

        void CheckGroundCollision(Ground groundSegment)
        {
            var deltaX = ball.position.X - groundSegment.x;
            var deltaY = ball.position.Y - groundSegment.y;

            var cosine = Math.Cos(groundSegment.rot);
            var sine = Math.Sin(groundSegment.rot);

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
        private void Timer1_Tick(object sender, EventArgs e)
        {
            ball.Move();
            ball.ballPicture.Location = new Point((int)(ball.position.X - ball.r), (int)(ball.position.Y - ball.r));
            ball.CheckWallCollision();


            for (int i = 0; i < Constants.GroundSegments; i++)
                CheckGroundCollision(ground[i]);

            var hitIndex = Hit();
            if (hitIndex != 0)
            {
                Cannons[hitIndex].IsAlive = false;
                Cannons[hitIndex].carriagePicture.Image = null;
                Cannons[hitIndex].cannonPicture.Image = null;
            }
            if (Math.Abs(ball.velocity.X) < 1 && Math.Abs(ball.velocity.Y) < 1)
            {
                if(!switchedMove)
                {
                    for (int i =0; i < 4; i++)
                    {
                        if (Cannons[(playerNum+1+i)%4].IsAlive)
                        {
                            playerNum = (playerNum + 1 + i) % 4;
                            switchedMove = true;
                            break;
                        }
                    }                    
                }                
            }
        }

        private int Hit()
        {
            for (int i = 0; i < 4; i++)
            {
                if(i!=playerNum)
                {
                    Rectangle rect = new Rectangle(Cannons[i].carriagePicture.Location, Cannons[i].carriagePicture.Size);
                    if(rect.Contains(new Point((int)ball.position.X, (int)ball.position.Y)))
                        return i;
                }
            }
            return 0;
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

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(!IsMouseCtrl)
            {
                if (e.KeyCode == Keys.A) angle--;
                else if (e.KeyCode == Keys.D) angle++;
                if (e.KeyCode == Keys.W) power++;
                else if (e.KeyCode == Keys.D) power--;
                if (angle < -90) angle = -90;
                if (angle > 90) angle = 90;
                if (power < 0) power = 0;
                label1.Text = "power :" + power;
                label2.Text = "angle :" + angle;

                Cannons[playerNum].cannonPicture.Image = RotateImage(Cannons[playerNum].ImageSource, new Point(5, 50), (float)angle);
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            IsMouseCtrl = !IsMouseCtrl;
            if (IsMouseCtrl) button1.Text = "Keyboard";
            else button1.Text = "Mouse";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            ball.position = new Vector(Cannons[playerNum].cannonPicture.Location.X + 5, Cannons[playerNum].cannonPicture.Location.Y);
            ball.velocity = new Vector(power * Math.Cos(angle*Math.PI/180), -power*Math.Sin(angle * Math.PI / 180));
            switchedMove = false;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if(IsMouseCtrl)
            {
                power = 0.1*Helper.DistanceFromTwoPoints(e.X, e.Y,
                    Cannons[playerNum].cannonPicture.Location.X, Cannons[playerNum].cannonPicture.Location.Y);
                label1.Text = "power :" + power;
                angle = (double)((-180 / Math.PI) * (Math.Atan2(Cannons[playerNum].cannonPicture.Location.Y - e.Y,
                   Cannons[playerNum].cannonPicture.Location.X - e.X)-Math.PI));
                label2.Text = "angle :" + angle;

                if (angle < 0) angle = 0;
                if (angle > 180) angle = 180;

                Cannons[playerNum].cannonPicture.Image = RotateImage(Cannons[playerNum].ImageSource, new Point(5, 50), (float)(-angle+90));
            }
        }
    }
}