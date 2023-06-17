using IronOcr;
using System.Reflection;
using Tesseract;
using System;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace BoxSpeaker
{
    public partial class Form1 : Form
    {
        String textParle = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //Save presse-papier
        {
            if (Clipboard.ContainsImage())
            {
                Image image = Clipboard.GetImage();
                string filePath = "image.png"; // Chemin du fichier PNG � enregistrer

                // Enregistre l'image dans un fichier PNG
                image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                MessageBox.Show("Fichier PNG g�n�r� : " + filePath);
            }
            else
            {
                MessageBox.Show("Aucune image n'a �t� d�tect�e dans le presse-papiers.");
            }

        }

        private void button2_Click(object sender, EventArgs e) //Start reading
        {
            var Ocr = new IronTesseract(); // nothing to configure
            Ocr.Language = OcrLanguage.French; //Langue
            Ocr.Configuration.TesseractVersion = TesseractVersion.Tesseract5; //Config
            using (var Input = new OcrInput()) //Instancie une OcrInput class
            {
                Input.AddImage(@"image.png"); //Chemin de l'image
                var Result = Ocr.Read(Input); //Lecture de l'image
                textParle = Result.Text; //Sortie du texte dans un fichier
            }
        }

        private void button3_Click(object sender, EventArgs e) //Stop reading
        {
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult); // S�lectionne une voix f�minine adulte (vous pouvez modifier cela selon vos pr�f�rences)

                synthesizer.Speak(textParle);
            }
        }

        private void button4_Click(object sender, EventArgs e) //Save text
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}