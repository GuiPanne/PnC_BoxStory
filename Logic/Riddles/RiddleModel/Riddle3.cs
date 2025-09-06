using Logic.Obs;
using Logic.SoundControl;

namespace Logic.Riddles.RiddleModel;

public class Riddle3 : Riddle
{
    public override string Name => "Riddle3";
    
    public InteractiveElement selectedElement;
    
    public Riddle3(ISoundService soundService) : base(soundService)
    {
        this.BackgroundName = "ZE300N";
        
        this.IElements.Add(new InteractiveElement("Tag0", "EE300N", 247, 581, 678, 140));
        this.IElements.Add(new InteractiveElement("Tag1", "EE300N", 247, 581, 678, 140));
        this.IElements.Add(new InteractiveElement("Tag2", "EE300N", 247, 581, 678, 140));
        this.IElements.Add(new InteractiveElement("Tag3", "EE300N", 247, 581, 678, 140));
        this.IElements.Add(new InteractiveElement("Tag4", "EE300N", 247, 581, 678, 140));
        this.IElements.Add(new InteractiveElement("Tag5", "EE300N", 247, 581, 678, 140));
        this.IElements.Add(new InteractiveElement("Tag6", "EE300N", 247, 581, 678, 140));
        this.IElements.Add(new InteractiveElement("Tag7", "EE300N", 247, 581, 678, 140));
        this.IElements.Add(new InteractiveElement("Tag8", "EE300N", 247, 581, 678, 140));
        this.IElements.Add(new InteractiveElement("Tag9", "EE300N", 247, 581, 678, 140));

        this.SElements.Add(new StaticElement("Lever", "EE300N", 247, 581, 678, 140));
        this.SElements.Add(new StaticElement("MiamMiam", "EE300N", 247, 581, 678, 140));

        this.IElements.Add(new InteractiveElement("Back", "FI000N", 95, 91, 1425, 745));
    }

    public override void ReactElement(string imageName)
    {
        if (this.IElements.Find(e => e.Name == "Selected") != null)
        {
            this.NotifyRemoveSpecificElement(this.IElements.Find(e => e.Name == "Selected"));
        }
        //correspond de levier
        if (imageName == this.IElements.Find(e => e.ImageName == "EE300N").ImageName)
        {
            if (this.selectedElement.Name == "Tag7")
            {
                this.IsSuccess = true;
            }
            this.NotifyRemoveSpecificElement(this.selectedElement);
        }
        else
        {
            this.selectedElement = this.IElements.Find(e => e.ImageName == imageName);
            this.SElements.Add(new StaticElement("Selected", "EE300N", this.selectedElement.ImageWidth, this.selectedElement.ImageHeight, this.selectedElement.ImageX, this.selectedElement.ImageY));
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
        foreach (IObserver obs in this.observers)
        {
            obs.RemoveSpecificElement(element);
        }    
    }

    public override void UpdateRoomRiddle(int successNumber, int changeRotation)
    {
        throw new NotImplementedException();
    }

}