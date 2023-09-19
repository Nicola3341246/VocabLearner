using System.IO;

namespace QuizletRipOff
{
    public partial class Form1 : Form
    {
        static string path = @"";
        List<VociWord> words = new List<VociWord>();
        int amountWords;
        int wordsDone = 0;
        int thisWord;

        public Form1()
        {
            InitializeComponent();
            label2.Text = "";
        }

        public void NewWord()
        {
            Random rd = new Random();

            do
            {
                thisWord = rd.Next(0, amountWords);
            }
            while (words[thisWord].Known);

            label1.Text = words[thisWord].NonTranslated;

            return;
        }

        public void CheckProgress()
        {
            label3.Text = $"Sie haben {wordsDone} von {amountWords} Wörtern bereits gelernt. ({(wordsDone * 100) / amountWords}%)";
            return;
        }

        public void CheckAnswer()
        {
            if (button1.Text == "OK")
            {
                button1.Text = "Next";
                button4.Enabled = true;
                if (textBox1.Text == words[thisWord].Translated)
                {
                    label2.Text = "Nice";
                    words[thisWord].Known = true;
                    wordsDone++;
                    CheckProgress();
                }
                else
                {
                    label2.Text = words[thisWord].Translated;
                }
            }
            else
            {
                NewWord();
                textBox1.Text = "";
                button4.Enabled = false;
                button1.Text = "OK";
                label2.Text = "";
            }
        }

        public void LoadWords()
        {
            if (File.Exists(path))
            {
                try
                {
                    string allText = File.ReadAllText(path);
                    string[] allLines = allText.Split("\n");
                    amountWords = allLines.Length;
                    wordsDone = 0;
                    words = new List<VociWord>();

                    for (int i = 0; i < allLines.Length; i++)
                    {
                        string[] singleLine = allLines[i].Split(";");
                        words.Add(new VociWord(singleLine[1], singleLine[0]));
                    }

                    CheckProgress();
                    NewWord();
                }
                catch (Exception)
                {
                    MessageBox.Show("The file could not been read correctly.\nPlease check the syntax and filetype.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("File does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckAnswer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePathTemp = "";
            if (textBox2.Text.StartsWith('"') && textBox2.Text.EndsWith('"'))
            {
                for (int i = 1; i < (textBox2.Text.Length - 1); i++)
                {
                    filePathTemp += textBox2.Text[i];
                }
            }
            else { filePathTemp = textBox2.Text; }

            path = @$"{filePathTemp}";

            LoadWords();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckAnswer();
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            WordChanger wordChanger = new WordChanger(words[thisWord].NonTranslated, words[thisWord].Translated, thisWord, path);
            wordChanger.Show();
            string[] result = await wordChanger.GetNewWord();
            words[thisWord].NonTranslated = result[0];
            words[thisWord].Translated = result[1];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            words[thisWord].Known = true;
            wordsDone++;
            CheckProgress();
            CheckAnswer();
        }
    }
}