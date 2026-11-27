using Logic.Obs;
using Logic.SoundControl;

namespace Logic.Riddles.RiddleModel;

/// <summary>
/// Represents the riddle 5 of the game
/// </summary>
public class Riddle5 : Riddle
{
    private InteractiveElement[] SelectedElements;
    public override string Name => "Riddle5";

    /// <summary>
    /// init all letters buttons
    /// </summary>
    /// <param name="soundService">to play sound</param>
    public Riddle5(ISoundService soundService) : base(soundService)
    {
        this.BackgroundName = "ZE500N";
        this.SelectedElements = new []
        {
            new InteractiveElement("null", "null", 0, 0, 0, 0),
            new InteractiveElement("null", "null", 0, 0, 0, 0),
            new InteractiveElement("null", "null", 0, 0, 0, 0),
            new InteractiveElement("null", "null", 0, 0, 0, 0),
            new InteractiveElement("null", "null", 0, 0, 0, 0)
        };
        
        this.IElements.Add(new InteractiveElement("b1c1", "EE500N", 105, 96, 439, 246));
        this.IElements.Add(new InteractiveElement("b2c1", "EE501N", 106, 100, 442, 364));
        this.IElements.Add(new InteractiveElement("b3c1", "EE502N", 106, 95, 439, 467));
        this.IElements.Add(new InteractiveElement("b4c1", "EE503N", 105, 102, 438, 571));

        this.IElements.Add(new InteractiveElement("b1c2", "EE510N", 107, 97, 589, 246));
        this.IElements.Add(new InteractiveElement("b2c2", "EE511N", 106, 97, 588, 364));
        this.IElements.Add(new InteractiveElement("b3c2", "EE512N", 106, 93, 587, 468));
        this.IElements.Add(new InteractiveElement("b4c2", "EE513N", 105, 94, 589, 572));

        this.IElements.Add(new InteractiveElement("b1c3", "EE520N", 105, 97, 739, 245));
        this.IElements.Add(new InteractiveElement("b2c3", "EE521N", 105, 100, 739, 365));
        this.IElements.Add(new InteractiveElement("b3c3", "EE522N", 106, 102, 739, 457));
        this.IElements.Add(new InteractiveElement("b4c3", "EE523N", 104, 100, 740, 570));

        this.IElements.Add(new InteractiveElement("b1c4", "EE530N", 107, 101, 892, 246));
        this.IElements.Add(new InteractiveElement("b2c4", "EE531N", 107, 100, 892, 366));
        this.IElements.Add(new InteractiveElement("b3c4", "EE532N", 106, 93, 892, 467));
        this.IElements.Add(new InteractiveElement("b4c4", "EE533N", 105, 101, 892, 571));
        
        this.IElements.Add(new InteractiveElement("b1c5", "EE540N", 105, 98, 1049, 249));
        this.IElements.Add(new InteractiveElement("b2c5", "EE541N", 105, 102, 1049, 364));
        this.IElements.Add(new InteractiveElement("b3c5", "EE542N", 106, 96, 1049, 467));
        this.IElements.Add(new InteractiveElement("b4c5", "EE543N", 103, 103, 1049, 571));
        
        this.IElements.Add(new InteractiveElement("Back", "FI000N", 95, 91, 1485, 745));

    }

    public override void ReactElement(string imageName)
    {
        int sIndex = (int)char.GetNumericValue(imageName[3]);
        int index = (int)char.GetNumericValue(imageName[4]);
        if (this.SelectedElements.All(e => e.ImageName != imageName))
        {
            if (this.SelectedElements[sIndex].Name != "null")
            {
                char[] newNameOldSelected = this.SelectedElements[sIndex].ImageName.ToCharArray();
                int i = (int)char.GetNumericValue(this.SelectedElements[sIndex].ImageName[4]);
                newNameOldSelected[4] = char.Parse((i-4).ToString());
                string oldNameOldSelected = this.SelectedElements[sIndex].ImageName;
                this.SelectedElements[sIndex].ImageName= new string(newNameOldSelected);
                NotifyChangeSpecificElement(this.SelectedElements[sIndex], oldNameOldSelected);
                this.soundService.PlaySfx("bruitRelachement.mp3");
            }
            this.SelectedElements[sIndex] = this.IElements.Find(e => e.ImageName == imageName);
            char[] newNameNewSelected = this.SelectedElements[sIndex].ImageName.ToCharArray();
            newNameNewSelected[4] = char.Parse((index+4).ToString());
            this.SelectedElements[sIndex].ImageName=new string(newNameNewSelected);
            NotifyChangeSpecificElement(this.SelectedElements[sIndex], imageName);
            this.soundService.PlaySfx("bruitAppuie.mp3");

            if (this.SelectedElements[0].Name == "b2c1" &&
                this.SelectedElements[1].Name == "b1c2" &&
                this.SelectedElements[2].Name == "b1c3" &&
                this.SelectedElements[3].Name == "b3c4" &&
                this.SelectedElements[4].Name == "b4c5"
               )
            {
                this.IsSuccess = true;
                this.soundService.PlaySfx("bruitDeverouiller.mp3");
            }else
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