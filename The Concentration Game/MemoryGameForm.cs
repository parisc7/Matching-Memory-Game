using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Concentration_Game
{
    public partial class MemoryGameForm : Form
    { 

        //Variables
        Random location = new Random(); //Selects a random value, and we use it in our loop to place our cards in random places
        List<Point> points = new List<Point>(); //a list to use for the position of the cards


        PictureBox pendingImage1 = new PictureBox();// store first flipped card
        PictureBox pendingImage2= new PictureBox();// store second flipped card

        //bool playAgain = false; // a bool fuction to play again or not


        public MemoryGameForm()
        {
            InitializeComponent();
            paint();
        }

        private void memoryGameForm_Load(object sender, EventArgs e)
        {
            timeCountLabel.Text = "3";



            foreach (PictureBox picture in gameFlowLayoutPanel.Controls)
            {
                int nextLocation = location.Next(points.Count); //we count each location to nextLocation and then move to another
                Point p = points[nextLocation];
                picture.Location = p;
                points.Remove(p);  //we need to remove it because some of them has the same value and we will be missing some cards
            }

            timer2.Start();
            timer1.Start();// we start our timer and after 5 seconds the cards will flip

        }

        //After 5 seconds the cards will fli to the cover image
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop(); //we stop the timer because we dont need it after this and save memory
           foreach(PictureBox picture in gameFlowLayoutPanel.Controls)
            {
              picture.Enabled=true; //we cannot interact with our pictures before the timer stops
              picture.Cursor = Cursors.Hand; //we added a hand cursor for style
              picture.Image = Properties.Resources.Cover;
            }
        }
     
        private void timer2_Tick(object sender, EventArgs e)
        {
            int timer = Convert.ToInt32(timeCountLabel.Text);// we use this to countdown in our label
            timer = timer - 1;//for our timer to decreased
            timeCountLabel.Text=Convert.ToString(timer);

            if (timer ==0)
            {
                timer2.Stop(); //we stop our secodnd timer to save memory again
            }
        }

     

        private void timer3_Tick(object sender, EventArgs e) //we added this counter to show the pair for 0.3 and then proceed
        {
            timer3.Stop();

            pendingImage1.Image = Properties.Resources.Cover; //if there are not similar we flip both of them back again
            pendingImage2.Image = Properties.Resources.Cover;

            pendingImage1 = new PictureBox(); //we send them back to null again so we can use them again in other cards
            pendingImage2 = new PictureBox();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

            paint();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();  //we exit our application
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();  //we use the default about box
            box.ShowDialog();
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // paint();
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void largeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }


        public void paint()
        { 
            gameFlowLayoutPanel.Controls.Clear();
            int numOfImages = 16;
            

            for (int i = 0; i < numOfImages; i++)
            {
                PictureBox picture = new PictureBox();

                picture.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("Cover");
                picture.SizeMode=PictureBoxSizeMode.StretchImage;
                picture.Width = 90;
                picture.Height = 120;
                picture.Name = $"name{i}";
                picture.Click += Picture_Click;
                picture.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("Card"+i);

                picture.Enabled = false;
                points.Add(picture.Location); //we add every picture location to our points List
              
                


                gameFlowLayoutPanel.Controls.Add(picture);
            }

            
        }

        private void Picture_Click(object sender, EventArgs e)
        {
            var picture = (sender as PictureBox);

            picture.Image = picture.BackgroundImage;

            if (pendingImage1.Tag == null)  // if we dont use the first one, then we can use it
            {
                pendingImage1 = sender as PictureBox;
            }
            else if (pendingImage1.Tag != null && pendingImage2.Tag == null) //if we use the first one then we use the second one
            {
                pendingImage2 = sender as PictureBox;
            }
            if (pendingImage1.Tag != null && pendingImage2.Tag != null) //if we use the first one then we use the second one
            {
                if (pendingImage1.Tag == pendingImage2.Tag) //we use the tags to identify each set of pair
                {
                    pendingImage1 = new PictureBox();
                    pendingImage2 = new PictureBox();
                    picture.Enabled = false; //this is so we cant click on the same card again after we match a pair
                    //secondCard.Enabled = false;
                    counterLabel.Text = Convert.ToString(Convert.ToInt32(counterLabel.Text) + 10); // i found it much nicer to add a score instead of how many times you click on a image
                }
                else
                {
                    timer3.Start(); //we start our timer so we can be able to see both cards at the same time
                    timer3.Tick += timer3_Tick;
                }
            }

        }
    }
}
