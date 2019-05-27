using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class GlobalBreachEvent : IEventHandler
    {
        private EventManager plugin;
        public GlobalBreachEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "GBreach";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, EventManager.EMRed + plugin.GetTranslation("event_ini") + ": Global Breach", false);
            EventManager.ToDSC.Initate(admin, "GBreach", forced);
        }
    }
}
