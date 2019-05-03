using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class DarkGameEvent : IEventHandler
    {
        private EventManager plugin;
        public DarkGameEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "Blackout";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)" + plugin.GetTranslation("event_ini") + ":Ciemną grę.", false);
            EventManager.ToDSC.Initate(admin, "Blackout", forced);
        }
    }
}
