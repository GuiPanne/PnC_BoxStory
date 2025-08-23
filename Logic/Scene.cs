using Logic.Enigmes;
using Logic.Enigmes.Riddle1;
using Logic.obs;

namespace Logic;

/// <summary>
/// Manage the link between logic and IHM
/// </summary>
public class Scene : Observable, IObserver
{
    private Riddle currentRiddle;
    private string currentBackGroundName;
    private int riddlesSuccess;
    private List<Riddle> riddles;

    /// <summary>
    /// the init of the scene with the start of the game
    /// </summary>
    public Scene()
    {
        this.riddles = new List<Riddle>();
        this.riddles.Add(new Riddle0());
        this.riddles.Add(new Riddle1());
        // etc...
        this.riddlesSuccess = 1;
        this.currentRiddle = this.riddles.Find(riddle => riddle.Name == "Riddle0" );
        this.currentBackGroundName = this.currentRiddle.BackgroundName;
    }

    /// <summary>
    /// when an image is clicked choose what to do thanks to the image name
    /// </summary>
    /// <param name="imageName"> the name of the image</param>
    public void Router(string imageName)
    {
        string code = imageName.Substring(0, 2);
        switch (code)
        {
            case "EE": // éléments d'énigme
                this.currentRiddle.ReactElement(imageName);
                this.VerifySuccess(currentRiddle.IsSuccess);
                break;
            case "EP": // éléments de la pièce
                this.currentRiddle.ReactElement(imageName);
                break;
            case "FI": // fléche d'intéraction 
                break;
            case "EM": // éléments menu 
                break;
            
        }
    }

    /// <summary>
    /// if the riddle is succeeded, return to the main scene bg and increment the number of success 
    /// </summary>
    /// <param name="currentRiddleIsSuccess">true if succeeded and false if not</param>
    private void VerifySuccess(bool currentRiddleIsSuccess)
    {
        if (currentRiddleIsSuccess)
        {
            this.riddlesSuccess++;
            this.currentRiddle = this.riddles.Find(riddle => riddle.Name == "Riddle0");
        }
    }
    
    protected override void NotifyBackgroundChange(string imageName)
    {
        foreach (IObserver obs in this.observers)
        {
            obs.DisplayBackGround(imageName);
        }
    }

    protected override void NotifyCleanCanvas()
    {
        foreach (IObserver obs in this.observers)
        {
            obs.CleanCanvas();
        }
    }

    protected override void NotifySetInteractiveElements()
    {
        foreach (IObserver obs in this.observers)
        {
            obs.SetInteractiveElements(currentRiddle.Elements);
        }
    }

    protected override void NotifyChangeSpecificIntElement(InteractiveElement intEl)
    {
        foreach (IObserver obs in this.observers)
        {
            obs.ChangeSpecificIntElement(intEl);
        }
    }

    public void DisplayBackGround(string imageName)
    {
        throw new NotImplementedException();
    }

    public void CleanCanvas()
    {
        throw new NotImplementedException();
    }

    public void SetInteractiveElements(List<InteractiveElement> interactiveElements)
    {
        throw new NotImplementedException();
    }

    public void ChangeSpecificIntElement(InteractiveElement intEl)
    {
        this.NotifyChangeSpecificIntElement(intEl);
    }
}