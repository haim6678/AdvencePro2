using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Multi.Game
{
    /// <summary>
    /// in charge of the no connection case
    /// </summary>
    /// <seealso cref="System.Exception" />
    class GameNotStartedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameNotStartedException"/> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        public GameNotStartedException(string reason)
            : base(reason)
        { }
    }
}
