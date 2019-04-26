﻿using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;

namespace EventManager
{
    class SCP1499Event : IEventHandler
    {
        private EventManager plugin;
        public SCP1499Event(EventManager plugin, Smod2.API.Player admin, bool forced)
        {
            EventManager.ActiveEvent = "1499";
            this.plugin = plugin;
            plugin.Server.Map.Broadcast(5, "(EventManager)Uruchomiono event: SCP 1499", false);
            EventManager.ToDSC.Initate(admin, "1499", forced);
        }
    }
}
