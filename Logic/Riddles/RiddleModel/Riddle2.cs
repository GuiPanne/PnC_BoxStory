using Logic.Obs;
using Logic.SoundControl;

namespace Logic.Riddles.RiddleModel;
/// <summary>
/// Second Riddle of the game
/// </summary>
public class Riddle2 : Riddle
{
    private int count;
    public override string Name => "Riddle2";
    /// <summary>
    /// init all binaries buttons for the riddle
    /// </summary>
    /// <param name="soundService">sound service for playing sfx</param>
    public Riddle2(ISoundService soundService) : base(soundService)
    {
        this.count = 0;
        this.BackgroundName = "ZE200N";
        this.SElements.Add(new StaticElement("32Label", "EE225N", 170,298, 287, 206));
        this.SElements.Add(new StaticElement("16Label", "EE224N", 170,298, 464, 208));
        this.SElements.Add(new StaticElement("8Label", "EE223N", 170,298, 642, 215));
        this.SElements.Add(new StaticElement("4Label", "EE222N", 170,298, 813, 217));
        this.SElements.Add(new StaticElement("2Label", "EE221N", 170,298, 987, 215));
        this.SElements.Add(new StaticElement("1Label", "EE220N", 170,298, 1159, 217));

        this.IElements.Add(new InteractiveElement("32Button", "EE205N", 139, 135, 300, 527));
        this.IElements.Add(new InteractiveElement("16Button", "EE204N", 139, 135, 483, 533));
        this.IElements.Add(new InteractiveElement("8Button", "EE203N", 139, 135, 653, 533));
        this.IElements.Add(new InteractiveElement("4Button", "EE202N", 139, 135, 826, 538));
        this.IElements.Add(new InteractiveElement("2Button", "EE201N", 139, 135, 1000, 542));
        this.IElements.Add(new InteractiveElement("1Button", "EE200N", 139, 135, 1173, 538));

        this.IElements.Add(new InteractiveElement("Back", "FI000N", 95, 91, 1485, 745));
    }
    public override void ReactElement(string imageName)
    {
        this.soundService.PlaySfx("bruitToucheMachineEcrire.mp3");
        switch (imageName)
        {
            case "EE200N": // button 1
                ToggleLabel("1Label", "EE220N", "EE210N");
                break;

            case "EE201N": // button 2
                ToggleLabel("2Label", "EE221N", "EE211N");
                break;

            case "EE202N": // button 4
                ToggleLabel("4Label", "EE222N", "EE212N");
                break;

            case "EE203N": // button 8
                ToggleLabel("8Label", "EE223N", "EE213N");
                break;

            case "EE204N": // button 16
                ToggleLabel("16Label", "EE224N", "EE214N");
                break;

            case "EE205N": // button 32
                ToggleLabel("32Label", "EE225N", "EE215N");
                break;
        }

        if (this.count == 27)
        {
            this.IsSuccess = true;
            this.soundService.PlaySfx("bruitDeverouiller.mp3");
        }
        else
        {
            this.IsSuccess = false;
        }
    }
    
    /// <summary>
    /// Find the static element corresponding and change is state, change the count
    /// </summary>
    /// <param name="labelName">the label corresponding</param>
    /// <param name="offImage">state 0</param>
    /// <param name="onImage">state 1</param>
    private void ToggleLabel(string labelName, string offImage, string onImage)
    {
        var label = this.SElements.Find(s => s.Name == labelName);
        if (label == null) return;

        if (label.ImageName == offImage)
        {
            label.ImageName = onImage;
            this.NotifyChangeSpecificElement(label, offImage);
            this.count += int.Parse(labelName.Replace("Label", ""));
        }
        else
        {
            label.ImageName = offImage;
            this.NotifyChangeSpecificElement(label, onImage);
            this.count -= int.Parse(labelName.Replace("Label", ""));
        }
    }

    public override void UpdateRoomRiddle(int successNumber, int changeRotation)
    {
        throw new NotImplementedException();
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
}