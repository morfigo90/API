using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class SCP689Event : IEventHandler
    {
        private EventManager plugin;
        public SCP689Event(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "689";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)Uruchomiono event:SCP 689 Breakout", false);
            EventManager.ToDSC.Initate(admin, "689", forced);
        }
    }
}
