using Logic.Obs;
using Logic.SoundControl;

namespace Logic.Riddles.RiddleModel;

public class Riddle6 : Riddle
{
    private string[,] expected =
    {
        { "1",  "2",  "3",  "4"  },
        { "5",  "6",  "7",  "8"  },
        { "9",  "10", "11", "12" },
        { "13", "14", "15", "0"  }
    };
    private List<(int dx, int dy)> directions = new List<(int dx, int dy)>
    {
        (-1, 0), 
        (1, 0),  
        (0, -1), 
        (0, 1)
    };
    private Dictionary<Coord, InteractiveElement> cases;
    
    public override string Name => "Riddle6";

    public Riddle6(ISoundService soundService) : base(soundService)
    {
        //line 1
        this.IElements.Add(new InteractiveElement("04", "EE004N", 100,100, 15,15));
        this.IElements.Add(new InteractiveElement("01", "EE001N", 100,100, 15,15));
        this.IElements.Add(new InteractiveElement("02", "EE002N", 100,100, 15,15));
        this.IElements.Add(new InteractiveElement("03", "EE003N", 100,100, 15,15));
        
        //line 2
        this.IElements.Add(new InteractiveElement("05", "EE005N", 100,100, 15,15));
        this.IElements.Add(new InteractiveElement("06", "EE006N", 100,100, 15,15));
        this.IElements.Add(new InteractiveElement("08", "EE008N", 100,100, 15,15));
        this.IElements.Add(new InteractiveElement("07", "EE007N", 100,100, 15,15));

        //line 3
        this.IElements.Add(new InteractiveElement("15", "EE015N", 100,100, 15,15));
        this.IElements.Add(new InteractiveElement("12", "EE012N", 100,100, 15,15));
        this.IElements.Add(new InteractiveElement("11", "EE011N", 100,100, 15,15));
        this.IElements.Add(new InteractiveElement("10", "EE010N", 100,100, 15,15));
        
        //line 4
        this.IElements.Add(new InteractiveElement("13", "EE013N", 100,100, 15,15));
        this.IElements.Add(new InteractiveElement("00", "EE000N", 100,100, 15,15));
        this.IElements.Add(new InteractiveElement("14", "EE014N", 100,100, 15,15));
        this.IElements.Add(new InteractiveElement("09", "EE009N", 100,100, 15,15));

        this.cases = new Dictionary<Coord, InteractiveElement>();
        
        this.cases.Add(new Coord(0,0), this.IElements.Find(e => e.Name == "04"));
        this.cases.Add(new Coord(1,0), this.IElements.Find(e => e.Name == "01"));
        this.cases.Add(new Coord(2,0), this.IElements.Find(e => e.Name == "02"));
        this.cases.Add(new Coord(3,0), this.IElements.Find(e => e.Name == "03"));
        
        this.cases.Add(new Coord(0,1), this.IElements.Find(e => e.Name == "05"));
        this.cases.Add(new Coord(1,1), this.IElements.Find(e => e.Name == "06"));
        this.cases.Add(new Coord(2,1), this.IElements.Find(e => e.Name == "08"));
        this.cases.Add(new Coord(3,1), this.IElements.Find(e => e.Name == "07"));
        
        this.cases.Add(new Coord(0,2), this.IElements.Find(e => e.Name == "15"));
        this.cases.Add(new Coord(1,2), this.IElements.Find(e => e.Name == "12"));
        this.cases.Add(new Coord(2,2), this.IElements.Find(e => e.Name == "11"));
        this.cases.Add(new Coord(3,2), this.IElements.Find(e => e.Name == "10"));
        
        this.cases.Add(new Coord(0,3), this.IElements.Find(e => e.Name == "13"));
        this.cases.Add(new Coord(1,3), this.IElements.Find(e => e.Name == "00"));
        this.cases.Add(new Coord(2,3), this.IElements.Find(e => e.Name == "14"));
        this.cases.Add(new Coord(3,3), this.IElements.Find(e => e.Name == "09"));
    }

    public override void ReactElement(string imageName)
    {
        if (imageName != "EE000N")
        {
            string name = imageName.Substring(3,2);
            InteractiveElement element = this.IElements.Find(e => e.Name == name);
            Coord actualPosition = this.cases.First(kv => kv.Value == element).Key;
            InteractiveElement neighbor = null;
            foreach (var (dx, dy) in directions)
            {
                Coord neighborCoord = new Coord(actualPosition.X + dx, actualPosition.Y + dy);

                if (this.cases.TryGetValue(neighborCoord, out var potentialNeighbor) 
                    && potentialNeighbor.Name == "0") 
                {
                    neighbor = potentialNeighbor;
                    break;
                }
            }

            if (neighbor != null)
            {
                Coord neighborCoord = this.cases.First(kv => kv.Value == neighbor).Key;
                this.cases[actualPosition] = neighbor;
                this.cases[neighborCoord] = element;
                double tmpX = element.ImageX;
                double tmpY = element.ImageY;
                element.ImageX = neighbor.ImageX;
                element.ImageY = neighbor.ImageY;
                neighbor.ImageX = tmpX;
                neighbor.ImageY = tmpY;
                this.NotifyChangeSpecificElement(element, element.ImageName);
                this.NotifyChangeSpecificElement(neighbor, neighbor.ImageName);
            }
            CheckSolved();
        }
    }
    /// <summary>
    /// verify if the sliding puzzle is solved
    /// </summary>
    private void CheckSolved()
    {
        bool solved = true;
        for (int y = 0; y < 4 && solved; y++)
        {
            for (int x = 0; x < 4 && solved; x++)
            {
                Coord coord = new Coord(x, y);
                if (cases.TryGetValue(coord, out InteractiveElement element))
                {
                    if (element.Name != expected[y, x])
                    {
                        solved = false;
                    }
                }
                else
                {
                    solved = false;
                }
            }
        }
        this.IsSuccess = solved;
    }
    protected override void NotifySceneTransition(string imageName, List<InteractiveElement> interactiveElements, List<StaticElement> staticElements)
    {
        throw new NotImplementedException();
    }

    protected override void NotifyChangeSpecificElement(Element element, string oldName)
    {
        foreach (IObserver o in this.observers)
        {
            o.ChangeSpecificElement(element, oldName);
        }
    }

    protected override void NotifyRemoveSpecificElement(Element element)
    {
        foreach (IObserver o in this.observers)
        {
            o.RemoveSpecificElement(element);
        }
    }

    protected override void NotifyLaunchDialogue(string dialogueFileName)
    {
        throw new NotImplementedException();
    }

    protected override void NotifyLaunchOption()
    {
        throw new NotImplementedException();
    }

    public override void UpdateRoomRiddle(int successNumber, int changeRotation)
    {
        throw new NotImplementedException();
    }

    private readonly record struct Coord(int X, int Y);
}