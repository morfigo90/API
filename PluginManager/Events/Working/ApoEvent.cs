using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class ApoEvent : IEventHandler
    {
        private EventManager plugin;
        public ApoEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "Apo";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)" + plugin.GetTranslation("event_ini") + ":Apokalipsa Zombie", false);
            EventManager.ToDSC.Initate(admin, "Apo", forced);
        }
    }
}
