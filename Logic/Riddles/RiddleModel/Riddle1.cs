using Logic.Obs;
using Logic.SoundControl;

namespace Logic.Riddles.RiddleModel;

/// <summary>
/// first riddle of the game
/// </summary>
public class Riddle1 : Riddle
{
    public override string Name => "Riddle1";
    
    public Riddle1(ISoundService soundService) : base(soundService)
    {
        this.BackgroundName = "ZE100N";
        this.IElements.Add(new InteractiveElement("OnOffButton", "EE100N", 247, 581, 678, 140));
        this.IElements.Add(new InteractiveElement("Back", "FI000N", 95, 91, 1425, 745));
    }
        
    public override void ReactElement(string imageName)
    {
        this.soundService.PlaySfx("bruitClicUltime.mp3");
        if (imageName == "EE100N")
        {
            this.IElements.Find(e => e.ImageName == "EE100N").ImageName= "EE101N";
            this.NotifyChangeSpecificElement(this.IElements.Find(e => e.ImageName == "EE101N"), "EE100N");
            this.IsSuccess = true;
            this.soundService.PlaySfx("bruitOuvertureR2.mp3");
        }
        else
        {
            this.IElements.Find(e => e.ImageName == "EE101N").ImageName= "EE100N";
            this.NotifyChangeSpecificElement(this.IElements.Find(e => e.ImageName == "EE100N"), "EE101N");
            this.IsSuccess = false;
        }
    }

    protected override void NotifySceneTransition(string imageName, List<InteractiveElement> interactiveElements, List<StaticElement> staticElements)
    {
        throw new NotImplementedException();
    }

    protected override void NotifyChangeSpecificElement(Element element, string oldName)
    {
        foreach (IObserver obs in this.observers)
        {
            obs.ChangeSpecificElement(element, oldName);
        }
    }

    protected override void NotifyRemoveSpecificElement(Element element)
    {
        throw new NotImplementedException();
    }

    protected override void NotifyLaunchDialogue(string dialogueFileName)
    {
        throw new NotImplementedException();
    }

    protected override void NotifyLaunchOption()
    {
        throw new NotImplementedException();
    }

    public override void UpdateRoomRiddle(int successNumber, int changeRotation)
    {
        throw new NotImplementedException();
    }
}