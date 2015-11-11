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
            string path = dlg.SelectedPath;
            textBox1.Text = path; 

            int file1 = 0;

            FileInfo[] files = null; // створюється порожній масив файлів
            DirectoryInfo[] subDirs = null;
            DirectoryInfo root = new DirectoryInfo(path);
            files = root.GetFiles("*.*"); // отримуються файли із вказаної користувачем папки
            subDirs = root.GetDirectories("*.*");

            foreach(var dirInfo in subDirs)         
            {
                foreach(FileInfo file in files)
                {         
                    file1++;
                    textBlock1.Text = file1.ToString();
                }      
            }
            
        }

        }
}
