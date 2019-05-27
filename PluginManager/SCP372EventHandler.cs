using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System;
using System.Collections.Generic;

namespace EventManager
{
	class SCP372EventHandler : IEventHandlerUpdate,IEventHandlerRoundStart,IEventHandlerRoundRestart,IEventHandlerPlayerTriggerTesla,IEventHandlerGeneratorInsertTablet,IEventHandlerPlayerHurt,IEventHandlerThrowGrenade,IEventHandlerPlayerDropItem,IEventHandlerMedkitUse,IEventHandlerReload,IEventHandlerSCP914ChangeKnob,IEventHandlerShoot,IEventHandlerPlayerPickupItemLate/*,IEventHandlerSetRole*/,IEventHandlerSetRoleMaxHP,IEventHandlerCheckRoundEnd
    {
		private EventManager plugin;
        DateTime T1;
        bool TB1;

        public SCP372EventHandler(EventManager plugin)
		{
			this.plugin = plugin;
		}

        public void OnRoundStart(RoundStartEvent ev)
        {
            if (EventManager.ActiveEvent == "372")
            {
                if (plugin.Server.NumPlayers <= 2 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)" + (EventManager.TranslationsEnabled ? plugin.GetTranslation("event_nep") : "Niewystarczająca ilość graczy by uruchomić event."), false);
                    return;
                }
                Spawn372(100,true,null);
            }
        }

        public void Spawn372(int hp,bool random, Player player)
        {
            plugin.Server.GetPlayers(Role.SCP_106).ForEach(playert => {
                playert.ChangeRole(Role.SCP_939_53);
            });
            EventManager.RoundLocked = true;
            if(random)
            {
                int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                EventManager.SCP372 = plugin.Server.GetPlayers()[rand];
            } else
            {
                EventManager.SCP372 = player;
            }

            EventManager.SCP372.ChangeRole(Role.TUTORIAL);
            EventManager.SCP372.SetHealth(hp);
            EventManager.SCP372.SetRank("silver", "SCP 372", EventManager.SCP372.GetRankName());
            EventManager.SCP372.PersonalBroadcast(10, (EventManager.TranslationsEnabled ? plugin.GetTranslation("event_373_scp") : "Jesteś SCP 372. Jesteś niewidzialny dopóki nie strzelasz,przeładowywujesze,leczysz się,zmieniasz ustawienie 914,podnosisz item"), false);
            plugin.Server.Map.GetDoors().ForEach(door =>
            {
                if (door.Name == "372")
                {
                    door.Open = true;
                    EventManager.SCP372.Teleport(new Vector(door.Position.x, door.Position.y + 2, door.Position.z));
                }
            });
            EventManager.SCP372.SetGhostMode(true, true, false);
        }

        public void OnUpdate(UpdateEvent ev)
        {
            if (TB1)
            {
                if (System.DateTime.Now.ToString() == T1.ToString())
                {
                    TB1 = false;
                    EventManager.SCP372.SetGhostMode(true, true, false);
                }
            }      
        }

        public void OnRoundRestart(RoundRestartEvent ev)
        {
            EventManager.RoundLocked = false;
            EventManager.ActiveEvent = "";
            EventManager.DisableRespawns = false;
            EventManager.Blackout_type = EventManager.BlackoutType.NONE;
        }

        public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
        {
            if(EventManager.SCP372 != null)
            {
                if(ev.Player.GetGhostMode())
                {
                    ev.Triggerable = false;
                }
            }
        }

        public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
        {
            if (EventManager.SCP372 != null)
            {
                if (ev.Player.SteamId == EventManager.SCP372.SteamId)
                {
                    ev.Allow = false;
                }
            }
        }

        public void OnPlayerHurt(PlayerHurtEvent ev)
        {
            if (EventManager.SCP372 != null)
            {
                if (ev.Player.SteamId == EventManager.SCP372.SteamId)
                {
                    if (ev.Attacker.TeamRole.Team == Smod2.API.Team.SCP) ev.Damage = 0;
                    if (ev.Player.GetHealth() - ev.Damage <= 0)
                    {
                        ev.Player.ChangeRole(Role.SPECTATOR);
                        plugin.Server.Map.AnnounceScpKill("3 7 2", ev.Attacker);
                        EventManager.SCP372 = null;
                        if (EventManager.ActiveEvent == "372")
                        {
                            EventManager.RoundLocked = false;
                            EventManager.ActiveEvent = "";
                            ev.Player.SetRank();
                        }
                    }
                }
                else if (ev.Attacker.SteamId == EventManager.SCP372.SteamId && ev.Player.TeamRole.Team == Smod2.API.Team.SCP) ev.Damage = 0;
            }
        }

