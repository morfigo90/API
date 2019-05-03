using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class ChowanyEvent : IEventHandler
    {
        private EventManager plugin;
        public ChowanyEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "Chowany";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)" + plugin.GetTranslation("event_ini") + ":Chowany", false);
            EventManager.ToDSC.Initate(admin, "Chowany", forced);
        }
    }
}
