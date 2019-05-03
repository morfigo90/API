using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class SCP343Event : IEventHandler
    {
        private EventManager plugin;
        public SCP343Event(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "343";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)" + plugin.GetTranslation("event_ini") + ":343 Breakout", false);
            EventManager.ToDSC.Initate(admin, "343", forced);
        }
    }
}
