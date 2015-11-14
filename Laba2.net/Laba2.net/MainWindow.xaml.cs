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
        List<string> AllHashedFiles1 = new List<string>();
        List<string> AllHashedFiles2 = new List<string>();
        List<string> AllFiles1 = new List<string>();
        List<string> AllFiles2 = new List<string>();
        List<string> SimilarFiles = new List<string>();        

        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.RootFolder = Environment.SpecialFolder.MyComputer; // стартовий шлях
            dlg.ShowDialog();
            string path = System.IO.Path.GetFullPath(dlg.SelectedPath);
            textBox1.Text = path;

            DirectoryInfo direct = new DirectoryInfo(path);
            GetAllFiles(path, AllHashedFiles1, AllFiles1);            
            System.Windows.MessageBox.Show(AllHashedFiles1.Count.ToString() + "files founded" );
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.RootFolder = Environment.SpecialFolder.MyComputer; // стартовий шлях
            dlg.ShowDialog();
            string path = System.IO.Path.GetFullPath(dlg.SelectedPath);
            textBox3.Text = path;

            DirectoryInfo direct = new DirectoryInfo(path);
            GetAllFiles(path, AllHashedFiles2, AllFiles2);
            System.Windows.MessageBox.Show(AllHashedFiles2.Count.ToString() + "files founded");       
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i < AllHashedFiles1.Count; i++)
            {
                for (int j = 1; j < AllHashedFiles2.Count; j++)
                {
                    if (AllHashedFiles1[i] == AllHashedFiles2[j])
                    {
                        SimilarFiles.Add(AllFiles1[i]);
                        SimilarFiles.Add(AllFiles2[j]);
                        AllHashedFiles1.RemoveAt(i);
                        AllHashedFiles2.RemoveAt(j);                    
                    }
            
                }                            
            }

            for(int i = 2; i < SimilarFiles.Count; i++)
            {
                string extention = System.IO.Path.GetExtension(SimilarFiles[i]);                
                if(!System.IO.Path.GetExtension(SimilarFiles[i-1]).Equals(extention))
                {
                    textBox2.Text += extention + "\n";
                }

                textBox2.Text += SimilarFiles[i].ToString() + "\n";
                    

            }
                    
        }

        // головна функція програми
        void GetAllFiles(string searchPath, List<string> hashedFiles, List<string> allfiles)
        {
            DirectoryInfo direct = new DirectoryInfo(searchPath);

            foreach (var file in direct.GetFileSystemInfos())
            {
                    string subdir = file.FullName;
                    string result = MD5Hash(subdir, allfiles);
                    hashedFiles.Add(result);
                if (Directory.Exists(subdir))
                    GetAllFiles(subdir,hashedFiles, allfiles);   
            }      
        
        }

        // функція для хешування файлів
        private string MD5Hash(string fileForHashPath, List<string> files)
        {
            if (File.Exists(fileForHashPath))
            {
                files.Add(fileForHashPath);
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
