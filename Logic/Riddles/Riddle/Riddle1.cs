namespace Logic.Enigmes.Riddle1;

/// <summary>
/// first riddle of the game
/// </summary>
public class Riddle1 : Riddle
{
    public override string Name => "Riddle1";
    
    private InteractiveElement element1;
    
    public Riddle1() : base()
    {
        //element1 = new InteractiveElement("EE100N", 354, 711, 50, 50);
    }
        
    public override void ReactElement(string imageName)
    {
        if (imageName == "EE100N")
        {
            element1.ImageName = "EE101N";
        }
    }

    protected override void NotifyBackgroundChange(string imageName)
    {
        throw new NotImplementedException();
    }

    protected override void NotifyCleanCanvas()
    {
        throw new NotImplementedException();
    }

    protected override void NotifySetInteractiveElements()
    {
        throw new NotImplementedException();
    }

    protected override void NotifyChangeSpecificIntElement(InteractiveElement intEl)
    {
        throw new NotImplementedException();
    }
}