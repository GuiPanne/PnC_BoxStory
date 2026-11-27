using Logic.Obs;
using Logic.SoundControl;

namespace Logic.Riddles.RiddleModel;
/// <summary>
/// Represents the Riddle 3 of the game
/// </summary>
public class Riddle3 : Riddle
{
    public override string Name => "Riddle3";
    private InteractiveElement selectedElement;
    
    /// <summary>
    /// init all possible answer of the riddle and the lever
    /// </summary>
    /// <param name="soundService">to play sfx sound</param>
    public Riddle3(ISoundService soundService) : base(soundService)
    {
        this.BackgroundName = "ZE300N";
        
        this.IElements.Add(new InteractiveElement("Tag0", "EE301N", 214, 40, 533, 436));
        this.IElements.Add(new InteractiveElement("Tag1", "EE302N", 214, 38, 527, 489));
        this.IElements.Add(new InteractiveElement("Tag2", "EE303N", 202, 41, 535, 543));
        this.IElements.Add(new InteractiveElement("Tag3", "EE304N", 237, 42, 527, 605));
        this.IElements.Add(new InteractiveElement("Tag4", "EE305N", 206, 45, 505, 664));
        this.IElements.Add(new InteractiveElement("Tag5", "EE306N", 254, 41, 879, 440));
        this.IElements.Add(new InteractiveElement("Tag6", "EE307N", 249, 40, 874, 491));
        this.IElements.Add(new InteractiveElement("Tag7", "EE308N", 256, 39, 871, 558));
        this.IElements.Add(new InteractiveElement("Tag8", "EE309N", 255, 44, 871, 610));
        this.IElements.Add(new InteractiveElement("Tag9", "EE310N", 250, 49, 866, 671));

        this.IElements.Add(new InteractiveElement("Lever", "EE300N", 90, 89, 1015, 727));
        this.SElements.Add(new StaticElement("Selected", "EE311N", 219, 73, -999, -999));
        
        this.IElements.Add(new InteractiveElement("Back", "FI000N", 95, 91, 1485, 745));
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
    
    /// <summary>
    /// Change the position of the elements who is indicated witch answer is selected
    /// </summary>
    /// <param name="e">the element</param>
    /// <param name="newX">new X position</param>
    /// <param name="newY">new Y position</param>
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
        this.IElements.Remove(this.IElements.Find(e => e.ImageName == element.ImageName));
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

    public override void UpdateRoomRiddle(int successNumber, int changeRotation)
    {
        throw new NotImplementedException();
    }

}