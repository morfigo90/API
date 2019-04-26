using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class DBBREvent : IEventHandler
    {
        private EventManager plugin;
        public DBBREvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "DBBR";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)Uruchomiono event:D-Boi Battle Royale", false);
            EventManager.ToDSC.Initate(admin, "DBBR", forced);
        }
    }
}
