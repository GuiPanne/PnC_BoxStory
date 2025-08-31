using Logic.Enigmes;

namespace Logic.obs;

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
    /// <param name="intEl">the element to update</param>
    /// <param name="oldName">the old name of the image needed to find it</param>
    protected abstract void NotifyChangeSpecificIntElement(InteractiveElement intEl, string oldName);

}