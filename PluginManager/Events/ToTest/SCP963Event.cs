using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class SCP963Event : IEventHandler
    {
        private EventManager plugin;
        public SCP963Event(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "963";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, EventManager.EMRed + plugin.GetTranslation("event_ini") + ":SCP 963 breakout", false);
            EventManager.ToDSC.Initate(admin, "963", forced);
        }
    }
}
