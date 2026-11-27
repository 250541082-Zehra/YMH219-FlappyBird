namespace YMH219_FlappyBird;

/// <summary>Oyunun içinde bulunabileceği üç durum.</summary>
public enum GameState
{
    Ready,      // Oyun açıldı, ilk SPACE bekleniyor
    Running,    // Oyun sürüyor
    GameOver    // Çarpma oldu, oyun durdu
}

/// <summary>
/// Oyunun tüm kurallarını yönetir: kuş, engeller, skor, seviye, hız ve çarpışma.
/// Form (arayüz) sadece bu sınıfı çağırır ve sonucu çizer.
/// </summary>
public class Game
{
    public const int Width = 480;
    public const int Height = 560;
    public const int GroundHeight = 70;

    private const float StartY = 250;       // Kuşun başlangıç yüksekliği
    private const int PipeCount = 3;        // Aynı anda dolaşan engel sayısı
    private const int PipeSpacing = 220;    // İki engel arasındaki yatay mesafe
    private const int BaseGap = 150;        // Seviye 1'deki boşluk yüksekliği
    private const int MinGap = 120;         // Boşluk bundan fazla daralmaz
    private const int PointsPerLevel = 5;   // Kaç puanda bir seviye atlanır

    private readonly Random _random = new Random();

    public Bird Bird { get; }
    public List<Pipe> Pipes { get; } = new List<Pipe>();
    public GameState State { get; private set; } = GameState.Ready;
    public int Score { get; private set; }
    public int HighScore { get; private set; }
    public int Level { get; private set; } = 1;

    /// <summary>Zeminin başladığı yükseklik.</summary>
    public int GroundY => Height - GroundHeight;

    /// <summary>Engellerin hızı: seviye arttıkça artar ama bir üst sınırı var.</summary>
    public float PipeSpeed => Math.Min(3f + (Level - 1) * 0.4f, 6.5f);

    /// <summary>Boşluğun yüksekliği: seviye arttıkça daralır ama bir alt sınırı var.</summary>
    private int CurrentGap => Math.Max(MinGap, BaseGap - (Level - 1) * 6);

    /// <summary>Oyun bittiğinde tetiklenir (Adım 6'da en yüksek skoru kaydetmek için kullanacağız).</summary>
    public event EventHandler? GameEnded;

    public Game(int initialHighScore = 0)
    {
        HighScore = initialHighScore;
        Bird = new Bird(100, StartY);
        CreatePipes();
    }

    /// <summary>SPACE tuşu: ilk basış oyunu başlatır, sonrakiler kuşu zıplatır.</summary>
    public void Flap()
    {
        if (State == GameState.Ready)
        {
            State = GameState.Running;
        }

        if (State == GameState.Running)
        {
            Bird.Flap();
        }
    }

    /// <summary>Yeni oyun: her şeyi başlangıç durumuna döndürür (en yüksek skor korunur).</summary>
    public void Restart()
    {
        Score = 0;
        Level = 1;
        State = GameState.Ready;
        Bird.Reset(StartY);
        CreatePipes();
    }

    /// <summary>Timer'ın her çalışmasında bir kez çağrılır (oyun döngüsü).</summary>
    public void Update()
    {
        if (State != GameState.Running)
        {
            return;
        }

        Bird.Update();

        foreach (Pipe pipe in Pipes)
        {
            pipe.Move(PipeSpeed);

            // Ekrandan tamamen çıktıysa, en sağdaki engelin arkasına yeniden yerleştir
            if (pipe.Right < 0)
            {
                float farthestX = Pipes.Max(p => p.X);
                int gap = CurrentGap;
                pipe.Reposition(farthestX + PipeSpacing, RandomGapY(gap), gap);
            }

            // Kuş engeli geçtiyse skoru artır (Passed: aynı engelden iki kez puan verilmesin)
            if (!pipe.Passed && pipe.Right < Bird.X)
            {
                pipe.Passed = true;
                Score++;
                Level = Score / PointsPerLevel + 1;
            }
        }

        CheckCollisions();
    }

    private void CheckCollisions()
    {
        // Zemine çarpma
        if (Bird.Y + Bird.Diameter >= GroundY)
        {
            Bird.PlaceOn(GroundY);
            EndGame();
            return;
        }

        // Boruya çarpma: kuşun çarpışma alanı borunun alanıyla kesişiyor mu?
        Rectangle hit = Bird.HitBox;
        foreach (Pipe pipe in Pipes)
        {
            if (pipe.TopRect.IntersectsWith(hit) || pipe.BottomRect(GroundY).IntersectsWith(hit))
            {
                EndGame();
                return;
            }
        }
    }

    private void EndGame()
    {
        State = GameState.GameOver;

        if (Score > HighScore)
        {
            HighScore = Score;
        }

        GameEnded?.Invoke(this, EventArgs.Empty);
    }

    private void CreatePipes()
    {
        Pipes.Clear();
        for (int i = 0; i < PipeCount; i++)
        {
            Pipes.Add(new Pipe(Width - 40 + i * PipeSpacing, RandomGapY(BaseGap), BaseGap));
        }
    }

    /// <summary>Boşluğun üst kenarı için, üstten ve zeminden pay bırakan rastgele bir yükseklik.</summary>
    private int RandomGapY(int gap)
    {
        const int margin = 60;
        return _random.Next(margin, GroundY - gap - margin);
    }
}