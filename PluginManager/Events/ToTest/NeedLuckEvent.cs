using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class NeedLuckEvent : IEventHandler
    {
        private EventManager plugin;
        public NeedLuckEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "NeedLuck";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, EventManager.EMRed + plugin.GetTranslation("event_ini") + ":All we need is luck", false);
            EventManager.ToDSC.Initate(admin, "NeedLuck", forced);
        }
    }
}
