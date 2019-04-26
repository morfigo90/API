using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class PiniataEvent : IEventHandler
    {
        private EventManager plugin;
        public PiniataEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "Piniata";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)Uruchomiono event:Piniata", false);
            EventManager.ToDSC.Initate(admin, "Piniata", forced);
        }
    }
}
