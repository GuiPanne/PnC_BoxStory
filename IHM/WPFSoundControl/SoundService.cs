using System.IO;
using System.Windows.Media;
using Logic.SoundControl;

namespace IHM.WPFSoundControl;

public class SoundService :ISoundService
{
    private List<MediaPlayer> activeSfx;
    private string sfxPath;
    private MediaPlayer musicPlayer;
    private double volume;
    private string[] musicFilesName;


    private bool isStopMusic;

    public bool IsStopMusic
    {
        get{ return isStopMusic;}
        set{ isStopMusic = value;}
    }

    public SoundService()
    {
        activeSfx = new List<MediaPlayer>();
        musicPlayer = new MediaPlayer();
        musicFilesName = GetMusicFilesName();
        sfxPath = "../Resources/Sound/SFX/";
        volume = 0.1;
        isStopMusic = false;
        musicPlayer.Volume = volume;
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

    public void PlaySfx(string fileName)
    {
        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Sound", "SFX", fileName);

        MediaPlayer sfx = new MediaPlayer();
        sfx.Volume = 1;
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