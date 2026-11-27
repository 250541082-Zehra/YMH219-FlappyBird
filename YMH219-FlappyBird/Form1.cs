using System.Drawing.Drawing2D;

namespace YMH219_FlappyBird
{
    public partial class Form1 : Form
    {
        // Oyun alanının ölçüleri (oyun koordinatları)
        private const int CanvasWidth = 480;
        private const int CanvasHeight = 560;
        private const int GroundHeight = 70;
        private const int GroundY = CanvasHeight - GroundHeight;   // Zeminin başladığı yer
        private const float StartY = 250;                          // Kuşun başlangıç yüksekliği

        private readonly Bird _bird = new Bird(100, StartY);
        private bool _gameOver = false;
        private bool _flapKeyDown = false;   // SPACE basılı tutulunca sürekli zıplamasın diye

        public Form1()
        {
            InitializeComponent();
            ClientSize = new Size(480, 640);

            // Olayları (events) kodla bağlıyoruz
            pbCanvas.Paint += pbCanvas_Paint;
            gameTimer.Tick += gameTimer_Tick;
            btnRestart.Click += btnRestart_Click;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // ---------- Oyun döngüsü: timer her 16 ms'de bir çalışır ----------
        private void gameTimer_Tick(object? sender, EventArgs e)
        {
            _bird.Update();

            // Zemine çarptı mı?
            if (_bird.Y + Bird.Diameter >= GroundY)
            {
                _bird.PlaceOn(GroundY);
                _gameOver = true;
                gameTimer.Stop();
                lblState.Text = "Zemine çarptı - R ile yeniden başla";
            }

            pbCanvas.Invalidate();   // Ekranı yeniden çiz
        }

        // ---------- Çizim ----------
        private void pbCanvas_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Ekran ölçeklemesi (%125 gibi) olsa bile oyun doğru boyutta çizilsin
            g.ScaleTransform(pbCanvas.Width / (float)CanvasWidth, pbCanvas.Height / (float)CanvasHeight);

            // Zemin
            g.FillRectangle(Brushes.SandyBrown, 0, GroundY, CanvasWidth, GroundHeight);
            g.FillRectangle(Brushes.ForestGreen, 0, GroundY, CanvasWidth, 14);

            // Kuş
            _bird.Draw(g);
        }

        // ---------- Klavye ----------
        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (!_flapKeyDown)
                {
                    _flapKeyDown = true;
                    Flap();
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.R)
            {
                RestartGame();
                e.Handled = true;
            }
        }

        private void Form1_KeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                _flapKeyDown = false;
            }
        }

        // ---------- Buton ----------
        private void btnRestart_Click(object? sender, EventArgs e)
        {
            RestartGame();
            ActiveControl = null;   // Odağı butondan al, yoksa SPACE butona basmış olur
        }

        // ---------- Yardımcı metotlar ----------
        private void Flap()
        {
            if (_gameOver)
            {
                return;
            }

            if (!gameTimer.Enabled)   // İlk SPACE oyunu başlatır
            {
                gameTimer.Start();
                lblState.Text = "Oyun sürüyor";
            }

            _bird.Flap();
        }

        private void RestartGame()
        {
            _gameOver = false;
            gameTimer.Stop();
            _bird.Reset(StartY);
            lblState.Text = "Hazır";
            pbCanvas.Invalidate();
        }
    }
}