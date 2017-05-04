using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    class SettingsModel
    {
        public delegate void Updated();

        public event Updated UpdateData;


        public SettingsModel()
        {
        }

        public string GetPort()
        {
            return ConfigurationManager.AppSettings["PortNum"];
        }

        public string GetIp()
        {
            return ConfigurationManager.AppSettings["Ip"];
        }

        public string GetSearchAlgo()
        {
            return ConfigurationManager.AppSettings["SearchAlgo"];
        }

        public string GetWidth()
        {
            return ConfigurationManager.AppSettings["Width"];
        }

        public string GetHeight()
        {
            return ConfigurationManager.AppSettings["Height"];
        }

        public void SetApp(string Port, string Ip, string Width, string Height, string SearchAlgo)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["Port"].Value = Port;
            configuration.AppSettings.Settings["Ip"].Value = Ip;
            configuration.AppSettings.Settings["Width"].Value = Width;
            configuration.AppSettings.Settings["Height"].Value = Height;
            configuration.AppSettings.Settings["SearchAlgo"].Value = SearchAlgo;

            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");

            UpdateData?.Invoke();
        }

        public bool CheckChanges(string ip, string port, string wid, string heigt, string algo)
        {
            string portNum = ConfigurationManager.AppSettings["PortNum"];
            string Ip = ConfigurationManager.AppSettings["Ip"];
            string width = ConfigurationManager.AppSettings["Width"];
            string algoS = ConfigurationManager.AppSettings["SearchAlgo"];
            string Height = ConfigurationManager.AppSettings["Height"];

            return ((!ip.Equals(Ip)) || (!portNum.Equals(port)) || (!wid.Equals(width))
                    || (!algo.Equals(algoS)) || (!heigt.Equals(Height)));
        }
    }
}