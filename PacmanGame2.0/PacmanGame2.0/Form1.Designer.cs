namespace PacmanGame2._0
{
    partial class PacmanGame
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
            this.components = new System.ComponentModel.Container();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.Scoreboard = new System.Windows.Forms.Label();
            this.PlayerLives = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GameTimer
            // 
            this.GameTimer.Interval = 30;
            this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // Scoreboard
            // 
            this.Scoreboard.AutoSize = true;
            this.Scoreboard.ForeColor = System.Drawing.Color.White;
            this.Scoreboard.Location = new System.Drawing.Point(12, 360);
            this.Scoreboard.Name = "Scoreboard";
            this.Scoreboard.Size = new System.Drawing.Size(55, 20);
            this.Scoreboard.TabIndex = 0;
            this.Scoreboard.Text = "Score:";
            // 
            // PlayerLives
            // 
            this.PlayerLives.AutoSize = true;
            this.PlayerLives.ForeColor = System.Drawing.Color.White;
            this.PlayerLives.Location = new System.Drawing.Point(12, 380);
            this.PlayerLives.Name = "PlayerLives";
            this.PlayerLives.Size = new System.Drawing.Size(53, 20);
            this.PlayerLives.TabIndex = 1;
            this.PlayerLives.Text = "Lives: ";
            // 
            // PacmanGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(778, 744);
            this.Controls.Add(this.PlayerLives);
            this.Controls.Add(this.Scoreboard);
            this.DoubleBuffered = true;
            this.Name = "PacmanGame";
            this.Text = "PacmanGame";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PacmanGame_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PacmanGame_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Label Scoreboard;
        private System.Windows.Forms.Label PlayerLives;
    }
}

