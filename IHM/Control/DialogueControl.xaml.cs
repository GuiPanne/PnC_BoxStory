using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Logic.SoundControl;
using NAudio.Wave;

namespace IHM;
/// <summary>
/// Represents the dialogue display
/// </summary>
public partial class DialogueControl : UserControl
{
    private readonly Queue<string> dialogues;
    private string voiceNamePath;
    
    private int defaultFrequency = 440;
    private bool isWriting = false;
    DispatcherTimer dt = new DispatcherTimer();
    private string adaptedDialog;
    private int i;
    private ISoundService soundService;
    
    /// <summary>
    /// init the dialogue for a specific file
    /// </summary>
    /// <param name="txtFileName">the file name who contains text of the dialogue</param>
    public DialogueControl(string txtFileName, ISoundService soundService)
    {
        string path = "Resources/text/" + txtFileName+".txt";
        this.soundService = soundService;
        var lines = File.ReadAllLines(path);
        dialogues = new Queue<string>(lines);
        dt.Interval = new TimeSpan(0, 0, 0, 0, 60); // vitesse d'affichage
        InitializeComponent();
        this.soundService.PlaySpecificMusic(txtFileName+".mp3");
        ShowNextDialogue();
    }

    /// <summary>
    /// Launch each lines of the .txt progressively 
    /// </summary>
    private void ShowNextDialogue()
    {
        i = 0;
        isWriting = true;
        if (dialogues.Count > 0)
        {
            DialogueText.Text = "";
            DialogueTextProgress.Text = "";
            string dialogue = dialogues.Dequeue();
            adaptedDialog = this.IdentifyVoice(dialogue);
            DialogueText.Text = adaptedDialog;
            dt.Tick -= DisplayDialogue;
            dt.Tick += DisplayDialogue;
            dt.Start();
        }
        else
        {
            if (this.Parent is Panel parent)
            {
                parent.IsHitTestVisible = false;
                parent.Children.Remove(this);
                
                this.soundService.LaunchMusic();
            }
        }
    }
    /// <summary>
    /// display the dialogue
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DisplayDialogue(object sender, EventArgs e)
    {
        if (i < adaptedDialog.Length)
        {
            char c = adaptedDialog[i];
            DialogueTextProgress.Text += c;
            this.soundService.PlayDialogue(c);
            i++;
        }
        else
        {
            dt.Stop();
            isWriting = false;
        }
    }
    /// <summary>
    /// when clicked launch the next line of the dialogue
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnClickNext(object sender, MouseButtonEventArgs e)
    {
        if (isWriting)
        {
            isWriting = false;
            dt.Stop();
            DialogueTextProgress.Text = adaptedDialog;
        }else
            ShowNextDialogue();
    }
    /// <summary>
    /// Identify the person who speaks in the dialogue thanks of the name in the beginning of the line
    /// </summary>
    /// <param name="d">the dialogue line</param>
    /// <returns>return the line of the dialogue without the name</returns>
    private string IdentifyVoice(string d)
    {
        string[] split = d.Split("---");
        string dialogue = split[1];
        this.voiceNamePath = split[0];
        this.ChangeTextColor();
        return dialogue;
    }

    /// <summary>
    /// Change the text color depending on the person who speak
    /// </summary>
    private void ChangeTextColor()
    {
        switch (this.voiceNamePath)
        {
            case "ETHAN":
                this.DialogueTextProgress.Foreground = new SolidColorBrush(Color.FromRgb(245,73,39));
                break;
            case "JEAN":
                this.DialogueTextProgress.Foreground = new SolidColorBrush(Color.FromRgb(251, 168, 35));
                break;
            case "MARIA":
                this.DialogueTextProgress.Foreground = new SolidColorBrush(Color.FromRgb(251, 35, 118));
                break;
            case "DESCRIPTION":
                this.DialogueTextProgress.Foreground = new SolidColorBrush(Color.FromRgb(111, 174, 127));
                break;
            case "ACTION":
                this.DialogueTextProgress.Foreground = new SolidColorBrush(Color.FromRgb(96, 107, 190));
                break;
        }
    }
    

}
