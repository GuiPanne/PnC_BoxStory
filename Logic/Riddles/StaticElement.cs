namespace Logic.Enigmes;

/// <summary>
/// Represents a static element
/// </summary>
public class StaticElement
{
    private string name;
    private string imageName;
    private int imageWidth;
    private int imageHeight;
    private double imageX;
    private double imageY;
    
    /// <summary>
    /// the name of the element
    /// </summary>
    public string Name => this.name;
    /// <summary>
    /// the name of the image link to the element
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public string ImageName
    {
        get => imageName;
        set => imageName = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// the width of the image link
    /// </summary>
    public int ImageWidth
    {
        get => imageWidth;
        set => imageWidth = value;
    }

    /// <summary>
    /// the height of the image link
    /// </summary>
    public int ImageHeight
    {
        get => imageHeight;
        set => imageHeight = value;
    }

    /// <summary>
    /// the left position of the image link
    /// </summary>
    public double ImageX
    {
        get => imageX;
        set => imageX = value;
    }

    /// <summary>
    /// the top position of the image link
    /// </summary>
    public double ImageY
    {
        get => imageY;
        set => imageY = value;
    }

    /// <summary>
    /// init the static element
    /// </summary>
    /// <param name="name">the name of the element</param>
    /// <param name="imageName">the name of the image link</param>
    /// <param name="imageWidth"> the width of the image</param>
    /// <param name="imageHeight"> the height of the image</param>
    /// <param name="imageX"> the left position of the image</param>
    /// <param name="imageY"> the right position of the image</param>
    public StaticElement(string name, string imageName, int imageWidth, int imageHeight, double imageX, double imageY)
    {
        this.name = name;
        this.imageName = imageName;
        this.imageWidth = imageWidth;
        this.imageHeight = imageHeight;
        this.imageX = imageX;
        this.imageY = imageY;
    }
    
    /// <summary>
    /// check if another element is similar to this one
    /// </summary>
    /// <param name="other"> the static element to compare</param>
    /// <returns> true if similar</returns>
    protected bool Equals(StaticElement other)
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