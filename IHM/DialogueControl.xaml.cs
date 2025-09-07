using System.IO;
using System.Windows.Controls;
using System.Windows.Input;

namespace IHM;

public partial class DialogueControl : UserControl
{
    private readonly Queue<string> dialogues;

    public DialogueControl(string txtFileName)
    {
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
            DialogueText.Text = dialogues.Dequeue();
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
}
