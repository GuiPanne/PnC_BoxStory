namespace Logic.SoundControl;

public interface ISoundService
{
    void LaunchMusic();
    void PlaySfx(string fileName);
    void StopMusic();
}