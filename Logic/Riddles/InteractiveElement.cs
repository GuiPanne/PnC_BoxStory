namespace Logic.Enigmes;

public class InteractiveElement
{
    private string imageName;
    private int imageWidth;
    private int imageHeight;
    private double imageX;
    private double imageY;
    
    public string ImageName => imageName;
    public int ImageWidth => imageWidth;
    public int ImageHeight => imageHeight;
    public double ImageX => imageX;
    public double ImageY => imageY;

    public InteractiveElement(string imageName, int imageWidth, int imageHeight, double imageX, double imageY)
    {
        this.imageName = imageName;
        this.imageWidth = imageWidth;
        this.imageHeight = imageHeight;
        this.imageX = imageX;
        this.imageY = imageY;
    }
}