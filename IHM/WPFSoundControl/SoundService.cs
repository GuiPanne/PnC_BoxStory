using System.IO;
using System.Windows.Media;
using Logic.SoundControl;
using NAudio.Wave;

namespace IHM.WPFSoundControl;
/// <summary>
/// The sound service who launch every sound who can be heard in the game
/// </summary>
public class SoundService :ISoundService
{
    private List<MediaPlayer> activeSfx;
    private MediaPlayer musicPlayer;
    private double volumeEffect;
    private double volumeMusic;
    private double volumeDialogue;
    private string[] musicFilesName;
    private Dictionary<char, int> frequencyMap = new Dictionary<char, int>
    {
        // Voyelles → graves claires (~150–200 Hz)
        { 'A', 160 }, { 'E', 180 }, { 'I', 200 }, { 'O', 170 }, { 'U', 190 },
        { 'a', 160 }, { 'e', 180 }, { 'i', 200 }, { 'o', 170 }, { 'u', 190 },

        // Consonnes fréquentes → médium grave (~220–280 Hz)
        { 'L', 220 }, { 'M', 240 }, { 'N', 250 }, { 'R', 260 }, { 'S', 280 },
        { 'l', 220 }, { 'm', 240 }, { 'n', 250 }, { 'r', 260 }, { 's', 280 },

        // Lettres rares → un peu plus aigu (~300–340 Hz, mais pas trop haut)
        { 'Q', 300 }, { 'Z', 320 }, { 'X', 330 }, { 'J', 340 }, { 'K', 310 },
        { 'V', 320 }, { 'W', 300 }, { 'Y', 330 },
        { 'q', 300 }, { 'z', 320 }, { 'x', 330 }, { 'j', 340 }, { 'k', 310 },
        { 'v', 320 }, { 'w', 300 }, { 'y', 330 },

        // Autres lettres → autour de 220–280 Hz
        { 'B', 230 }, { 'C', 240 }, { 'D', 250 }, { 'F', 260 }, { 'G', 270 },
        { 'H', 280 }, { 'P', 260 }, { 'T', 270 },

        // Ponctuation (effets un peu spéciaux mais toujours graves)
        { '.', 200 }, { ',', 180 }, { '!', 300 }, { '?', 320 }, { ':', 220 }, { ';', 240 }
    };
    
    public double VolumeMusic
    {
        get => musicPlayer.Volume;
        set
        {
            volumeMusic = value* 0.5;
            musicPlayer.Volume = volumeMusic; ;
        }
    }
    public double VolumeEffect
    {
        get => volumeEffect;
        set => volumeEffect = value;
    }
    public double VolumeDialogue
    {
        get => volumeDialogue;
        set => volumeDialogue = value;
    }

    /// <summary>
    /// init the sound service
    /// </summary>
    public SoundService()
    {
        activeSfx = new List<MediaPlayer>();
        musicPlayer = new MediaPlayer();
        musicFilesName = GetMusicFilesName();
        volumeEffect = 0.5;
        volumeDialogue = 0.5;
        VolumeMusic = 0.5;
        musicPlayer.MediaEnded += (sender, args) =>
        {
            LaunchMusic();
        };
    }
    /// <summary>
    /// get all music file name to launch them more easily
    /// </summary>
    /// <returns></returns>
    private string[] GetMusicFilesName()
    {
        string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Sound", "Music");
        return Directory.GetFileSystemEntries(basePath, "*.mp3");
    }
    public async void LaunchMusic()
    {
        await FadeVolume(volumeMusic, 0, 1000);
        musicPlayer.Position = TimeSpan.Zero;
        Random r = new Random();
        string chosenFile = musicFilesName[r.Next(0, musicFilesName.Length)];
        musicPlayer.Open(new Uri(chosenFile, UriKind.Absolute));
        
        musicPlayer.Play();
        await FadeVolume(0, volumeMusic, 1000);
    }
    public void PlaySfx(string fileName)
    {
        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Sound", "SFX", fileName);
        MediaPlayer sfx = new MediaPlayer();
        sfx.Volume = volumeEffect;
        sfx.Open(new Uri(fullPath, UriKind.Absolute));
        sfx.MediaEnded += (sender, args) =>
        {
            sfx.Close();
            activeSfx.Remove(sfx);
        };
        activeSfx.Add(sfx);
        sfx.Play();
    }

    public async void PlaySpecificMusic(string fileName)
    {
        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Sound", "SpecificMusic", fileName);
        musicPlayer.Open(new Uri(fullPath, UriKind.Absolute));
        this.musicPlayer.Volume = 0;
        musicPlayer.Play();
        
        await FadeVolume(0, volumeMusic, 1000);
    }
    
    /// <summary>
    /// fade in and out for the volume of the music
    /// </summary>
    /// <param name="from">the lowest volume</param>
    /// <param name="to">the high volume we go for</param>
    /// <param name="durationMs"> duration of the fade</param>
    private async Task FadeVolume(double from, double to, int durationMs)
    {
        int steps = 20;
        double stepSize = (to - from) / steps;
        int stepDelay = durationMs / steps;

        musicPlayer.Volume = from;
        for (int i = 0; i < steps; i++)
        {
            musicPlayer.Volume += stepSize;
            await Task.Delay(stepDelay);
        }
        musicPlayer.Volume = to;
    }
    
    public void PlayDialogue(char c)
    {
        int baseFreq = frequencyMap.ContainsKey(c) ? frequencyMap[c] : 200;
        Random rnd = new Random();
        int freq = baseFreq + rnd.Next(-10, 11); // random ±10 Hz
        int durationMs = rnd.Next(30, 60);       // random durée
        int sampleRate = 44100;
        int samples = (int)((sampleRate * durationMs) / 1000.0);
        var buffer = new float[samples];

        for (int i = 0; i < samples; i++)
        {
            double t = (double)i / sampleRate;

            // Vibrato léger
            double wobble = 0.02 * Math.Sin(2 * Math.PI * 5 * t);
            double sample = Math.Sin(2 * Math.PI * freq * (1 + wobble) * t);

            // Onde carrée mixée pour effet rétro
            double square = Math.Sign(Math.Sin(2 * Math.PI * freq * t));
            sample = 0.7 * sample + 0.3 * square;

            // Enveloppe ADSR simple
            double envelope;
            if (i < samples * 0.1) // attaque rapide
                envelope = (double)i / (samples * 0.1);
            else
                envelope = Math.Exp(-(i - samples * 0.1) / (samples * 0.9 / 5.0));

            buffer[i] = (float)(sample * envelope * (this.volumeDialogue*0.20));
        }

        var provider = new BufferedWaveProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 1));
        provider.AddSamples(buffer.SelectMany(BitConverter.GetBytes).ToArray(), 0, buffer.Length * 4);

        var waveOut = new WaveOutEvent();
        waveOut.Init(provider);
        waveOut.Play();

        Task.Delay(durationMs).ContinueWith(_ => waveOut.Dispose());
    }

}