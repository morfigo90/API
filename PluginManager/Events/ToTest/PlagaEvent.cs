using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class PlagaEvent : IEventHandler
    {
        private EventManager plugin;
        public PlagaEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "Plaga";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)Uruchomiono event:Plaga", false);
            EventManager.ToDSC.Initate(admin, "Plaga", forced);
        }
    }
}
