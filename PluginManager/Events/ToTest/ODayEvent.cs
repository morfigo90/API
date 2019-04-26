using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class ODayEvent : IEventHandler
    {
        private EventManager plugin;
        public ODayEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "ODay";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)Uruchomiono event:Przeciwny dzień", false);
            EventManager.ToDSC.Initate(admin, "ODay", forced);
        }
    }
}
