namespace Logic.obs;

public abstract class Observable
{
    private List<IObserver> observers;

    public Observable()
    {
        this.observers = new List<IObserver>();
    }

    public void Register(IObserver o)
    {
        this.observers.Add(o);
    }

    public void Unregister(IObserver o)
    {
        this.observers.Remove(o);
    }

    protected void NotifyBackgroundChange(string imageName)
    {
        throw new NotImplementedException();
    }
    
}