using System.Drawing.Drawing2D;

namespace YMH219_FlappyBird
{
    public partial class Form1 : Form
    {
        private readonly Game _game = new Game();
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

            gameTimer.Start();   // Timer hep çalışır, Game sınıfı oyun başlayana kadar bir şey yapmaz
            UpdateStatusTexts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // ---------- Oyun döngüsü: timer her 16 ms'de bir çalışır ----------
        private void gameTimer_Tick(object? sender, EventArgs e)
        {
            _game.Update();          // Kuralları Game sınıfı işletir
            UpdateStatusTexts();     // Skor, seviye, hız yazılarını güncelle
            pbCanvas.Invalidate();   // Ekranı yeniden çiz
        }

        // ---------- Klavye ----------
        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (!_flapKeyDown)
                {
                    _flapKeyDown = true;
                    _game.Flap();
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.R)
            {
                _game.Restart();
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
            _game.Restart();
            ActiveControl = null;   // Odağı butondan al, yoksa SPACE butona basmış olur
        }

        // ---------- Üstteki ve alttaki yazılar ----------
        private void UpdateStatusTexts()
        {
            lblScore.Text = $"Skor: {_game.Score}    En Yüksek: {_game.HighScore}    " +
                            $"Seviye: {_game.Level}    Hız: {_game.PipeSpeed:0.0}";

            switch (_game.State)
            {
                case GameState.Ready:
                    lblState.Text = "Hazır - SPACE'e bas";
                    break;
                case GameState.Running:
                    lblState.Text = "Oyun sürüyor";
                    break;
                default:
                    lblState.Text = "Oyun bitti - R'ye bas";
                    break;
            }
        }

        // ---------- Çizim ----------
        private void pbCanvas_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Ekran ölçeklemesi (%125 gibi) olsa bile oyun doğru boyutta çizilsin
            g.ScaleTransform(pbCanvas.Width / (float)Game.Width, pbCanvas.Height / (float)Game.Height);

            // Borular (zeminin altında kalsın diye önce çiziyoruz)
            foreach (Pipe pipe in _game.Pipes)
            {
                pipe.Draw(g, _game.GroundY);
            }

            // Zemin
            g.FillRectangle(Brushes.SandyBrown, 0, _game.GroundY, Game.Width, Game.GroundHeight);
            g.FillRectangle(Brushes.ForestGreen, 0, _game.GroundY, Game.Width, 14);

            // Kuş
            _game.Bird.Draw(g);

            // Yazılar (başlık, skor, Game Over)
            DrawOverlay(g);
        }

        private void DrawOverlay(Graphics g)
        {
            using Font big = new Font("Segoe UI", 44f, FontStyle.Bold, GraphicsUnit.Pixel);
            using Font medium = new Font("Segoe UI", 22f, FontStyle.Bold, GraphicsUnit.Pixel);

            switch (_game.State)
            {
                case GameState.Ready:
                    DrawCenteredText(g, "Flappy Bird", big, 110);
                    DrawCenteredText(g, "Başlamak için SPACE'e bas", medium, 190);
                    break;

                case GameState.Running:
                    DrawCenteredText(g, _game.Score.ToString(), big, 25);
                    break;

                case GameState.GameOver:
                    DrawCenteredText(g, "GAME OVER", big, 100);
                    DrawCenteredText(g, $"Skor: {_game.Score}", medium, 180);
                    DrawCenteredText(g, $"En Yüksek: {_game.HighScore}", medium, 215);
                    DrawCenteredText(g, "Yeniden başlamak için R", medium, 260);
                    break;
            }
        }

        /// <summary>Yazıyı yatayda ortalar, okunsun diye siyah gölgeli çizer.</summary>
        private static void DrawCenteredText(Graphics g, string text, Font font, float y)
        {
            using StringFormat format = new StringFormat { Alignment = StringAlignment.Center };
            RectangleF area = new RectangleF(0, y, Game.Width, font.Height + 10);

            g.DrawString(text, font, Brushes.Black, new RectangleF(area.X + 2, area.Y + 2, area.Width, area.Height), format);
            g.DrawString(text, font, Brushes.White, area, format);
        }
    }
}