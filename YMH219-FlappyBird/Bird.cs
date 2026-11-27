namespace YMH219_FlappyBird;

/// <summary>
/// Oyuncunun kontrol ettiği kuş. Konum, hız ve yer çekimi bu sınıfta tutulur.
/// </summary>
public class Bird
{
    public const int Diameter = 34;              // Kuşun çapı (piksel)

    private const float Gravity = 0.6f;          // Her tick'te hıza eklenen değer (yer çekimi)
    private const float FlapStrength = -9f;      // Zıplayınca verilen hız (negatif = yukarı)

    public float X { get; }                      // Yatay konum (sabit kalır)
    public float Y { get; private set; }         // Dikey konum (sadece kuşun kendisi değiştirir)
    public float VelocityY { get; private set; } // Dikey hız

    public Bird(float x, float startY)
    {
        X = x;
        Y = startY;
    }

    /// <summary>Kuşun kapladığı dikdörtgen alan.</summary>
    public Rectangle Bounds => new Rectangle((int)X, (int)Y, Diameter, Diameter);

    /// <summary>Çarpışma için biraz küçültülmüş alan (oyuncuya karşı daha adil).</summary>
    public Rectangle HitBox => Rectangle.Inflate(Bounds, -5, -5);

    /// <summary>SPACE'e basılınca: kuşa ani bir yukarı hız verir.</summary>
    public void Flap()
    {
        VelocityY = FlapStrength;
    }

    /// <summary>Timer'ın her çalışmasında çağrılır: yer çekimi hızı artırır, hız da konumu değiştirir.</summary>
    public void Update()
    {
        VelocityY += Gravity;
        Y += VelocityY;

        // Ekranın üstünden çıkmasın
        if (Y < 0)
        {
            Y = 0;
            VelocityY = 0;
        }
    }

    /// <summary>Kuşu zeminin hemen üstüne oturtur.</summary>
    public void PlaceOn(float groundY)
    {
        Y = groundY - Diameter;
        VelocityY = 0;
    }

    /// <summary>Yeni oyunda kuşu başlangıç konumuna alır.</summary>
    public void Reset(float startY)
    {
        Y = startY;
        VelocityY = 0;
    }

    /// <summary>Kuşu çizer (gövde, kanat, göz, gaga).</summary>
    public void Draw(Graphics g)
    {
        Rectangle r = Bounds;

        g.FillEllipse(Brushes.Gold, r);
        g.DrawEllipse(Pens.DarkOrange, r);
        g.FillEllipse(Brushes.Orange, r.X + 3, r.Y + 16, 16, 10);
        g.FillEllipse(Brushes.White, r.X + 20, r.Y + 6, 12, 12);
        g.FillEllipse(Brushes.Black, r.X + 25, r.Y + 10, 5, 5);

        Point[] beak =
        {
            new Point(r.Right - 2, r.Y + 16),
            new Point(r.Right + 12, r.Y + 20),
            new Point(r.Right - 2, r.Y + 26)
        };
        g.FillPolygon(Brushes.OrangeRed, beak);
    }
}