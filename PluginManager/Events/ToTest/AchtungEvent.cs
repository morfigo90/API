using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class AchtungEvent : IEventHandler
    {
        private EventManager plugin;
        public AchtungEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "Achtung";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)"+plugin.GetTranslation("event_ini")+":Achtung", false);
            EventManager.ToDSC.Initate(admin, "Achtung", forced);
        }
    }
}
