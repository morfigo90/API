using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class Fight173Event : IEventHandler
    {
        private EventManager plugin;
        public Fight173Event(EventManager plugin,Smod2.API.Player admin,bool forced)
        {
            EventManager.ActiveEvent = "Fight173";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, EventManager.EMRed + plugin.GetTranslation("event_ini") + ":Walka z 173", false);
            EventManager.ToDSC.Initate(admin, "Fight173", forced);
        }
    }
}
