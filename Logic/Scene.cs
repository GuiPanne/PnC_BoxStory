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
    private int riddlesSuccess;
    private List<Riddle> riddles;

    /// <summary>
    /// the init of the scene with the start of the game
    /// </summary>
    public Scene()
    {
        this.riddles = new List<Riddle> {
            new Riddle0(),
            new Riddle1(),
        };
        this.riddlesSuccess = 1;
        this.SetCurrentRiddle("Riddle0");
    }

    public void Update()
    {
        this.NotifySceneTransition(this.currentRiddle.BackgroundName, this.currentRiddle.Elements);
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
                switch (imageName)
                {
                    case "EP003N":
                        this.SetCurrentRiddle("Riddle1");
                        this.Update();
                        break;
                }
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
        }
    }
    
    protected override void NotifySceneTransition(string imageName, List<InteractiveElement> interactiveElements)
    {
        foreach (IObserver obs in this.observers)
        {
            obs.SceneTransition(imageName, interactiveElements);
        }
    }

    protected override void NotifyChangeSpecificIntElement(InteractiveElement intEl, string oldName)
    {
        foreach (IObserver obs in this.observers)
        {
            obs.ChangeSpecificIntElement(intEl, oldName);
        }
    }

    public void SceneTransition(string imageName, List<InteractiveElement> interactiveElements)
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

    public void ChangeSpecificIntElement(InteractiveElement intEl, string oldName)
    {
        this.NotifyChangeSpecificIntElement(intEl, oldName);
    }

    private void SetCurrentRiddle(string name)
    {
        if (this.currentRiddle != null)
            this.currentRiddle.Unregister(this);

        this.currentRiddle = this.riddles.Find(r => r.Name == name);

        if (this.currentRiddle != null)
            this.currentRiddle.Register(this);
    }
}