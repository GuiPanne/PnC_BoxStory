namespace Logic.Enigmes.Riddle1;

/// <summary>
/// Fake riddle it's more the "main view of the game"
/// </summary>
public class Riddle0 : Riddle
{
    public int cubeRotation;
    public override string Name => "Riddle0";

    public Riddle0()
    {
        cubeRotation = 0;
        this.BackgroundName = "DE000N";
        this.IElements = new List<InteractiveElement>();
        this.SElements.Add(new StaticElement("Tiroir","EP000N",815,105,355,730));
        this.IElements.Add(new InteractiveElement("OnOff","EP003N",80,155,840,218));
        this.SElements.Add(new StaticElement("Chien","EP001N",149,206,1339,147));
    }

    public override void UpdateRoomRiddle(int successNumber)
    {
        if (cubeRotation == 0)
        {
            switch (successNumber)
            {
                case 1:
                    break;
                case 2:
                    this.BackgroundName = "DE002N";
                    this.IElements.Clear();
                    this.SElements.Add(new StaticElement("Chien","EP001N",149,206,1339,147));
                    this.SElements.Add(new StaticElement("Tiroir","EP000N",815,105,355,730));
                    this.SElements.Add(new StaticElement("OnOff","EP003N",80,155,840,218));
                    this.IElements.Add(new InteractiveElement("Binaries", "EP005N", 177, 110, 794, 535));
                    break;
                case 3:
                    break;
                case 4:
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
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }
        }else if (cubeRotation == 2)
        {
            switch (successNumber)
            {
                case 6:
                    break;
                case 7:
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

    protected override void NotifyChangeSpecificIntElement(InteractiveElement intEl, string oldName)
    {
        throw new NotImplementedException();
    }
}