using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class BDeathEvent : IEventHandler
    {
        private EventManager plugin;
        public BDeathEvent(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "BDeath";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, EventManager.EMRed + plugin.GetTranslation("event_ini") + ":Czarna Śmierć", false);
            EventManager.ToDSC.Initate(admin, "BDeath", forced);
        }
    }
}
