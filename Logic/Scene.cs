using Logic.Obs;
using Logic.Riddles;
using Logic.Riddles.RiddleModel;
using Logic.SoundControl;

namespace Logic;

/// <summary>
/// Manage the link between logic and IHM
/// </summary>
public class Scene : Observable, IObserver
{
    private Riddle currentRiddle;
    private int riddlesSuccess;
    private List<Riddle> riddles;
    private ISoundService soundService;

    /// <summary>
    /// the init of the scene with the start of the game
    /// </summary>
    public Scene(ISoundService soundService)
    {
        this.soundService = soundService;
        this.riddles = new List<Riddle> {
            new Riddle0(soundService),
            new Riddle1(soundService),
            new Riddle2(soundService),
            new Riddle4(soundService)
        };
        this.riddlesSuccess = 1;
        this.SetCurrentRiddle("Riddle0");
    }
    
    public void Update()
    {
        this.NotifySceneTransition(this.currentRiddle.BackgroundName, this.currentRiddle.IElements, this.currentRiddle.SElements);
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
                break;
            case "EP": // éléments de la pièce
                switch (imageName)
                {
                    case "EP003N":
                        this.soundService.PlaySfx("bruitChaise.mp3");
                        this.SetCurrentRiddle("Riddle1");
                        this.Update();
                        break;
                    case "EP005N":
                        this.soundService.PlaySfx("bruitChaise.mp3");
                        this.SetCurrentRiddle("Riddle2");
                        this.Update();
                        break;
                    case "EP006N":
                        this.soundService.PlaySfx("bruitChaise.mp3");
                        this.SetCurrentRiddle("Riddl4");
                        this.Update();
                        break;
                }
                break;
            case "FI": // fléche d'intéraction 
                switch (imageName)
                {
                    case "FI000N":
                        this.VerifySuccess(currentRiddle.IsSuccess);
                        this.soundService.PlaySfx("bruitChaise2.mp3");
                        this.SetCurrentRiddle("Riddle0");
                        this.Update();
                        break;
                }
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
    
    protected override void NotifySceneTransition(string imageName, List<InteractiveElement> interactiveElements, List<StaticElement> staticElements)
    {
        foreach (IObserver obs in this.observers)
        {
            obs.SceneTransition(imageName, interactiveElements, staticElements);
        }
    }

    protected override void NotifyChangeSpecificIntElement(Element element, string oldName)
    {
        foreach (IObserver obs in this.observers)
        {
            obs.ChangeSpecificIntElement(element, oldName);
        }
    }

    public void SceneTransition(string imageName, List<InteractiveElement> interactiveElements, List<StaticElement> staticElements)
    {
        throw new NotImplementedException();
    }

    public void ChangeSpecificIntElement(Element element, string oldName)
    {
        this.NotifyChangeSpecificIntElement(element, oldName);
    }

    private void SetCurrentRiddle(string name)
    {
        if (this.currentRiddle != null)
            this.currentRiddle.Unregister(this);

        this.currentRiddle = this.riddles.Find(r => r.Name == name);
        
        //Vérification cohérence salle du cube
        if (this.currentRiddle.Name == "Riddle0")
        {
            this.currentRiddle.UpdateRoomRiddle(this.riddlesSuccess);
        }

        if (this.currentRiddle != null)
            this.currentRiddle.Register(this);
    }
}