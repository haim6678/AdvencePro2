using SharedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.View
{
    /// <summary>
    /// an interface that define what 
    /// someone that want to be in touch with client
    /// should have.
    /// </summary>
    interface IClientNotifier
    {
        void NotifyClient(IClient client, Notification notif);
    }
}
