using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class WarHeadRunEvent : IEventHandler
    {
        private EventManager plugin;
        public WarHeadRunEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "WarHeadRun";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)" + plugin.GetTranslation("event_ini") + ":Warhead Run", false);
            EventManager.ToDSC.Initate(admin, "WarHeadRun", forced);
        }
    }
}
