using Logic.Enigmes;
using Logic.obs;

namespace Logic;

public class Scene : Observable
{
    private Riddle currentRiddle;
    private string currentBackGroundName;
    private int riddlesSuccess;

    public Scene()
    {
        this.riddlesSuccess = 0;
    }

    public void Router(string imageName)
    {
        string code = imageName.Substring(0, 2);
        switch (code)
        {
            case "EE": // éléments d'énigme
                break;
            case "EP": // éléments de la pièce
                break;
            case "FI": // fléche d'intéraction 
                break;
            case "EM": // éléments menu 
                break;
            
        }
    }
    
}