        public void OnThrowGrenade(PlayerThrowGrenadeEvent ev)
        {
            if(EventManager.SCP372 != null)
            {
                if(ev.Player.SteamId == EventManager.SCP372.SteamId)
                {
                    ev.Player.SetGhostMode(false);
                    T1 = DateTime.Now.AddSeconds(5);
                    TB1 = true;
                }
            }
        }

        public void OnPlayerDropItem(PlayerDropItemEvent ev)
        {
            if (EventManager.SCP372 != null)
            {
                if (ev.Player.SteamId == EventManager.SCP372.SteamId)
                {
                    ev.Player.SetGhostMode(false);
                    T1 = DateTime.Now.AddSeconds(5);
                    TB1 = true;
                }
            }
        }

        public void OnMedkitUse(PlayerMedkitUseEvent ev)
        {
            if (EventManager.SCP372 != null)
            {
                if (ev.Player.SteamId == EventManager.SCP372.SteamId)
                {
                    ev.Player.SetGhostMode(false);
                    T1 = DateTime.Now.AddSeconds(5);
                    TB1 = true;
                }
            }
        }

        public void OnShoot(PlayerShootEvent ev)
        {
            if (EventManager.SCP372 != null)
            {
                if (ev.Player.SteamId == EventManager.SCP372.SteamId)
                {
                    ev.Player.SetGhostMode(false);
                    T1 = DateTime.Now.AddSeconds(5);
                    TB1 = true;
                }
            }
        }

        public void OnReload(PlayerReloadEvent ev)
        {
            if (EventManager.SCP372 != null)
            {
                if (ev.Player.SteamId == EventManager.SCP372.SteamId)
                {
                    ev.Player.SetGhostMode(false);
                    T1 = DateTime.Now.AddSeconds(5);
                    TB1 = true;
                }
            }
        }

        public void OnSCP914ChangeKnob(PlayerSCP914ChangeKnobEvent ev)
        {
            if (EventManager.SCP372 != null)
            {
                if (ev.Player.SteamId == EventManager.SCP372.SteamId)
                {
                    ev.Player.SetGhostMode(false);
                    T1 = DateTime.Now.AddSeconds(5);
                    TB1 = true;
                }
            }
        }

        public void OnPlayerPickupItemLate(PlayerPickupItemLateEvent ev)
        {
            if (EventManager.SCP372 != null)
            {
                if (ev.Player.SteamId == EventManager.SCP372.SteamId)
                {
                    ev.Player.SetGhostMode(false);
                    T1 = DateTime.Now.AddSeconds(5);
                    TB1 = true;
                }
            }
        }

        /*public void OnSetRole(PlayerSetRoleEvent ev)
        {
            if(EventManager.SCP372 != null)
            {
                EventManager.visions.Clear();
                Player temp = null;
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if(player.SteamId == EventManager.SCP372.SteamId)
                    {
                        temp = player;
                    }
                });
                plugin.Server.GetPlayers().ForEach(player => {

                    if(player.TeamRole.Team != Smod2.API.Team.SPECTATOR && player.TeamRole.Team != Smod2.API.Team.NONE && player.TeamRole.Team != Smod2.API.Team.SCP && player.TeamRole.Team != Smod2.API.Team.TUTORIAL)
                    {
                        EventManager.visions.Add(new EventManager.Vision() { player = player,invisible = new List<Player>() { temp } });
                    }
                });
            }
        }*/

        public void OnSetRoleMaxHP(SetRoleMaxHPEvent ev)
        {
            if(EventManager.SCP372 != null)
            {
                if(ev.Role == Role.TUTORIAL)
                {
                    ev.MaxHP = 3000;
                }
            }
        }

        public void OnCheckRoundEnd(CheckRoundEndEvent ev)
        {
            if (EventManager.SCP372 != null)
            {
                var onlyscp = true;
                plugin.Server.GetPlayers().ForEach(player => {
                    if(player.TeamRole.Team != Smod2.API.Team.SCP && EventManager.SCP372.SteamId != player.SteamId && player.TeamRole.Team != Smod2.API.Team.CHAOS_INSURGENCY)
                    {
                        onlyscp = false;
                    }
                });
                if(onlyscp)
                {
                    if(EventManager.ActiveEvent == "372")
                    {
                        EventManager.RoundLocked = false;
                        EventManager.ActiveEvent = "";
                    }
                }
            }
        }
    }
}


