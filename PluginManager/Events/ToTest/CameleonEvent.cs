using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class CameleonEvent : IEventHandler
    {
        private EventManager plugin;
        public CameleonEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "Cameleon";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)Uruchomiono event:SCP - Cameleon", false);
            EventManager.ToDSC.Initate(admin, "Cameleon", forced);
        }
    }
}
