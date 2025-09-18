namespace Logic.SoundControl;

public interface ISoundService
{
    void LaunchMusic();
    void PlaySfx(string fileName);
    void StopMusic();
    public void DialogSFX(string voiceName);

    double VolumeMusic { get; set; }
    double VolumeEffect { get; set; }
    double VolumeDialogue { get; set; }
    
}