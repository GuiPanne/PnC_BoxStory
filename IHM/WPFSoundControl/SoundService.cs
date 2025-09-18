using System.IO;
using System.Windows.Media;
using Logic.SoundControl;

namespace IHM.WPFSoundControl;

public class SoundService :ISoundService
{
    public double VolumeMusic
    {
        get => musicPlayer.Volume;
        set
        {
            musicPlayer.Volume = value;
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

    private List<MediaPlayer> activeSfx;
    private string sfxPath;
    private MediaPlayer musicPlayer;
    private double volumeEffect;
    private double volumeDialogue;
    private string[] musicFilesName;
    

    public SoundService()
    {
        activeSfx = new List<MediaPlayer>();
        musicPlayer = new MediaPlayer();
        musicFilesName = GetMusicFilesName();
        sfxPath = "../Resources/Sound/SFX/";
        volumeEffect = 0.5;
        volumeDialogue = 0.5;
        musicPlayer.Volume = 0.5;
    }

    private string[] GetMusicFilesName()
    {
        string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Sound", "Music");
        return Directory.GetFileSystemEntries(basePath, "*.mp3");
    }
    
    public void StopMusic()
    {
            musicPlayer.Stop();
    }

    public void LaunchMusic()
    {
        musicPlayer.Position = TimeSpan.Zero;
        Random r = new Random();
        string chosenFile = musicFilesName[r.Next(0, musicFilesName.Length)];
        musicPlayer.Open(new Uri(chosenFile, UriKind.Absolute));
        musicPlayer.MediaEnded += (sender, args) =>
        {
            LaunchMusic();
        };
        musicPlayer.Play();
    }

    public void DialogSFX(string voiceName)
    {
        throw new NotImplementedException();
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
}