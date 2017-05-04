using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
            Ip = PrevIp = settingsModel.GetIp();
            Port = PrevPort = settingsModel.GetPort();
            Width = settingsModel.GetWidth();
            Height = settingsModel.GetHeight();
            SearchAlgo = settingsModel.GetSearchAlgo();
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
            return settingsModel.CheckChanges(Ip, Port, Width, Height, SearchAlgo);
        }

        private void SetVals()
        {
            settingsModel.UpdateData += UpdatedBack;
            settingsModel.SetApp(Port, Ip, Width, Height, SearchAlgo);
        }

        public void UpdatedBack()
        {
            NotifyChange?.Invoke();
        }

        
    }
}