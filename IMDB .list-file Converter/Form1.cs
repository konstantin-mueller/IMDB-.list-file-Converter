using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMDB.list_file_Converter
{
    public partial class Form1 : Form {
        private bool listFileSelected = false,
            folderSelected = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void browseListFileButton_Click(object sender, EventArgs e) {
            DialogResult result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK) {
                listFileSelected = true;
                browseListFileLabel.Text = openFileDialog1.FileName;

                string[] lines = File.ReadAllLines(openFileDialog1.FileName);


                startButton.Enabled = listFileSelected && folderSelected;
            } else {
                listFileSelected = false;
                startButton.Enabled = false;
            }
        }

        private void browseFolderButton_Click(object sender, EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if(result == DialogResult.OK) {
                folderSelected = true;
                browseFolderLabel.Text = folderBrowserDialog1.SelectedPath;

                startButton.Enabled = listFileSelected && folderSelected;
            } else {
                folderSelected = false;
                startButton.Enabled = false;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {

        }
    }
}
