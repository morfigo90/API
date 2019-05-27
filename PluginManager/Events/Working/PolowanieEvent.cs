using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class PolowanieEvent : IEventHandler
    {
        private EventManager plugin;
        public PolowanieEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "Hunt";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, EventManager.EMRed + plugin.GetTranslation("event_ini") + ":Polowanie", false);
            EventManager.ToDSC.Initate(admin, "Hunt", forced);
        }
    }
}
