using System;
using System.Drawing;
using System.Windows.Forms;
using SlimDX.XInput;

namespace WumpusAppXBox
{
    public partial class Form1 : Form
    {
        //the controller - set to player one
        XBoxGamepad controller = new XBoxGamepad(UserIndex.One);

        //the UI
        WumpusUI ui;

        //the timer that sends timer events for real time gaming
        Timer t;

        //makes game faster when rendering/painting
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        //constructor
        public Form1()
        {
            InitializeComponent();

            
            //set the timer
            t = new Timer();

            //set the interval to 15ms to get decent FPS (optimally, it would be 66 FPS, but it's always lower than that)
            t.Interval = 15;
            //when the timer gets a tick event, call this eventhandler
            t.Tick += T_Tick;
            //start the timer
            t.Start();

            //when the gamePanel needs to be painted, call this function (the gamePanel has all of the game images)
            gamePanel.Paint += gamePanel_Paint;
        }

        //when the gamePanel is painted
        private void gamePanel_Paint(object sender, PaintEventArgs e)
        {
            //loop through all the TransparentPictureBoxes in the ordered list (based on z-order)
            foreach (TransparentPictureBox pb in TransparentPictureBox.getAllPictureBoxes())
            {
                //paint the ones that are on the panel
                if (pb.Parent != null && pb.Parent.Equals(gamePanel))
                    pb.Paint(e);
            }
        }

        //the function called when the timer gets a tick event
        private void T_Tick(object sender, EventArgs e)
        {
            //if there is an active gc, the game is being played - call the controller and gc updates
            if (ui.getGc() != null)
            {                
                //stop the timer to ensure that another time event is not called before this first one finishes.
                t.Stop();
                //update the controller
                controller.Update();

                //call the onTimer function 
                ui.getGc().onTimer(controller);

                //start the timer so it can continue to the next time event.
                t.Start();
            }           
        }      

        //returns the screen bounds in the form of a rectangle
        public Rectangle getScreen()
        {
            return Screen.GetBounds(this);
        }

        //when the form is loaded
        private void Form1_Load(object sender, EventArgs e)
        {
            //make the ui
            ui = new WumpusUI(this, PlayerImg, new TransparentPictureBox[] { bat1, bat2 },
                 new TransparentPictureBox[] { Pit1, Pit2 }, new TransparentPictureBox[] { rock1, rock2, rock3, rock4 }, wumpus, room,
                 gold, arrow, fireball, null, winScreen, looseScreen, main, chest, door,
                 textBox1, getScreen(), gamePanel);
        }



        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void gamePanel_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
