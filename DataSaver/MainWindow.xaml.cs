using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace DataSaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void File_Click1(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    UrnTextBlock.Text = fbd.SelectedPath;
                }
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            bool startCompressor = true;

            if(TimeDays.Text == "" && TimeHours.Text == "" && TimeMinutes.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Please enter a valid time", "Menu Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                startCompressor = false;
            }
            if(UrnTextBlock.Text == "" || !Directory.Exists(UrnTextBlock.Text))
            {
                System.Windows.Forms.MessageBox.Show("Please enter a valid location", "Menu Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                startCompressor = false;
            }

            if(startCompressor)
            {
                string monitoredLocation = UrnTextBlock.Text;
                double compressionRate = Selector.Value;

                int totalTimeSeconds = 0;
                int days = 0;
                int hours = 0;
                int minutes = 0;

                if(TimeDays.Text != "")
                {
                    days = Convert.ToInt32(TimeDays.Text);
                }
                if(TimeHours.Text != "")
                {
                    hours = Convert.ToInt32(TimeHours.Text);

                }
                if(TimeMinutes.Text != "")
                {
                    minutes = Convert.ToInt32(TimeMinutes.Text);
                }

                totalTimeSeconds += days * 86400;
                totalTimeSeconds += hours * 3600;
                totalTimeSeconds += minutes * 60;

                DataSaverProgram program = new DataSaverProgram();
                program.StartCompression(monitoredLocation, compressionRate, totalTimeSeconds);
            }
        }
    }
}
