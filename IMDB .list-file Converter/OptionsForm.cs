using System;
using System.Windows.Forms;

namespace IMDB.list_file_Converter
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();

            movieNameTextbox.Text = Properties.Settings.Default.MovieNameColumn;
            movieYearTextbox.Text = Properties.Settings.Default.MovieYearColumn;
            seriesNameTextbox.Text = Properties.Settings.Default.SeriesNameColumn;
            seriesYearTextbox.Text = Properties.Settings.Default.SeriesYearColumn;
            episodeNameTextbox.Text = Properties.Settings.Default.EpisodeNameColumn;
            seasonTextbox.Text = Properties.Settings.Default.SeasonColumn;
            episodeTextbox.Text = Properties.Settings.Default.EpisodeColumn;
            isSeriesTextbox.Text = Properties.Settings.Default.IsSeriesColumn;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if(movieNameTextbox.Text != string.Empty) {
                Properties.Settings.Default.MovieNameColumn = movieNameTextbox.Text;
            }
            if (movieYearTextbox.Text != string.Empty)
            {
                Properties.Settings.Default.MovieYearColumn = movieYearTextbox.Text;
            }
            if (seriesNameTextbox.Text != string.Empty)
            {
                Properties.Settings.Default.SeriesNameColumn = seriesNameTextbox.Text;
            }
            if (seriesYearTextbox.Text != string.Empty)
            {
                Properties.Settings.Default.SeriesYearColumn = seriesYearTextbox.Text;
            }
            if (episodeNameTextbox.Text != string.Empty)
            {
                Properties.Settings.Default.EpisodeNameColumn = episodeNameTextbox.Text;
            }
            if (seasonTextbox.Text != string.Empty)
            {
                Properties.Settings.Default.SeasonColumn = seasonTextbox.Text;
            }
            if (episodeTextbox.Text != string.Empty)
            {
                Properties.Settings.Default.EpisodeColumn = episodeTextbox.Text;
            }
            if (isSeriesTextbox.Text != string.Empty)
            {
                Properties.Settings.Default.IsSeriesColumn = isSeriesTextbox.Text;
            }
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
