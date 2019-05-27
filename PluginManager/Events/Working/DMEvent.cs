using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class DMEvent : IEventHandler
    {
        private EventManager plugin;
        public DMEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "DM";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, EventManager.EMRed + plugin.GetTranslation("event_ini") + ":DeatchMatch", false);
            EventManager.ToDSC.Initate(admin, "DM", forced);
        }
    }
}
