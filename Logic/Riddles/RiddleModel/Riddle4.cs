using Logic.Obs;
using Logic.SoundControl;

namespace Logic.Riddles.RiddleModel;

public class Riddle4 : Riddle
{
    private int ondeValue1;
    private int ondeValue2;
    private int ondeValue3;
    private int ondeValue4;
    
    public override string Name => "Riddle4";

    public Riddle4(ISoundService soundService) : base(soundService)
    {
        this.ondeValue1 = 2;
        this.ondeValue2 = 1;
        this.ondeValue3 = 0;
        this.ondeValue4 = 1;
        
        this.BackgroundName = "ZE400N";
        this.SElements.Add(new StaticElement("Onde1", "EE430N", 245,95, 200, 206));
        this.SElements.Add(new StaticElement("Onde2", "EE422N", 245,22, 445, 237.5));
        this.SElements.Add(new StaticElement("Onde3", "EE430N", 245,95, 690, 206));
        this.SElements.Add(new StaticElement("Onde4", "EE443N", 245,22, 935, 237.5));

        this.IElements.Add(new InteractiveElement("Button1", "EE400N", 175, 215, 50, 500));
        this.IElements.Add(new InteractiveElement("Button2", "EE401N", 175, 215, 250, 500));
        this.IElements.Add(new InteractiveElement("Button3", "EE402N", 175, 215, 450, 500));
        this.IElements.Add(new InteractiveElement("Button4", "EE403N", 175, 215, 650, 500));

        this.IElements.Add(new InteractiveElement("Back", "FI000N", 95, 91, 1425, 745));
    }

    public override void ReactElement(string imageName)
    {
        // Table des transitions cycliques
        var transitions = new Dictionary<int, string[]>
        {
            { 0, new[] { "EE400N", "EE404N", "EE408N", "EE412N" } }, // Button1
            { 1, new[] { "EE401N", "EE405N", "EE409N", "EE413N" } }, // Button2
            { 2, new[] { "EE402N", "EE406N", "EE410N", "EE414N" } }, // Button3
            { 3, new[] { "EE403N", "EE407N", "EE411N", "EE415N" } }  // Button4
        };

        switch (imageName)
        {
            case string s1 when transitions[0].Contains(s1):
                this.ondeValue1 -= 1;
                this.ondeValue2 += 1;
                CycleImage(0, s1, transitions[0]);
                break;

            case string s2 when transitions[1].Contains(s2):
                this.ondeValue1 += 1;
                this.ondeValue3 -= 1;
                CycleImage(1, s2, transitions[1]);
                break;

            case string s3 when transitions[2].Contains(s3):
                this.ondeValue2 -= 2;
                CycleImage(2, s3, transitions[2]);
                break;

            case string s4 when transitions[3].Contains(s4):
                this.ondeValue3 += 1;
                this.ondeValue4 += 2;
                CycleImage(3, s4, transitions[3]);
                break;
        }

        // Clamp et mise à jour des ondes
        UpdateOnde(0, ref this.ondeValue1);
        UpdateOnde(1, ref this.ondeValue2);
        UpdateOnde(2, ref this.ondeValue3);
        UpdateOnde(3, ref this.ondeValue4);

        if (this.ondeValue1 == 1 && this.ondeValue2 == 1 && this.ondeValue3 == 1 && this.ondeValue4 == 1)
        {
            this.IsSuccess = true;
            this.soundService.PlaySfx("bruitOuvertureR4.mp3");
        }
        else
        {
            this.IsSuccess = false;
        }
    }

    // Fait tourner l'image cycliquement
    private void CycleImage(int elementIndex, string currentImage, string[] cycle)
    {
        int idx = Array.IndexOf(cycle, currentImage);
        int nextIdx = (idx + 1) % cycle.Length;

        string oldName = this.IElements[elementIndex].ImageName;
        this.IElements[elementIndex].ImageName = cycle[nextIdx];
        this.NotifyChangeSpecificIntElement(this.IElements[elementIndex], oldName);
    }
    
    private void UpdateOnde(int index, ref int ondeValue)
    {
        // Clamp entre 0 et 2
        ondeValue = Math.Clamp(ondeValue, 0, 2);

        string[,] images =
        {
            { "EE420N", "EE440N", "EE430N" }, // Onde1
            { "EE421N", "EE441N", "EE431N" }, // Onde2
            { "EE422N", "EE442N", "EE432N" }, // Onde3
            { "EE423N", "EE443N", "EE433N" }  // Onde4
        };

        string oldName = this.SElements[index].ImageName;
        this.SElements[index].ImageName = images[index, ondeValue];
        this.NotifyChangeSpecificIntElement(this.SElements[index], oldName);
        this.soundService.PlaySfx("longBruitEnregistrement.mp3");
    }
    
    public override void UpdateRoomRiddle(int successNumber)
    {
        throw new NotImplementedException();
    }
    
    
    protected override void NotifySceneTransition(string imageName, List<InteractiveElement> interactiveElements, List<StaticElement> staticElements)
    {
        throw new NotImplementedException();
    }

    protected override void NotifyChangeSpecificIntElement(Element element, string oldName)
    {
        foreach (IObserver obs in this.observers)
        {
            obs.ChangeSpecificIntElement(element, oldName);
        }
    }
}