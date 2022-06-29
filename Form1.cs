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

namespace GetFileName
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSelectDirectory_Click(object sender, EventArgs e)
        {
            //ProgressBar1.Maximum = 0;

            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.ShowDialog(Parent);
            string selectesDir = fd.SelectedPath;

            lblSelectedPath.Text = "";
            lblSelectedPath.Text = selectesDir;

            //Thư mục cha
            DirectoryInfo di = new DirectoryInfo(selectesDir);
            if (di.Exists)
            {
                string Root = di.Name;

                List<string> fileInDir = new List<string>();
                fileInDir = FindingFileInDir(selectesDir);

                if (fileInDir.Count() != 0 || fileInDir != null)
                {
                    ProgressBar1.Value = 0;
                    lblStatus.Text = "";
                    lblStatus.Text = $"Có tổng cộng {fileInDir.Count()} file trong thư mục này";
                    AddFilesToTreeView(Root, fileInDir, treeView_Directory);
                    treeView_Directory.ExpandAll();
                }
                else
                {
                    ProgressBar1.Value = 0;
                    lblStatus.Text = "";
                    lblStatus.Text = "Thư mục rỗng";
                }
            }


        }

        private void AddFilesToTreeView(string root, List<string> fileInDir, TreeView treeView_Directory)
        {
            TreeNode _root = new TreeNode(root);
            treeView_Directory.Nodes.Add(root);

            foreach (string item in fileInDir)
            {
                FileInfo fi = new FileInfo(item);
                

                TreeNode itm = new TreeNode(item);
                _root.Nodes.Add(itm);
            }
        }

        private List<string> FindingFileInDir(string selectesDir)
        {
            if (string.IsNullOrEmpty(selectesDir)) return null;
            List<string> fileInDir = new List<string>();

            //ProgressBar1.Value = 0;
            foreach (string item in Directory.GetFiles(selectesDir))
            {
                fileInDir.Add(item);
                //ProgressBar1.Value++;
            }
            return fileInDir;
        }
    }
}
