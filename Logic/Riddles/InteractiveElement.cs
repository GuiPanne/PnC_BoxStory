namespace Logic.Enigmes;

public class InteractiveElement
{
    private string imageName;
    private int imageWidth;
    private int imageHeight;
    private double imageX;
    private double imageY;
    
    public string ImageName
    {
        get => imageName;
        set => imageName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int ImageWidth
    {
        get => imageWidth;
        set => imageWidth = value;
    }

    public int ImageHeight
    {
        get => imageHeight;
        set => imageHeight = value;
    }

    public double ImageX
    {
        get => imageX;
        set => imageX = value;
    }

    public double ImageY
    {
        get => imageY;
        set => imageY = value;
    }

    public InteractiveElement(string imageName, int imageWidth, int imageHeight, double imageX, double imageY)
    {
        this.imageName = imageName;
        this.imageWidth = imageWidth;
        this.imageHeight = imageHeight;
        this.imageX = imageX;
        this.imageY = imageY;
    }
    
    protected bool Equals(InteractiveElement other)
    {
        return imageName == other.imageName && imageWidth == other.imageWidth && imageHeight == other.imageHeight && imageX.Equals(other.imageX) && imageY.Equals(other.imageY);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((InteractiveElement)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = imageName.GetHashCode();
            hashCode = (hashCode * 397) ^ imageWidth;
            hashCode = (hashCode * 397) ^ imageHeight;
            hashCode = (hashCode * 397) ^ imageX.GetHashCode();
            hashCode = (hashCode * 397) ^ imageY.GetHashCode();
            return hashCode;
        }
    }
}