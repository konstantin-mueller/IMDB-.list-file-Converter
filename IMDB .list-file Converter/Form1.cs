using System;
using System.IO;
using System.Windows.Forms;

namespace IMDB.list_file_Converter
{
    public partial class Form1 : Form {
        private bool listFileSelected, folderSelected, databaseNameEntered;
        private string[] lines;

        public Form1()
        {
            InitializeComponent();
        }

        private void browseListFileButton_Click(object sender, EventArgs e) {
            openFileDialog1.Filter = @"list Files (*.list)|*.list";
            DialogResult result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK) {
                Cursor.Current = Cursors.WaitCursor;
                listFileSelected = true;
                browseListFileLabel.Text = openFileDialog1.FileName;

                lines = File.ReadAllLines(openFileDialog1.FileName);

                ToggleStartButton();
                Cursor.Current = Cursors.Default;
            } else if(browseListFileLabel.Text == string.Empty) {
                listFileSelected = false;
                startButton.Enabled = false;
            }
        }

        private void browseFolderButton_Click(object sender, EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if(result == DialogResult.OK) {
                folderSelected = true;
                browseFolderLabel.Text = folderBrowserDialog1.SelectedPath;

                ToggleStartButton();
            } else if(browseFolderLabel.Text == string.Empty) {
                folderSelected = false;
                startButton.Enabled = false;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void databaseNameTextbox_TextChanged(object sender, EventArgs e) {
            databaseNameEntered = databaseNameTextbox.Text != string.Empty;
            ToggleStartButton();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Program.WriteSqlFile(lines, folderBrowserDialog1.SelectedPath);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.GetType().Name + ": " + exception.Message);
            }
            Cursor.Current = Cursors.Default;
        }


        private void ToggleStartButton() {
            startButton.Enabled = listFileSelected && folderSelected && databaseNameEntered;
        }
    }
}
