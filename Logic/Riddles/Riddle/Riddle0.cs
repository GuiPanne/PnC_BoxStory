namespace Logic.Enigmes.Riddle1;

/// <summary>
/// Fake riddle it's more the "main view of the game"
/// </summary>
public class Riddle0 : Riddle
{
    public override string Name => "Riddle0";

    public Riddle0()
    {
        this.BackgroundName = "DE000N";
        this.Elements = new List<InteractiveElement>();
        this.Elements.Add(new InteractiveElement("OnOff","EP003N",80,155,840,218));
    }

    public override void ReactElement(string imageName)
    {
        throw new NotImplementedException();
    }

    protected override void NotifyBackgroundChange(string imageName)
    {
        throw new NotImplementedException();
    }

    protected override void NotifyCleanCanvas()
    {
        throw new NotImplementedException();
    }

    protected override void NotifySetInteractiveElements()
    {
        throw new NotImplementedException();
    }

    protected override void NotifyChangeSpecificIntElement(InteractiveElement intEl, string oldName)
    {
        throw new NotImplementedException();
    }
}