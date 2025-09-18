using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Logic.SoundControl;

namespace IHM.Control;

public partial class OptionControl : UserControl
{
    private ISoundService soundService;
    private Action closeAction;
    
    public OptionControl( ISoundService soundService, Action CloseAction)
    {
        this.soundService = soundService;
        InitializeComponent();
        this.closeAction = closeAction;
        this.MusicSlider.Value = soundService.VolumeMusic;
        this.DialogueSlider.Value = soundService.VolumeDialogue;
        this.SFXSlider.Value = soundService.VolumeEffect;
    }

    private void Retour_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        this.Visibility = Visibility.Collapsed;
    }

    private void DialogueSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        this.soundService.VolumeDialogue = e.NewValue;
    }

    private void SFXSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        this.soundService.VolumeEffect = e.NewValue;
    }

    private void MusicSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        this.soundService.VolumeMusic = e.NewValue;
    }
}