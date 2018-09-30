using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WumpusAppXBox
{
    class WumpusUI
    {
        Sound sound = new Sound();
        #region Fields
        #region Game Object Fields - Central to the Game
        // game form

        private Form gameForm;
        private Image mainBackground;
        private GameControl gc;
        private HighScore hs;
        // game panel - images drawn here
        DrawingPanel gamePanel;
        // screen dimensions
        private Rectangle screen;
        private int scX;
        private int scY;
        #endregion

        #region Main Menu Fields
        //has the game started
        private bool gameStarted = false;
        // Main Buttons
        private Button playGameButton;
        private Button highScoreButton;
        private Button helpButton;
        private Button exitGameButton;

        // Help Form
        private Form helpForm;

        // HighScore Form
        private Form highScoreForm;
        private Button exitHighScore;

        private readonly Label nameHeader = makeLabel("Players:", 400, 50);
        private readonly Label scoreHeader = makeLabel("Scores:", 400, 50);
        private Label[] playerNames = new Label[10];
        private Label[] highScores = new Label[10];

        private Image highscoresBackground = new Bitmap("highscoresBackground.jpg");

        // Submit Name Form
        private Form submitNameForm;
        private Button submitNameButton;

        private TextBox nameBox;
        private String playerName;

        // Select Cave Form
        private Form selectCaveForm;
        private int selectedCave;
        #endregion

        #region Game Form Fields - Popups in game    
        // Trivia 
        private Form triviaForm;
        private Button submit;
        private Image triviaBackground = new Bitmap("triviaBackground.jpg");
        private TextBox answerBox;

        // Shop
        private Form shopForm;
        private Button exitShop;
        private Image shopBackground = new Bitmap("shopBackground.jpg");
        private Button shopButton;
        private Button buyArrow;
        private Button buySecret;
        #endregion

        #region GUI Fields
        // Game Images - Modified Regularly
        private TransparentPictureBox player;
        private TransparentPictureBox wumpus;
        private TransparentPictureBox room;
        private TransparentPictureBox chest;
        private TransparentPictureBox gold;
        private TransparentPictureBox arrow;
        private TransparentPictureBox secret;
        private TransparentPictureBox fireball;
        private TransparentPictureBox door;

        private TransparentPictureBox[] doors;
        private TransparentPictureBox[] bats;
        private TransparentPictureBox[] pits;
        private TransparentPictureBox[] rocks;
        private List<TransparentPictureBox> arrowList;
        private List<TransparentPictureBox> fireballList;

        // Player Item Labels
        private Label golds;
        private Label arrows;

        // alerts - visible to user
        private TextBox alertBox;
        #endregion

        #region Endgame
        private TransparentPictureBox win;
        private TransparentPictureBox lose;


        Button mainMenu;

        #endregion

        #endregion
        /////////////////////////////////////////////////////// Constructor ///////////////////////////////////////////////////////

        public WumpusUI(Form gameForm, TransparentPictureBox player, TransparentPictureBox[] bats,
            TransparentPictureBox[] pits, TransparentPictureBox[] rocks, TransparentPictureBox wumpus, TransparentPictureBox room,
            TransparentPictureBox gold, TransparentPictureBox arrow, TransparentPictureBox fireball,
            TransparentPictureBox secret, TransparentPictureBox win, TransparentPictureBox lose, TransparentPictureBox main, TransparentPictureBox chest,
            TransparentPictureBox door, TextBox alertBox, Rectangle screen, DrawingPanel gamePanel)
        {
            main.Location = new System.Drawing.Point(-10000, -10000);
            mainBackground = main.Image;
            gameForm.BackgroundImage = main.Image;
            sound.playSound1();
            this.gameForm = gameForm;
            hs = new HighScore();

            // Get screen dimensions - used for scaling and GC creation
            this.screen = screen;
            scX = Screen.FromControl(gameForm).Bounds.Width;
            scY = Screen.FromControl(gameForm).Bounds.Height;

            #region Main Menu construction
            playGameButton = new Button(); playGameButton.Text = "Play Game"; playGameButton.Size = new Size (scX / 10, scX / 40);
            playGameButton.Location = new System.Drawing.Point((scX - playGameButton.Width) / 2, scY * 23 / 34);
            // playGameButton = makeButton("play game", (scX - scX / 10) / 2, scY * 14 / 22);
            highScoreButton = new Button(); highScoreButton.Text = "Highscores"; highScoreButton.Size = playGameButton.Size;
            highScoreButton.Location = new System.Drawing.Point((scX - highScoreButton.Width) / 2, scY * 25 / 34);
            // highScoreButton = makeButton("highscores", (scX - scX / 10) / 2, scY * 15 / 22);
            helpButton = new Button(); helpButton.Text = "Help and Credits"; helpButton.Size = playGameButton.Size;
            helpButton.Location = new System.Drawing.Point((scX - helpButton.Width) / 2, scY * 27 / 34);
            // helpButton = makeButton("help and credits", (scX - scX / 10) / 2, scY * 16 / 22);
            // helpButton.AutoSize = true;
            exitGameButton = new Button(); exitGameButton.Text = "Exit Game"; exitGameButton.Size = playGameButton.Size;
            exitGameButton.Location = new System.Drawing.Point((scX - exitGameButton.Width) / 2, scY * 29 / 34);
            // exitGameButton = makeButton("exit", (scX - scX / 10) / 2, scY * 17 / 22);

            playGameButton.Click += new System.EventHandler(playGameButton_Click);
            highScoreButton.Click += new System.EventHandler(highScoreButton_Click);
            helpButton.Click += new System.EventHandler(helpButton_Click);
            exitGameButton.Click += new System.EventHandler(exitGameButton_Click);

            gameForm.Controls.Add(playGameButton);
            gameForm.Controls.Add(highScoreButton);
            gameForm.Controls.Add(helpButton);
            gameForm.Controls.Add(exitGameButton);

            // Help Form
            helpForm = makeForm("Help and Credits", 640, 370);
            helpForm.StartPosition = FormStartPosition.Manual;
            helpForm.Location = new System.Drawing.Point((scX - helpForm.Width) / 2, (scY - helpForm.Height) / 2);
            // HighScore Form
            highScoreForm = makeForm("High-scores", scX / 3, scY * 3 / 4);
            highScoreForm.StartPosition = FormStartPosition.Manual;
            highScoreForm.Location = new System.Drawing.Point((scX - highScoreForm.Width) / 2 + 150, (scY - highScoreForm.Height) / 2);


            playerNames = new Label[10];
            highScores = new Label[10];

            exitHighScore = makeButton("Exit", highScoreForm.Width * 4 / 6, highScoreForm.Height * 7 / 8);
            exitHighScore.Click += ExitHighScore_Click;
            highScoreForm.Controls.Add(exitHighScore);

            // Submit Name Form
            submitNameForm = makeForm("Enter Name: ", scX / 5, scY / 6);
            submitNameForm.StartPosition = FormStartPosition.Manual;
            submitNameForm.Location = new System.Drawing.Point((scX - submitNameForm.Width) / 2, (scY - submitNameForm.Height) / 2);
            nameBox = makeTextBox(submitNameForm.Width * 5 / 7, submitNameForm.Height / 4, submitNameForm.Width * 1 / 7, submitNameForm.Height / 7);
            submitNameButton = makeButton("This is me!", submitNameForm.Width * 3 / 5, submitNameForm.Height * 3 / 7);
            submitNameButton.Click += SubmitNameButton_Click;
            submitNameForm.AcceptButton = submitNameButton;

            submitNameForm.Controls.Add(nameBox);
            submitNameForm.Controls.Add(submitNameButton);

            // Select Cave Form
            selectCaveForm = makeForm("Choose a Cave: ", scX / 6, scY / 3);
            selectCaveForm.StartPosition = FormStartPosition.Manual;
            selectCaveForm.Location = new System.Drawing.Point((scX - selectCaveForm.Width) / 2, (scY - selectCaveForm.Height) / 2);
            Button[] caves = new Button[5];
            for (int i = 1; i <= 5; i++)
            {
                caves[i - 1] = makeButton("Cave " + i, selectCaveForm.Width / 3, selectCaveForm.Height * i / 7);
                selectCaveForm.AcceptButton = caves[i - 1];
            }

            caves[0].Click += new System.EventHandler(this.cave1_Click);
            caves[1].Click += new System.EventHandler(this.cave2_Click);
            caves[2].Click += new System.EventHandler(this.cave3_Click);
            caves[3].Click += new System.EventHandler(this.cave4_Click);
            caves[4].Click += new System.EventHandler(this.cave5_Click);

            selectCaveForm.Controls.AddRange(caves);

            selectCaveForm.FormClosing += SelectCaveForm_FormClosing;
            #endregion          

            #region Shop Form
            shopForm = makeForm("Shop", scX / 5, scY * 1 / 2);
            shopForm.StartPosition = FormStartPosition.Manual;


            shopForm.BackgroundImage = shopBackground;
            shopForm.Width = shopBackground.Width + 20;
            shopForm.Height = shopBackground.Height + 30;

            shopForm.Location = new System.Drawing.Point((scX - shopForm.Width) / 2, (scY - shopForm.Height) / 2);
            // buyArrow Button
            buyArrow = makeButton("Buy", shopForm.Width * 3 / 5, shopForm.Height * 25 / 42);
            buyArrow.Click += new System.EventHandler(buyArrow_Click);
            // buySecret Button
            buySecret = makeButton("Buy", shopForm.Width * 3 / 5, shopForm.Height * 2 / 7);
            buySecret.Click += new System.EventHandler(buySecret_Click);
            // exitShop Button
            exitShop = makeButton("Exit", shopForm.Width * 3 / 5, shopForm.Height * 4 / 5);
            exitShop.Click += new System.EventHandler(exitShop_Click);

            shopForm.Controls.Add(buyArrow);
            shopForm.Controls.Add(buySecret);
            shopForm.Controls.Add(exitShop);
            #endregion

            #region GUI Construction

            // Image construction
            this.player = player;
            this.wumpus = wumpus;
            this.room = room;
            this.chest = chest;
            this.gold = gold;
            this.arrow = arrow;
            this.secret = secret;
            this.fireball = fireball;
            this.door = door;

            // Image array construction
            this.door = door;
            this.bats = bats;
            this.pits = pits;
            this.rocks = rocks;

            // Projectile image list - images copied from the template picturebox
            arrowList = new List<TransparentPictureBox>();
            fireballList = new List<TransparentPictureBox>();

            // Initial image locations 
            player.Location = new System.Drawing.Point(scX / 2, scY / 2);
            this.gold.Location = new System.Drawing.Point(scX * 1 / 10, scY * 7 / 9);
            this.arrow.Location = new System.Drawing.Point(scX * 3 / 10, scY * 7 / 9);
            this.fireball.Location = new System.Drawing.Point(scX * 4 / 3, scY * 4 / 3);

            // Set door location
            door.Location = new System.Drawing.Point(-1000, -1000);
            doors = new TransparentPictureBox[6];
            for (int i = 0; i < doors.Length; i++)
            {
                TransparentPictureBox newDoor = new TransparentPictureBox(door.getZOrder());
                newDoor.Image = door.Image;
                gamePanel.Controls.Add(newDoor);
                doors[i] = newDoor;
                doors[i].Location = new System.Drawing.Point(-1000, -1000);
            }

            #region Player HUD
            // Shop button
            shopButton = makeButton("Shop", scX * 1 / 30, scY * 4 / 5);
            shopButton.Click += new System.EventHandler(shopButton_Click);
            // Items
            gold.Location = new System.Drawing.Point(scX * 1 / 30, scY * 17 / 20);
            arrow.Location = new System.Drawing.Point(scX * 3 / 30, scY * 8 / 10);
            golds = makeLabel("0", scX * 2 / 30, scY * 7 / 8); golds.AutoSize = true;
            arrows = makeLabel("0", scX * 4 / 30, scY * 7 / 8); arrows.AutoSize = true;

            // Alerts
            this.alertBox = alertBox;
            alertBox.Location = new System.Drawing.Point(scX - alertBox.Width, 1 / 80);
            #endregion

            #endregion

            #region Game Panel - Draw images here!
            // Game panel displays game images
            this.gamePanel = gamePanel;
            gamePanel.Size = new Size(0, 0);

            gamePanel.Controls.Add(shopButton);
            gamePanel.Controls.Add(golds);
            gamePanel.Controls.Add(arrows);

            shopButton.BackColor = Color.White;
            golds.BackColor = Color.White;
            arrows.BackColor = Color.White;
            gamePanel.BackColor = Color.Black;
            gameForm.BackColor = Color.White;
            #endregion

            #region Endgame
            this.win = win;
            this.lose = lose;
            win.Location = new System.Drawing.Point(0, 0);
            lose.Location = new System.Drawing.Point(0, 0);

            // Keep hidden until game ends
            win.Hide();
            lose.Hide();

            // show button that takes the user to main menu
            mainMenu = makeButton("Main Menu", scX / 2, scY * 7 / 8);
            mainMenu.AutoSize = true;
            gameForm.Controls.Add(mainMenu);
            mainMenu.Click += MainMenu_Click;
            mainMenu.Hide();
            #endregion
        }

        #region Main Menu
        // Opens the main menu
        public void openMainMenu()
        {
            playGameButton.Show();
            highScoreButton.Show();
            exitGameButton.Show();
        }
        private void playGameButton_Click(object sender, EventArgs e)
        {
            if (!gameStarted)
            {
                submitName();
            }
        }
        private void highScoreButton_Click(object sender, EventArgs e)
        {
            openHighScore();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            helpForm.BackColor = Color.Black;
            TextBox helpTextBox = makeTextBox(300, 300, 10, 10);
            helpTextBox.ScrollBars = ScrollBars.Vertical;
            helpTextBox.Multiline = true;
            helpTextBox.ReadOnly = true;
            helpTextBox.Text = "HOW TO PLAY \r\n\r\n" +
                               "Use the left joystick to move \r\n\r\n" +
                               "Use the rightstick to aim your arrows. Press \r\n\r\n" +
                               "Look at your alerts tab on the top right to get alerts about your environment. \r\n\r\n" +
                               "Buy arrows and secrets in the shop. Click on the shop button to open it. \r\n\r\n" +
                               "The arrows are used to hurt the wumpus. Try to collect as many arrows as possible before facing off against the wumpus. \r\n\r\n" +
                               "Secrets are hints for trivia questions. These hints can be used to halp answer questions in chests to aquire gold. \r\n\r\n" +
                               "Shoot the wumpus from an adjacent room to stop it from running away. If this action is not preformed, the wumpus will run away when it is at half health. \r\n\r\n";
            TextBox creditsTextBox = makeTextBox(300, 300, 310, 10);
            creditsTextBox.ForeColor = Color.Aqua;
            creditsTextBox.ScrollBars = ScrollBars.Vertical;
            creditsTextBox.Multiline = true;
            creditsTextBox.ReadOnly = true;
            creditsTextBox.Text = "CREDITS \r\n\r\n" +
                                  "Shiven: GameControl, Physics, Object Behaviors, Slim Dx and Xbox \r\n\r\n" +
                                  "Kenny: Game UI, Music \r\n\r\n Angela: All Game Art, Trivia, Question and Secret Objects \r\n\r\n Ryan: Sound and Highscore Objects, Highscore text file management \r\n\r\n Shruti: Created caves/map text files \r\n\r\n Kevin: Map Data Object";


            helpForm.Controls.Add(helpTextBox);
            helpForm.Controls.Add(creditsTextBox);
            helpForm.ShowDialog();
        }
        private void exitGameButton_Click(object sender, EventArgs e)
        {
            gameForm.Close();
        }
        public void submitName()
        {
            submitNameForm.ShowDialog();
        }
        private void SubmitNameButton_Click(object sender, EventArgs e)
        {
            playerName = nameBox.Text;
            submitNameForm.Close();
            selectCave();
        }
        public string getName()
        {
            return playerName;
        }
        public void selectCave()
        {
            selectCaveForm.ShowDialog();
        }
        private void cave1_Click(Object sender, EventArgs e)
        {
            selectedCave = 1;
            selectCaveForm.Close();
        }
        private void cave2_Click(Object sender, EventArgs e)
        {
            selectedCave = 2;
            selectCaveForm.Close();
        }
        private void cave3_Click(Object sender, EventArgs e)
        {
            selectedCave = 3;
            selectCaveForm.Close();
        }
        private void cave4_Click(Object sender, EventArgs e)
        {
            selectedCave = 4;
            selectCaveForm.Close();
        }
        private void cave5_Click(Object sender, EventArgs e)
        {
            selectedCave = 5;
            selectCaveForm.Close();
        }
        private void SelectCaveForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (selectedCave != 0)
            {
                startGame();
            }
            else
            {
                openMainMenu();
            }
        }
        private void startGame()
        {
            gameStarted = true;
            gamePanel.Size = new Size(scX, scY);
            gamePanel.Location = new System.Drawing.Point(0, 0);
            gc = new GameControl(this, screen, selectedCave, sound);
        }
        #endregion

        #region Templates - efficiently creates UI elements
        // makes a Form with the given title and dimensions
        private static Form makeForm(string title, int width, int height)
        {
            Form newForm = new Form();
            newForm.Text = title;
            newForm.Size = new Size(width, height);
            newForm.Location = new System.Drawing.Point(0, 0);
            return newForm;
        }
        // makes a label with given text and position
        private static Label makeLabel(string text, int x, int y)
        {
            Label newLabel = new Label();
            newLabel.Text = text;
            newLabel.Location = new System.Drawing.Point(x, y);
            return newLabel;
        }
        // makes a button with given text and position
        private static Button makeButton(string text, int x, int y)
        {
            Button newButton = new Button();
            newButton.Text = text;
            newButton.Location = new System.Drawing.Point(x, y);
            return newButton;
        }
        // makes a textbox of given dimensions and position
        private static TextBox makeTextBox(int xSize, int ySize, int x, int y)
        {
            TextBox newTextBox = new TextBox();
            newTextBox.Size = new Size(xSize, ySize);
            newTextBox.Location = new System.Drawing.Point(x, y);
            return newTextBox;
        }
        #endregion

        #region Images - drawn at specified location
        public void drawPlayer(Point p, bool flip)
        {
            if (flip)
            {
                player.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            player.Location = new System.Drawing.Point((int)(p.getX() - 90), (int)(p.getY() - 90));
            player.BringToFront();
        }
        public void drawWumpus(Point p)
        {
            wumpus.Location = new System.Drawing.Point((int)(p.getX() - wumpus.Width / 2), (int)(p.getY() - wumpus.Height / 2));
        }
        public void drawRoom(Point p)
        {
            room.SetBounds((int)p.getX() - room.Width / 2, (int)p.getY() - room.Height / 2, 1840, 1600);
        }
        public void drawChest(Point p)
        {
            chest.Location = new System.Drawing.Point((int)p.getX() - chest.Width / 2, (int)p.getY() - chest.Height / 2);
        }
        public void drawDoor(Point p, int i)
        {
            doors[i].Location = new System.Drawing.Point((int)(p.getX() - door.Width / 2), (int)(p.getY() - door.Height / 2));
        }
        public void drawBat(Point p, int i)
        {
            bats[i].Location = new System.Drawing.Point((int)(p.getX() - bats[i].Width / 2), (int)(p.getY() - bats[i].Height / 2));
        }
        public void drawPit(Point p, int i)
        {
            pits[i].SetBounds((int)p.getX() - 200, (int)p.getY() - 200, 400, 400);
        }
        public void drawRock(int i, Point p)
        {
            rocks[i].SetBounds((int)p.getX() - 70, (int)p.getY() - 60, 100, 100);
        }
        #endregion

        #region Arrow & Fireball Projectile Functions
        // Create new arrow/fireball
        public void newArrowProjectile(Point p, float angle)
        {
            TransparentPictureBox newArrow = new TransparentPictureBox(arrow.getZOrder());
            newArrow.Image = arrow.Image;
            newArrow.rotateTo(angle);
            newArrow.SetBounds((int)p.getX(), (int)p.getY(), arrow.Width, arrow.Height);
            arrowList.Add(newArrow);
            gamePanel.Controls.Add(newArrow);
            newArrow.BringToFront();
        }
        public void newFireball(Point p, float angle)
        {
            TransparentPictureBox newFireball = new TransparentPictureBox(fireball.getZOrder());
            newFireball.Image = fireball.Image;
            newFireball.rotateTo(angle);
            newFireball.SetBounds((int)p.getX(), (int)p.getY(), fireball.Width, fireball.Height);
            fireballList.Add(newFireball);
            gamePanel.Controls.Add(newFireball);
            newFireball.BringToFront();
        }
        // Access specific arrow/fireball
        public TransparentPictureBox getArrow(int arrowNum)
        {
            if (arrowNum < arrowList.Count && arrowNum >= 0)
            {
                return arrowList[arrowNum];
            }
            return null;
        }
        public TransparentPictureBox getFireball(int fireballNum)
        {
            return fireballList[fireballNum];
        }
        // Draw arrow/fireball
        public void drawArrow(Point p, int arrowNum)
        {
            arrowList[arrowNum].Location = new System.Drawing.Point((int)p.getX() - arrowList[arrowNum].Width / 2, (int)p.getY() - arrowList[arrowNum].Height / 2);
        }
        public void drawFireball(Point p, int angle, int fireballNum)
        {
            fireballList[fireballNum].Location = new System.Drawing.Point((int)p.getX() - fireballList[fireballNum].Width / 2, (int)p.getY() - fireballList[fireballNum].Height / 2);
        }
        // Kill arrow/fireball
        public void killArrow(int arrowNum)
        {
            arrowList[arrowNum].Parent = null;
            gameForm.Controls.Remove(arrowList[arrowNum]);
            arrowList.RemoveAt(arrowNum);
        }
        public void killFireball(int fireballNum)
        {
            fireballList[fireballNum].Parent = null;
            gameForm.Controls.Remove(fireballList[fireballNum]);
            fireballList.RemoveAt(fireballNum);
        }
        #endregion

        #region Update HUD
        public void updateGold(int golds)
        {
            this.golds.Text = golds.ToString();
        }
        public void updateArrows(int arrows)
        {
            this.arrows.Text = arrows.ToString();
        }
        public void displayAlerts(string alerts)
        {
            alertBox.AppendText(alerts);
        }
        #endregion

        #region Trivia

        int answer = -1;
        public void openTrivia(int questionNumber, String question, String[] answers, string secrets)
        {

            answer = -1;
            // Creates a new form for each question
            triviaForm = makeForm("Trivia", scX / 3, scX / 2);

            triviaForm.BackgroundImage = triviaBackground;
            triviaForm.Width = triviaBackground.Width;
            triviaForm.Height = triviaBackground.Height;

            triviaForm.StartPosition = FormStartPosition.Manual;
            triviaForm.Location = new System.Drawing.Point((scX - triviaForm.Width) / 2, (scY - triviaForm.Height) / 2);

            // Question and Answer Tag Labels
            Label questionNum = makeLabel("Q" + questionNumber + ":", triviaForm.Width / 2 - 20, triviaForm.Height * 3 / 10);
            questionNum.AutoSize = true;

            Label aLabel = makeLabel("A.", triviaForm.Width * 2 / 10 + 40, triviaForm.Height * 6 / 10);
            aLabel.AutoSize = true;
            Label bLabel = makeLabel("B.", triviaForm.Width * 2 / 10 + 40, triviaForm.Height * 7 / 10);
            bLabel.AutoSize = true;
            Label cLabel = makeLabel("C.", triviaForm.Width * 7 / 10 - 20, triviaForm.Height * 6 / 10);
            cLabel.AutoSize = true;
            Label dLabel = makeLabel("D.", triviaForm.Width * 7 / 10 - 20, triviaForm.Height * 7 / 10);
            dLabel.AutoSize = true;

            // Question and Answers printed on labels
            Label questionText = makeLabel(question, triviaForm.Width / 2 - 50, triviaForm.Height * 4 / 10);
            questionText.MaximumSize = new Size(200, 0);
            questionText.AutoSize = true;
            questionText.Location = new System.Drawing.Point(triviaForm.Width / 2 - questionText.Size.Width / 2 - 20, triviaForm.Height * 4 / 10);

            // Answer A label
            Label answerA = makeLabel(answers[0], triviaForm.Width * 2 / 10 + 60, triviaForm.Height * 6 / 10);
            answerA.AutoSize = true;
            answerA.MaximumSize = new Size(200, 0);
            // Answer B label
            Label answerB = makeLabel(answers[1], triviaForm.Width * 2 / 10 + 60, triviaForm.Height * 7 / 10);
            answerB.AutoSize = true;
            answerB.MaximumSize = new Size(200, 0);
            // Answer C label
            Label answerC = makeLabel(answers[2], triviaForm.Width * 7 / 10, triviaForm.Height * 6 / 10);
            answerC.AutoSize = true;
            answerC.MaximumSize = new Size(200, 0);
            // Answer D label
            Label answerD = makeLabel(answers[3], triviaForm.Width * 7 / 10, triviaForm.Height * 7 / 10);
            answerD.AutoSize = true;
            answerD.MaximumSize = new Size(200, 0);

            questionNum.BackColor = Color.Transparent;
            questionText.BackColor = Color.Transparent;
            aLabel.BackColor = Color.Transparent;
            bLabel.BackColor = Color.Transparent;
            cLabel.BackColor = Color.Transparent;
            dLabel.BackColor = Color.Transparent;
            answerA.BackColor = Color.Transparent;
            answerB.BackColor = Color.Transparent;
            answerC.BackColor = Color.Transparent;
            answerD.BackColor = Color.Transparent;

            // Secrets
            TextBox secretsTextBox = makeTextBox(300, 100, triviaForm.Width / 2 - 150, triviaForm.Height * 8 / 10);
            secretsTextBox.Multiline = true;
            secretsTextBox.ScrollBars = ScrollBars.Vertical;
            secretsTextBox.Text = "Secrets Obtained: \r\n\r\n" + secrets;

            // Answer box and submit button
            answerBox = makeTextBox(25, 50, triviaForm.Width / 2 - 15, triviaForm.Height * 6 / 10);
            submit = new Button(); submit.Text = "Submit"; submit.Size = new Size(triviaForm.Width / 10, triviaForm.Height / 22);
            submit.Location = new System.Drawing.Point((triviaForm.Width - submit.Width) / 2, triviaForm.Height * 8 / 11);
        
            this.submit.Click += new System.EventHandler(this.submit_Click);
            triviaForm.AcceptButton = submit;

            // Add controls to trivia form
            triviaForm.Controls.AddRange(new Control[] {questionNum, aLabel, bLabel, cLabel, dLabel,
                                                        questionText, answerA, answerB, answerC, answerD,
                                                        answerBox, submit,secretsTextBox});
            triviaForm.ShowDialog();
        }



        // when submit is clicked, the answer is updated
        public void submit_Click(object sender, EventArgs e)
        {
            String[] answerLetters = { "A", "B", "C", "D" };
            for (int i = 0; i <= 3; i++)
            {
                if (answerBox.Text.ToUpper().Equals(answerLetters[i]))
                {
                    answer = i + 1;
                    triviaForm.Close();
                }
            }
        }

        public int getRecentAnswer()
        {
            return answer;
        }

        #endregion

        public GameControl getGc()
        {
            return gc;
        }

        

        /////////////////////////////////////////////////////// Highscore ///////////////////////////////////////////////////////
        private void updateHighScore(Label[] playerNames, Label [] highScores)
        {

            // modifies highScore form
            for (int i = 0; i < playerNames.Length; i++)
            {
                playerNames[i] = new Label();
                highScores[i] = new Label();

                playerNames[i].Text = hs.topScoreNames()[i];
                playerNames[i].BackColor = Color.Transparent;
                highScores[i].Text = hs.topScores()[i].ToString();
                highScores[i].BackColor = Color.Transparent;
            }
        }
        public void openHighScore()
        {
            highScoreForm.BackgroundImage = highscoresBackground;
            highScoreForm.Height = highscoresBackground.Height;
            highScoreForm.Width = highscoresBackground.Width + 10;

            for (int i = 0; i < 10; i++)
            {
                playerNames[i] = new Label();
                highScores[i] = new Label();

                playerNames[i].Location = new System.Drawing.Point(highScoreForm.Width / 6, (i + 3) * (highScoreForm.Height / 15));
                playerNames[i].AutoSize = true;
                highScores[i].Location = new System.Drawing.Point((highScoreForm.Width * 4) / 6, (i + 3) * (highScoreForm.Height / 15));
                highScores[i].AutoSize = true;
                highScoreForm.Controls.Add(playerNames[i]);
                highScoreForm.Controls.Add(highScores[i]);
            }

            updateHighScore(playerNames, highScores);
            highScoreForm.ShowDialog();
        }
        private void ExitHighScore_Click(object sender, EventArgs e)
        {
            highScoreForm.Close();
        }

        ////////////////////////////////////////////////////////// Shop //////////////////////////////////////////////////////////
        public event EventHandler arrowBought = delegate { };
        public event EventHandler secretBought = delegate { };
        public void shopButton_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {

                shopForm.ShowDialog();
            }
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
        public void winScreen()
        {
            gameForm.BackgroundImage = win.Image;
            // Update highscore if needed
            hs.modifyLeaderboard(playerName, "Cave " + selectedCave, gc.getGold(), gc.getTurns(), gc.getArrows());
            updateHighScore(playerNames, highScores);
            endGame();
        }
        public void loseScreen()
        {
            gameForm.BackgroundImage = lose.Image;
            endGame();
        }
        public void endGame()
        {
            //the game is over
            gameStarted = false;

            playGameButton.Hide();
            helpButton.Hide();
            highScoreButton.Hide();
            exitGameButton.Hide();


            mainMenu.Show();

            // Reset game panel
            gamePanel.Size = new Size(0, 0);

            // Stop the GC
            gc = null;

            refresh();
        }

        private void MainMenu_Click(object sender, EventArgs e)
        {
            mainMenu.Hide();
            gameForm.BackgroundImage = mainBackground;
            playGameButton.Show();
            helpButton.Show();
            highScoreButton.Show();
            exitGameButton.Show();
            refresh();
        }

        public void refresh()
        {
            gamePanel.Invalidate();
        }

    }
}
