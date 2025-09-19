using System.IO;
using System.Windows.Media;
using Logic.SoundControl;

namespace IHM.WPFSoundControl;
/// <summary>
/// The sound service who launch every sound who can be heard in the game
/// </summary>
public class SoundService :ISoundService
{
    private List<MediaPlayer> activeSfx;
    private string sfxPath;
    private MediaPlayer musicPlayer;
    private double volumeEffect;
    private double volumeDialogue;
    private string[] musicFilesName;
    
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
    /// <summary>
    /// init the sound service
    /// </summary>
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
    /// <summary>
    /// get all music file name to launch them more easily
    /// </summary>
    /// <returns></returns>
    private string[] GetMusicFilesName()
    {
        string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Sound", "Music");
        return Directory.GetFileSystemEntries(basePath, "*.mp3");
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