using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace X86OrX64Finder
{
    public class PlatformDetails : INotifyPropertyChanged
    {
        private string platform;

        public string Platform
        {
            get { return platform; }
            set
            {
                platform = value;
                RaiseProperChanged();
            }
        }

        private string fullFilePath;

        public string FullFilePath
        {
            get { return fullFilePath; }
            set
            {
                fullFilePath = value;
                RaiseProperChanged();
            }
        }
        


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private string UpdateResult(string fileName)
        {
            ProcessStartInfo ProcessInfo;
            string result = string.Empty;
            Process Process  =null ;
            try
            {
                string path = @"""C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\bin\dumpbin.exe""";
                ProcessInfo = new ProcessStartInfo("cmd.exe", "/K " + path + " /headers " + fileName);
                ProcessInfo.CreateNoWindow = true;
                ProcessInfo.UseShellExecute = false;
                ProcessInfo.RedirectStandardOutput = true;
                ProcessInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                Process = Process.Start(ProcessInfo);
                //System.IO.StreamReader myOutput = Process.StandardOutput;
                //string output1 = myOutput.ReadToEnd();
                string line;
                List<string> results = new List<string>();
                while ((line = Process.StandardOutput.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    line = line.Trim();
                    if (line.Contains("is an MS-DOS executable; use EXEHDR to dump it") )
                    {
                        result = line;
                        break;
                    }
                        
                    if (line.Contains("machine"))
                    {
                        string arc = line.Substring(line.Length - 4, 3);
                        if (arc.Equals("x86"))
                        {
                            CorflagsCheck(fileName,ref arc);
                        }

                        result = arc;
                        
                        
                        break;
                    }
                }
            }
            catch { }
            finally
                {
                
                Process.Kill();
                Process.Close();
            }
            return result;
        }


        private void CorflagsCheck(string fileName,ref string  resArc)
        {
           
            Process Process = null;
            try
            {
                ProcessStartInfo ProcessInfo;
                string path = @"""C:\Program Files (x86)\Microsoft SDKs\Windows\v8.1A\bin\NETFX 4.5.1 Tools\x64\CorFlags.exe""";
                ProcessInfo = new ProcessStartInfo("cmd.exe", "/K " + path + " " + fileName);
                ProcessInfo.CreateNoWindow = true;
                ProcessInfo.UseShellExecute = false;
                ProcessInfo.RedirectStandardOutput = true;
                ProcessInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                Process = Process.Start(ProcessInfo);
                string line;
                List<string> results = new List<string>();
                while ((line = Process.StandardOutput.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    line = line.Trim();
                    if (line.Contains("The specified file does not have a valid NT file header") ||
                        line.Contains("The specified file does not have a valid managed header"))
                        break;
                    if (line.Contains("32BITREQ"))
                    {
                        string arc = line.Substring(line.Length - 1, 1);
                        if (arc =="0")
                        {
                            resArc = "Any CPU";

                        }
                   
                        break;
                    }
                }
            }
            catch { }
            finally
            {
                Process.Kill();
                Process.Close();
            }
        }
        
        private void Window_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var detail = new ObservableCollection<PlatformDetails>();
                // Note that you can have more than one file.
                foreach (var file in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    detail.Add(new PlatformDetails()
                    {
                        Platform = UpdateResult(file),
                        FullFilePath = file,
                    });
                }
                dataGrid.ItemsSource = detail;
            }
        }
    }
}
