using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class SettingsViewModel
    {
        public delegate void Notify();

        public event Notify NotifyChange;
        public event Notify NotifyFinish;


        public string PrevIp { get; set; }
        public string PrevPort { get; set; }

        public string Ip { get; set; }
        public string Port { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string SearchAlgo { get; set; }
        private SettingsModel settingsModel;

        public SettingsViewModel()
        {
            settingsModel = new SettingsModel();
            Ip = PrevIp = ConfigurationManager.AppSettings["Ip"];
            Port = PrevPort = ConfigurationManager.AppSettings["Port"];
            Width = ConfigurationManager.AppSettings["Width"];
            Height = ConfigurationManager.AppSettings["Height"];
            SearchAlgo = ConfigurationManager.AppSettings["Search Algo"];
        }

        public void ClickedOk()
        {
            if (Changed())
            {
                SetVals();
            }
            else
            {
                NotifyFinish?.Invoke();
            }   
        }

        private bool Changed()
        {
            string portNum = ConfigurationManager.AppSettings["PortNum"];
            string ip = ConfigurationManager.AppSettings["Ip"];
            string width = ConfigurationManager.AppSettings["Width"];
            string algoS = ConfigurationManager.AppSettings["SearchAlgo"];
            string Height = ConfigurationManager.AppSettings["Height"];

            return ((!ip.Equals(Ip)) || (!portNum.Equals(Port)) || (!Width.Equals(width))
                    || (!SearchAlgo.Equals(algoS)) || (!Height.Equals(Height)));
        }

        private void SetVals()
        {
            
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings["Port"].Value = Port;
            configuration.AppSettings.Settings["Ip"].Value = Ip;
            configuration.AppSettings.Settings["Width"].Value = Width;
            configuration.AppSettings.Settings["Height"].Value = Height;
            configuration.AppSettings.Settings["SearchAlgo"].Value = SearchAlgo;

            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");

            NotifyChange?.Invoke();
        }

        /*public void UpdatedBack()
        {
            NotifyChange?.Invoke();
        }*/

        
    }
}