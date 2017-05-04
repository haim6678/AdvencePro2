using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Multi
{
    public class PreMultiViewModel
    {
        public delegate void StartGame(string s);


        public event StartGame NotifyStart;

        public ObservableCollection<string> List { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Name { get; set; }
        public PreMultiModel model;

        public PreMultiViewModel()
        {
            model = new PreMultiModel();
            model.NotifyList += GetList;

            //todo handle reading exception
            model.GetList();
        }

        private void GetList()
        {
            List = this.model.list;
            model.NotifyList -= GetList;
        }

        public void JoinClick()
        {
            NotifyStart?.Invoke(Name);
        }

        public void Start()
        {
            //todo check if he input the fields or it's empty!
            NotifyStart?.Invoke(Name + " " + Width + " " + Height);
        }
    }
}