using Logic.Obs;
using Logic.SoundControl;

namespace Logic.Riddles.RiddleModel;

/// <summary>
/// Fake riddle it's more the "main view of the game"
/// </summary>
public class Riddle0 : Riddle
{
    private int cubeRotation;
    public int CubeRotation;
    public override string Name => "Riddle0";

    /// <summary>
    /// init the logic of the main view of the game
    /// </summary>
    /// <param name="soundService">the soundService</param>
    public Riddle0(ISoundService soundService) : base(soundService)
    {
        cubeRotation = 0;
        this.BackgroundName = "DE005N";
        this.IElements = new List<InteractiveElement>();
        this.IElements.Add(new InteractiveElement("Option", "OP000N", 50,50, 1522, 20));
        this.SElements.Add(new StaticElement("Tiroir","EP000N",815,105,369,795));
        this.IElements.Add(new InteractiveElement("OnOff","EP003N",80,155,874,234));
        this.SElements.Add(new StaticElement("Chien","EP001N",149,206,1401,170));
    }

    public override void UpdateRoomRiddle(int successNumber, int changeRotation)
    {
        //DEBOGAGE
        cubeRotation = 2;
        successNumber = 6;

        if (changeRotation != -1)
        {
            cubeRotation = changeRotation;
        }
        if (cubeRotation == 0)
        {
            switch (successNumber)
            {
                case 1:
                    break;
                case 2:
                    this.IElements.Clear();
                    this.SElements.Clear();
                    this.BackgroundName = "DE002N";
                    this.IElements.Add(new InteractiveElement("Option", "OP000N", 50,50, 1522, 20));
                    this.SElements.Add(new StaticElement("Chien", "EP001N", 149, 206, 1401, 170));
                    this.IElements.Add(new InteractiveElement("Tiroir", "EP000N", 815, 105, 369, 795));
                    this.SElements.Add(new StaticElement("OnOff", "EP003N", 80, 155, 874, 234));
                    this.IElements.Add(new InteractiveElement("Binaries", "EP005N", 177, 110, 828, 579));
                    break;
                case 3: 
                    this.IElements.Clear();
                    this.SElements.Clear();
                    this.BackgroundName = "DE001N";
                    this.IElements.Add(new InteractiveElement("Option", "OP000N", 50, 50, 1522, 20));
                    this.SElements.Add(new StaticElement("Chien", "EP001N", 149, 206, 1401, 170));
                    this.IElements.Add(new InteractiveElement("Tiroir", "EP000N", 815, 105, 369, 795));
                    this.IElements.Add(new InteractiveElement("cesear", "EP004N", 183, 372, 532, 260));
                    break;
                case 4:
                    
                    this.IElements.Clear();
                    this.SElements.Clear();
                    this.BackgroundName = "DE001N";
                    this.IElements.Add(new InteractiveElement("Option", "OP000N", 50, 50, 1522, 20));
                    this.SElements.Add(new StaticElement("Chien", "EP001N", 149, 206, 1401, 170));
                    this.IElements.Add(new InteractiveElement("Tiroir", "EP000N", 815, 105, 369, 795));
                    this.SElements.Add(new StaticElement("cesear", "EP004N", 183, 372, 532, 260));
                    this.IElements.Add(new InteractiveElement("Turn", "FI001N", 91, 134, 1194, 530));
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }
        }else if (cubeRotation == 1)
        {
            switch (successNumber)
            {
                case 4:
                    
                    this.IElements.Clear();
                    this.SElements.Clear();
                    this.BackgroundName = "DE003N";
                    this.IElements.Add(new InteractiveElement("Option", "OP000N", 50, 50, 1522, 20));
                    this.SElements.Add(new StaticElement("Chien", "EP001N", 149, 206, 1401, 170));
                    this.IElements.Add(new InteractiveElement("Tiroir", "EP000N", 815, 105, 369, 795));
                    this.IElements.Add(new InteractiveElement("Onde", "EP006N", 290, 103, 624, 584));
                    break;
                case 5:
                    this.IElements.Clear();
                    this.SElements.Clear();
                    this.BackgroundName = "DE004N";
                    this.IElements.Add(new InteractiveElement("Option", "OP000N", 50, 50, 1522, 20));
                    this.SElements.Add(new StaticElement("Chien", "EP001N", 149, 206, 1401, 170));
                    this.IElements.Add(new InteractiveElement("Tiroir", "EP000N", 815, 105, 369, 795));
                    this.IElements.Add(new InteractiveElement("Name", "EP007N", 197, 143, 546, 247));
                    break;
                case 6:
                    this.IElements.Clear();
                    this.SElements.Clear();
                    this.BackgroundName = "DE004N";
                    this.IElements.Add(new InteractiveElement("Option", "OP000N", 50, 50, 1522, 20));
                    this.SElements.Add(new StaticElement("Chien", "EP001N", 149, 206, 1401, 170));
                    this.IElements.Add(new InteractiveElement("Tiroir", "EP000N", 815, 105, 369, 795));
                    this.SElements.Add(new StaticElement("Name", "EP007N", 197, 143, 540, 249));
                    this.IElements.Add(new InteractiveElement("Turn", "FI001N", 91, 134, 1194, 530));
                    break;
            }
        }else if (cubeRotation == 2)
        {
            switch (successNumber)
            {
                case 6:
                    this.IElements.Clear();
                    this.SElements.Clear();
                    this.BackgroundName = "DE005N";
                    this.IElements.Add(new InteractiveElement("Option", "OP000N", 50, 50, 1522, 20));
                    this.SElements.Add(new StaticElement("Chien", "EP001N", 149, 206, 1401, 170));
                    this.IElements.Add(new InteractiveElement("Tiroir", "EP000N", 815, 105, 369, 795));
                    this.IElements.Add(new InteractiveElement("Taquin", "EP008N", 400, 400, 565, 265));
                    break;
                case 7:
                    this.IElements.Clear();
                    this.SElements.Clear();
                    this.BackgroundName = "DE005N";
                    this.IElements.Add(new InteractiveElement("Option", "OP000N", 50, 50, 1522, 20));
                    this.SElements.Add(new StaticElement("Chien", "EP001N", 149, 206, 1401, 170));
                    this.IElements.Add(new InteractiveElement("Tiroir", "EP000N", 815, 105, 369, 795));
                    this.SElements.Add(new StaticElement("Taquin", "EP008N", 400, 400, 250, 500));
                    break;
            }
        }
    }

    public override void ReactElement(string imageName)
    {
        throw new NotImplementedException();
    }

    protected override void NotifySceneTransition(string imageName, List<InteractiveElement> interactiveElements, List<StaticElement> staticElements)
    {
        throw new NotImplementedException();
    }

    protected override void NotifyChangeSpecificElement(Element element, string oldName)
    {
        throw new NotImplementedException();
    }

    protected override void NotifyRemoveSpecificElement(Element element)
    {
        throw new NotImplementedException();
    }

    protected override void NotifyLaunchDialogue(string dialogueFileName)
    {
        foreach (IObserver observer in observers)
        {
            observer.LaunchDialogue(dialogueFileName);
        }
    }

    protected override void NotifyLaunchOption()
    {
        throw new NotImplementedException();
    }
}