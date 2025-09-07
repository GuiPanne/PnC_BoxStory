using Logic.Obs;
using Logic.SoundControl;

namespace Logic.Riddles.RiddleModel;

public class Riddle5 : Riddle
{
    public override string Name => "Riddle5";
    
    public InteractiveElement[] SelectedElements;

    public Riddle5(ISoundService soundService) : base(soundService)
    {
        this.SelectedElements = new []
        {
            new InteractiveElement("null", "null", 0, 0, 0, 0),
            new InteractiveElement("null", "null", 0, 0, 0, 0),
            new InteractiveElement("null", "null", 0, 0, 0, 0),
            new InteractiveElement("null", "null", 0, 0, 0, 0),
            new InteractiveElement("null", "null", 0, 0, 0, 0)
        };
        
        this.IElements.Add(new InteractiveElement("b1c1", "EE500N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b2c1", "EE500N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b3c1", "EE500N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b4c1", "EE500N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b5c1", "EE500N", 0, 0, 0, 0));

        this.IElements.Add(new InteractiveElement("b1c2", "EE510N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b2c2", "EE510N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b3c2", "EE510N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b4c2", "EE510N", 0, 0, 0, 0));

        this.IElements.Add(new InteractiveElement("b1c3", "EE520N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b2c3", "EE520N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b3c3", "EE520N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b4c3", "EE520N", 0, 0, 0, 0));

        this.IElements.Add(new InteractiveElement("b1c4", "EE530N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b2c4", "EE530N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b3c4", "EE530N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b4c4", "EE530N", 0, 0, 0, 0));
        
        this.IElements.Add(new InteractiveElement("b1c5", "EE540N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b2c5", "EE540N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b3c5", "EE540N", 0, 0, 0, 0));
        this.IElements.Add(new InteractiveElement("b4c5", "EE540N", 0, 0, 0, 0));
    }

    public override void ReactElement(string imageName)
    {
        int index = Convert.ToInt32(imageName[3]);
        this.SelectedElements[index] = this.IElements.Find(e => e.ImageName == imageName);

        if (this.SelectedElements[0].Name == "b2c1" &&
            this.SelectedElements[1].Name == "b1c2" &&
            this.SelectedElements[2].Name == "b1c3" &&
            this.SelectedElements[3].Name == "b3c4" &&
            this.SelectedElements[4].Name == "b4c5"
           )
        {
            this.IsSuccess = true;
        }else
            this.IsSuccess = false;
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

    public override void UpdateRoomRiddle(int successNumber, int changeRotation)
    {
        throw new NotImplementedException();
    }
}