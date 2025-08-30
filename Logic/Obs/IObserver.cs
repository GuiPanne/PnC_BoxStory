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
    /// <param name="interactiveElements"> the name of interactive elements to display</param>
    public void SceneTransition(string imageName, List<InteractiveElement> interactiveElements);
    
    /// <summary>
    /// Update the image of a specific element 
    /// </summary>
    /// <param name="intEl"> the interactive element to update</param>
    /// <param name="oldName"> the old name of the image needed to find it</param>
    public void ChangeSpecificIntElement(InteractiveElement intEl, string oldName);
}