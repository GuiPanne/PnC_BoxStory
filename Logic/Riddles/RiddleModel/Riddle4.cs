using Logic.Obs;
using Logic.SoundControl;

namespace Logic.Riddles.RiddleModel;

public class Riddle4 : Riddle
{
    // Positions actuelles des SE (0,1,2) pour correspondre aux 3 images
    private int[] currentSEPositions = new int[4];
    
    // Positions actuelles des IE (1..4)
    private int[] currentIEPositions = new int[4];
    
    // Multiplicateurs des IE sur les SE
    private readonly Dictionary<int, Dictionary<int,int>> ieEffects = new()
    {
        { 1, new Dictionary<int,int>{{1,-1},{2,1}} },
        { 2, new Dictionary<int,int>{{1,1},{3,-1}} },
        { 3, new Dictionary<int,int>{{2,-2}} },
        { 4, new Dictionary<int,int>{{3,1},{4,2}} }
    };
    
    private Dictionary<int, List<StaticElementState>> seStates = new()
    {
        { 1, new List<StaticElementState>
            {
                new StaticElementState("EE420N", 246, 95, 317, 229),
                new StaticElementState("EE440N", 245, 22, 317, 306),
                new StaticElementState("EE430N", 245, 95, 317, 310),
            }
        },
        { 2, new List<StaticElementState>
            {
                new StaticElementState("EE421N", 246, 95, 535, 229),
                new StaticElementState("EE441N", 245, 22, 535, 306),
                new StaticElementState("EE431N", 245, 95, 535, 310),
            }
        },
        { 3, new List<StaticElementState>
            {
                new StaticElementState("EE422N", 246, 95, 763, 229),
                new StaticElementState("EE442N", 245, 22, 763, 306),
                new StaticElementState("EE432N", 245, 95, 763, 310),
            }
        },
        { 4, new List<StaticElementState>
            {
                new StaticElementState("EE423N", 246, 95, 987, 229),
                new StaticElementState("EE443N", 245, 22, 987, 306),
                new StaticElementState("EE433N", 245, 95, 987, 310),
            }
        }
    };
    
    public override string Name => "Riddle4";

    public Riddle4(ISoundService soundService) : base(soundService)
    {
        // Initialisation des positions des SE
        currentSEPositions[0] = 2; 
        currentSEPositions[1] = 1; 
        currentSEPositions[2] = 0; 
        currentSEPositions[3] = 1; 

        // Initialisation des positions des IE
        currentIEPositions[0] = 1;
        currentIEPositions[1] = 1;
        currentIEPositions[2] = 1;
        currentIEPositions[3] = 1;
        
        this.BackgroundName = "ZE400N";
        this.SElements.Add(new StaticElement("Onde1", "EE430N", 245,95, 317, 310));
        this.SElements.Add(new StaticElement("Onde2", "EE441N", 245,22, 535, 306));
        this.SElements.Add(new StaticElement("Onde3", "EE422N", 246,95, 763, 229));
        this.SElements.Add(new StaticElement("Onde4", "EE443N", 245,22, 983, 306));

        this.IElements.Add(new InteractiveElement("Button1", "EE400N", 175, 215, 362, 500));
        this.IElements.Add(new InteractiveElement("Button2", "EE401N", 175, 215, 597, 500));
        this.IElements.Add(new InteractiveElement("Button3", "EE402N", 175, 215, 828, 500));
        this.IElements.Add(new InteractiveElement("Button4", "EE403N", 175, 215, 1053, 500));

        this.IElements.Add(new InteractiveElement("Back", "FI000N", 95, 91, 1425, 745));
    }

    public override void ReactElement(string imageName)
    {
        // Trouver l'IE cliqué
        var ie = IElements.FirstOrDefault(x => x.ImageName == imageName);
        if (ie == null) return;

        int ieIndex = IElements.IndexOf(ie);

        // On ignore le "Back" (index 4)
        if (ieIndex < 0 || ieIndex > 3) return;

        int oldPos = currentIEPositions[ieIndex];   // ancienne position (1..4)
        int newPos = oldPos + 1;
        if (newPos > 4) newPos = 1;
        currentIEPositions[ieIndex] = newPos;

        // ✅ Calcul du delta
        int delta = newPos - oldPos;
        if (delta == -3) delta = +1; // sécurité au cas où
        if (delta == 3) delta = -1; // pour éviter tout bug de wrap
        // (mais normalement newPos - oldPos donne toujours 1 ou -3)

        ApplyIEEffectDelta(ieIndex, delta);

        // Mise à jour des images
        UpdateImages(imageName);
        this.soundService.PlaySfx("longBruitEnregistrement.mp3");
        
        if (this.currentSEPositions.All(pos => pos == 1))
        {
            this.IsSuccess = true;
            this.soundService.PlaySfx("bruitOuvertureR4.mp3");
        }
        else
        {
            this.IsSuccess = false;
        }
    }
    
    private void ApplyIEEffectDelta(int ieIndex, int delta)
    {
        if (!ieEffects.TryGetValue(ieIndex + 1, out var effects)) return;

        foreach (var kvp in effects)
        {
            int seIndex = kvp.Key - 1; // 0-based
            int mult = kvp.Value;

            // On applique uniquement la variation
            currentSEPositions[seIndex] += delta * mult;
            currentSEPositions[seIndex] = ((currentSEPositions[seIndex] % 3) + 3) % 3;
        }
    }
    
    private void UpdateImages(string imageName)
    {
        // Met à jour l'IE qui a été cliqué
        InteractiveElement ie = IElements.FirstOrDefault(x => x.ImageName == imageName);
        if (ie != null)
        {
            string oldName = ie.ImageName;
            int ieIndex = IElements.IndexOf(ie);
            int pos = currentIEPositions[ieIndex];
            int nb = (pos-1)*4+ieIndex;
            if (nb >= 10)
            {
                ie.ImageName = $"EE4{nb}N";
            }
            else
                ie.ImageName = $"EE40{nb}N";
            this.NotifyChangeSpecificElement(ie, oldName);
        }

        // Met à jour chaque SE
        for (int i = 0; i < SElements.Count; i++)
        {
            int pos = currentSEPositions[i]; // 0,1,2
            var state = seStates[i+1][pos];  // liste des 3 états
            string oldName = SElements[i].ImageName;
            SElements[i].ImageName = state.ImageName;
            SElements[i].ImageHeight = state.Height;
            SElements[i].ImageWidth = state.Width;
            SElements[i].ImageX = state.X;
            SElements[i].ImageY = state.Y;
            this.NotifyChangeSpecificElement(SElements[i], oldName);
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

    // T'inquiète c'est propre comme archi 😊
    private class StaticElementState
    {
        public string ImageName { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public StaticElementState(string imageName, int width, int height, int x, int y)
        {
            ImageName = imageName;
            Height = height;
            Width = width;
            X = x;
            Y = y;
        }
    }
}



