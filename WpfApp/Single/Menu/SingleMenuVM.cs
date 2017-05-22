using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Single.Menu
{
    public class SingleMenuVM
    {
        private SingleMenuModel model;
        public SingleMenuVM(SingleMenuModel model)
        {
            this.model = model;
        }

        public string VM_MazeName
        {
            get { return model.MazeName; }
            set { model.MazeName = value;}
        }

        public uint VM_MazeRows
        {
            get { return model.MazeRows; }
            set { model.MazeRows = value; }
        }

        public uint VM_MazeColumns
        {
            get { return model.MazeColumns; }
            set { model.MazeColumns = value; }
        }

        public void SetCommand()
        {
            model.SetCommand();
        }

        public string GetCommand()
        {
            return model.CommandToSend;
        }
    }
}
