using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class Run123Event : IEventHandler
    {
        private EventManager plugin;
        public Run123Event(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "Run123";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)" + plugin.GetTranslation("event_ini") + ":1 2 3 Idziemy gdy nie patrzysz ty", false);
            EventManager.ToDSC.Initate(admin, "Run123", forced);
        }
    }
}
