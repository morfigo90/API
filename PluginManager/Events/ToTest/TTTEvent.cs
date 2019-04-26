using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class TTTEvent : IEventHandler
    {
        private EventManager plugin;
        public TTTEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "TSL";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)Uruchomiono event:Trouble in Secret Labolatory", false);
            EventManager.ToDSC.Initate(admin, "TSL", forced);
        }
    }
}
