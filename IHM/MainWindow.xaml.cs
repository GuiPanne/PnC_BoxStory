using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IHM.Control;
using IHM.WPFSoundControl;
using Logic;
using Logic.Obs;
using Logic.Riddles;
using Logic.SoundControl;

namespace IHM;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IObserver
{
    private string path;
    private Scene scene;
    private ISoundService soundService;
    private OptionControl optionControl;
    private Rectangle optionBackdrop;
    
    /// <summary>
    /// init the window of the game 
    /// </summary>
    public MainWindow()
    {
        this.soundService = new SoundService();
        InitializeComponent();
        this.path = "Resources/IMG_clickable/";
        this.scene = new Scene(soundService);
        this.scene.Register(this);
        this.scene.Update();
        
        this.optionControl = new OptionControl(this.soundService, CloseOption);
        this.optionControl.Visibility = Visibility.Collapsed;
        RootGrid.Children.Add(this.optionControl);
        optionControl.HorizontalAlignment = HorizontalAlignment.Left;
        optionControl.VerticalAlignment = VerticalAlignment.Top;
        
        soundService.LaunchMusic();
        this.Loaded += MainWindow_Loaded;
    }

    /// <summary>
    /// reaction when an image is clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ElementClicked(object sender, MouseButtonEventArgs e)
    {
        if (sender is Image img)
        {
            string name = img.Tag?.ToString() ?? img.Name;
            // Envoyer le nom à la scène
            this.scene.Router(img.Tag.ToString());
        }
    }

    public void SceneTransition(string imageName, List<InteractiveElement> interactiveElements, List<StaticElement> staticElements)
    {
        Start_Transition(imageName, interactiveElements, staticElements);
    }

    /// <summary>
    /// display all interactive elements in the windows 
    /// </summary>
    /// <param name="interactiveElements">all interactive elements</param>
    private void SetInteractiveElements(List<InteractiveElement> interactiveElements)
    {
        foreach (InteractiveElement intEl in interactiveElements)
        {
            this.AddImage(intEl.ImageName, intEl.ImageWidth, intEl.ImageHeight, intEl.ImageX, intEl.ImageY, true);
        }
    }
    /// <summary>
    /// display all static elements in the windows
    /// </summary>
    /// <param name="staticElements">all static elements</param>
    private void SetStaticElements(List<StaticElement> staticElements)
    {
        foreach (StaticElement staticEl in staticElements)
        {
            this.AddImage(staticEl.ImageName, staticEl.ImageWidth, staticEl.ImageHeight, staticEl.ImageX, staticEl.ImageY, false);
        }
    }

    public void ChangeSpecificElement(Element element, string oldName)
    {
        Image img = this.MainCanvas.Children
            .OfType<Image>()
            .FirstOrDefault(img => (string)img.Tag == oldName);

        if (img != null)
        {
            img.Source = new BitmapImage(new Uri(path+element.ImageName+".png", UriKind.Relative));
            img.Width = element.ImageWidth;
            img.Height = element.ImageHeight;
            img.Tag = element.ImageName;
            Canvas.SetLeft(img, element.ImageX);
            Canvas.SetTop(img, element.ImageY);
        }
    }

    public void RemoveSpecificElement(Element element)
    {
        Image img = this.MainCanvas.Children
            .OfType<Image>()
            .FirstOrDefault(img => (string)img.Tag == element.ImageName);
        this.MainCanvas.Children.Remove(img);
    }

    public void LaunchDialogue(string dialogueFileName)
    {
        DialogueControl dc = new DialogueControl(dialogueFileName, soundService);
        RootGrid.Children.Add(dc);
    }

    public void LaunchOption()
    {
        optionControl.Visibility = Visibility.Visible;
    }
    
    /// <summary>
    /// Close the option panel
    /// </summary>
    public void CloseOption()
    {
        optionControl.Visibility = Visibility.Collapsed;
    }

