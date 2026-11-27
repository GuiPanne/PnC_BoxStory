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

    /// <summary>
    /// play a sound for a specific character
    /// </summary>
    /// <param name="c">the character</param>
    void PlayDialogue(char c);
    
    /// <summary>
    /// Play a specific music and relaunch normal music after
    /// </summary>
    /// <param name="fileName">the music to launch</param>
    void PlaySpecificMusic(string fileName);
}