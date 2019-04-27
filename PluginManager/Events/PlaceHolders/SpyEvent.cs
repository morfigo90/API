using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class SpyEvent : IEventHandler
    {
        private EventManager plugin;
        public SpyEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            plugin.Server.Map.Broadcast(5, "(EventManager)Zablokowano uruchamianie eventy:Szpiedzy. To tylko placeholder", false);
            EventManager.ToDSC.Info("Blocked event initation. It's placeholder.");
            return;
            EventManager.ActiveEvent = "Spy";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)Uruchomiono event:Szpiedzy", false);
            EventManager.ToDSC.Initate(admin, "Spy", forced);
        }
    }
}
