namespace YMH219_FlappyBird
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblScore = new Label();
            pbCanvas = new PictureBox();
            btnRestart = new Button();
            lblState = new Label();
            gameTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pbCanvas).BeginInit();
            SuspendLayout();
            // 
            // lblScore
            // 
            lblScore.BackColor = Color.DarkSlateGray;
            lblScore.ForeColor = Color.White;
            lblScore.Location = new Point(0, 0);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(480, 36);
            lblScore.TabIndex = 0;
            lblScore.Text = "Skor: 0";
            lblScore.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pbCanvas
            // 
            pbCanvas.BackColor = Color.LightSkyBlue;
            pbCanvas.Location = new Point(0, 36);
            pbCanvas.Name = "pbCanvas";
            pbCanvas.Size = new Size(480, 560);
            pbCanvas.TabIndex = 1;
            pbCanvas.TabStop = false;
            // 
            // btnRestart
            // 
            btnRestart.Location = new Point(10, 603);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(230, 30);
            btnRestart.TabIndex = 2;
            btnRestart.TabStop = false;
            btnRestart.Text = "Yeni Oyun / Yeniden Başlat (R)";
            btnRestart.UseVisualStyleBackColor = true;
            // 
            // lblState
            // 
            lblState.Location = new Point(250, 603);
            lblState.Name = "lblState";
            lblState.Size = new Size(225, 30);
            lblState.TabIndex = 3;
            lblState.Text = "Hazır";
            lblState.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // gameTimer
            // 
            gameTimer.Interval = 16;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(482, 653);
            Controls.Add(lblState);
            Controls.Add(btnRestart);
            Controls.Add(pbCanvas);
            Controls.Add(lblScore);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Flappy Bird - YMH219";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pbCanvas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblScore;
        private PictureBox pbCanvas;
        private Button btnRestart;
        private Label lblState;
        private System.Windows.Forms.Timer gameTimer;
    }
}
