using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp.Multi.Waitnig
{
    /// <summary>
    /// Interaction logic for Waiting.xaml
    /// </summary>
    public partial class Waiting : Window
    {
        public delegate void Notify(string s, Window win);
        public event Notify FinishWaiting;

        public WaitViewModel ViewModel;

        public Waiting()
        {
            InitializeComponent();
           // ViewModel = new WaitViewModel();
          
        }

        
        /*public void finish(string s)
        {
            FinishWaiting?.Invoke(s, this);
        }*/
    }
}