using Logic.obs;

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
        this.BackgroundName = "ZE100N";
        this.Elements = new List<InteractiveElement>();
        this.Elements.Add(new InteractiveElement("OnOffButton", "EE100N", 354, 711, 623, 94.5));
    }
        
    public override void ReactElement(string imageName)
    {
        if (imageName == "EE100N")
        {
            this.Elements.Find(e => e.ImageName == "EE100N").ImageName= "EE101N";
            this.NotifyChangeSpecificIntElement(this.Elements.Find(e => e.ImageName == "EE101N"), "EE100N");
            this.IsSuccess = true;
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

    protected override void NotifyChangeSpecificIntElement(InteractiveElement intEl, string oldName)
    {
        foreach (IObserver obs in this.observers)
        {
            obs.ChangeSpecificIntElement(intEl, oldName);
        }
    }
}