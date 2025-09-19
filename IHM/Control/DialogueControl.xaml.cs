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
    private Dictionary<char, int> frequencyMap = new Dictionary<char, int>
    {
        // Voyelles fréquentes → graves espacées
        { 'A', 200 }, { 'E', 240 }, { 'I', 280 }, { 'O', 320 }, { 'U', 360 },
        { 'a', 200 }, { 'e', 240 }, { 'i', 280 }, { 'o', 320 }, { 'u', 360 },

        // Consonnes douces → graves/moyennes
        { 'L', 330 }, { 'M', 350 }, { 'N', 370 }, { 'R', 390 }, { 'S', 410 },
        { 'l', 330 }, { 'm', 350 }, { 'n', 370 }, { 'r', 390 }, { 's', 410 },

        // Lettres rares → aiguës rapprochées
        { 'Q', 700 }, { 'Z', 740 }, { 'X', 780 }, { 'J', 820 }, { 'K', 860 },
        { 'V', 880 }, { 'W', 900 }, { 'Y', 920 },
        { 'q', 700 }, { 'z', 740 }, { 'x', 780 }, { 'j', 820 }, { 'k', 860 },
        { 'v', 880 }, { 'w', 900 }, { 'y', 920 },

        // Autres lettres → moyennes
        { 'B', 440 }, { 'C', 460 }, { 'D', 480 }, { 'F', 500 }, { 'G', 520 },
        { 'H', 540 }, { 'P', 560 }, { 'T', 580 },

        // Ponctuation
        { '.', 330 }, { ',', 310 }, { '!', 800 }, { '?', 750 }, { ':', 400 }, { ';', 450 }
    };
    private int defaultFrequency = 440;
    private bool isWriting = false;
    DispatcherTimer dt = new DispatcherTimer();
    private string adaptedDialog;
    private int i;

    /// <summary>
    /// init the dialogue for a specific file
    /// </summary>
    /// <param name="txtFileName">the file name who contains text of the dialogue</param>
    public DialogueControl(string txtFileName)
    {
        string path = "Resources/text/" + txtFileName+".txt";
        
        var lines = File.ReadAllLines(path);
        dialogues = new Queue<string>(lines);
        dt.Interval = new TimeSpan(0, 0, 0, 0, 60); // vitesse d'affichage
        InitializeComponent();
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
                parent.Children.Remove(this);
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
            int freq = frequencyMap.ContainsKey(c) ? frequencyMap[c] : defaultFrequency;
            PlayBlip(freq, 50, "accordion");
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
    
    /// <summary>
    /// play the sound when a character appear
    /// </summary>
    /// <param name="frequency">the frequency of the character</param>
    /// <param name="durationMs">the duration of the note</param>
    /// <param name="instrument">the instrument who is simulated</param>
    /// <param name="volume">the volume to play</param>
    private void PlayBlip(int frequency, int durationMs, string instrument = "synth", float volume = 0.05f)
    { 
        int sampleRate = 44100;
        int samples = (int)((sampleRate * durationMs) / 1000.0);
        var buffer = new float[samples];

        for (int i = 0; i < samples; i++)
        {
            double t = (double)i / sampleRate;
            double sample = 0;

            switch (instrument.ToLower())
            {
                case "synth": // machine à écrire / synthé grave
                    // sinusoïde + octave, attaque ultra courte
                    sample = Math.Sin(2 * Math.PI * frequency * t) + 0.2 * Math.Sin(2 * Math.PI * frequency * 2 * t);
                    break;

                case "accordion":
                    // sinusoïde + dent de scie + vibrato léger
                    double vibrato = Math.Sin(2 * Math.PI * 5 * t) * 0.02; // 5Hz vibrato ±2%
                    sample = Math.Sin(2 * Math.PI * frequency * (1 + vibrato) * t)
                           + 0.3 * Math.Sin(2 * Math.PI * frequency * 2 * (1 + vibrato) * t)
                           + 0.15 * Math.Sin(2 * Math.PI * frequency * 3 * (1 + vibrato) * t);
                    break;

                case "violin":
                    // triangle approximée avec harmoniques + léger glissando
                    double gliss = 0.003 * i / samples; // variation très faible
                    sample = Math.Sin(2 * Math.PI * frequency * (1 + gliss) * t)
                           + 0.5 * Math.Sin(2 * Math.PI * frequency * 2 * (1 + gliss) * t)
                           + 0.3 * Math.Sin(2 * Math.PI * frequency * 3 * (1 + gliss) * t);
                    break;

                default:
                    // fallback : simple sinusoïde
                    sample = Math.Sin(2 * Math.PI * frequency * t);
                    break;
            }

            // enveloppe ADSR simplifiée (attaque rapide pour synth, medium pour accordéon/violon)
            double envelope = 1.0 - (double)i / samples;
            if (envelope < 0) envelope = 0;

            buffer[i] = (float)(sample * envelope * volume);
        }

        var provider = new BufferedWaveProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 1));
        provider.AddSamples(buffer.SelectMany(BitConverter.GetBytes).ToArray(), 0, buffer.Length * 4);

        var waveOut = new WaveOutEvent();
        waveOut.Init(provider);
        waveOut.Play();

        Task.Delay(durationMs).ContinueWith(_ => waveOut.Dispose());
    }

}
