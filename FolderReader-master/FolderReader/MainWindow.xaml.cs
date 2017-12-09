using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;

namespace FolderReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Elements = new List<string>();
        }


        #region Properties

        public string CurrentPath { get; set; }

        public List<string> Elements { get; set; }

        #endregion

        #region Events

        private void cmdSelectFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VistaFolderBrowserDialog tmpSelectFolderDialog = new VistaFolderBrowserDialog();
                if (tmpSelectFolderDialog.ShowDialog().GetValueOrDefault())
                {
                    this.CurrentPath = tmpSelectFolderDialog.SelectedPath;

                    this.txbSeletedFolder.Text = this.CurrentPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0}::{1}", MethodBase.GetCurrentMethod().Name, ex.Message), "Error!");
            }
        }

        private void cmdGetContent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(this.CurrentPath) && Directory.Exists(this.CurrentPath))
                {
                    foreach (string dir in Directory.GetDirectories(this.CurrentPath))
                    {
                        GetFolderContent(dir, 0, true);
                        this.Elements.Add(" ");
                    }

                    //this.txbContent.Text = String.Join(System.Environment.NewLine, this.Elements);
                    File.WriteAllLines("output.txt", this.Elements);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0}::{1}", MethodBase.GetCurrentMethod().Name, ex.Message), "Error!");
            }
        }

        #endregion
        #region Methods

        private void GetFolderContent(string path, int lvl, bool isRoot = false)
        {
            try
            {
                string elementName = String.Empty;

                if (isRoot)
                    this.Elements.Add(path);
                else
                    this.Elements.Add(String.Format("{0} {1}", new StringBuilder().Insert(0, "->", lvl).ToString(), path));

                foreach (string dir in Directory.GetDirectories(path))
                {

                    int newLvl = lvl + 1; ;
                    GetFolderContent(dir, newLvl, false);
                }

                // loop through files
                //foreach (string file in Directory.GetFiles(path).Where(_f => (new List<string> { "mp3", "ogg", "wav", "flac"}).Any(_e => _f.EndsWith(_e, StringComparison.InvariantCultureIgnoreCase))))
                //    this.Elements.Add(String.Format("{0} {1}", new StringBuilder().Insert(0, "->", lvl + 1).ToString(), file));
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
