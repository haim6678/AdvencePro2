using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.View
{
    /// <summary>
    /// an interface that define what 
    /// someone that want to handle a client
    /// should have.
    /// </summary>
    interface IClientHandler
    {
        void HandleClient(Client client);
    }
}
