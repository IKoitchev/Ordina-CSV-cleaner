using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSVcleaner
{
    public partial class Form1 : Form
    {
        Cleaner cleaner;
        string fullPath;
        string nameOnly;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void BtnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            openFileDialog.Multiselect = false;

            fullPath = openFileDialog.FileName;
            nameOnly = openFileDialog.SafeFileName;

            cleaner = new Cleaner(fullPath, nameOnly);

            this.lblLoadedFile.Text = nameOnly;

        }

        private void BtnCleanFile_Click(object sender, EventArgs e)
        {
            lblLoading.Text = "Loading ... ";
            cleaner.CleanFile();
            lblLoading.Text = "";
        }
    }
}
