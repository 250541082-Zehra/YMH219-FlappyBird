namespace YMH219_FlappyBird;

/// <summary>
/// Sağdan sola hareket eden engel: üstte ve altta iki boru, ortada kuşun geçeceği boşluk.
/// </summary>
public class Pipe
{
    public const int Width = 60;        // Borunun genişliği
    private const int CapHeight = 24;   // Boru ağzının yüksekliği (sadece görünüm için)

    private static readonly Brush BodyBrush = new SolidBrush(Color.FromArgb(76, 175, 80));
    private static readonly Brush CapBrush = new SolidBrush(Color.FromArgb(56, 142, 60));
    private static readonly Pen OutlinePen = new Pen(Color.FromArgb(27, 94, 32), 2);

    public float X { get; private set; }         // Borunun sol kenarı
    public int GapY { get; private set; }        // Boşluğun üst kenarı
    public int GapHeight { get; private set; }  // Boşluğun yüksekliği
    public bool Passed { get; set; }             // Kuş bu engeli geçti mi? (Adım 5'te skor için kullanacağız)

    public float Right => X + Width;             // Borunun sağ kenarı

    public Pipe(float x, int gapY, int gapHeight)
    {
        X = x;
        GapY = gapY;
        GapHeight = gapHeight;
    }

    /// <summary>Üst borunun alanı (en yukarıdan boşluğa kadar).</summary>
    public Rectangle TopRect => new Rectangle((int)X, 0, Width, GapY);

    /// <summary>Alt borunun alanı (boşluktan zemine kadar).</summary>
    public Rectangle BottomRect(int groundY)
    {
        int top = GapY + GapHeight;
        return new Rectangle((int)X, top, Width, groundY - top);
    }

    /// <summary>Boruyu sola kaydırır.</summary>
    public void Move(float speed)
    {
        X -= speed;
    }

    /// <summary>Ekrandan çıkan boruyu, sağ tarafta yeni bir boşluk yüksekliğiyle yeniden konumlar.</summary>
    public void Reposition(float x, int gapY, int gapHeight)
    {
        X = x;
        GapY = gapY;
        GapHeight = gapHeight;
        Passed = false;
    }

    public void Draw(Graphics g, int groundY)
    {
        Rectangle top = TopRect;
        Rectangle bottom = BottomRect(groundY);

        g.FillRectangle(BodyBrush, top);
        g.DrawRectangle(OutlinePen, top);
        g.FillRectangle(BodyBrush, bottom);
        g.DrawRectangle(OutlinePen, bottom);

        // Boru ağızları (biraz daha geniş)
        Rectangle topCap = new Rectangle(top.X - 4, top.Bottom - CapHeight, Width + 8, CapHeight);
        Rectangle bottomCap = new Rectangle(bottom.X - 4, bottom.Y, Width + 8, CapHeight);

        g.FillRectangle(CapBrush, topCap);
        g.DrawRectangle(OutlinePen, topCap);
        g.FillRectangle(CapBrush, bottomCap);
        g.DrawRectangle(OutlinePen, bottomCap);
    }
}