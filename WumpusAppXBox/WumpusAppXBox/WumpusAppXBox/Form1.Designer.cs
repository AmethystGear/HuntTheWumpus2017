using System.Windows.Forms;

namespace WumpusAppXBox
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.winScreen = new WumpusAppXBox.TransparentPictureBox(0);
            this.looseScreen = new WumpusAppXBox.TransparentPictureBox(0);
            this.gamePanel = new WumpusAppXBox.DrawingPanel();
            this.main = new WumpusAppXBox.TransparentPictureBox(0);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.door = new WumpusAppXBox.TransparentPictureBox(4);
            this.rock4 = new WumpusAppXBox.TransparentPictureBox(2);
            this.rock3 = new WumpusAppXBox.TransparentPictureBox(2);
            this.rock2 = new WumpusAppXBox.TransparentPictureBox(2);
            this.rock1 = new WumpusAppXBox.TransparentPictureBox(2);
            this.fireball = new WumpusAppXBox.TransparentPictureBox(8);
            this.PlayerImg = new WumpusAppXBox.TransparentPictureBox(7);
            this.arrow = new WumpusAppXBox.TransparentPictureBox(9);
            this.wumpus = new WumpusAppXBox.TransparentPictureBox(6);
            this.Pit2 = new WumpusAppXBox.TransparentPictureBox(1);
            this.bat2 = new WumpusAppXBox.TransparentPictureBox(5);
            this.gold = new WumpusAppXBox.TransparentPictureBox(10);
            this.chest = new WumpusAppXBox.TransparentPictureBox(3);
            this.Pit1 = new WumpusAppXBox.TransparentPictureBox(1);
            this.room = new WumpusAppXBox.TransparentPictureBox(0);
            this.bat1 = new WumpusAppXBox.TransparentPictureBox(5);
            ((System.ComponentModel.ISupportInitialize)(this.winScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.looseScreen)).BeginInit();
            this.gamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.door)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rock4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rock3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rock2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rock1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fireball)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wumpus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bat2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.room)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bat1)).BeginInit();
            this.SuspendLayout();
            // 
            // winScreen
            // 
            this.winScreen.BackColor = System.Drawing.Color.Transparent;
            this.winScreen.Image = ((System.Drawing.Image)(resources.GetObject("winScreen.Image")));
            this.winScreen.Location = new System.Drawing.Point(838, 583);
            this.winScreen.Name = "winScreen";
            this.winScreen.Size = new System.Drawing.Size(190, 103);
            this.winScreen.TabIndex = 13;
            this.winScreen.TabStop = false;
            this.winScreen.Visible = false;
            this.winScreen.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // looseScreen
            // 
            this.looseScreen.BackColor = System.Drawing.Color.Transparent;
            this.looseScreen.Image = ((System.Drawing.Image)(resources.GetObject("looseScreen.Image")));
            this.looseScreen.Location = new System.Drawing.Point(785, 500);
            this.looseScreen.Name = "looseScreen";
            this.looseScreen.Size = new System.Drawing.Size(100, 50);
            this.looseScreen.TabIndex = 14;
            this.looseScreen.TabStop = false;
            this.looseScreen.Visible = false;
            // 
            // gamePanel
            // 
            this.gamePanel.Controls.Add(this.main);
            this.gamePanel.Controls.Add(this.textBox1);
            this.gamePanel.Controls.Add(this.door);
            this.gamePanel.Controls.Add(this.rock4);
            this.gamePanel.Controls.Add(this.rock3);
            this.gamePanel.Controls.Add(this.rock2);
            this.gamePanel.Controls.Add(this.rock1);
            this.gamePanel.Controls.Add(this.fireball);
            this.gamePanel.Controls.Add(this.PlayerImg);
            this.gamePanel.Controls.Add(this.arrow);
            this.gamePanel.Controls.Add(this.wumpus);
            this.gamePanel.Controls.Add(this.Pit2);
            this.gamePanel.Controls.Add(this.bat2);
            this.gamePanel.Controls.Add(this.gold);
            this.gamePanel.Controls.Add(this.chest);
            this.gamePanel.Controls.Add(this.Pit1);
            this.gamePanel.Controls.Add(this.room);
            this.gamePanel.Controls.Add(this.bat1);
            this.gamePanel.Location = new System.Drawing.Point(1, -1);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(695, 332);
            this.gamePanel.TabIndex = 12;
            // 
            // main
            // 
            this.main.BackColor = System.Drawing.Color.Transparent;
            this.main.Image = ((System.Drawing.Image)(resources.GetObject("main.Image")));
            this.main.Location = new System.Drawing.Point(585, 10);
            this.main.Name = "main";
            this.main.Size = new System.Drawing.Size(100, 50);
            this.main.TabIndex = 16;
            this.main.TabStop = false;
            this.main.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1466, 39);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(374, 300);
            this.textBox1.TabIndex = 11;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // door
            // 
            this.door.BackColor = System.Drawing.Color.Transparent;
            this.door.Image = ((System.Drawing.Image)(resources.GetObject("door.Image")));
            this.door.Location = new System.Drawing.Point(219, 10);
            this.door.Name = "door";
            this.door.Size = new System.Drawing.Size(203, 192);
            this.door.TabIndex = 15;
            this.door.TabStop = false;
            this.door.Visible = false;
            // 
            // rock4
            // 
            this.rock4.BackColor = System.Drawing.Color.Transparent;
            this.rock4.Image = ((System.Drawing.Image)(resources.GetObject("rock4.Image")));
            this.rock4.Location = new System.Drawing.Point(1341, 459);
            this.rock4.Name = "rock4";
            this.rock4.Size = new System.Drawing.Size(132, 132);
            this.rock4.TabIndex = 14;
            this.rock4.TabStop = false;
            this.rock4.Visible = false;
            // 
            // rock3
            // 
            this.rock3.BackColor = System.Drawing.Color.Transparent;
            this.rock3.Image = ((System.Drawing.Image)(resources.GetObject("rock3.Image")));
            this.rock3.Location = new System.Drawing.Point(1319, 613);
            this.rock3.Name = "rock3";
            this.rock3.Size = new System.Drawing.Size(117, 129);
            this.rock3.TabIndex = 13;
            this.rock3.TabStop = false;
            this.rock3.Visible = false;
            // 
            // rock2
            // 
            this.rock2.BackColor = System.Drawing.Color.Transparent;
            this.rock2.Image = ((System.Drawing.Image)(resources.GetObject("rock2.Image")));
            this.rock2.Location = new System.Drawing.Point(416, 182);
            this.rock2.Name = "rock2";
            this.rock2.Size = new System.Drawing.Size(109, 127);
            this.rock2.TabIndex = 12;
            this.rock2.TabStop = false;
            this.rock2.Visible = false;
            // 
            // rock1
            // 
            this.rock1.BackColor = System.Drawing.Color.Transparent;
            this.rock1.Image = ((System.Drawing.Image)(resources.GetObject("rock1.Image")));
            this.rock1.Location = new System.Drawing.Point(396, 112);
            this.rock1.Name = "rock1";
            this.rock1.Size = new System.Drawing.Size(115, 128);
            this.rock1.TabIndex = 11;
            this.rock1.TabStop = false;
            this.rock1.Visible = false;
            // 
            // fireball
            // 
            this.fireball.BackColor = System.Drawing.Color.Transparent;
            this.fireball.Image = ((System.Drawing.Image)(resources.GetObject("fireball.Image")));
            this.fireball.Location = new System.Drawing.Point(468, -198);
            this.fireball.Name = "fireball";
            this.fireball.Size = new System.Drawing.Size(214, 201);
            this.fireball.TabIndex = 8;
            this.fireball.TabStop = false;
            this.fireball.Visible = false;
            // 
            // PlayerImg
            // 
            this.PlayerImg.BackColor = System.Drawing.Color.Transparent;
            this.PlayerImg.Image = ((System.Drawing.Image)(resources.GetObject("PlayerImg.Image")));
            this.PlayerImg.Location = new System.Drawing.Point(24, -33);
            this.PlayerImg.Name = "PlayerImg";
            this.PlayerImg.Size = new System.Drawing.Size(179, 178);
            this.PlayerImg.TabIndex = 0;
            this.PlayerImg.TabStop = false;
            this.PlayerImg.Visible = false;
            // 
            // arrow
            // 
            this.arrow.BackColor = System.Drawing.Color.Transparent;
            this.arrow.Image = ((System.Drawing.Image)(resources.GetObject("arrow.Image")));
            this.arrow.Location = new System.Drawing.Point(1030, -137);
            this.arrow.Name = "arrow";
            this.arrow.Size = new System.Drawing.Size(100, 131);
            this.arrow.TabIndex = 3;
            this.arrow.TabStop = false;
            this.arrow.Visible = false;
            // 
            // wumpus
            // 
            this.wumpus.BackColor = System.Drawing.Color.Transparent;
            this.wumpus.Image = ((System.Drawing.Image)(resources.GetObject("wumpus.Image")));
            this.wumpus.Location = new System.Drawing.Point(-9, -167);
            this.wumpus.Name = "wumpus";
            this.wumpus.Size = new System.Drawing.Size(367, 476);
            this.wumpus.TabIndex = 7;
            this.wumpus.TabStop = false;
            this.wumpus.Visible = false;
            // 
            // Pit2
            // 
            this.Pit2.BackColor = System.Drawing.Color.Transparent;
            this.Pit2.Image = ((System.Drawing.Image)(resources.GetObject("Pit2.Image")));
            this.Pit2.Location = new System.Drawing.Point(-290, -198);
            this.Pit2.Name = "Pit2";
            this.Pit2.Size = new System.Drawing.Size(460, 298);
            this.Pit2.TabIndex = 10;
            this.Pit2.TabStop = false;
            this.Pit2.Visible = false;
            // 
            // bat2
            // 
            this.bat2.BackColor = System.Drawing.Color.Transparent;
            this.bat2.Image = ((System.Drawing.Image)(resources.GetObject("bat2.Image")));
            this.bat2.Location = new System.Drawing.Point(42, -12);
            this.bat2.Name = "bat2";
            this.bat2.Size = new System.Drawing.Size(237, 157);
            this.bat2.TabIndex = 6;
            this.bat2.TabStop = false;
            this.bat2.Visible = false;
            // 
            // gold
            // 
            this.gold.BackColor = System.Drawing.Color.Transparent;
            this.gold.Image = ((System.Drawing.Image)(resources.GetObject("gold.Image")));
            this.gold.Location = new System.Drawing.Point(102, 39);
            this.gold.Name = "gold";
            this.gold.Size = new System.Drawing.Size(68, 63);
            this.gold.TabIndex = 1;
            this.gold.TabStop = false;
            this.gold.Visible = false;
            // 
            // chest
            // 
            this.chest.BackColor = System.Drawing.Color.Transparent;
            this.chest.Image = ((System.Drawing.Image)(resources.GetObject("chest.Image")));
            this.chest.Location = new System.Drawing.Point(42, -33);
            this.chest.Name = "chest";
            this.chest.Size = new System.Drawing.Size(222, 216);
            this.chest.TabIndex = 4;
            this.chest.TabStop = false;
            this.chest.Visible = false;
            // 
            // Pit1
            // 
            this.Pit1.BackColor = System.Drawing.Color.Transparent;
            this.Pit1.Image = ((System.Drawing.Image)(resources.GetObject("Pit1.Image")));
            this.Pit1.Location = new System.Drawing.Point(557, -12);
            this.Pit1.Name = "Pit1";
            this.Pit1.Size = new System.Drawing.Size(470, 659);
            this.Pit1.TabIndex = 9;
            this.Pit1.TabStop = false;
            this.Pit1.Visible = false;
            // 
            // room
            // 
            this.room.BackColor = System.Drawing.Color.Transparent;
            this.room.Image = ((System.Drawing.Image)(resources.GetObject("room.Image")));
            this.room.Location = new System.Drawing.Point(-20, -71);
            this.room.Name = "room";
            this.room.Size = new System.Drawing.Size(464, 282);
            this.room.TabIndex = 2;
            this.room.TabStop = false;
            this.room.Visible = false;
            // 
            // bat1
            // 
            this.bat1.BackColor = System.Drawing.Color.Transparent;
            this.bat1.Image = ((System.Drawing.Image)(resources.GetObject("bat1.Image")));
            this.bat1.Location = new System.Drawing.Point(-20, -33);
            this.bat1.Name = "bat1";
            this.bat1.Size = new System.Drawing.Size(320, 157);
            this.bat1.TabIndex = 5;
            this.bat1.TabStop = false;
            this.bat1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1906, 783);
            this.Controls.Add(this.winScreen);
            this.Controls.Add(this.looseScreen);
            this.Controls.Add(this.gamePanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.winScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.looseScreen)).EndInit();
            this.gamePanel.ResumeLayout(false);
            this.gamePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.door)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rock4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rock3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rock2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rock1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fireball)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wumpus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bat2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.room)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bat1)).EndInit();
            this.ResumeLayout(false);

        }



        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;


        private TransparentPictureBox player;
        private TransparentPictureBox PlayerImg;
        private TransparentPictureBox gold;
        private TransparentPictureBox room;
        private TransparentPictureBox arrow;
        private TransparentPictureBox chest;
        private TransparentPictureBox bat1;
        private TransparentPictureBox bat2;
        private TransparentPictureBox wumpus;
        private TransparentPictureBox fireball;
        private TransparentPictureBox Pit1;
        private TransparentPictureBox Pit2;
        private System.Windows.Forms.TextBox textBox1;
        private DrawingPanel gamePanel;
        private TransparentPictureBox rock4;
        private TransparentPictureBox rock3;
        private TransparentPictureBox rock2;
        private TransparentPictureBox rock1;
        private TransparentPictureBox door;
        private TransparentPictureBox looseScreen;
        private TransparentPictureBox winScreen;
        private TransparentPictureBox main;
    }
}

