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
        public DBBREvent(EventManager plugint, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "DBBR";
            this.plugin = plugint;
            plugin.Server.Map.Broadcast(10, EventManager.EMRed + plugin.GetTranslation("event_ini")+ ":D-Boi Battle Royale. " + (EventManager.TranslationsEnabled ? plugin.GetTranslation("event_ld") : "Uwaga! Może wystąpić chwilowy lag!"), false);
            EventManager.ToDSC.Initate(admin, "DBBR", forced);
        }
    }
}
