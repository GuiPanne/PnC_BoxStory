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
        this.IElements.Add(new InteractiveElement("OnOffButton", "EE100N", 247, 581, 678, 140));
        this.IElements.Add(new InteractiveElement("Back", "FI000N", 95, 91, 1425, 745));
    }
        
    public override void ReactElement(string imageName)
    {
        if (imageName == "EE100N")
        {
            this.IElements.Find(e => e.ImageName == "EE100N").ImageName= "EE101N";
            this.NotifyChangeSpecificIntElement(this.IElements.Find(e => e.ImageName == "EE101N"), "EE100N");
            this.IsSuccess = true;
        }
    }

    protected override void NotifySceneTransition(string imageName, List<InteractiveElement> interactiveElements, List<StaticElement> staticElements)
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

    public override void UpdateRoomRiddle(int successNumber)
    {
        throw new NotImplementedException();
    }
}