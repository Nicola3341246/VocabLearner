using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizletRipOff
{
    public partial class WordChanger : Form
    {
        public string _translated;
        public string _nonTranslated;
        bool _canGo = true;
        int _position;
        string _filePath;

        public WordChanger(string nonTrans, string trans, int position, string filePath)
        {
            InitializeComponent();
            label1.Text = $"Please Enter the Words\nOriginal:\t{trans}\nTranslated:\t{nonTrans}";
            _nonTranslated = nonTrans;
            _translated = trans;
            _position = position;
            _filePath = filePath;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you shure to chnage?\nThe Word will also be changed in your file.", "Change", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string[] wordChanged = new string[2];
                string fileContent = File.ReadAllText(_filePath);
                string[] fileContentLines = fileContent.Split("\n");

                wordChanged[0] = textBox1.Text;
                wordChanged[1] = textBox2.Text;

                fileContentLines[_position] = $"{wordChanged[0]};{wordChanged[1]}";
                fileContent = "";
                for (int i = 0; i < fileContentLines.Length; i++)
                {
                    if (i == fileContentLines.Length - 1)
                    {
                        fileContent += fileContentLines[i];
                    }
                    else
                    {
                        fileContent += $"{fileContentLines[i]}\n";
                    }
                }

                File.WriteAllText(_filePath, fileContent);
                _canGo = false;
                this.Close();
            }
        }

        public async Task<string[]> GetNewWord()
        {
            string[] returnWords = new string[2];
            while (_canGo)
            {

            }
            returnWords[0] = _nonTranslated;
            returnWords[1] = _translated;

            return returnWords;
        }
    }
}
