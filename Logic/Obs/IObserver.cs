using Logic.Enigmes;

namespace Logic.obs;

/// <summary>
/// interface of an observer who react to the notification of an observable
/// </summary>
public interface IObserver
{
    
    /// <summary>
    /// change the background 
    /// </summary>
    /// <param name="imageName">the name of the image who is the new BG</param>
    public void DisplayBackGround(string imageName);
    
    /// <summary>
    /// Remove all element in the canva (image)
    /// </summary>
    public void CleanCanvas();
    
    /// <summary>
    /// Add interactive elements from the scene in the canvas
    /// </summary>
    /// <param name="interactiveElements"></param>
    public void SetInteractiveElements(List<InteractiveElement> interactiveElements);
    
    /// <summary>
    /// Update the image of a specific element 
    /// </summary>
    /// <param name="intEl"> the interactive element to update</param>
    public void ChangeSpecificIntElement(InteractiveElement intEl);
}