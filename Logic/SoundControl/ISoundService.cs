namespace Logic.SoundControl;

/// <summary>
/// interface who represents the sound service 
/// </summary>
public interface ISoundService
{
    /// <summary>
    /// play music in background
    /// </summary>
    void LaunchMusic();
    /// <summary>
    /// play an SFX sound
    /// </summary>
    /// <param name="fileName">the file name to play/param>
    void PlaySfx(string fileName);
    /// <summary>
    /// Get or set the music volume
    /// </summary>
    double VolumeMusic { get; set; }
    /// <summary>
    /// get or set the SFX volume
    /// </summary>
    double VolumeEffect { get; set; }
    /// <summary>
    /// get or set the dialogue sfx volume
    /// </summary>
    double VolumeDialogue { get; set; }

    void PlayDialogue(char c);

}