using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class Breakout372Event : IEventHandler
    {
        private EventManager plugin;
        public Breakout372Event(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            if (!plugin.ConfigManager.Config.GetBoolValue("sm_enable_ghostmode", false))
            {
                plugin.Info("(EM)Unable to run SCP 372 Breakout. GhostMode disabled");
                EventManager.ToDSC.Warn("Unable to run SCP 372 Breakout. GhostMode disabled");
            }
            else
            {
                EventManager.ActiveEvent = "372";
                this.plugin = plugin;
                plugin.Server.Map.Broadcast(5, "(EventManager)" + plugin.GetTranslation("event_ini") + ":Wyłom 372", false);
                EventManager.ToDSC.Initate(admin, "372", forced);
            }
        }
    }
}
