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
}