using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Logic.SoundControl;

namespace IHM;

public partial class DialogueControl : UserControl
{
    private readonly Queue<string> dialogues;
    private string voiceNamePath;
    private ISoundService soundService;

    public DialogueControl(string txtFileName, ISoundService soundService)
    {
        this.soundService = soundService;
        string path = "Resources/text/" + txtFileName+".txt";
        InitializeComponent();

        // Charger le fichier texte (une ligne = un dialogue)
        var lines = File.ReadAllLines(path);
        dialogues = new Queue<string>(lines);

        ShowNextDialogue();
    }

    private void ShowNextDialogue()
    {
        if (dialogues.Count > 0)
        {
            DialogueText.Text = "";
            string dialogue = dialogues.Dequeue();
            
            //know the voice
            string adaptedDialog = this.IdentifyVoice(dialogue);
            
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0,0,0,0,10);
            int i = 0;
            dt.Tick += (s, e) =>
            {
                DialogueText.Text += adaptedDialog[i];
                
                // Tous les 2 caractères → jouer un son
                if (i % 45 == 0 && !string.IsNullOrWhiteSpace(adaptedDialog[i].ToString()))
                {
                    this.soundService.DialogSFX(this.voiceNamePath);
                }
                
                i++;
                if (i >= adaptedDialog.Length)
                {
                    dt.Stop();
                }
            };
            dt.Start();
        }
        else
        {
            // Fin → supprimer le contrôle
            if (this.Parent is Panel parent)
                parent.Children.Remove(this);
        }
    }

    private void OnClickNext(object sender, MouseButtonEventArgs e)
    {
        ShowNextDialogue();
    }

    private string IdentifyVoice(string d)
    {
        string[] split = d.Split("---");
        string dialogue = split[1];
        this.voiceNamePath = split[0];
        return dialogue;
    }
}
