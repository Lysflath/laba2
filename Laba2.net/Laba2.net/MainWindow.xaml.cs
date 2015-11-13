using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace Laba2.net
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.RootFolder = Environment.SpecialFolder.MyComputer; // стартовий шлях
            dlg.ShowDialog();
            string path = System.IO.Path.GetFullPath(dlg.SelectedPath);
            textBox1.Text = path;

            List<string> files = new List<string>(); 

            DirectoryInfo direct = new DirectoryInfo(path);
            GetAllFiles(path);

        }

        void GetAllFiles(string searchPath)
        {
            DirectoryInfo direct = new DirectoryInfo(searchPath);

            foreach (var file in direct.GetFileSystemInfos())
            {              
                string subdir = file.FullName;
                string result = MD5Hash(subdir);
                textBox2.Text += result + "\n"; 
                if (Directory.Exists(subdir))
                    GetAllFiles(subdir);
            }      
        
        }

        private string MD5Hash(string fileForHashPath)
        {
            if (File.Exists(fileForHashPath))
            {
                using (FileStream fs = File.OpenRead(fileForHashPath))
                {
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] filebytes = new byte [1024];
                    fs.Read(filebytes,0,filebytes.Length);
                    byte[] Sum = md5.ComputeHash(filebytes);
                    string result = BitConverter.ToString(Sum).Replace("-", String.Empty);
                    return result;
                }
            }
            else
                return fileForHashPath;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
