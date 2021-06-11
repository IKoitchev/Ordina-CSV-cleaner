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

            if (string.IsNullOrEmpty(fullPath))
                MessageBox.Show("No file selected");

            else
            {
                cleaner = new Cleaner(fullPath, nameOnly);
                this.lblLoadedFile.Text = nameOnly;
                this.btnCleanFile.Enabled = true;
            }
        }

        private void BtnCleanFile_Click(object sender, EventArgs e)
        {
            
            lblLoading.Text = "Processing ... ";
            
            cleaner.CleanFile();
            lblLoading.Text = "Cleaning Process Finished. You can now select a new file.";
            this.btnCleanFile.Enabled = false;

        }

       
    }
}
