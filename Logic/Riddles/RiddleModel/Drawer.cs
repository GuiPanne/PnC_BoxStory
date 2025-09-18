using Logic.Obs;
using Logic.SoundControl;

namespace Logic.Riddles.RiddleModel;

public class Drawer : Riddle
{
    public override string Name { get => "Drawer"; }
    public Drawer(ISoundService soundService) : base(soundService)
    {
        this.BackgroundName = "ZE000N";
        this.IElements.Add(new InteractiveElement("feuilles", "EE000N", 308, 425, 300, 150));
        this.IElements.Add(new InteractiveElement("Back", "FI000N", 95, 91, 1425, 745));
    }

    protected override void NotifySceneTransition(string imageName, List<InteractiveElement> interactiveElements, List<StaticElement> staticElements)
    {
        foreach (IObserver obs in this.observers)
        {
            obs.SceneTransition(imageName, interactiveElements, staticElements);
        }
    }

    protected override void NotifyChangeSpecificElement(Element element, string oldName)
    {
        throw new NotImplementedException();
    }

    protected override void NotifyRemoveSpecificElement(Element element)
    {
        foreach (IObserver obs in this.observers)
        {
            obs.RemoveSpecificElement(element);
        }
    }

    protected override void NotifyLaunchDialogue(string dialogueFileName)
    {
        throw new NotImplementedException();
    }

    protected override void NotifyLaunchOption()
    {
        throw new NotImplementedException();
    }
    
    public override void ReactElement(string imageName)
    {
        this.BackgroundName = "ZE001N";
        InteractiveElement element = this.IElements.Find(el => el.ImageName == imageName);
        this.NotifyRemoveSpecificElement(element);
        this.IElements.Remove(element);
        this.NotifySceneTransition(this.BackgroundName, this.IElements, this.SElements);
    }

    public override void UpdateRoomRiddle(int successNumber, int changeRotation)
    {
        throw new NotImplementedException();
    }
}