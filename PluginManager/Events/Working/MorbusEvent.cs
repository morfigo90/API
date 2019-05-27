using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class MorbusEvent : IEventHandler
    {
        private EventManager plugin;
        public MorbusEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "Morbus";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, EventManager.EMRed + plugin.GetTranslation("event_ini") + ":Morbus", false);
            EventManager.ToDSC.Initate(admin, "Morbus", forced);
        }
    }
}
