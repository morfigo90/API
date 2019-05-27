using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class VIPEvent : IEventHandler
    {
        private EventManager plugin;
        public VIPEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "VIP";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, EventManager.EMRed + plugin.GetTranslation("event_ini") + ":V.I.P.(WIP)", false);
            EventManager.ToDSC.Initate(admin, "VIP", forced);
        }
    }
}