    /// <summary>
    /// Create an image (uiElement) in the canvas
    /// </summary>
    /// <param name="name"> the name of the image</param>
    /// <param name="width"> the width of the image</param>
    /// <param name="height"> the height of the image</param>
    /// <param name="x"> the left coordonnees of the image</param>
    /// <param name="y"> the top coordonnees of the image</param>
    private void AddImage(string name, int width, int height, double x, double y, bool interactive)
    {
        Image image = new Image
        {
            Source = new BitmapImage(new Uri(path+name+".png", UriKind.Relative)),
            Width = width,
            Height = height,
            Tag = name
        };
        if (interactive)
        {
            image.MouseLeftButtonUp += ElementClicked;

            image.MouseEnter += Image_MouseEnter;
            image.MouseLeave += Image_MouseLeave;
        }

        Canvas.SetLeft(image, x);
        Canvas.SetTop(image, y);
        MainCanvas.Children.Add(image);
    }

    /// <summary>
    /// When the mouse is over an interactive element
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Image_MouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is Image img)
        {
            string tempName = img.Tag?.ToString() ?? img.Name;
            tempName = tempName.Remove(5);
            tempName += 'S';
            img.Source = new BitmapImage(new Uri(path+tempName+".png", UriKind.Relative));
        }
    }
    /// <summary>
    /// When the mouse leave an interactive element
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Image_MouseLeave(object sender, MouseEventArgs e)
    {
        if (sender is Image img)
        {
            img.Source = new BitmapImage(new Uri(path+img.Tag+".png", UriKind.Relative));
        }
    }
    /// <summary>
    /// made a black screen transition to display a new scene 
    /// </summary>
    /// <param name="newBackgroundName">the background name</param>
    /// <param name="interactiveElements">all interactive elements to display</param>
    /// <param name="staticElements">all static elements to display</param>
    private void Start_Transition(string newBackgroundName, List<InteractiveElement> interactiveElements, List<StaticElement> staticElements)
    {
        List<UIElement> copy = MainCanvas.Children.OfType<UIElement>().ToList();
        Rectangle rect = new Rectangle
        {
            Tag = "rect",
            Fill = new SolidColorBrush(Colors.Black),
            Opacity = 0,
            Width = MainCanvas.ActualWidth,
            Height = MainCanvas.ActualHeight
        };
        MainCanvas.Children.Add(rect);
        Panel.SetZIndex(rect, int.MaxValue);

        var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));
        fadeIn.Completed += (s, e) =>
        {
            foreach (UIElement el in copy)
            {
                if (el != rect)
                {
                    MainCanvas.Children.Remove(el);
                }
            }
            // Étape 2 : changer le background
            string folder = "Resources/IMG_background/";
            string path = folder + newBackgroundName + ".png";
            this.Background = new ImageBrush(new BitmapImage(new Uri(path, UriKind.Relative)));

            // Étape 3 : ajouter les éléments 
            this.SetInteractiveElements(interactiveElements);
            this.SetStaticElements(staticElements);

            // Étape 4 : fade-out
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
            fadeOut.Completed += (s2, e2) => MainCanvas.Children.Remove(rect);

            rect.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        };

        rect.BeginAnimation(UIElement.OpacityProperty, fadeIn);
    }

    /// <summary>
    /// init the menu who is before the start of the game
    /// </summary>
    private void InitializeMenu()
    {
        Grid menuGrid = new Grid
        {
            Background = new SolidColorBrush(Color.FromRgb(15, 15, 40)),
            Width = this.Width,
            Height = this.Height
        };
        RootGrid.Children.Add(menuGrid);
        Panel.SetZIndex(menuGrid, 1000);

        StackPanel stack = new StackPanel
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        menuGrid.Children.Add(stack);

        Button  playButton = new Button { Content = "Jouer", Width = 200, Height = 50, Margin = new Thickness(0,10,0,10) };
        Button quitButton = new Button { Content = "Quitter", Width = 200, Height = 50, Margin = new Thickness(0,10,0,10) };

        playButton.Click += (s, e) =>
        {
            
            menuGrid.Visibility = Visibility.Collapsed;
            this.LaunchDialogue("IntroText");
        };
        quitButton.Click += (s, e) =>this.Close();

        stack.Children.Add(playButton);
        stack.Children.Add(quitButton);
    }

    /// <summary>
    /// When all is charge display the menu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        InitializeMenu();
    }

}