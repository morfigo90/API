using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class SearchEvent : IEventHandler
    {
        private  EventManager plugin;
        public SearchEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "Search";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, EventManager.EMRed + plugin.GetTranslation("event_ini") + ":Poszukiwania", false);
            EventManager.ToDSC.Initate(admin, "Search", forced);
        }
    }
}
