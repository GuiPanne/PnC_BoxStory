namespace Logic.Riddles;

/// <summary>
/// represents an element who can be interacted by the player
/// </summary>
public class InteractiveElement : Element
{
    /// <summary>
    /// init the interactive element
    /// </summary>
    /// <param name="name">the name of the element</param>
    /// <param name="imageName">the name of the image link</param>
    /// <param name="imageWidth"> the width of the image</param>
    /// <param name="imageHeight"> the height of the image</param>
    /// <param name="imageX"> the left position of the image</param>
    /// <param name="imageY"> the right position of the image</param>
    public InteractiveElement(string name, string imageName, int imageWidth, int imageHeight, double imageX,
        double imageY)
        : base(name, imageName, imageWidth, imageHeight, imageX, imageY)
    {
    }
}