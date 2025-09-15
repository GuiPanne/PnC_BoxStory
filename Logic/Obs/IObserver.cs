using Logic.Riddles;

namespace Logic.Obs;

/// <summary>
/// interface of an observer who react to the notification of an observable
/// </summary>
public interface IObserver
{
    /// <summary>
    /// change the background 
    /// </summary>
    /// <param name="imageName">the name of the image who is the new BG</param>
    /// <param name="interactiveElements"> interactive elements to display</param>
    /// <param name="staticElements">the static elements to display </param>
    public void SceneTransition(string imageName, List<InteractiveElement> interactiveElements, List<StaticElement> staticElements);

    /// <summary>
    /// Update the image of a specific element 
    /// </summary>
    /// <param name="element"></param>
    /// <param name="oldName"> the old name of the image needed to find it</param>
    public void ChangeSpecificElement(Element element, string oldName);
    
    public void RemoveSpecificElement(Element element);
    public void LaunchDialogue(string dialogueFileName);
}