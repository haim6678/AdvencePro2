using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Multi.Game
{
    class GameNotStartedException : Exception
    {
        public GameNotStartedException(string reason)
            : base(reason)
        { }
    }
}
