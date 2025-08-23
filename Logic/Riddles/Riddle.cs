using Logic.obs;

namespace Logic.Enigmes;

/// <summary>
/// abstract class who represents the basic of a riddle
/// </summary>
public abstract class Riddle : Observable
{
    private bool isSuccess;
    private string backgroundName;
    private List<InteractiveElement> elements;

    /// <summary>
    /// all elements contain in the riddle
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public List<InteractiveElement> Elements
    {
        get => elements;
        set => elements = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// the name of the background of the riddle
    /// </summary>
    public string BackgroundName
    {
        get => backgroundName;
        set => backgroundName = value;
    }

    /// <summary>
    /// the name of the riddle
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// define if a riddle is a success or not
    /// </summary>
    public bool IsSuccess => this.isSuccess;

    /// <summary>
    /// init the riddle
    /// </summary>
    public Riddle()
    {
        this.elements = new List<InteractiveElement>();
        this.isSuccess = false;
    }

    /// <summary>
    /// reaction of the riddle for a specific return of a image name
    /// </summary>
    /// <param name="imageName"> the image name who has been click in IHM</param>
    public abstract void ReactElement(string imageName);
}