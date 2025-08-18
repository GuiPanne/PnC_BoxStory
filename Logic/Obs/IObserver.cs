using Logic.Enigmes;

namespace Logic.obs;

public interface IObserver
{
    public void DisplayBackGround(string imageName);
    public void CleanCanvas();
    public void SetInteractiveElements(List<InteractiveElement> interactiveElements);
}