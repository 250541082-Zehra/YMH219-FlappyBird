namespace YMH219_FlappyBird;

/// <summary>
/// En yüksek skoru bilgisayardaki küçük bir metin dosyasında saklar,
/// böylece oyunu kapatıp açınca skor kaybolmaz.
/// </summary>
public static class HighScoreStorage
{
    // Dosya şurada durur: C:\Users\<kullanıcı>\AppData\Local\YMH219-FlappyBird\highscore.txt
    private static readonly string FilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "YMH219-FlappyBird",
        "highscore.txt");

    /// <summary>Dosyadan en yüksek skoru okur. Dosya yoksa veya bozuksa 0 döner.</summary>
    public static int Load()
    {
        try
        {
            if (!File.Exists(FilePath))
            {
                return 0;
            }

            string text = File.ReadAllText(FilePath).Trim();
            return int.TryParse(text, out int score) && score > 0 ? score : 0;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return 0;   // Okunamazsa oyun çökmesin, 0'dan başlasın
        }
    }

    /// <summary>En yüksek skoru dosyaya yazar.</summary>
    public static void Save(int score)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            File.WriteAllText(FilePath, score.ToString());
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            // Skor kaydedilemese bile oyun devam etmeli, bu yüzden sessizce geçiyoruz
        }
    }
}