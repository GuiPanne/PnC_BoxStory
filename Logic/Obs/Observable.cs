using Logic.Riddles;

namespace Logic.Obs;

/// <summary>
/// Abstract class who can be observed and indicate something change
/// </summary>
public abstract class Observable
{
    protected List<IObserver> observers;

    /// <summary>
    /// init the observable
    /// </summary>
    public Observable()
    {
        this.observers = new List<IObserver>();
    }

    /// <summary>
    /// add an observer to the list of observers
    /// </summary>
    /// <param name="o"> the  new observer</param>
    public void Register(IObserver o)
    {
        this.observers.Add(o);
    }

    /// <summary>
    /// remove an observer to the of observers
    /// </summary>
    /// <param name="o">the observer to remove</param>
    public void Unregister(IObserver o)
    {
        this.observers.Remove(o);
    }

    /// <summary>
    /// notify the observers the bg changes
    /// </summary>
    /// <param name="imageName">the new background name</param>
    /// <param name="interactiveElements"> elements to display</param>
    protected abstract void NotifySceneTransition(string imageName, List<InteractiveElement> interactiveElements, List<StaticElement> staticElements);

    /// <summary>
    /// notify the observer to update a specific element 
    /// </summary>
    /// <param name="element">the new state of the element</param>
    /// <param name="oldName">the old name of the image needed to find it</param>
    protected abstract void NotifyChangeSpecificElement(Element element, string oldName);
    
    /// <summary>
    /// notify the observers to remove a specific element
    /// </summary>
    /// <param name="element">the element to remove</param>
    protected abstract void NotifyRemoveSpecificElement(Element element);

    /// <summary>
    /// Notify the observers to launch a specific dialogue
    /// </summary>
    /// <param name="dialogueFileName">the file name of the dialogue</param>
    protected abstract void NotifyLaunchDialogue(string dialogueFileName);

    /// <summary>
    /// Notify the observers to launch the option pannel
    /// </summary>
    protected abstract void NotifyLaunchOption();

}