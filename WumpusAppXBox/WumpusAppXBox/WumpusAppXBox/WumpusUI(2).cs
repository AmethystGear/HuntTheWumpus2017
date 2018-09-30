using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WumpusProject
{
    //use System.Drawing.Point, Not Point
    //make sure to subtract positions by width/2 and height/2
    //declare me at the top "GameControl gc;"
    //How to make me: gc = new GameControl(this,new Rectangle(0,0,scX,scY),selectedCave);
    //I added door code to the constructor
    //noice scaleing
    //turned pit into pits [] 

    /// <summary>
    /// 
    /// </summary>

    class WumpusUI2
    {
        ////////////////////////////////////////////////////////// Fields //////////////////////////////////////////////////////////
        // game form
        private Form gameForm;
        // game control
        // private GameControl gc;
        // high score
        // private HighScore highScore;
        // screen dimensions
        private int scX;
        private int scY;

        // Main menu
        Button playGameButton;
        Button highScoreButton;
        Button exitGameButton;

        private int selectedCave;

        // highScore
        private Form highScoreForm;
        private static readonly Label nameHeader = makeLabel("Players:", 200, 50);
        private static readonly Label scoreHeader = makeLabel("Scores:", 400, 50);
        private Label[] playerNames = new Label[10];
        private Label[] highScores = new Label[10];
        private Button exitHighScore;

        // Game images
        private PictureBox player;
        private PictureBox bat;
        private PictureBox [] pits;
        private PictureBox wumpus;
        private PictureBox room;
        private PictureBox door;
        private PictureBox gold;
        private PictureBox secret;

        private PictureBox arrow;
        private PictureBox fireball;

        private List<PictureBox> arrowList;
        private List<PictureBox> fireballList;

        // trivia
        private Form triviaForm;
        private Button submit;
        private TextBox answerBox;

        // shop form
        private Form shopForm;
        private Button shopButton;
        private Button buyArrow;
        private Button buySecret;
        private Button exitShop;

        // dynamic labels
        private Label golds;
        private Label arrows;
        private Label secrets;

        // dynamic to-user messages pane
        private TextBox messages;
        
        // end game
        private PictureBox win;
        private PictureBox death;
        Button mainMenu;

        private TransparentPictureBox[] doors;
        /////////////////////////////////////////////////////// Constructor ///////////////////////////////////////////////////////

        public WumpusUI2(Form gameForm, PictureBox player, PictureBox bat, PictureBox[] pits, 
            PictureBox wumpus, PictureBox room, PictureBox door, PictureBox gold, 
            PictureBox arrow, PictureBox fireball, PictureBox secret, PictureBox win, 
            PictureBox death, Button playGameButton, Button highScoreButton, Button exitGameButton)
        {
            //make all of the doors
            doors = new TransparentPictureBox[6];
            for (int i = 0; i < doors.Length; i++)
            {
                TransparentPictureBox newDoor = new TransparentPictureBox(3);
                newDoor.Image = door.Image;
                gameForm.Controls.Add(newDoor);
                doors[i] = newDoor;
            }

            // passes in the game form
            this.gameForm = gameForm;

            // highScore = new HighScore();

            // get screen dimensions
            scX = Screen.FromControl(gameForm).Bounds.Width;
            scY = Screen.FromControl(gameForm).Bounds.Height;

            /////////////// Main Menu //////////////
            this.playGameButton = playGameButton;
            this.highScoreButton = highScoreButton;
            this.exitGameButton = exitGameButton;

            playGameButton.Click += new System.EventHandler(playGameButton_Click);
            highScoreButton.Click += new System.EventHandler(highScoreButton_Click);
            exitGameButton.Click += new System.EventHandler(exitGameButton_Click);

            //////////// HighScore Form ////////////
            highScoreForm = makeForm("High-scores", scX / 3, scY * 3 / 4);
            highScoreForm.Controls.Add(nameHeader);
            highScoreForm.Controls.Add(scoreHeader);
            iniHighScore();

            exitHighScore = makeButton("Exit", highScoreForm.Width * 5 / 6, highScoreForm.Height * 7 / 8);
            highScoreForm.Controls.Add(exitHighScore);

            /////////////// Shop Form //////////////
            shopForm = makeForm("Shop", scX / 3, scY * 1 / 2);
            // buyArrow Button
            buyArrow = makeButton("Buy", shopForm.Width * 2 / 3, shopForm.Height * 2 / 5);
            buyArrow.Click += new System.EventHandler(buyArrow_Click);
            // buySecret Button
            buySecret = makeButton("Buy", shopForm.Width * 2 / 3, shopForm.Height * 3 / 5);
            buySecret.Click += new System.EventHandler(buySecret_Click);
            // exitShop Button
            exitShop = makeButton("Exit", shopForm.Width * 2 / 3, shopForm.Height * 4 / 5);
            exitShop.Click += new System.EventHandler(exitShop_Click);

            shopForm.Controls.Add(buyArrow);
            shopForm.Controls.Add(buySecret);
            shopForm.Controls.Add(exitShop);

            //////////////////////////////// on screen GUI ////////////////////////////////////
            shopButton = makeButton("Shop", scX * 1 / 10, scY * 4 / 5);
            shopButton.Click += new System.EventHandler(shopButton_Click);
            gameForm.Controls.Add(shopButton);
            // player stats
            golds = makeLabel("0", scX * 1 / 10, scY * 7 / 8);
            arrows = makeLabel("0", scX * 2 / 10, scY * 7 / 8);
            secrets = makeLabel("0", scX * 3 / 10, scY * 7 / 8);

            // initial locations
            this.player = player;
            player.Location = new System.Drawing.Point(scX / 2, scY / 2);
            this.gold = gold;
            this.gold.Location = new System.Drawing.Point(scX / 10, scY * 7 / 9);
            this.secret = secret;
            this.secret.Location = new System.Drawing.Point(scX / 10, scY * 7 / 9);
            this.arrow = arrow;
            this.arrow.Location = new Point(scX / 10, scY * 7 / 9);
            this.fireball = fireball;
            this.fireball.Location = new Point(scX * 4 / 3, scY * 4 / 3);

            // message pane
            messages = makeTextBox(scX / 6, scY / 4, scX * 10 / 13, scY / 13);
            messages.Text = "HEYOOOO";
            messages.Show();        

            ////////////////////////////////// end of GUI /////////////////////////////////////

            // single dynamic game images
            this.bat = bat;
            this.pits = pits;
            this.wumpus = wumpus;
            this.room = room;
            this.door = door;

            // multiple projectile images
            arrowList = new List<PictureBox>();
            fireballList = new List<PictureBox>();

            // end game
            this.win = win;
            this.death = death;
            mainMenu = makeButton("Main Menu", scX / 2, scY * 7 / 8);
            
        }

        private void createGC()
        {
            // gc = new GameControl(this);
        }

        ////////////////////////////////////////////////////////// Main Menu //////////////////////////////////////////////////////////
        private void playGameButton_Click(object sender, EventArgs e) 
        {
            playGameButton.Hide();
            highScoreButton.Hide();
            exitGameButton.Hide();
            submitName();
        }
        private void highScoreButton_Click(object sender, EventArgs e)
        {
            openHighScore();
        }
        private void exitGameButton_Click(object sender, EventArgs e)
        {
            gameForm.Close();
        }
        public void openMainMenu(){
            playGameButton.Show();
            highScoreButton.Show();
            exitGameButton.Show();
            
        }
        public String submitName()
        {
            Form enterNameForm = makeForm("Enter Name: ", scX / 7, scY / 7);
            TextBox nameBox = makeTextBox(enterNameForm.Width * 5 / 7, enterNameForm.Height / 4, enterNameForm.Width * 1 / 7, enterNameForm.Height / 3);
            Button submitName = makeButton("This is me.", enterNameForm.Width * 3 / 5, enterNameForm.Height * 2 / 3);
            enterNameForm.AcceptButton = submitName;

            enterNameForm.Controls.Add(nameBox);
            enterNameForm.Controls.Add(submitName);
            enterNameForm.ShowDialog();
            if (enterNameForm.DialogResult == DialogResult.Cancel)
            {
                openMainMenu();
            }
            selectCave();
            enterNameForm.Hide();
            return nameBox.Text;
        }
        public int selectCave()
        {
            Form selectCaveForm = makeForm("Choose a Cave: ", scX / 6, scY / 3);
            Button cave1 = makeButton("Cave 1", selectCaveForm.Width / 3, selectCaveForm.Height * 1 / 7);
            Button cave2 = makeButton("Cave 2", selectCaveForm.Width / 3, selectCaveForm.Height * (2 / 7 + 1 / 28));
            Button cave3 = makeButton("Cave 3", selectCaveForm.Width / 3, selectCaveForm.Height * (3 / 7 + 1/ 28));
            Button cave4 = makeButton("Cave 4", selectCaveForm.Width / 3, selectCaveForm.Height * (4 / 7 + 1 / 28));
            Button cave5 = makeButton("Cave 5", selectCaveForm.Width / 3, selectCaveForm.Height * (5 / 7 + 1/ 28));

            cave1.Click += new System.EventHandler(this.cave1_Click);
            cave2.Click += new System.EventHandler(this.cave2_Click);
            cave3.Click += new System.EventHandler(this.cave3_Click);
            cave4.Click += new System.EventHandler(this.cave4_Click);
            cave5.Click += new System.EventHandler(this.cave5_Click);

            selectCaveForm.Controls.Add(cave1);
            selectCaveForm.Controls.Add(cave2);
            selectCaveForm.Controls.Add(cave3);
            selectCaveForm.Controls.Add(cave4);
            selectCaveForm.Controls.Add(cave5);
            selectCaveForm.ShowDialog();
            if (selectCaveForm.DialogResult == DialogResult.Cancel)
            {
                openMainMenu();
            }
            selectCaveForm.Hide();
            return selectedCave;
        }
        private void cave1_Click(Object sender, EventArgs e)
        {
            selectedCave = 1;
        }
        private void cave2_Click(Object sender, EventArgs e)
        {
            selectedCave = 2;
        }
        private void cave3_Click(Object sender, EventArgs e)
        {
            selectedCave = 3;
        }
        private void cave4_Click(Object sender, EventArgs e)
        {
            selectedCave = 4;
        }
        private void cave5_Click(Object sender, EventArgs e)
        {
            selectedCave = 5;
        }
        GameControl gc;
        private void startGame()
        {
            gc = new GameControl(this,new Rectangle(0,0,scX,scY),selectedCave);
            gold.Visible = true;
            arrow.Visible = true;
            secret.Visible = true;
            player.Visible = true;
            room.Visible = true;
        }
        ////////////////////////////////////////////////////////// Templates //////////////////////////////////////////////////////////
        // makes a Form with the given title and dimensions
        private static Form makeForm(String title, int width, int height)
        {
            Form newForm = new Form();
            newForm.Text = title;
            newForm.Size = new Size(width, height);
            newForm.Location = new Point(0, 0);
            return newForm;
        }
        // makes a label with given text and position
        private static Label makeLabel(String text, int x, int y)
        {
            Label newLabel = new Label();
            newLabel.Text = text;
            newLabel.Location = new Point(x, y);
            return newLabel;
        }
        // makes a button with given text and position
        private static Button makeButton(String text, int x, int y)
        {
            Button newButton = new Button();
            newButton.Text = text;
            newButton.Location = new Point(x, y);
            return newButton;
        }
        // makes a textbox of given dimensions and position
        private static TextBox makeTextBox(int xSize, int ySize, int x, int y)
        {
            TextBox newTextBox = new TextBox();
            newTextBox.Size = new Size(xSize, ySize);
            newTextBox.Location = new Point(x, y);
            return newTextBox;
        }

        /////////////////////////////////////////////////////// Drawing ///////////////////////////////////////////////////////
        // IMAGE DRIVE WILL NEED TO BE ALTERED (COMP HAS TWO DRIVES)

        public void drawPlayer(int x, int y, Boolean flipRight)
        {
            player.Location = new Point(x, y);
        }
        public void drawBat(int x, int y)
        {
            bat.Location = new Point(x, y);
        }
        public void drawWumpus(int x, int y)
        {
            wumpus.Location = new Point(x, y);
        }
        public void drawPit(int x, int y)
        {
            pit.Location = new Point(x, y);
        }
        public void drawRoom(int x, int y)
        {
            room.Location = new Point(x, y);
        }
        public void drawDoor(Point p, int i)
        {
            doors[i].Location = new System.Drawing.Point((int)(p.getX() - door.Width / 2), (int)(p.getY() - door.Height / 2));
        }

        // Arrows and Fireballs

        public void newArrowProjectile()
        {
            PictureBox newArrow = new PictureBox();
            newArrow.Image = new Bitmap(arrow.Image);
            arrowList.Add(newArrow);
        }

        public void newFireball()
        {
            PictureBox newFireball = new PictureBox();
            newFireball.Image = new Bitmap(fireball.Image);
            fireballList.Add(newFireball);
        }
        public void drawArrow(int x, int y, int angle, int arrowNum)
        {
            arrowList[arrowNum].Location = new Point(x, y);
        }
        public void drawFireball(int x, int y, int angle, int fireballNum)
        {
            arrowList[fireballNum].Location = new Point(x, y);
        }
        public void killArrow(int arrowNum)
        {
            gameForm.Controls.Remove(arrowList[arrowNum]);
            arrowList.RemoveAt(arrowNum);
            
        }
        public void killFireball(int fireballNum)
        {
            gameForm.Controls.Remove(fireballList[fireballNum]);
            fireballList.RemoveAt(fireballNum);
        }

        //////////////////////////////////////////////////////// Player Items ///////////////////////////////////////////////////////

        public void updateGold(int golds)
        {
            this.golds.Text = golds.ToString();
        }
        public void updateArrows(int arrows)
        {
            this.arrows.Text = arrows.ToString();
        }
        public void updateSecrets(int secrets)
        {
            this.secrets.Text = secrets.ToString();
        }
        public void warningMessage(Boolean wumpusNearby, Boolean pitNearby, Boolean batNearby)
        {
            if(wumpusNearby)
            {
                Console.WriteLine("There is a Wumpus nearby...");
            }
            if(pitNearby)
            {
                Console.WriteLine("There is a pit nearby...");
            }
            if(batNearby)
            {
                Console.WriteLine("There is a bat nearby...");
            }
        }
        public void trapped(Boolean wumpus, Boolean pit, Boolean bat)
        {
            if (wumpus)
            {
                Console.WriteLine("You encountered a Wumpus!");
            }
            if (pit)
            {
                Console.WriteLine("You fell into a pit...!");
            }
            if (bat)
            {
                Console.WriteLine("There's a bat attacking you!");
            }
        }

        ////////////////////////////////////////////////////// Trivia ///////////////////////////////////////////////////////

        public int openTrivia(int questionNumber, String question, String[] answers)
        {
            // creates a new window for a round of trivia
            triviaForm = makeForm("Trivia", 800, 600);

            // displays the trivia question and answers provided by the trivia object, 

            // question and Answer Labels
            Label questionNum = makeLabel("Q" + questionNumber + ":", 325, 200);
            questionNum.AutoSize = true;

            // Question and Answer Tag Labels
            Label aLabel = makeLabel("A.", 175, 300);
            aLabel.AutoSize = true;
            Label bLabel = makeLabel("B.", 175, 350);
            bLabel.AutoSize = true;
            Label cLabel = makeLabel("C.", 475, 300);
            cLabel.AutoSize = true;
            Label dLabel = makeLabel("D.", 475, 350);
            dLabel.AutoSize = true;

            // Question and Answers printed on labels
            Label questionLabel = makeLabel(question, 350, 200);
            questionLabel.AutoSize = true;

            // answer A label
            Label answerA = makeLabel(answers[0], 200, 300);
            answerA.Bounds = new Rectangle(answerA.Location.X, answerA.Location.Y, 200, 50);

            // answer B label
            Label answerB = makeLabel(answers[1], 200, 350);
            answerB.Bounds = new Rectangle(answerB.Location.X, answerB.Location.Y, 200, 50);

            // answer C label
            Label answerC = makeLabel(answers[2], 500, 300);
            answerC.Bounds = new Rectangle(answerC.Location.X, answerC.Location.Y, 200, 50);

            // answer D label
            Label answerD = makeLabel(answers[3], 500, 350);
            answerD.Bounds = new Rectangle(answerD.Location.X, answerD.Location.Y, 200, 50);

            // add answer box and submit button to triviaForm
            answerBox = makeTextBox(25, 50, 350, 400);
            submit = makeButton("Submit", 400, 400);
            this.submit.Click += new System.EventHandler(this.submit_Click);
            triviaForm.AcceptButton = submit;

            // add labels/textBoxes/buttons to triviaForm
            triviaForm.Controls.Add(questionNum);
            triviaForm.Controls.Add(aLabel);
            triviaForm.Controls.Add(bLabel);
            triviaForm.Controls.Add(cLabel);
            triviaForm.Controls.Add(dLabel);
            triviaForm.Controls.Add(questionLabel);
            triviaForm.Controls.Add(answerA);
            triviaForm.Controls.Add(answerB);
            triviaForm.Controls.Add(answerC);
            triviaForm.Controls.Add(answerD);
            triviaForm.Controls.Add(answerBox);
            triviaForm.Controls.Add(submit);

            triviaForm.ShowDialog();  // Blocks until user closes
            // Extract answer from form
            String[] answerLetters = {"A", "B", "C", "D"};
            for(int i = 0; i <= 3; i++)
            {
                if(answerBox.Text.ToUpper().Equals(answers[i]))
                {
                    Console.WriteLine(i);
                    return i;
                }
            }
            return -1;
        }
        // when submit is clicked, the answer is updated
        public void submit_Click(object sender, EventArgs e)
        {
            triviaForm.Close();
        }

        /////////////////////////////////////////////////////// Highscore ///////////////////////////////////////////////////////

        private void iniHighScore()
        {
            for (int i = 0; i < 10; i++)
            {
                playerNames[i] = new Label();
                highScores[i] = new Label();
            }
        }
        private void updateHighScore(String[] newPlayerNames, int[] newHighScores)
        {
            // modifies highScore form
            for (int i = 0; i < playerNames.Length; i++)
            {
                playerNames[i].Text = newPlayerNames[i];
                highScores[i].Text = newHighScores[i].ToString();
            }
        }
        public void openHighScore()
        {
            
            for (int i = 0; i < 10; i++)
            {
                playerNames[i].Location = new Point(200, i * 50 + 100);
                highScores[i].Location = new Point(400, i * 50 + 100);
                highScoreForm.Controls.Add(playerNames[i]);
                highScoreForm.Controls.Add(highScores[i]);
            }
            highScoreForm.ShowDialog();
        }

        ////////////////////////////////////////////////////////// Shop //////////////////////////////////////////////////////////
        public event EventHandler arrowBought = delegate { };
        public event EventHandler secretBought = delegate { };
        public void shopButton_Click(object sender, EventArgs e)
        {
            shopForm.Show();
        }
        public void buyArrow_Click(object sender, EventArgs e)
        {
            arrowBought(this, new EventArgs());
        }
        public void buySecret_Click(object sender, EventArgs e)
        {
            secretBought(this, new EventArgs());
        }
        public void exitShop_Click(object sender, EventArgs e)
        {
            shopForm.Close();
        }

        ////////////////////////////////////////////////////////// Other //////////////////////////////////////////////////////////

        public static String getPlayerName()
        {
            // asks for the player's name this game
            // returns the player name as a String
            return "Clifford";
        }
        public void winScreen()
        {
            // load in image center on screen
            win.Location = new Point(0, 0);
            win.Show();
        }
        public void deathScreen()
        {
            // load in image center on screen
            death.Location = new Point(0, 0);
            death.Show();
        }
        public void endGame()
        {
            // updateHighScore(topScoreNames(), topScores());
            // kill gameControl
            // gc = null;
            // hide all images
            player.Hide();
            bat.Hide();
            pit.Hide();
            wumpus.Hide(); 
            room.Hide();
            door.Hide(); 
            gold.Hide();
            arrow.Hide();
            secret.Hide();
            // remove projectiles from form
            arrowList.RemoveRange(0, arrowList.Count);
            fireballList.RemoveRange(0, fireballList.Count);
            // show button that takes the user to main menu
            gameForm.Controls.Add(mainMenu);
        }
        private void mainMenu_Click(object sender, EventArgs e) {
            openMainMenu();
        }
        public void refresh()
        {
            gameForm.Invalidate();
        }
    }
}
