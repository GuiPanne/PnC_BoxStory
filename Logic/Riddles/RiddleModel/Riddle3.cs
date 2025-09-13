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
        
        this.IElements.Add(new InteractiveElement("Tag0", "EE301N", 214, 40, 533, 421));
        this.IElements.Add(new InteractiveElement("Tag1", "EE302N", 214, 38, 527, 474));
        this.IElements.Add(new InteractiveElement("Tag2", "EE303N", 202, 41, 535, 528));
        this.IElements.Add(new InteractiveElement("Tag3", "EE304N", 237, 42, 527, 590));
        this.IElements.Add(new InteractiveElement("Tag4", "EE305N", 206, 45, 505, 649));
        this.IElements.Add(new InteractiveElement("Tag5", "EE306N", 254, 41, 879, 425));
        this.IElements.Add(new InteractiveElement("Tag6", "EE307N", 249, 40, 874, 486));
        this.IElements.Add(new InteractiveElement("Tag7", "EE308N", 256, 39, 871, 543));
        this.IElements.Add(new InteractiveElement("Tag8", "EE309N", 255, 44, 871, 595));
        this.IElements.Add(new InteractiveElement("Tag9", "EE310N", 250, 49, 866, 656));

        this.IElements.Add(new InteractiveElement("Lever", "EE300N", 90, 89, 1015, 712));
        this.SElements.Add(new StaticElement("Selected", "EE311N", 219, 73, -999, -999));
        
        this.IElements.Add(new InteractiveElement("Back", "FI000N", 95, 91, 1425, 745));
    }

    public override void ReactElement(string imageName)
    {
        //correspond de levier
        if (imageName == this.IElements.Find(e => e.ImageName == "EE300N").ImageName && this.selectedElement != null)
        {
            this.soundService.PlaySfx("bruitPapierDechiree.mp3");
            if (this.selectedElement.Name == "Tag7")
            {
                this.IsSuccess = true;
                this.soundService.PlaySfx("bruitOuvertureR3.mp3");
            }
            this.NotifyRemoveSpecificElement(this.selectedElement);
            this.selectedElement = null;
            this.MoveSelectIndicator(this.SElements.Find(e => e.ImageName == "EE311N"), -999, -999);
        }
        else
        {
            if (imageName != this.IElements.Find(e => e.ImageName == "EE300N").ImageName)
            {
                this.selectedElement = this.IElements.Find(e => e.ImageName == imageName);
                this.MoveSelectIndicator(this.SElements.Find(e => e.ImageName == "EE311N"), this.selectedElement.ImageX, this.selectedElement.ImageY-20);
                this.soundService.PlaySfx("bruitFeutre.mp3");
            }
        }
    }

    private void MoveSelectIndicator(Element e, double newX, double newY)
    {
        e.ImageX = newX;
        e.ImageY = newY;
        this.NotifyChangeSpecificElement(e, e.ImageName);
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
        this.IElements.Remove(this.IElements.Find(e => e.ImageName == e.ImageName));
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