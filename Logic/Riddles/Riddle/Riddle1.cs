namespace Logic.Enigmes.Riddle1;

public class Riddle1 : Riddle
{
    public override string Name => "Riddle1";
    
    private InteractiveElement element1;
    
    public Riddle1() : base()
    {
        element1 = new InteractiveElement("EE100N", 354, 711, 50, 50);
    }
        
    public override void ReactElement(string imageName)
    {
        if (imageName == "EE100N")
        {
            element1.ImageName = "EE101N";
        }
    }
}