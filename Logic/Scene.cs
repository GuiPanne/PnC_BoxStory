using Logic.Enigmes;
using Logic.Enigmes.Riddle1;
using Logic.obs;

namespace Logic;

public class Scene : Observable
{
    private Riddle currentRiddle;
    private string currentBackGroundName;
    private int riddlesSuccess;
    private List<Riddle> riddles;

    public Scene()
    {
        this.riddles = new List<Riddle>();
        this.riddles.Add(new Riddle0());
        this.riddles.Add(new Riddle1());
        // etc...
        this.riddlesSuccess = 0;
        this.currentRiddle = this.riddles.Find(riddle => riddle.Name == "Riddle0" );
        this.currentBackGroundName = this.currentRiddle.BackgroundName;
    }

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
                break;
            case "FI": // fléche d'intéraction 
                break;
            case "EM": // éléments menu 
                break;
            
        }
    }

    private void VerifySuccess(bool currentRiddleIsSuccess)
    {
        throw new NotImplementedException();
    }
}