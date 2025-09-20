using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Logic.SoundControl;

namespace IHM.Control;

/// <summary>
/// represents the pannel of option
/// </summary>
public partial class OptionControl : UserControl
{
    private WindowState _previousWindowState;
    private WindowStyle _previousWindowStyle;
    private ResizeMode _previousResizeMode;
    
    private ISoundService soundService;
    private Action closeAction;
    
    /// <summary>
    /// init the pannel option
    /// </summary>
    /// <param name="soundService">the soundService who can be modified</param>
    /// <param name="CloseAction">the action link to the close of the pannel</param>
    public OptionControl( ISoundService soundService, Action CloseAction)
    {
        this.soundService = soundService;
        InitializeComponent();
        this.closeAction = closeAction;
        this.MusicSlider.Value = soundService.VolumeMusic;
        this.DialogueSlider.Value = soundService.VolumeDialogue;
        this.SFXSlider.Value = soundService.VolumeEffect;
    }

    /// <summary>
    /// Hide the menu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Retour_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        this.Visibility = Visibility.Collapsed;
        closeAction?.Invoke();
    }
    /// <summary>
    /// change the dialogue Volume
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DialogueSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        this.soundService.VolumeDialogue = e.NewValue;
    }

    /// <summary>
    /// Change the SFX volume
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SFXSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        this.soundService.VolumeEffect = e.NewValue;
    }

    /// <summary>
    /// Change the music volume
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MusicSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        this.soundService.VolumeMusic = e.NewValue;
    }

    private void FullScreenToggle_Checked(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this);
        _previousWindowState = parentWindow.WindowState;
        _previousWindowStyle = parentWindow.WindowStyle;
        _previousResizeMode = parentWindow.ResizeMode;

        // Passe en plein écran
        parentWindow.WindowStyle = WindowStyle.None;
        parentWindow.ResizeMode = ResizeMode.NoResize;
        parentWindow.WindowState = WindowState.Maximized;
        parentWindow.Topmost = true;
    }
    
    private void FullScreenToggle_Unchecked(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this);
        parentWindow.Topmost = false;
        parentWindow.WindowStyle = _previousWindowStyle;
        parentWindow.ResizeMode = _previousResizeMode;
        parentWindow.WindowState = _previousWindowState;
    }
}