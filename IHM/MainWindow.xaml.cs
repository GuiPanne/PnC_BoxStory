using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logic;
using Logic.Enigmes;
using Logic.obs;

namespace IHM;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IObserver
{
    private string path;
    private Scene scene;
    
    /// <summary>
    /// init the window of the game 
    /// </summary>
    public MainWindow()
    {
        this.path = "IHM/Ressources/IMG_clickable/";
        this.scene = new Scene();
        this.scene.Register(this);
        InitializeComponent();
    }

    /// <summary>
    /// reaction when an image is clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ElementClicked(object sender, MouseButtonEventArgs e)
    {
        if (sender is Image img)
        {
            string name = img.Tag?.ToString() ?? img.Name;
            // Envoyer le nom à la scène
            this.scene.Router(img.Tag.ToString());
        }
    }

    public void DisplayBackGround(string imageName)
    {
        string folder = "IHM/Ressources/IMG_background/";
        string path = folder+imageName+".png";
        ImageBrush bg = new ImageBrush();
        bg.ImageSource = new BitmapImage(new Uri(path, UriKind.Relative));
        this.Background = bg;
    }

    public void CleanCanvas()
    {
        foreach (UIElement el in this.MainCanvas.Children)
        {
            MainCanvas.Children.Remove(el);
        }
    }

    public void SetInteractiveElements(List<InteractiveElement> interactiveElements)
    {
        foreach (InteractiveElement intEl in interactiveElements)
        {
            this.AddImage(intEl.ImageName, intEl.ImageWidth, intEl.ImageHeight, intEl.ImageX, intEl.ImageY);
        }
    }

    public void ChangeSpecificIntElement(InteractiveElement intEl)
    {
        Image img = this.MainCanvas.Children
            .OfType<Image>()
            .FirstOrDefault(img => (string)img.Tag == intEl.ImageName);

        if (img != null)
        {
            img.Source = new BitmapImage(new Uri(path+intEl.ImageName+".png", UriKind.Relative));
            img.Tag = intEl.ImageName;
        }
    }
    
    /// <summary>
    /// Create an image (uiElement) in the canvas
    /// </summary>
    /// <param name="name"> the name of the image</param>
    /// <param name="width"> the width of the image</param>
    /// <param name="height"> the height of the image</param>
    /// <param name="x"> the left coordonnees of the image</param>
    /// <param name="y"> the top coordonnees of the image</param>
    private void AddImage(string name, int width, int height, double x, double y)
    {
        Image image = new Image
        {
            Source = new BitmapImage(new Uri(path+name+".png", UriKind.Relative)),
            Width = width,
            Height = height,
            Tag = name
        };
        image.MouseLeftButtonUp += ElementClicked;
        
        // A faire quand on a les version altérés des images
        //image.MouseEnter =
        //image.MouseLeave = 
        
        Canvas.SetLeft(image, x);
        Canvas.SetTop(image, y);
        MainCanvas.Children.Add(image);
    }
}