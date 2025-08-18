namespace Logic.Enigmes;

public abstract class Riddle
{
    private bool isSuccess;
    private string backgroundName;


    public string BackgroundName
    {
        get => backgroundName;
        set => backgroundName = value;
    }

    public abstract string Name { get; }

    public bool IsSuccess => this.isSuccess;

    public Riddle()
    {
        this.isSuccess = false;
    }

    public abstract void ReactElement(string imageName);
}