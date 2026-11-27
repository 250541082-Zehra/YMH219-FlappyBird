# YMH219-FlappyBird

Windows Forms ile geliştirilmiş, Flappy Bird benzeri bir masaüstü oyunu.
Nesne Tabanlı Programlama (YMH 219) dersi ek uygulama ödevi kapsamında hazırlanmıştır.

- **Öğrenci:** Zehra Özdemir
- **Öğrenci No:** 250541082

## Projenin Amacı

Zamanlayıcı (Timer) ve klavye girişiyle çalışan bir oyun döngüsü kurmak; ekrandaki nesneleri hareket ettirmek,
çarpışma kontrolü yapmak ve oyun durumunu sınıflarla (nesne tabanlı) yönetmek.

## Kullanılan Teknolojiler

- C# ve .NET (Windows Forms)
- `System.Windows.Forms.Timer` ile oyun döngüsü (yaklaşık 60 FPS)
- `System.Drawing` (GDI+) ile çizim
- Harici paket kullanılmamıştır

## Kurulum ve Çalıştırma

1. Depoyu indirin: `git clone https://github.com/250541082-Zehra/YMH219-FlappyBird.git`
2. `YMH219-FlappyBird.slnx` dosyasını Visual Studio ile açın.
3. `F5` ile çalıştırın.

Not: Proje Windows Forms kullandığı için yalnızca Windows'ta çalışır.

## Kontroller

| Tuş | İşlev |
|---|---|
| `SPACE` | Zıpla (ilk basış oyunu başlatır) |
| `R` veya **Yeni Oyun / Yeniden Başlat** butonu | Oyunu sıfırla |

## Özellikler

- Kuş yer çekimiyle düşer, SPACE ile yukarı sıçrar.
- 3 engel sağdan sola ilerler. Ekrandan çıkan engel, sağ tarafta rastgele bir boşluk yüksekliğiyle yeniden konumlanır.
- Boruya veya zemine çarpınca oyun durur ve **GAME OVER** gösterilir.
- Kuş her engeli geçtiğinde skor artar.
- Yeni Oyun / Yeniden Başlat özelliği vardır.
- Skor, en yüksek skor, seviye ve hız ekranın üstünde canlı gösterilir.
- **Seviye sistemi:** Her 5 puanda seviye artar. Engeller hızlanır (en fazla 6.5) ve boşluk daralır (en az 120 piksel).
- **En yüksek skor** dosyada saklanır, oyunu kapatıp açınca kaybolmaz.

## Sınıf Yapısı

| Sınıf | Görevi |
|---|---|
| `Bird` | Kuşun konumu, hızı, yer çekimi ve zıplama |
| `Pipe` | Bir engel (üst ve alt boru, boşluk), hareket ve yeniden konumlanma |
| `Game` | Oyun kuralları: skor, seviye, çarpışma, engel yönetimi |
| `GameState` | Oyunun durumu: `Ready`, `Running`, `GameOver` |
| `HighScoreStorage` | En yüksek skoru dosyaya yazma ve okuma |
| `Form1` | Arayüz, Timer, klavye girişi ve çizim |

## Oyun Nasıl Çalışır?

1. `Form1` içindeki Timer her 16 ms'de bir çalışır.
2. Her çalışmada `Game.Update()` çağrılır: kuş düşer, borular sola kayar, skor ve çarpışma kontrol edilir.
3. Ardından ekran yeniden çizilir (`Invalidate`, `Paint`).
4. Oyun kuralları `Game` sınıfındadır. Form yalnızca tuşları iletir ve çizim yapar.
5. Çarpışma, kuşun `HitBox` dikdörtgeni ile boruların dikdörtgenlerinin kesişimine bakılarak bulunur.

## Ekran Görüntüleri

<img width="487" height="673" alt="ekran2" src="https://github.com/user-attachments/assets/f7b8176a-a674-4451-b43d-4cd9b9ff5bc0" />

<img width="481" height="677" alt="ekran1" src="https://github.com/user-attachments/assets/f30ed815-6cfa-4c8a-8994-f2471acf23b2" />


## Notlar

- En yüksek skor `%LocalAppData%\YMH219-FlappyBird\highscore.txt` dosyasında tutulur.
- Ses efekti yoktur. Tüm grafikler kodla çizilmiştir, görsel dosya kullanılmamıştır.
