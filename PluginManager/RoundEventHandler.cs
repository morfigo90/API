using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System;
using System.Collections.Generic;

namespace EventManager
{
	class RoundEventHandler : IEventHandlerUpdate,IEventHandlerRoundStart,IEventHandlerCheckRoundEnd,IEventHandlerCheckEscape,IEventHandlerRoundRestart,IEventHandlerTeamRespawn,IEventHandlerPocketDimensionEnter,IEventHandlerElevatorUse,IEventHandlerGeneratorFinish,IEventHandlerGeneratorEjectTablet,IEventHandlerPlayerTriggerTesla,IEventHandlerGeneratorAccess,IEventHandlerGeneratorInsertTablet,IEventHandlerPlayerDie,IEventHandlerCallCommand,IEventHandlerPlayerJoin,IEventHandlerWaitingForPlayers,IEventHandlerPlayerHurt,IEventHandlerThrowGrenade,IEventHandlerSpawn,IEventHandlerPlayerDropItem,IEventHandlerMedkitUse,IEventHandlerReload,IEventHandlerSCP914ChangeKnob,IEventHandlerShoot,IEventHandlerPlayerPickupItemLate,IEventHandlerPlayerPickupItem,IEventHandlerDoorAccess,IEventHandlerHandcuffed,IEventHandlerLCZDecontaminate
    {
		private EventManager plugin;
        #region Vars
        string Chowany_Strefa = "";
        int Achtung_Decresser = 0;
        bool BDeath_ReconCompleted = false;
        bool DGame_Generators2 = false;
        string DBBR_TD = "";
        string DBBR_TD2 = "";
        bool DBBR_D = false;
        int DBBR_DL = 0;
        int BDeath_GeneratorsDone = 0;
        int votes = 0;
        int rounds_without_event = 0;
        Player winner;
        List<string> DM_players = new List<string>();
        List<string> DM_PRole = new List<string>();
        List<string> TSL_T = new List<string>();
        List<string> TSL_I = new List<string>();
        List<string> TSL_D = new List<string>();
        int TSL_TKC = 0;
        int TSL_TTK = 0;
        int TSL_ITK = 0;
        int TSL_IKC = 0;
        int TSL_C_DP = 10;
        int TSL_C_TP = 30;
        Player Cam_SCP = null;
        Player Morbus_Mother = null;
        List<string> Morbus_SCP_hidden = new List<string>();
        List<string> Morbus_SCP_939 = new List<string>();
        bool Morbus_Respawn = false;
        bool Pin_pre = false;
        List<ItemType> Pin_Ditems = new List<ItemType>();
        List<ItemType> Pin_Sitems = new List<ItemType>();
        Player SCP372 = null;
        Player SCP343 = null;
        Player SCP689 = null;
        Player SCP689_Last = null;
        int Hunt_scpsc = 0;
        #endregion

        public RoundEventHandler(EventManager plugin)
		{
			this.plugin = plugin;
		}

        public void OnCheckEscape(PlayerCheckEscapeEvent ev)
        {
            
            if (EventManager.ActiveEvent == "WarHeadRun")
            {
                if(ev.Player.TeamRole.Role == Role.CLASSD && winner == null)
                {
                    winner = ev.Player;
                }
                ev.AllowEscape = false;
            }
            else if(EventManager.ActiveEvent == "Search")
            {
                if (ev.Player.TeamRole.Role == Role.CLASSD && winner == null && ev.Player.HasItem(ItemType.MICROHID))
                {
                    winner = ev.Player;
                } else if(ev.Player.TeamRole.Role == Role.CLASSD && winner == null)
                {
                    ev.Player.PersonalBroadcast(10,"Musisz mieć Micro-HID by móc uciec.",false);
                }
                ev.AllowEscape = false;
            }
            else if (EventManager.ActiveEvent == "Apo")
            {
                if (ev.Player.TeamRole.Role == Role.CLASSD && winner == null)
                {
                    winner = ev.Player;
                }
                ev.AllowEscape = false;
            }
            else if(EventManager.ActiveEvent == "Cameleon")
            {
                if(ev.Player.SteamId == Cam_SCP.SteamId)
                {
                    ev.AllowEscape = false;
                }
            }
        }

        public void OnCheckRoundEnd(CheckRoundEndEvent ev)
        {
            if (EventManager.RoundLocked) ev.Status = ROUND_END_STATUS.ON_GOING;
            if(EventManager.ActiveEvent == "Cameleon")
            {
                bool spotted = false;
                bool onlyscp = true;
                plugin.Server.GetPlayers().ForEach(player => {
                    if(player.SteamId == Cam_SCP.SteamId && player.TeamRole.Role != Role.SPECTATOR)
                    {
                        spotted = true;
                    }
                    if (player.TeamRole.Team != Team.SCP && player.SteamId != Cam_SCP.SteamId)
                    {
                        onlyscp = false;
                    }
                });
                if(spotted && !onlyscp)
                {
                    ev.Status = ROUND_END_STATUS.ON_GOING;
                }
            }
        }

        public void OnRoundStart(RoundStartEvent ev)
        {
            plugin.CommandManager.CallCommand(null, "nuke", new string[] { "off" });
            if(plugin.GetConfigBool("AutoEvent"))
            {
                if (rounds_without_event >= plugin.GetConfigInt("AutoEventRoundCount"))
                {
                    int rand = new Random().Next(0, EventManager.Events.Count - 1);
                    switch (EventManager.Events[rand])
                    {
                        case "WINauki":
                            {
                                new WINaukiEvent(plugin, null, false);
                                plugin.Debug("Starting event. WINauki");
                                break;
                            }
                        case "WarHeadRun":
                            {
                                new WarHeadRunEvent(plugin, null, false);
                                plugin.Debug("Starting event. WarHeadRun");
                                break;
                            }
                        case "Chowany":
                            {
                                new ChowanyEvent(plugin, null, false);
                                plugin.Debug("Starting event. Chowany");
                                break;
                            }
                        case "Achtung":
                            {
                                new AchtungEvent(plugin, null, false);
                                plugin.Debug("Starting event. Achtung");
                                break;
                            }
                        case "BDeath":
                            {
                                new BDeathEvent(plugin, null, false);
                                plugin.Debug("Starting event. Black Death");
                                break;
                            }
                        case "VIP":
                            {
                                new VIPEvent(plugin, null, false);
                                plugin.Debug("Starting event. V.I.P");
                                break;
                            }
                        case "Fight173":
                            {
                                new Fight173Event(plugin, null, false);
                                plugin.Debug("Starting event. Fight with 173");
                                break;
                            }
                        case "Blackout":
                            {
                                new DarkGameEvent(plugin, null, false);
                                plugin.Debug("Starting event. Blackout");
                                break;
                            }
                        case "Run123":
                            {
                                new Run123Event(plugin, null, false);
                                plugin.Debug("Starting event. Run 1 2 3");
                                break;
                            }
                        case "Search":
                            {
                                new SearchEvent(plugin, null, false);
                                plugin.Debug("Starting event. Search");
                                break;
                            }
                        case "Apo":
                            {
                                new ApoEvent(plugin, null, false);
                                plugin.Debug("Starting event. Apokalipse");
                                break;
                            }
                        case "DBBR":
                            {
                                new DBBREvent(plugin, null, false);
                                plugin.Debug("Starting event. D-Boi Battle Royale");
                                break;
                            }
                        case "DM":
                            {
                                new DMEvent(plugin, null, false);
                                plugin.Debug("Starting event. Deathmatch");
                                break;
                            }
                        case "ODay":
                            {
                                new ODayEvent(plugin, null, false);
                                plugin.Debug("Starting event. Oposite day");
                                break;
                            }
                        case "Cameleon":
                            {
                                new CameleonEvent(plugin, null, false);
                                plugin.Debug("Starting event. SCP-Cameleon");
                                break;
                            }
                        case "Morbus":
                            {
                                new MorbusEvent(plugin, null, false);
                                plugin.Debug("Starting event. Morbus");
                                break;
                            }
                        case "Spy":
                            {
                                new SpyEvent(plugin, null, false);
                                plugin.Debug("Starting event. Spy");
                                break;
                            }
                        case "Piniata":
                            {
                                new PiniataEvent(plugin, null, false);
                                plugin.Debug("Starting event. Piniata");
                                break;
                            }
                        case "372":
                            {
                                new Breakout372Event(plugin, null, false);
                                plugin.Debug("Starting event. Breakout of SCP 372");
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    rounds_without_event = 0;
                }
                else
                {
                    rounds_without_event++;
                }
            }

            if (EventManager.ActiveEvent != "")
            {
                rounds_without_event = 0;
            }

            votes = 0;
            EventManager.RoundStarted = true;
            winner = null;
            if (EventManager.ActiveEvent == "WINauki")
            {
                if (plugin.Server.NumPlayers <= 3 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                bool is173 = false, is106 = false, is096 = false, is049 = false, is939_53 = false, is939_89 = false, is079 = false;
                int scp1 = 4;
                int scp2 = 6;
                int scp3 = 8;
                int scp4 = 11;
                int scp5 = 14;
                int scp6 = 17;
                int scp7 = 20;
                int scpcount = 0;
                if (plugin.Server.NumPlayers >= scp1) scpcount = 1;
                if (plugin.Server.NumPlayers >= scp2) scpcount = 2;
                if (plugin.Server.NumPlayers >= scp3) scpcount = 3;
                if (plugin.Server.NumPlayers >= scp4) scpcount = 4;
                if (plugin.Server.NumPlayers >= scp5) scpcount = 5;
                if (plugin.Server.NumPlayers >= scp6) scpcount = 6;
                if (plugin.Server.NumPlayers >= scp7) scpcount = 7;
                if (scpcount == 0)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                int asc = 0;
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if (player.TeamRole.Role == Smod2.API.Role.SCP_049) { is049 = true; asc++; }
                    if (player.TeamRole.Role == Smod2.API.Role.SCP_079) { is079 = true; asc++; }
                    if (player.TeamRole.Role == Smod2.API.Role.SCP_096) { is096 = true; asc++; }
                    if (player.TeamRole.Role == Smod2.API.Role.SCP_106) { is106 = true; asc++; }
                    if (player.TeamRole.Role == Smod2.API.Role.SCP_173) { is173 = true; asc++; }
                    if (player.TeamRole.Role == Smod2.API.Role.SCP_939_53) { is939_53 = true; asc++; }
                    if (player.TeamRole.Role == Smod2.API.Role.SCP_939_89) { is939_89 = true; asc++; }
                });
                while (asc < scpcount)
                {
                    int rand = new Random().Next(plugin.Server.NumPlayers - 1);
                    if (plugin.Server.GetPlayers()[rand].TeamRole.Team != Smod2.API.Team.SCP && plugin.Server.GetPlayers()[rand].TeamRole.Team != Smod2.API.Team.NONE)
                    {
                        int rand2 = new Random().Next(7);
                        Smod2.API.Role role = Smod2.API.Role.SCIENTIST;
                        switch (rand2)
                        {
                            case 0:
                                {
                                    if (is173) return;
                                    role = Smod2.API.Role.SCP_173;
                                    asc++;
                                    break;
                                }
                            case 1:
                                {
                                    if (is106) return;
                                    role = Smod2.API.Role.SCP_106;
                                    asc++;
                                    break;
                                }
                            case 2:
                                {
                                    if (is079) return;
                                    role = Smod2.API.Role.SCP_079;
                                    asc++;
                                    break;
                                }
                            case 3:
                                {
                                    if (is049) return;
                                    role = Smod2.API.Role.SCP_049;
                                    asc++;
                                    break;
                                }
                            case 4:
                                {
                                    if (is096) return;
                                    role = Smod2.API.Role.SCP_096;
                                    asc++;
                                    break;
                                }
                            case 5:
                                {
                                    if (is939_53) return;
                                    role = Smod2.API.Role.SCP_939_53;
                                    asc++;
                                    break;
                                }
                            case 6:
                                {
                                    if (is939_89) return;
                                    role = Smod2.API.Role.SCP_939_89;
                                    asc++;
                                    break;
                                }
                            default:
                                {
                                    plugin.Debug("(WINaukiEvent)Unknown role!");
                                    break;
                                }
                        }
                        if (role == Smod2.API.Role.SCIENTIST) return;
                        plugin.Server.GetPlayers()[rand].ChangeRole(role);
                    }
                }
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if (player.TeamRole.Team == Smod2.API.Team.SCP)
                    {
                        player.PersonalBroadcast(10, "Jesteś SCP. Twoje zadanie to nie pozwolić naukowcom uciec.", false);
                    }
                    else
                    {
                        player.ChangeRole(Smod2.API.Role.SCIENTIST);
                        EventManager.T1 = DateTime.Now.AddSeconds(1);
                        EventManager.TB1 = true;
                        EventManager.T1W = "WINauki";
                        player.PersonalBroadcast(10, "Jesteś naukowcem. Twoje zadanie to uciec z placówki.", false);
                    }
                });
            }
            else if (EventManager.ActiveEvent == "WarHeadRun")
            {
                if (plugin.Server.NumPlayers <= 1 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                EventManager.DisableRespawns = true;
                EventManager.RoundLocked = true;
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    player.ChangeRole(Role.CLASSD);
                });
                plugin.Server.Map.Broadcast(10, "Jesteś klasą D. Twoje zadanie to uciec z placówki!", false);
                plugin.Server.Map.AnnounceCustomMessage("nato_a warhead will be initiated in t minus 1 minute");
                EventManager.T1 = DateTime.Now.AddMinutes(1).AddSeconds(8);
                EventManager.TB1 = true;
                EventManager.T1W = "WHRun";
                plugin.Server.Map.GetDoors().ForEach(door =>
                {
                    door.Open = true;
                    if (door.Name != "") door.Locked = true;
                });
            }
            else if (EventManager.ActiveEvent == "Chowany")
            {
                int scp1 = 4;
                int scp2 = 8;
                int scps = 0;
                if (plugin.Server.NumPlayers <= 3)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                if (plugin.Server.NumPlayers >= scp1)
                {
                    scps = 1;
                }
                if (plugin.Server.NumPlayers >= scp2)
                {
                    scps = 2;
                }
                plugin.Server.Map.GetDoors().ForEach(door =>
                {
                    if (door.Name != "") door.Locked = true;
                });
                EventManager.DisableRespawns = true;
                EventManager.RoundLocked = true;

                EventManager.T1 = DateTime.Now.AddSeconds(30);
                EventManager.T1W = "Chowany1";
                EventManager.TB1 = true;

                int rand = new Random().Next(1, 3);
                if (rand == 1)
                {
                    Chowany_Strefa = "EZ";
                }
                else if (rand == 2)
                {
                    Chowany_Strefa = "HCZ";
                }
                else if (rand == 3)
                {
                    Chowany_Strefa = "LCZ";
                }
                else
                {
                    plugin.Server.Map.Broadcast(5, "(EventManager.Chowany)Error:Nieznana strefa!" + rand, false);
                }
                if (Chowany_Strefa == "") { EventManager.ActiveEvent = ""; plugin.Server.Map.Broadcast(5, "(EventManager.Chowany)Error:Nieznana strefa!@" + rand, false); }
                else
                {
                    int scpc = 0;
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.TeamRole.Team == Team.SCP) scpc++;
                    });
                    plugin.Server.Map.GetElevators().ForEach(elevator =>
                    {
                        elevator.Locked = true;
                    });
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.TeamRole.Team == Team.SCP && scpc > scps)
                        {
                            scpc++;
                            player.ChangeRole(Role.CLASSD);
                            player.PersonalBroadcast(10, "Jesteś klasą D. Twoje zadanie to przeżyć.", false);
                            foreach (Room room in plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA))
                            {
                                if (Chowany_Strefa == "LCZ")
                                {
                                    if (room.ZoneType == ZoneType.LCZ && room.RoomType == RoomType.CAFE)
                                    {
                                        if (player.TeamRole.Team == Team.CLASSD) player.Teleport(new Vector(room.Position.x, room.Position.y + 2, room.Position.z));
                                    }

                                }
                                else if (Chowany_Strefa == "HCZ")
                                {
                                    if (room.ZoneType == ZoneType.HCZ && room.RoomType == RoomType.SERVER_ROOM)
                                    {
                                        if (player.TeamRole.Team == Team.CLASSD) player.Teleport(new Vector(room.Position.x, room.Position.y + 2, room.Position.z));
                                    }

                                }
                                else if (Chowany_Strefa == "EZ")
                                {
                                    if (room.ZoneType == ZoneType.ENTRANCE && room.RoomType == RoomType.PC_LARGE)
                                    {
                                        if (player.TeamRole.Team == Team.CLASSD) player.Teleport(new Vector(room.Position.x, room.Position.y + 2, room.Position.z));
                                    }

                                }
                            }
                        }
                        else if (player.TeamRole.Team != Team.SCP && player.TeamRole.Team != Team.NONE)
                        {
                            player.ChangeRole(Role.CLASSD);
                            player.PersonalBroadcast(10, "Jesteś klasą D. Twoje zadanie to przeżyć.", false);
                            foreach (Room room in plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA))
                            {
                                if (Chowany_Strefa == "LCZ")
                                {
                                    if (room.ZoneType == ZoneType.LCZ && room.RoomType == RoomType.CAFE)
                                    {
                                        if (player.TeamRole.Team == Team.CLASSD) player.Teleport(new Vector(room.Position.x, room.Position.y + 2, room.Position.z));
                                    }

                                }
                                else if (Chowany_Strefa == "HCZ")
                                {
                                    if (room.ZoneType == ZoneType.HCZ && room.RoomType == RoomType.SERVER_ROOM)
                                    {
                                        if (player.TeamRole.Team == Team.CLASSD) player.Teleport(new Vector(room.Position.x, room.Position.y + 2, room.Position.z));
                                    }

                                }
                                else if (Chowany_Strefa == "EZ")
                                {
                                    if (room.ZoneType == ZoneType.ENTRANCE && room.RoomType == RoomType.PC_LARGE)
                                    {
                                        if (player.TeamRole.Team == Team.CLASSD) player.Teleport(new Vector(room.Position.x, room.Position.y + 2, room.Position.z));
                                    }

                                }
                            }
                        }
                        if (player.TeamRole.Team == Team.SCP)
                        {
                            player.ChangeRole(Role.SCP_939_53);
                            player.PersonalBroadcast(10, "Za 30 sekund zostaniesz przeniesiony. Twoje zadanie to zabić wszystkie klasy D.", false);
                            player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.NTF_COMMANDER));
                        }
                    });


                }
            }
            else if (EventManager.ActiveEvent == "Achtung")
            {
                if (plugin.Server.NumPlayers <= 1 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                EventManager.DisableRespawns = true;
                EventManager.RoundLocked = true;
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    player.ChangeRole(Role.CLASSD);
                    player.Teleport(plugin.Server.Map.GetSpawnPoints(Role.SCP_106)[0]);
                });
                plugin.Server.Map.Broadcast(10, "Twoje zadanie to przeżyć. Granaty respią się co 30 sekund a każdy następny sekundę mniej.", false);
                EventManager.T1W = "Achtung";
                EventManager.T1 = DateTime.Now.AddSeconds(30);
                EventManager.TB1 = true;
            }
            else if (EventManager.ActiveEvent == "BDeath")
            {
                if (plugin.Server.NumPlayers <= 1 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                EventManager.DisableRespawns = true;
                EventManager.RoundLocked = true;
                BDeath_ReconCompleted = false;
                int rand = new Random().Next(plugin.Server.NumPlayers - 1);
                Player p106 = plugin.Server.GetPlayers()[rand];
                p106.ChangeRole(Role.SCP_106);
                p106.PersonalBroadcast(10, "Jesteś Czarną Śmiercią. Twoje zadanie to zabić klasy D zanim uruchomią wszystkie generatory.", false);
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if (player.GetAuthToken() != p106.GetAuthToken())
                    {
                        player.ChangeRole(Role.CLASSD);
                        player.SetAmmo(AmmoType.DROPPED_5, 0);
                        player.SetAmmo(AmmoType.DROPPED_9, 0);
                        player.SetAmmo(AmmoType.DROPPED_7, 0);
                        foreach (Room room in plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA))
                        {
                            if (room.RoomType == RoomType.NUKE)
                            {
                                player.Teleport(new Vector(room.Position.x, room.Position.y + 2, room.Position.z));
                                player.PersonalBroadcast(10, "Jesteś klasą D. Twoje zadanie to uruchomić wszystkie generatory by zabić Czarną Śmierć.", false);
                            }

                        }

                    }
                });
                plugin.Server.Map.GetDoors().ForEach(door =>
                {
                    if (door.Name != "" && door.Name != "106_BOTTOM" && door.Name != "106_PRIMARY" && door.Name != "106_SECONDARY" && door.Name != "HID_LEFT" && door.Name != "HID_RIGHT")
                    {
                        door.Locked = true;
                    }
                });
                EventManager.BlackOut = true;
                EventManager.T_BO = DateTime.Now.AddSeconds(1);
                int tabletcount = 0;
                int cardCount = 0;
                int FlashCount = 0;
                int FlashGrenadeCount = 0;
                bool c106 = false, c079 = false, Servers = false;
                foreach (Room room in plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA))
                {
                    if (room.RoomType != RoomType.NUKE && room.RoomType != RoomType.ENTRANCE_CHECKPOINT && room.RoomType != RoomType.TESLA_GATE && room.RoomType != RoomType.SERVER_ROOM && room.RoomType != RoomType.SCP_049 && room.RoomType != RoomType.SCP_096 && room.RoomType != RoomType.SCP_106 && room.RoomType != RoomType.SCP_079 && room.RoomType != RoomType.HCZ_ARMORY)
                    {
                        if (cardCount <= 4)
                        {
                            if (room.ZoneType == ZoneType.HCZ)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.MTF_COMMANDER_KEYCARD, room.Position, new Vector(0, 0, 0));
                                cardCount++;
                            }

                        }
                        else
                        if (tabletcount <= 6)
                        {
                            if (room.ZoneType == ZoneType.HCZ)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.WEAPON_MANAGER_TABLET, room.Position, new Vector(0, 0, 0));
                                tabletcount++;
                            }
                        }
                        if (FlashCount <= 14)
                        {
                            if (room.ZoneType == ZoneType.HCZ)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.FLASHLIGHT, room.Position, new Vector(0, 0, 0));
                                FlashCount++;
                            }
                        }
                        if (FlashGrenadeCount <= 14)
                        {
                            if (room.ZoneType == ZoneType.HCZ)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.FLASHBANG, room.Position, new Vector(0, 0, 0));
                                FlashGrenadeCount++;
                            }
                        }
                    }
                    else
                    {
                        if (!c106 && room.RoomType == RoomType.SCP_106)
                        {
                            if (room.ZoneType == ZoneType.HCZ)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.WEAPON_MANAGER_TABLET, room.Position, new Vector(0, 0, 0));
                                tabletcount++;
                            }
                            c106 = true;
                        }
                        else if (!c079 && room.RoomType == RoomType.SCP_079)
                        {
                            rand = new Random().Next(1, 2);
                            if (rand == 1)
                            {
                                if (room.ZoneType == ZoneType.HCZ) plugin.Server.Map.SpawnItem(ItemType.MTF_COMMANDER_KEYCARD, room.Position, new Vector(0, 0, 0));
                            }
                            else if (rand == 2)
                            {
                                if (room.ZoneType == ZoneType.HCZ)
                                {
                                    plugin.Server.Map.SpawnItem(ItemType.WEAPON_MANAGER_TABLET, room.Position, new Vector(0, 0, 0));
                                    tabletcount++;
                                }
                            }
                            c079 = true;
                        }
                        else if (!Servers && room.RoomType == RoomType.SERVER_ROOM)
                        {
                            rand = new Random().Next(1, 2);
                            if (rand == 1)
                            {
                                if (room.ZoneType == ZoneType.HCZ) plugin.Server.Map.SpawnItem(ItemType.MTF_COMMANDER_KEYCARD, room.Position, new Vector(0, 0, 0));
                            }
                            else if (rand == 2)
                            {
                                if (room.ZoneType == ZoneType.HCZ)
                                {
                                    plugin.Server.Map.SpawnItem(ItemType.WEAPON_MANAGER_TABLET, room.Position, new Vector(0, 0, 0));
                                    tabletcount++;
                                }
                            }
                            Servers = true;
                        }
                    }

                }
                if (tabletcount <= 6)
                {
                    foreach (Room room in plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA))
                    {
                        if (room.RoomType != RoomType.NUKE && room.RoomType != RoomType.ENTRANCE_CHECKPOINT && room.RoomType != RoomType.TESLA_GATE && room.RoomType != RoomType.SERVER_ROOM && room.RoomType != RoomType.SCP_049 && room.RoomType != RoomType.SCP_096 && room.RoomType != RoomType.SCP_106 && room.RoomType != RoomType.SCP_079 && room.RoomType != RoomType.HCZ_ARMORY)
                        {
                            if (room.ZoneType == ZoneType.HCZ && tabletcount <= 6)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.WEAPON_MANAGER_TABLET, room.Position, new Vector(0, 0, 0));
                                tabletcount++;
                            }
                        }
                    }
                }
                if (tabletcount <= 6)
                {
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.TeamRole.Team == Team.CLASSD)
                        {
                            player.GiveItem(ItemType.WEAPON_MANAGER_TABLET);
                        }
                    });
                }
                plugin.Server.Map.GetElevators().ForEach(elevator =>
                {
                    if (elevator.Lockable)
                    {
                        elevator.Locked = true;
                    }
                });
            }
            else if (EventManager.ActiveEvent == "VIP")
            {
                if (plugin.Server.NumPlayers <= 9 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                EventManager.RoundLocked = true;
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if (player.TeamRole.Team != Team.SCP)
                    {
                        player.ChangeRole(Role.CHAOS_INSURGENCY);

                    }
                });
                int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                Player vip = plugin.Server.GetPlayers()[rand];
                vip.ChangeRole(Role.SCIENTIST);
                vip.PersonalBroadcast(10, "Jesteś VIPem twoje zadanie to przeżyć i uciec z pomocą MTF.", false);
                vip.SetHealth(600);
                vip.Teleport(plugin.Server.Map.GetSpawnPoints(Role.SCP_173)[0]);
                int commanders = 0;
                int lieutenant = 0;
                while (commanders < 2)
                {
                    rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                    if (plugin.Server.GetPlayers()[rand].TeamRole.Role == Role.CHAOS_INSURGENCY)
                    {
                        Player tmp1 = plugin.Server.GetPlayers()[rand];
                        tmp1.ChangeRole(Role.NTF_COMMANDER);
                        tmp1.GiveItem(ItemType.MICROHID);
                        tmp1.Teleport(plugin.Server.Map.GetSpawnPoints(Role.SCP_173)[0]);
                        tmp1.PersonalBroadcast(10, "Jesteś Dowódcą. Twoje zadanie to eskortować naukowca", false);
                        commanders++;
                    }
                }
                while (lieutenant < 4)
                {
                    rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                    if (plugin.Server.GetPlayers()[rand].TeamRole.Role == Role.CHAOS_INSURGENCY)
                    {
                        Player tmp1 = plugin.Server.GetPlayers()[rand];
                        tmp1.ChangeRole(Role.NTF_LIEUTENANT);
                        tmp1.Teleport(plugin.Server.Map.GetSpawnPoints(Role.SCP_173)[0]);
                        tmp1.PersonalBroadcast(10, "Jesteś Porucznikiem. Twoje zadanie to eskortować naukowca", false);
                        lieutenant++;
                    }
                }
                plugin.Server.GetPlayers().ForEach(player => {
                    if(player.TeamRole.Team == Team.CHAOS_INSURGENCY)
                    {
                        player.PersonalBroadcast(10, "Jesteś Rebelią Chaosu. Twoje zadanie to nie pozwolić zabic naukowca.", false);
                    } else if(player.TeamRole.Team == Team.SCP)
                    {
                        player.PersonalBroadcast(10, "Jesteś podmiotem SCP. Twoje zadanie to nie pozwolić zabic naukowca.", false);
                    }
                });
            }
            else if (EventManager.ActiveEvent == "Fight173")
            {
                if (plugin.Server.NumPlayers <= 2 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                EventManager.DisableRespawns = true;
                EventManager.RoundLocked = true;
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    player.ChangeRole(Role.CLASSD);
                });
                int c173c = 2;
                if (plugin.Server.NumPlayers < 6)
                {
                    c173c = 1;
                }
                int c173 = 0;
                while (c173 < c173c)
                {
                    int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                    Player t1 = plugin.Server.GetPlayers()[rand];
                    if (t1.TeamRole.Role != Role.SCP_173)
                    {
                        t1.ChangeRole(Role.SCP_173);
                        t1.PersonalBroadcast(10, "Jesteś 173. Twoje zadanie to zabić klasy D. Za 30 sekund zostaniesz przeniesiony", false);
                        c173++;
                    }
                }
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if (player.TeamRole.Team != Team.SCP)
                    {
                        player.Teleport(plugin.Server.Map.GetSpawnPoints(Role.SCP_106)[0]);
                        player.PersonalBroadcast(10, "Jesteś klasą D. Twoje zadanie to z przeżyć.", false);
                    }
                });
                EventManager.T1W = "Fight173";
                EventManager.T1 = DateTime.Now.AddSeconds(30);
                EventManager.TB1 = true;
            }
            else if (EventManager.ActiveEvent == "Blackout")
            {
                if (plugin.Server.NumPlayers <= 1 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if (player.TeamRole.Role == Role.SCP_096)
                    {
                        //player.ChangeRole(Role.SCP_939_53);
                    }
                    if (player.TeamRole.Role == Role.SCP_173)
                    {
                        player.ChangeRole(Role.SCP_939_89);
                    }
                    player.GiveItem(ItemType.FLASHLIGHT);
                });
                plugin.Server.Map.Broadcast(10, "Jest to zwykła runda. Prawie.", false);
                EventManager.BlackOut = true;
                EventManager.T_BO = DateTime.Now.AddSeconds(1);
            }
            else if (EventManager.ActiveEvent == "Run123")
            {
                if (plugin.Server.NumPlayers <= 2 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                EventManager.DisableRespawns = true;
                EventManager.RoundLocked = true;
                int c173c = 0;
                int n173c = 2;
                if (plugin.Server.NumPlayers >= 3)
                {
                    n173c = 1;
                }
                if (plugin.Server.NumPlayers >= 7)
                {
                    n173c = 2;
                }
                if (plugin.Server.NumPlayers >= 10)
                {
                    n173c = 3;
                }
                if (plugin.Server.NumPlayers >= 15)
                {
                    n173c = 4;
                }
                if (plugin.Server.NumPlayers >= 19)
                {
                    n173c = 5;
                }
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    player.ChangeRole(Role.CLASSD);
                });
                while (c173c < n173c)
                {
                    int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                    Player choosen = plugin.Server.GetPlayers()[rand];
                    choosen.ChangeRole(Role.SCP_173);
                    choosen.PersonalBroadcast(10, "Jesteś SCP 173. Twoje zadanie to zabić klasy D zanim uciekną", false);
                    c173c++;
                }
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if (player.TeamRole.Role == Role.CLASSD)
                    {
                        player.PersonalBroadcast(10, "Jesteś klasą D . Twoje zadanie to uciec", false);
                    }
                });
            }
            else if (EventManager.ActiveEvent == "Search")
            {
                if (plugin.Server.NumPlayers <= 1 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                EventManager.RoundLocked = true;
                EventManager.DisableRespawns = true;
                plugin.Server.Map.GetElevators().ForEach(elevator =>
                {
                    if (elevator.Lockable) elevator.Locked = true;
                });
                int count = 0;
                foreach (Room room in plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA))
                {
                    if (room.ZoneType == ZoneType.HCZ)
                    {
                        if (room.RoomType != RoomType.CURVE && room.RoomType != RoomType.STRAIGHT && room.RoomType != RoomType.T_INTERSECTION && room.RoomType != RoomType.X_INTERSECTION && room.RoomType != RoomType.HCZ_ARMORY && room.RoomType != RoomType.ENTRANCE_CHECKPOINT && room.RoomType != RoomType.TESLA_GATE && room.RoomType != RoomType.NUKE)
                        {
                            count++;
                        }
                    }
                }
                int rand0 = new Random().Next(0, count - 1);
                count = 0;
                foreach (Room room in plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA))
                {
                    if (room.ZoneType == ZoneType.HCZ)
                    {
                        if (room.RoomType != RoomType.CURVE && room.RoomType != RoomType.STRAIGHT && room.RoomType != RoomType.T_INTERSECTION && room.RoomType != RoomType.X_INTERSECTION && room.RoomType != RoomType.HCZ_ARMORY && room.RoomType != RoomType.ENTRANCE_CHECKPOINT && room.RoomType != RoomType.TESLA_GATE && room.RoomType != RoomType.NUKE)
                        {
                            if (rand0 == count)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.MICROHID, new Vector(room.Position.x, room.Position.y + 2, room.Position.z), new Vector(0, 0, 0));
                            }
                            count++;
                        }
                    }
                }

                for (int i = 0; i < plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA).Length; i++)
                {
                    Room room = plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA)[i];
                    if (room.ZoneType == ZoneType.ENTRANCE || room.ZoneType == ZoneType.HCZ || room.ZoneType == ZoneType.UNDEFINED)
                    {
                        int rand = new Random().Next(1, 10);
                        if (rand == 1)
                        {
                            plugin.Server.Map.SpawnItem(ItemType.E11_STANDARD_RIFLE, new Vector(room.Position.x, room.Position.y + 2, room.Position.z), new Vector(0, 0, 0));
                        }
                        else if (rand == 2)
                        {
                            plugin.Server.Map.SpawnItem(ItemType.USP, new Vector(room.Position.x, room.Position.y + 2, room.Position.z), new Vector(0, 0, 0));
                        }
                        else if (rand == 3)
                        {
                            plugin.Server.Map.SpawnItem(ItemType.LOGICER, new Vector(room.Position.x, room.Position.y + 2, room.Position.z), new Vector(0, 0, 0));
                        }
                        else if (rand == 4)
                        {
                            plugin.Server.Map.SpawnItem(ItemType.MP4, new Vector(room.Position.x, room.Position.y + 2, room.Position.z), new Vector(0, 0, 0));
                        }
                        else if (rand == 5)
                        {
                            plugin.Server.Map.SpawnItem(ItemType.P90, new Vector(room.Position.x, room.Position.y + 2, room.Position.z), new Vector(0, 0, 0));
                        }
                        else if (rand == 6)
                        {
                            plugin.Server.Map.SpawnItem(ItemType.COM15, new Vector(room.Position.x, room.Position.y + 2, room.Position.z), new Vector(0, 0, 0));
                        }
                        else if (rand == 7)
                        {
                            plugin.Server.Map.SpawnItem(ItemType.MEDKIT, new Vector(room.Position.x, room.Position.y + 2, room.Position.z), new Vector(0, 0, 0));
                        }
                        else
                        {
                            plugin.Server.Map.SpawnItem(ItemType.FLASHLIGHT, new Vector(room.Position.x, room.Position.y + 2, room.Position.z), new Vector(0, 0, 0));
                        }
                    }
                }
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    player.ChangeRole(Role.CLASSD);
                    player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.NTF_COMMANDER));
                });
                plugin.Server.Map.Broadcast(10, "Jesteś klasą D. Twoje zadanie to znaleść Micro-HID i uciec z nim. Kto pierwszy ten lepszy. Możecie się zabijać.", false);
                plugin.Server.Map.GetDoors().ForEach(door =>
                {
                    if (door.Name == "GATE_A" || door.Name == "GATE_B" || door.Name == "CHECKPOINT_ENT" || door.Name == "096" || door.Name == "106_BOTTOM" || door.Name == "106_PRIMARY" || door.Name == "106_SECONDARY" || door.Name == "NUKE_ARMORY" || door.Name == "049_ARMORY" || door.Name == "079_FIRST" || door.Name == "079_SECOND" || door.Name == "HCZ_ARMORY")
                    {
                        door.Open = true;
                        door.Locked = true;
                    }
                    else if (door.Name == "HID")
                    {
                        door.Locked = true;
                    }
                });
                EventManager.BlackOut = true;
                EventManager.T_BO = DateTime.Now.AddSeconds(1);
            }
            else if (EventManager.ActiveEvent == "Apo")
            {
                if (plugin.Server.NumPlayers <= 1 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                EventManager.RoundLocked = true;
                EventManager.DisableRespawns = true;
                plugin.Server.Map.GetDoors().ForEach(door =>
                {
                    if (door.Name == "914" || door.Name == "096" || door.Name == "049_ARMORY" || door.Name == "CHECKPOINT_LCZ_A" || door.Name == "012" || door.Name == "LCZ_ARMORY")
                    {
                        door.Open = true;
                        door.Locked = true;
                    }
                });
                bool first = true;
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if (player.TeamRole.Team != Team.CLASSD)
                    {
                        if (first)
                        {
                            player.ChangeRole(Role.CLASSD);
                            player.GiveItem(ItemType.USP);
                            player.SetAmmo(AmmoType.DROPPED_9, 100);
                            player.PersonalBroadcast(10, "Jesteś klasą D. Twoje zadanie uciec. Jeśli umrzesz zostaniesz zombie", false);
                            first = false;
                        }
                        else
                        {
                            player.ChangeRole(Role.SCP_049_2);
                            player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_173));
                            player.PersonalBroadcast(10, "Jesteś zombie. Twoje zadanie to zabić klasy D i nie pozwolić im uciec.", false);
                        }

                    }
                    else
                    {
                        player.ChangeRole(Role.CLASSD);
                        player.GiveItem(ItemType.USP);
                        player.SetAmmo(AmmoType.DROPPED_9, 100);
                        player.PersonalBroadcast(10, "Jesteś klasą D. Twoje zadanie uciec. Jeśli umrzesz zostaniesz zombie", false);
                    }
                });
            }
            else if (EventManager.ActiveEvent == "DBBR")
            {
                if (plugin.Server.NumPlayers <= 1 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                DBBR_D = false;
                DBBR_DL = 0;
                EventManager.RoundLocked = true;
                EventManager.DisableRespawns = true;
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    player.ChangeRole(Role.CLASSD);
                    player.SetAmmo(AmmoType.DROPPED_5, 100);
                    player.SetAmmo(AmmoType.DROPPED_9, 100);
                    player.SetAmmo(AmmoType.DROPPED_7, 100);
                    player.GiveItem(ItemType.MEDKIT);
                });
                plugin.Server.Map.GetDoors().ForEach(door =>
                {
                    if (door.Name == "GATE_A" || door.Name == "GATE_B")
                    {
                        door.Locked = true;
                    }
                    if (door.Name == "CHECKPOINT_LCZ_A" || door.Name == "CHECKPOINT_LCZ_B" || door.Name == "CHECKPOINT_ENT")
                    {
                        door.Locked = true;
                        door.Open = true;
                    }
                });
                EventManager.T1 = DateTime.Now.AddMinutes(1);
                EventManager.T1W = "DBBR1";
                EventManager.TB1 = true;
                int rand = new Random().Next(1, 6);
                if (rand == 1 || rand == 2)
                {
                    plugin.Server.Map.Broadcast(10, "Strefa niższego nadzoru zostanie zablokowana za 4 minuty.", false);
                    DBBR_TD = "LCZ";
                }
                else if (rand == 3 || rand == 4)
                {
                    plugin.Server.Map.Broadcast(10, "Strefa wyższego nadzoru zostanie zablokowana za 4 minuty.(Nie stój blisko checkpointa)", false);
                    DBBR_TD = "HCZ";
                }
                else if (rand == 5 || rand == 6)
                {
                    plugin.Server.Map.Broadcast(10, "Strefa wejściowa zostanie zablokowana za 4 minuty.(Nie stój blisko checkpointa)", false);
                    DBBR_TD = "EZ";
                }
            }
            else if (EventManager.ActiveEvent == "DM")
            {
                if (plugin.Server.NumPlayers <= 3 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                #region old
                /*DM_PRole.Clear();
                DM_players.Clear();
                EventManager.RoundLocked = true;
                EventManager.DisableRespawns = true;
                plugin.Server.Map.GetElevators().ForEach(elevator =>
                {
                    if (elevator.Lockable) elevator.Locked = true;
                    if (elevator.ElevatorType == ElevatorType.GateA || elevator.ElevatorType == ElevatorType.GateB) elevator.Locked = true;
                });
                Vector CISpawn = null;
                for (int i = 0; i < plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.SPEAKER).Length; i++)
                {
                    Room room = plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.SPEAKER)[i];
                    if (room.RoomType == RoomType.GATE_A)
                    {
                        CISpawn = new Vector(room.Position.x, room.Position.y + 2, room.Position.z);
                    }
                }
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if (player.TeamRole.Team == Team.CLASSD || player.TeamRole.Team == Team.SCIENTIST)
                    {
                        player.ChangeRole(Role.NTF_LIEUTENANT);
                        DM_players.Add(player.SteamId);
                        DM_PRole.Add("MTF");
                        player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_096));
                        plugin.Info("MTF:" + player.Name);
                    }
                    else
                    {
                        player.ChangeRole(Role.CHAOS_INSURGENCY);
                        DM_players.Add(player.SteamId);
                        DM_PRole.Add("CI");
                        player.Teleport(CISpawn);
                        plugin.Info("CI:" + player.Name);
                    }
                });*/
                #endregion
                EventManager.DisableRespawns = true;
                EventManager.RoundLocked = true;
                plugin.Server.Map.GetElevators().ForEach(elevator => {
                    elevator.Locked = true;
                });
                plugin.Server.Map.GetDoors().ForEach(door => {
                    if (door.Name == "GATE_A")
                    {
                        door.Open = true;
                    }
                });
                plugin.Server.GetPlayers(new Role[] { Role.CHAOS_INSURGENCY, Role.FACILITY_GUARD, Role.SCP_049, Role.SCP_079, Role.SCP_096, Role.SCP_106, Role.SCP_173, Role.SCP_939_53, Role.SCP_939_89 }).ForEach(player => {
                    player.ChangeRole(Role.NTF_LIEUTENANT);
                    player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_096));
                });
                plugin.Server.GetPlayers(new Role[] { Role.CLASSD, Role.SCIENTIST }).ForEach(player => {
                    player.ChangeRole(Role.CHAOS_INSURGENCY);
                    player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.FACILITY_GUARD));
                });
            }
            else if (EventManager.ActiveEvent == "TSL")
            {
                if (plugin.Server.NumPlayers <= 3 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                EventManager.DisableRespawns = true;
                EventManager.RoundLocked = true;
                plugin.Server.Map.GetDoors().ForEach(door =>
                {
                    if (door.Name == "914" || door.Name == "CHECKPOINT_LCZ_B" || door.Name == "CHECKPOINT_LCZ_A")
                    {
                        door.Locked = true;
                    }
                    if (door.Name == "LCZ_ARMORY" || door.Name == "372" || door.Name == "173_ARMORY")
                    {
                        door.Open = true;
                        door.Locked = true;
                    }
                });
                for (int i = 0; i < plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA).Length; i++)
                {
                    Room room = plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA)[i];
                    if (room.ZoneType == ZoneType.HCZ || room.ZoneType == ZoneType.LCZ)
                    {
                        if (room.RoomType != RoomType.X_INTERSECTION && room.RoomType != RoomType.T_INTERSECTION && room.RoomType != RoomType.CURVE && room.RoomType != RoomType.STRAIGHT)
                        {
                            Vector pos = new Vector(room.Position.x, room.Position.y + 2, room.Position.z);
                            int randI = new Random().Next(1, 7);
                            if (randI == 1)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.COM15, pos, new Vector(0, 0, 0));
                            }
                            else if (randI == 2)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.MEDKIT, pos, new Vector(0, 0, 0));
                            }
                            else if (randI == 3)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.E11_STANDARD_RIFLE, pos, new Vector(0, 0, 0));
                            }
                            else if (randI == 4)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.MEDKIT, pos, new Vector(0, 0, 0));
                            }
                            else if (randI == 5)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.P90, pos, new Vector(0, 0, 0));
                            }
                            else if (randI == 6)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.MP4, pos, new Vector(0, 0, 0));
                            }
                            else if (randI == 7)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.LOGICER, pos, new Vector(0, 0, 0));
                            }
                        }
                    }
                }
                int DC = 0;
                int TC = 0;
                TSL_D.Clear();
                TSL_T.Clear();
                TSL_I.Clear();
                while (DC < plugin.Server.NumPlayers * (TSL_C_DP / 100))
                {
                    int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                    Player choosen = plugin.Server.GetPlayers()[rand];
                    if (!TSL_D.Contains(choosen.SteamId))
                    {
                        choosen.ChangeRole(Role.SCIENTIST);
                        TSL_D.Add(choosen.SteamId);
                        choosen.PersonalBroadcast(10, "Jesteś detektywem. Twoje zadanie to zabić zdrajców.", false);
                        choosen.SetAmmo(AmmoType.DROPPED_9, 200);
                        choosen.SetAmmo(AmmoType.DROPPED_5, 100);
                        choosen.SetAmmo(AmmoType.DROPPED_7, 50);
                        choosen.GiveItem(ItemType.P90);
                        DC++;
                    }
                }
                while (TC < plugin.Server.NumPlayers * (TSL_C_TP / 100))
                {
                    int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                    Player choosen = plugin.Server.GetPlayers()[rand];
                    if (!TSL_T.Contains(choosen.SteamId) && !TSL_D.Contains(choosen.SteamId))
                    {
                        choosen.ChangeRole(Role.CLASSD);
                        choosen.PersonalBroadcast(10, "Jesteś zdrajcą. Twoje zadanie to zabić niewinnych i detektywów.", false);
                        choosen.SetAmmo(AmmoType.DROPPED_9, 100);
                        choosen.SetAmmo(AmmoType.DROPPED_5, 50);
                        choosen.SetAmmo(AmmoType.DROPPED_7, 300);
                        TSL_T.Add(choosen.SteamId);
                        TC++;
                    }
                }
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if (!TSL_T.Contains(player.SteamId) && !TSL_D.Contains(player.SteamId) && !TSL_I.Contains(player.SteamId))
                    {
                        player.ChangeRole(Role.CLASSD);
                        player.PersonalBroadcast(10, "Jesteś niewinny. Twoje zadanie to pomóc detektywowi zabić zdrajców.", false);
                        player.SetAmmo(AmmoType.DROPPED_7, 50);
                        player.SetAmmo(AmmoType.DROPPED_7, 50);
                        player.SetAmmo(AmmoType.DROPPED_7, 50);
                        TSL_I.Add(player.SteamId);
                    }
                });
                EventManager.T1W = "TSLT";
                EventManager.T1 = DateTime.Now.AddMinutes(1);
                EventManager.TB1 = true;
            }
            else if (EventManager.ActiveEvent == "ODay")
            {
                plugin.Server.Map.AnnounceCustomMessage("Alert . Alert . doctor . . . spotted in facility . Full site alert initiated . Code red . Scanning for facility damage");
                plugin.Server.Map.Broadcast(20, "Transkrypcja:Alarm. Alarm. Doktor Bright wykryty w placówce. Alarm całej strefy uruchomiony. Kod czerwony. Skanowanie w poszukiwaniu uszkodzień placówki.", false);
                plugin.Server.Map.GetDoors().ForEach(door =>
                {
                    if (door.Name == "GATE_A" || door.Name == "GATE_B" || door.Name == "079_FIRST")
                    {
                        door.Locked = true;
                        door.Open = true;
                    }
                });
            }
            else if (EventManager.ActiveEvent == "Cameleon")
            {
                if (plugin.Server.NumPlayers <= 2 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                Cam_SCP = plugin.Server.GetPlayers()[rand];
                Cam_SCP.ChangeRole(Role.TUTORIAL);
                plugin.Server.Map.GetDoors().ForEach(door =>
                {
                    if (door.Name == "079_FIRST")
                    {
                        door.Open = true;
                        Cam_SCP.Teleport(new Vector(door.Position.x, door.Position.y + 2, door.Position.z));
                    }
                });
                Cam_SCP.GiveItem(ItemType.O5_LEVEL_KEYCARD);
                Cam_SCP.GiveItem(ItemType.COM15);
                Cam_SCP.GiveItem(ItemType.COIN);
                Cam_SCP.GiveItem(ItemType.JANITOR_KEYCARD);
                Cam_SCP.GiveItem(ItemType.WEAPON_MANAGER_TABLET);
                Cam_SCP.SetHealth(3000);
                Cam_SCP.SetAmmo(AmmoType.DROPPED_5, 150);
                Cam_SCP.SetAmmo(AmmoType.DROPPED_7, 150);
                Cam_SCP.SetAmmo(AmmoType.DROPPED_9, 150);
                Cam_SCP.PersonalBroadcast(10, "Jesteś SCP-Kameleon. Twoje zadanie to zabić wszystkich poza SCP. SCP nie mogą cię skrzywdzić(poza teslą).", false);
                Cam_SCP.PersonalBroadcast(10, "W ekwipunku masz pistolet,kartę O5,monetę,kartę sprzątacza,tablet.", false);
                Cam_SCP.PersonalBroadcast(10, "Po wyrzuceniu monety przybierasz postać klasy D, Karta sprzątacza - Naukowiec,Tablet - Tutorial,Masz 3000 hp. Nie możesz zadawać obrażeń w innej formie niż tutorial.", false);
                Cam_SCP.PersonalBroadcast(10, "Nie możesz ranić SCP. Powodzenia", false);
            }
            else if (EventManager.ActiveEvent == "Morbus")
            {
                if (plugin.Server.NumPlayers <= 2 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                EventManager.BlackOut = true;
                Morbus_Respawn = true;
                EventManager.DisableRespawns = true;
                EventManager.RoundLocked = true;
                Morbus_SCP_939.Clear();
                Morbus_SCP_hidden.Clear();
                plugin.Server.Map.GetDoors().ForEach(door =>
                {
                    if (door.Name == "CHECKPOINT_LCZ_A" || door.Name == "CHECKPOINT_LCZ_B")
                    {
                        door.Open = true;
                        door.Locked = true;
                    }
                    if (door.Name == "CHECKPOINT_ENT")
                    {
                        door.Open = false;
                        door.Locked = true;
                    }
                });
                for (int i = 0; i < plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA).Length; i++)
                {
                    Room room = plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA)[i];
                    if (room.ZoneType == ZoneType.HCZ || room.ZoneType == ZoneType.LCZ)
                    {
                        if (room.RoomType != RoomType.X_INTERSECTION && room.RoomType != RoomType.T_INTERSECTION && room.RoomType != RoomType.CURVE && room.RoomType != RoomType.STRAIGHT)
                        {
                            Vector pos = new Vector(room.Position.x, room.Position.y + 2, room.Position.z);
                            int randI = new Random().Next(1, 8);
                            if (randI == 1)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.COM15, pos, new Vector(0, 0, 0));
                            }
                            else if (randI == 2)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.USP, pos, new Vector(0, 0, 0));
                            }
                            else if (randI == 3)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.E11_STANDARD_RIFLE, pos, new Vector(0, 0, 0));
                            }
                            else if (randI == 4)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.CHAOS_INSURGENCY_DEVICE, pos, new Vector(0, 0, 0));
                            }
                            else if (randI == 5)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.P90, pos, new Vector(0, 0, 0));
                            }
                            else if (randI == 6)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.MP4, pos, new Vector(0, 0, 0));
                            }
                            else if (randI == 7)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.MEDKIT, pos, new Vector(0, 0, 0));
                            }
                            else if (randI == 8)
                            {
                                plugin.Server.Map.SpawnItem(ItemType.LOGICER, pos, new Vector(0, 0, 0));
                            }
                        }
                    }
                }
                int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                Morbus_Mother = plugin.Server.GetPlayers()[rand];
                plugin.Server.GetPlayers()[rand].ChangeRole(Role.CLASSD);
                plugin.Server.GetPlayers()[rand].SetHealth(700);
                plugin.Server.GetPlayers()[rand].PersonalBroadcast(10, "Jesteś SCP 939 'Matka'. Za 2 minuty otrzymasz kubek. Jeśli go wyrzucisz staniesz się scp 939. Aby zamienić się spowrotem wpisz .z w konsoli pod ~. Twoje zadanie to zabic klasy D. Jeśli umrzesz to przegrywasz.", false);
                plugin.Server.GetPlayers()[rand].PersonalBroadcast(10, "Jeśli kogoś zabijesz to staje się on ukrytym 939. Jeśli ukryty 939 umrze staje sie zwykłym 939", false);
                EventManager.T1 = DateTime.Now.AddMinutes(2);
                EventManager.T1W = "Morbus";
                EventManager.TB1 = true;
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if (player.SteamId != Morbus_Mother.SteamId)
                    {
                        player.ChangeRole(Role.CLASSD);
                        player.PersonalBroadcast(10, "Jesteś klasą D. Twoje zadanie to przeżyć. Uważaj na SCP 939 'Matka'.", false);
                    }
                    player.GiveItem(ItemType.FLASHLIGHT);
                });
                EventManager.T_BO = DateTime.Now.AddSeconds(1);
            }
            else if (EventManager.ActiveEvent == "Piniata")
            {
                if (plugin.Server.NumPlayers <= 5 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                EventManager.DisableRespawns = true;
                EventManager.RoundLocked = true;
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if (player.TeamRole.Team == Team.NINETAILFOX || player.TeamRole.Team == Team.SCIENTIST)
                    {
                        player.ChangeRole(Role.SCIENTIST);
                        player.GetInventory().ForEach(item =>
                        {
                            item.Remove();
                        });
                    }
                    else if (player.TeamRole.Team == Team.CHAOS_INSURGENCY || player.TeamRole.Team == Team.CLASSD || player.TeamRole.Team == Team.SCP)
                    {
                        player.ChangeRole(Role.CLASSD);
                    }
                });
                int scps = 0;
                int need_scps = plugin.Server.NumPlayers > 7 ? 6 : plugin.Server.NumPlayers % 2 == 0 ? plugin.Server.NumPlayers - 2 : plugin.Server.NumPlayers - 3;
                while (scps >= need_scps)
                {
                    int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                    Player scp = plugin.Server.GetPlayers()[rand];
                    scp.ChangeRole(Role.SCP_173);
                    scp.SetHealth(1);
                    int item = new Random().Next(0, 30);
                    switch (item)
                    {
                        case 0:
                            {
                                scp.GiveItem(ItemType.JANITOR_KEYCARD);
                                break;
                            }
                        case 1:
                            {
                                scp.GiveItem(ItemType.SCIENTIST_KEYCARD);
                                break;
                            }
                        case 2:
                            {
                                scp.GiveItem(ItemType.MAJOR_SCIENTIST_KEYCARD);
                                break;
                            }
                        case 3:
                            {
                                scp.GiveItem(ItemType.ZONE_MANAGER_KEYCARD);
                                break;
                            }
                        case 4:
                            {
                                scp.GiveItem(ItemType.GUARD_KEYCARD);
                                break;
                            }
                        case 5:
                            {
                                scp.GiveItem(ItemType.SENIOR_GUARD_KEYCARD);
                                break;
                            }
                        case 6:
                            {
                                scp.GiveItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD);
                                break;
                            }
                        case 7:
                            {
                                scp.GiveItem(ItemType.MTF_LIEUTENANT_KEYCARD);
                                break;
                            }
                        case 8:
                            {
                                scp.GiveItem(ItemType.MTF_COMMANDER_KEYCARD);
                                break;
                            }
                        case 9:
                            {
                                scp.GiveItem(ItemType.FACILITY_MANAGER_KEYCARD);
                                break;
                            }
                        case 10:
                            {
                                scp.GiveItem(ItemType.CHAOS_INSURGENCY_DEVICE);
                                break;
                            }
                        case 11:
                            {
                                scp.GiveItem(ItemType.O5_LEVEL_KEYCARD);
                                break;
                            }
                        case 12:
                            {
                                scp.GiveItem(ItemType.RADIO);
                                break;
                            }
                        case 13:
                            {
                                scp.GiveItem(ItemType.COM15);
                                break;
                            }
                        case 14:
                            {
                                scp.GiveItem(ItemType.MEDKIT);
                                break;
                            }
                        case 15:
                            {
                                scp.GiveItem(ItemType.FLASHLIGHT);
                                break;
                            }
                        case 16:
                            {
                                scp.GiveItem(ItemType.MICROHID);
                                break;
                            }
                        case 17:
                            {
                                scp.GiveItem(ItemType.COIN);
                                break;
                            }
                        case 18:
                            {
                                scp.GiveItem(ItemType.CUP);
                                break;
                            }
                        case 19:
                            {
                                scp.GiveItem(ItemType.WEAPON_MANAGER_TABLET);
                                break;
                            }
                        case 20:
                            {
                                scp.GiveItem(ItemType.E11_STANDARD_RIFLE);
                                break;
                            }
                        case 21:
                            {
                                scp.GiveItem(ItemType.P90);
                                break;
                            }
                        case 22:
                            {
                                scp.GiveItem(ItemType.DROPPED_5);
                                break;
                            }
                        case 23:
                            {
                                scp.GiveItem(ItemType.MP4);
                                break;
                            }
                        case 24:
                            {
                                scp.GiveItem(ItemType.LOGICER);
                                break;
                            }
                        case 25:
                            {
                                scp.GiveItem(ItemType.FRAG_GRENADE);
                                break;
                            }
                        case 26:
                            {
                                scp.GiveItem(ItemType.FLASHBANG);
                                break;
                            }
                        case 27:
                            {
                                scp.GiveItem(ItemType.DISARMER);
                                break;
                            }
                        case 28:
                            {
                                scp.GiveItem(ItemType.DROPPED_7);
                                break;
                            }
                        case 29:
                            {
                                scp.GiveItem(ItemType.DROPPED_9);
                                break;
                            }
                        case 30:
                            {
                                scp.GiveItem(ItemType.USP);
                                break;
                            }
                    }
                    scps++;
                }
                bool found = false;
                for (int i = 0; i < 3; i++)
                {
                    while (!found)
                    {
                        int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                        Player founded = plugin.Server.GetPlayers()[rand];
                        if (founded.TeamRole.Team == Team.CLASSD)
                        {
                            founded.SetGodmode(true);
                            founded.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_173));
                            founded.GiveItem(ItemType.LOGICER);
                            found = true;
                        }
                    }
                    while (!found)
                    {
                        int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                        Player founded = plugin.Server.GetPlayers()[rand];
                        if (founded.TeamRole.Team == Team.CLASSD)
                        {
                            founded.SetGodmode(true);
                            founded.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_173));
                            founded.GiveItem(ItemType.LOGICER);
                            found = true;
                        }
                    }
                }

            }
            else if (EventManager.ActiveEvent == "Spy")
            {
                plugin.Server.Map.Broadcast(10,"Ten event jest tylko place holderem!",false);
            }
            else if (EventManager.ActiveEvent == "372")
            {
                if (plugin.Server.NumPlayers <= 2 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                EventManager.RoundLocked = true;
                int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                SCP372 = plugin.Server.GetPlayers()[rand];
                SCP372.ChangeRole(Role.TUTORIAL);
                SCP372.SetHealth(300);
                SCP372.SetRank("silver", "SCP 372", SCP372.GetRankName());
                SCP372.PersonalBroadcast(10, "Jesteś SCP 372", false);
                plugin.Server.Map.GetDoors().ForEach(door =>
                {
                    if (door.Name == "372")
                    {
                        door.Open = true;
                        SCP372.Teleport(new Vector(door.Position.x, door.Position.y + 2, door.Position.z));
                    }
                });
                SCP372.SetGhostMode(true,true,false);

            }
            else if (EventManager.ActiveEvent == "343")
            {
                if (plugin.Server.NumPlayers <= 2 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                SCP343 = plugin.Server.GetPlayers()[rand];
                SCP343.ChangeRole(Role.TUTORIAL);
                SCP343.SetGodmode(true);
                SCP343.SetRank("silver", "SCP 343", SCP343.GetRankName());
                SCP343.PersonalBroadcast(10, "Jesteś SCP 343. Jesteś bogiem. Jesteś nieśmiertelny oraz możesz otwierać wszystkie drzwi. Każda broń którą podniesiesz staje się monetą lub czymś innym.", false);
            }
            else if (EventManager.ActiveEvent == "689")
            {
                if (plugin.Server.NumPlayers <= 2 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                SCP689 = plugin.Server.GetPlayers()[rand];
                SCP689.ChangeRole(Role.SCP_049);
                SCP689.SetHealth(5000);
                SCP689.PersonalBroadcast(10, "Jesteś SCP 689", false);
                SCP689.SetRank("silver", "SCP 689", SCP689.GetRankName());
                EventManager.T1W = "689Attack";
                EventManager.T1 = DateTime.Now.AddSeconds(60);
                EventManager.TB1 = true;
            }
            else if (EventManager.ActiveEvent == "1499")
            {
                plugin.Server.Map.SpawnItem(ItemType.COIN, new Vector(+0, +2, +0), new Vector(0, 0, 0));
                plugin.Server.Map.Broadcast(10, "Ten event to tylko place holder", false);
            }
            else if (EventManager.ActiveEvent == "Hunt")
            {
                if (plugin.Server.NumPlayers <= 2 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                Hunt_scpsc = 0;
                EventManager.DisableRespawns = true;
                EventManager.RoundLocked = true;
                plugin.Server.Map.GetDoors().ForEach(door => {
                    if (door.Name == "CHECKPOINT_EZ")
                    {
                        door.Locked = true;
                    }
                });
                plugin.Server.Map.GetElevators().ForEach(elevator => {
                    if (elevator.Lockable) elevator.Locked = true;
                });
                plugin.Server.Map.Broadcast(10, "Zadaniem SCP jest zabić MTF i odwrotnie. Jeśli SCP zginie jego zabójca staje się SCP. Powodzenia", false);
                plugin.Server.GetPlayers().ForEach(player => {
                    player.ChangeRole(Role.NTF_LIEUTENANT);
                    player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_939_53));
                });
                int rand = new Random().Next(0, plugin.Server.NumPlayers - 1);
                Player scp = plugin.Server.GetPlayers()[rand];
                scp.ChangeRole(Role.SCP_173);
                scp.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_096));
                scp.SetHealth(1500);
            }
            else if (EventManager.ActiveEvent == "Plaga")
            {
                if (plugin.Server.NumPlayers <= 2 && !EventManager.DNPN)
                {
                    EventManager.ActiveEvent = "";
                    plugin.Server.Map.Broadcast(10, "(EventManager)Niewystarczająca ilość graczy by uruchomić event.", false);
                    return;
                }
                EventManager.DisableRespawns = true;
                EventManager.RoundLocked = true;
                plugin.Server.Map.GetDoors().ForEach(door => {
                    if(door.Name == "CHECKPOINT_LCZ_A" || door.Name == "CHECKPOINT_LCZ_B")
                    {
                        door.Locked = true;
                    }
                });
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if(player.TeamRole.Team == Team.SCP)
                    {
                        player.ChangeRole(Role.SCP_939_53);
                        player.SetHealth(800);
                        player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.CLASSD));
                        player.PersonalBroadcast(10, "Jesteś SCP. Twoje zadanie to zabić wszystkich MTF przed dekontaminacją LCZ.", false);
                    }
                    else
                    {
                        player.ChangeRole(Role.NTF_LIEUTENANT);
                        player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCIENTIST));
                        player.PersonalBroadcast(10, "Jesteś MTF. Twoje zadanie to przerzyć do dekontaminacji LCZ. Każdy kto umrze staje się SCP", false);
                    }
                });
            }
        }

        public void OnUpdate(UpdateEvent ev)
        {
            if (EventManager.BlackOut)
            {
                if (System.DateTime.Now.ToString() == EventManager.T_BO.ToString()) {
                    foreach (Room room in plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA))
                    {
                        if (room.ZoneType == ZoneType.HCZ || room.ZoneType == ZoneType.LCZ || room.RoomType == RoomType.NUKE || room.RoomType == RoomType.ENTRANCE_CHECKPOINT) {
                            room.FlickerLights();
                        }
                    }
                    plugin.Debug("1 Blackout cycle");
                    EventManager.T_BO = DateTime.Now.AddSeconds(1);
                }
            }
            if (EventManager.TB1)
            {
                if (System.DateTime.Now.ToString() == EventManager.T1.ToString())
                {
                    EventManager.TB1 = false;
                    switch (EventManager.T1W)
                    {
                        case "WINauki":
                            {
                                plugin.Server.GetPlayers().ForEach(player =>
                                {
                                    if (player.TeamRole.Team == Smod2.API.Team.SCP) return;
                                    player.GiveItem(ItemType.GUARD_KEYCARD);
                                    player.GiveItem(ItemType.MEDKIT);
                                });
                                break;
                            }
                        case "WHRun":
                            {
                                plugin.CommandManager.CallCommand(null, "nuke", new string[] { "on" });
                                plugin.CommandManager.CallCommand(null, "nuke", new string[] { "start" });
                                plugin.CommandManager.CallCommand(null, "nuke", new string[] { "lock" });
                                plugin.Debug("Starting warhead");
                                break;
                            }
                        case "Chowany1":
                            {
                                
                                plugin.Server.GetPlayers().ForEach(player =>
                                {
                                    foreach (Room room in plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA))
                                    {
                                        if (Chowany_Strefa == "LCZ")
                                        {
                                            if (room.ZoneType == ZoneType.LCZ && room.RoomType == RoomType.WC00) {
                                                if (player.TeamRole.Team == Team.SCP) player.Teleport(new Vector(room.Position.x, room.Position.y + 2, room.Position.z));
                                            }
                                            
                                        }
                                        else if (Chowany_Strefa == "HCZ")
                                        {
                                            if (room.ZoneType == ZoneType.HCZ && room.RoomType == RoomType.NUKE)
                                            {
                                                if (player.TeamRole.Team == Team.SCP) player.Teleport(new Vector(room.Position.x,room.Position.y+2,room.Position.z));
                                            }
                                            
                                        }
                                        else if (Chowany_Strefa == "EZ")
                                        {
                                            if (room.ZoneType == ZoneType.ENTRANCE && room.RoomType == RoomType.PC_SMALL) {
                                                if (player.TeamRole.Team == Team.SCP) player.Teleport(new Vector(room.Position.x, room.Position.y + 2, room.Position.z));
                                            }
                                            
                                        }
                                    }
                                });
                                if(Chowany_Strefa == "LCZ")
                                {
                                    EventManager.T1W = "Chowany2";
                                    EventManager.T1 = DateTime.Now.AddMinutes(10);
                                    EventManager.TB1 = true;
                                }
                                break;
                            }
                        case "Chowany2":
                            {
                                plugin.Server.Map.Broadcast(10,"Checkpointy odblokowane. Pierwsza żywa klasa D w HCZ wygrywa!",false);
                                plugin.Server.Map.GetDoors().ForEach(door =>
                                {
                                    if (door.Name == "CHECKPONT_LCZ_A" || door.Name == "CHECKPONT_LCZ_B") door.Locked = false;
                                });
                                break;
                            }
                        case "Achtung":
                            {
                                if(EventManager.ActiveEvent != "")
                                {
                                    if (winner != null) return;
                                    plugin.CommandManager.CallCommand(null, "grenade", new string[] { "all" });
                                    Achtung_Decresser++;
                                    EventManager.T1W = "Achtung";
                                    int tmpi1 = 30 - Achtung_Decresser;
                                    if (tmpi1 <= 0) tmpi1 = 1;
                                    EventManager.T1 = DateTime.Now.AddSeconds(tmpi1);
                                    EventManager.TB1 = true;
                                } 
                                break;
                            }
                        case "Fight173":
                            {
                                plugin.Server.GetPlayers().ForEach(player => {
                                    if(player.TeamRole.Team == Team.SCP)
                                    {
                                        player.Teleport(plugin.Server.Map.GetSpawnPoints(Role.SCP_106)[0]);
                                        player.SetHealth(1);
                                    }
                                });
                                break;
                            }
                        case "DBBR1":
                            {
                                DBBR_DL++;
                                if(DBBR_TD == "LCZ")
                                {
                                    plugin.Server.Map.Broadcast(10, "Strefa niższego nadzoru została zablokowana.", false);
                                    plugin.Server.Map.GetElevators().ForEach(elevator => {
                                        if (elevator.Lockable) elevator.Locked = true;
                                    });
                                } else if (DBBR_TD == "HCZ")
                                {
                                    plugin.Server.Map.Broadcast(10, "Strefa wyższego nadzoru została zablokowana.", false);
  
                                    plugin.Server.Map.GetElevators().ForEach(elevator => {
                                        if (elevator.Lockable) elevator.Locked = true;
                                    });
                                    plugin.Server.Map.GetDoors().ForEach(door => {
                                        if(door.Name == "CHECKPOINT_ENT")
                                        {
                                            door.Open = false;
                                            door.Locked = true;
                                        }
                                    });
                                }
                                else if (DBBR_TD == "EZ")
                                {
                                    plugin.Server.Map.Broadcast(10, "Strefa wejściowa została zablokowana.", false);
                                    plugin.Server.Map.GetDoors().ForEach(door => {
                                        if (door.Name == "CHECKPOINT_ENT")
                                        {
                                            door.Open = false;
                                            door.Locked = true;
                                        }
                                    });
                                }

                                DBBR_D = true;
                                EventManager.T1 = DateTime.Now.AddMinutes(1);
                                EventManager.T1W = "DBBR2";
                                EventManager.TB1 = true;
                                break;
                            }
                        case "DBBR2":
                            {
                                if (DBBR_TD == "LCZ")
                                {
                                    plugin.Server.Map.Broadcast(10, "Strefa niższego nadzoru została odblokowana", false);
                                    plugin.Server.Map.GetElevators().ForEach(elevator => {
                                        if (elevator.Lockable) elevator.Locked = false;
                                    });
                                }
                                else if (DBBR_TD == "HCZ")
                                {
                                    plugin.Server.Map.Broadcast(10, "Strefa wyższego nadzoru została odblokowana", false);
                                    plugin.Server.Map.GetElevators().ForEach(elevator => {
                                        if (elevator.Lockable) elevator.Locked = false;
                                    });
                                    plugin.Server.Map.GetDoors().ForEach(door => {
                                        if (door.Name == "CHECKPOINT_ENT")
                                        {
                                            door.Open = true;
                                            door.Locked = true;
                                        }
                                    });
                                }
                                else if (DBBR_TD == "EZ")
                                {
                                    plugin.Server.Map.Broadcast(10, "Strefa wejściowa została odblokowana", false);
                                    plugin.Server.Map.GetDoors().ForEach(door => {
                                        if (door.Name == "CHECKPOINT_ENT")
                                        {
                                            door.Open = true;
                                            door.Locked = true;
                                        }
                                    });
                                }

                                DBBR_D = false;
                                if(DBBR_DL >= 1)
                                {
                                    EventManager.T1W = "DBBR3";
                                    EventManager.T1 = DateTime.Now.AddMinutes(2);
                                    EventManager.TB1 = true;
                                    int rand = new Random().Next(1, 6);
                                    if (rand == 1 ||rand == 2)
                                    {
                                        DBBR_TD = "LCZ";
                                        rand = new Random().Next(1, 2);
                                        if (rand == 1)
                                        {
                                            plugin.Server.Map.Broadcast(10, "LCZ oraz HCZ zostaną zablokowane za 2 minuty.(Nie stój blisko checkpointa)", false);
                                            DBBR_TD2 = "HCZ";
                                        }
                                        else if (rand == 2)
                                        {
                                            plugin.Server.Map.Broadcast(10, "LCZ oraz EZ zostaną zablokowane za 2 minuty.(Nie stój blisko checkpointa)", false);
                                            DBBR_TD2 = "EZ";
                                        }
                                    }
                                    else if (rand == 3 || rand == 4)
                                    {
                                        DBBR_TD = "HCZ";
                                        rand = new Random().Next(1, 2);
                                        if (rand == 1)
                                        {
                                            plugin.Server.Map.Broadcast(10, "HCZ oraz LCZ zostaną zablokowane za 2 minuty.(Nie stój blisko checkpointa)", false);
                                            DBBR_TD2 = "LCZ";
                                        }
                                        else if (rand == 2)
                                        {
                                            plugin.Server.Map.Broadcast(10, "HCZ oraz EZ zostaną zablokowane za 2 minuty.", false);
                                            DBBR_TD2 = "EZ";
                                        }
                                    }
                                    else if (rand == 5 || rand == 6)
                                    {
                                        DBBR_TD = "EZ";
                                        rand = new Random().Next(1, 2);
                                        if (rand == 1)
                                        {
                                            plugin.Server.Map.Broadcast(10, "EZ oraz LCZ zostaną zablokowane za 2 minuty.(Nie stój blisko checkpointa)", false);
                                            DBBR_TD2 = "LCZ";
                                        }
                                        else if (rand == 2)
                                        {
                                            plugin.Server.Map.Broadcast(10, "EZ oraz HCZ zostaną zablokowane za 2 minuty.", false);
                                            DBBR_TD2 = "HCZ";
                                        }
                                    }
                                    
                                }
                                else
                                {
                                    EventManager.T1W = "DBBR1";
                                    EventManager.T1 = DateTime.Now.AddMinutes(2);
                                    EventManager.TB1 = true;
                                    int rand = new Random().Next(1, 6);
                                    if (rand == 1||rand == 2)
                                    {
                                        plugin.Server.Map.Broadcast(10, "Strefa niższego nadzoru zostanie zablokowana za 2 minuty.", false);
                                        DBBR_TD = "LCZ";
                                    }
                                    else if (rand == 3 ||rand == 4)
                                    {
                                        plugin.Server.Map.Broadcast(10, "Strefa wyższego nadzoru zostanie zablokowana za 2 minuty.(Nie stój blisko checkpointa)", false);
                                        DBBR_TD = "HCZ";
                                    }
                                    else if (rand == 5 ||rand == 6)
                                    {
                                        plugin.Server.Map.Broadcast(10, "Strefa wejściowa zostanie zablokowana za 2 minuty.(Nie stój blisko checkpointa)", false);
                                        DBBR_TD = "EZ";
                                    }
                                }
                                
                                break;
                            }
                        case "DBBR3":
                            {
                                if (DBBR_TD == "LCZ")
                                {
                                    plugin.Server.Map.GetElevators().ForEach(elevator => {
                                        if (elevator.Lockable) elevator.Locked = true;
                                    });
                                }
                                else if (DBBR_TD == "HCZ")
                                {

                                    plugin.Server.Map.GetElevators().ForEach(elevator => {
                                        if (elevator.Lockable) elevator.Locked = true;
                                    });
                                    plugin.Server.Map.GetDoors().ForEach(door => {
                                        if (door.Name == "CHECKPOINT_ENT")
                                        {
                                            door.Open = false;
                                            door.Locked = true;
                                        }
                                    });
                                }
                                else if (DBBR_TD == "EZ")
                                {
                                    plugin.Server.Map.GetDoors().ForEach(door => {
                                        if (door.Name == "CHECKPOINT_ENT")
                                        {
                                            door.Open = false;
                                            door.Locked = true;
                                        }
                                    });
                                }
                                if (DBBR_TD2 == "LCZ")
                                {
                                    plugin.Server.Map.GetElevators().ForEach(elevator => {
                                        if (elevator.Lockable) elevator.Locked = true;
                                    });
                                }
                                else if (DBBR_TD2 == "HCZ")
                                {

                                    plugin.Server.Map.GetElevators().ForEach(elevator => {
                                        if (elevator.Lockable) elevator.Locked = true;
                                    });
                                    plugin.Server.Map.GetDoors().ForEach(door => {
                                        if (door.Name == "CHECKPOINT_ENT")
                                        {
                                            door.Open = false;
                                            door.Locked = true;
                                        }
                                    });
                                }
                                else if (DBBR_TD2 == "EZ")
                                {
                                    plugin.Server.Map.GetDoors().ForEach(door => {
                                        if (door.Name == "CHECKPOINT_ENT")
                                        {
                                            door.Open = false;
                                            door.Locked = true;
                                        }
                                    });
                                }
                                plugin.Server.Map.Broadcast(10, DBBR_TD+" oraz "+DBBR_TD2+" zostały pernamentnie zablokowane.", false);
                                DBBR_D = true;
                                break;
                            }
                        case "TSLT":
                            {
                                plugin.Server.GetPlayers().ForEach(player => {
                                    if(TSL_T.Contains(player.SteamId))
                                    {
                                        player.GiveItem(ItemType.COM15);
                                    }
                                });
                                plugin.Server.Map.Broadcast(10, "Traitorzy otrzymali swoją broń", false);
                                break;
                            }
                        case "Morbus":
                            {
                                Morbus_Mother.GiveItem(ItemType.CUP);
                                break;
                            } 
                        case "MorbusEnd":
                            {
                                plugin.Server.GetPlayers().ForEach(player => {
                                    if(Morbus_SCP_939.Contains(player.SteamId) || Morbus_SCP_hidden.Contains(player.SteamId) || Morbus_Mother.SteamId == player.SteamId)
                                    {
                                        player.ChangeRole(Role.SPECTATOR);
                                    } 
                                });
                                plugin.Server.Map.AnnounceCustomMessage("All SCP 9 3 9 successfully terminated by facility security system initiated by keter class SCPS containment breach");
                                EventManager.RoundLocked = false;
                                break;
                            }
                        case "Morbus_BOFF":
                            {
                                EventManager.T1 = DateTime.Now.AddMinutes(1);
                                EventManager.T1W = "MorbusEnd";
                                EventManager.TB1 = true;
                                EventManager.RoundLocked = false;
                                break;
                            }
                        case "372Hide":
                            {
                                SCP372.SetGhostMode(true, true, false);
                                break;
                            }
                        case "689Attack":
                            {
                                if (SCP689_Last != null)
                                {
                                    plugin.Server.GetPlayers().ForEach(player => {
                                        if (player.SteamId == SCP689_Last.SteamId)
                                        {
                                            SCP689.Teleport(player.GetPosition());
                                            player.Damage(50,DamageType.POCKET);
                                        }
                                    });
                                }
                                if(EventManager.ActiveEvent == "689")
                                {
                                    EventManager.T1W = "689Attack";
                                    EventManager.T1 = DateTime.Now.AddSeconds(5 + SCP689.GetHealth() / 200);
                                    EventManager.TB1 = true;
                                }
                                break;
                            }
                        default:
                            {
                                plugin.Debug("Unknown Timer Work");
                                break;
                            }
                    }
                    
                }
            }
            if (EventManager.TB2)
            {
                if (System.DateTime.Now.ToString() == EventManager.T2.ToString())
                {
                    EventManager.TB2 = false;
                    switch (EventManager.T2W)
                    {
                        case "BDeath2":
                            {
                                plugin.Server.GetPlayers().ForEach(player => {
                                    if (player.TeamRole.Team == Team.SCP)
                                    {
                                        player.Kill(DamageType.CONTAIN);
                                    }
                                });
                                plugin.Server.Map.Broadcast(10,"Klasy D wygrały",false);
                                EventManager.DisableRespawns = false;
                                EventManager.RoundLocked = false;
                                plugin.Server.Round.EndRound();
                                EventManager.ActiveEvent = "";
                                //plugin.Server.Map.FemurBreaker(true);
                                EventManager.BlackOut = false;
                                break;
                            }
                        case "DGame":
                            {
                                EventManager.BlackOut = false;
                                break;
                            }
                        default:
                            {
                                plugin.Debug("Unknown Timer Work2");
                                break;
                            }
                    }

                }
            }
            if (EventManager.RoundStarted == true)
            {
                if (EventManager.ActiveEvent == "WarHeadRun")
                {
                    int alive = 0;
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.TeamRole.Team == Team.CLASSD)
                        {
                            alive++;
                        }
                    });
                    if (alive == 0 && winner == null)
                    {
                        EventManager.RoundLocked = false;
                        EventManager.DisableRespawns = false;
                        EventManager.ActiveEvent = "";
                        plugin.Server.Map.Broadcast(10, "(EventManager)Nikt nie wygrał.", false);
                    }
                }
                if (EventManager.ActiveEvent == "Chowany" && winner == null && Chowany_Strefa == "LCZ")
                {
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.TeamRole.Team != Team.CLASSD) return;
                        if (player.GetPosition().y < 500) winner = player;
                    });
                }
                if (EventManager.ActiveEvent == "Chowany")
                {
                    int alive = 0;
                    Player lastalive = null;
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.TeamRole.Team == Team.CLASSD)
                        {
                            alive++;
                            lastalive = player;
                        }
                    });
                    if (alive == 1)
                    {
                        winner = lastalive;
                    }
                    else if (alive == 0 && winner == null)
                    {
                        EventManager.RoundLocked = false;
                        EventManager.DisableRespawns = false;
                        EventManager.ActiveEvent = "";
                        plugin.Server.Map.Broadcast(10, "(EventManager)Nikt nie wygrał.", false);
                    }
                }
                if (EventManager.ActiveEvent == "Achtung")
                {
                    int alive = 0;
                    Player lastalive = null;
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.TeamRole.Team == Team.CLASSD)
                        {
                            alive++;
                            lastalive = player;
                        }
                    });
                    if (alive == 1)
                    {
                        winner = lastalive;
                    }
                    else if (alive == 0 && winner == null)
                    {
                        EventManager.RoundLocked = false;
                        EventManager.DisableRespawns = false;
                        EventManager.ActiveEvent = "";
                        plugin.Server.Map.Broadcast(10, "(EventManager)Nikt nie wygrał.", false);
                    }
                }
                if (EventManager.ActiveEvent == "BDeath" && !BDeath_ReconCompleted)
                {
                    bool tmp1 = true;
                    foreach (Generator generator in plugin.Server.Map.GetGenerators())
                    {
                        if (generator.Engaged == false) tmp1 = false;

                    }
                    if (tmp1)
                    {
                        EventManager.T2W = "BDeath2";
                        EventManager.T2 = DateTime.Now.AddMinutes(1).AddSeconds(10);
                        EventManager.TB2 = true;
                        BDeath_ReconCompleted = true;
                    }
                    else
                    {
                        EventManager.TB2 = false;
                        BDeath_ReconCompleted = false;
                    }
                }
                if (EventManager.ActiveEvent == "BDeath")
                {
                    Player scp106 = null;
                    plugin.Server.GetPlayers().ForEach(player => {
                        if (player.TeamRole.Role == Role.SCP_106) scp106 = player;
                    });
                    
                    int alive = 0;
                    Player lastalive = null;
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.TeamRole.Team == Team.CLASSD)
                        {
                            alive++;
                        }
                        if (player.TeamRole.Role == Role.SCP_106)
                        {
                            lastalive = player;
                        }
                    });
                    if (alive == 0 && winner == null && lastalive != null)
                    {
                        EventManager.RoundLocked = false;
                        EventManager.DisableRespawns = false;
                        EventManager.ActiveEvent = "";
                        plugin.Server.Map.Broadcast(10, "(EventManager)Wygrywa " + lastalive.Name, false);
                        EventManager.BlackOut = false;
                    }
                    else if (lastalive == null && winner == null)
                    {
                        EventManager.RoundLocked = false;
                        EventManager.DisableRespawns = false;
                        EventManager.ActiveEvent = "";
                        plugin.Server.Map.Broadcast(10, "(EventManager)Klasa D wygrała.", false);
                        EventManager.BlackOut = false;
                    }
                }
                if (EventManager.ActiveEvent == "VIP")
                {
                    Player lastalive = null;
                    bool VIPalive = false;
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.TeamRole.Team == Team.SCIENTIST)
                        {
                            VIPalive = true;
                            lastalive = player;
                        }
                        if (player.TeamRole.Team != Team.SCP && player.TeamRole.Team != Team.CHAOS_INSURGENCY && VIPalive)
                        {
                            EventManager.ActiveEvent = "";
                            EventManager.RoundLocked = false;
                            plugin.Server.Round.EndRound();
                            plugin.Server.Map.Broadcast(10, "(EventManager)MFO wygrało.", false);
                        }
                        else if (player.TeamRole.Team != Team.NINETAILFOX && !VIPalive)
                        {
                            EventManager.ActiveEvent = "";
                            EventManager.RoundLocked = false;
                            plugin.Server.Round.EndRound();
                            plugin.Server.Map.Broadcast(10, "(EventManager)Rebelia Chaosu wygrała.", false);
                        }
                    });
                }
                if (EventManager.ActiveEvent == "Fight173")
                {
                    int alive = 0;
                    Player lastalive = null;
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.TeamRole.Team == Team.CLASSD)
                        {
                            alive++;
                            lastalive = player;
                        }
                    });
                    if (alive == 1)
                    {
                        winner = lastalive;
                    }
                    else if (alive == 0 && winner == null)
                    {
                        EventManager.RoundLocked = false;
                        EventManager.DisableRespawns = false;
                        EventManager.ActiveEvent = "";
                        plugin.Server.Map.Broadcast(10, "(EventManager)Nikt nie wygrał.", false);
                    }
                }
                if (EventManager.ActiveEvent == "Blackout" && !DGame_Generators2)
                {
                    bool tmp1 = true;
                    foreach (Generator generator in plugin.Server.Map.GetGenerators())
                    {
                        if (generator.Engaged == false) tmp1 = false;
                    }
                    if (tmp1)
                    {
                        EventManager.T2W = "DGame";
                        EventManager.T2 = DateTime.Now.AddMinutes(1).AddSeconds(0);
                        EventManager.TB2 = true;
                        DGame_Generators2 = true;
                    }
                    else
                    {
                        EventManager.TB2 = false;
                        DGame_Generators2 = false;
                    }
                }
                if (EventManager.ActiveEvent == "Run123")
                {
                    plugin.Server.GetPlayers().ForEach(player => {
                        if(player.TeamRole.Role == Role.SPECTATOR)
                        {
                            player.ChangeRole(Role.SCP_173);
                        }
                    });
                }
                if (EventManager.ActiveEvent == "Run123")
                {
                    int alive = 0;
                    Player lastalive = null;
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.TeamRole.Team == Team.CLASSD)
                        {
                            alive++;
                            lastalive = player;
                        }
                    });
                    if (alive == 1)
                    {
                        winner = lastalive;
                    }
                    else if (alive == 0 && winner == null)
                    {
                        EventManager.RoundLocked = false;
                        EventManager.DisableRespawns = false;
                        EventManager.ActiveEvent = "";
                        plugin.Server.Map.Broadcast(10, "(EventManager)Nikt nie wygrał.", false);
                    }
                }
                if (EventManager.ActiveEvent == "Search")
                {
                    int alive = 0;
                    Player lastalive = null;
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.TeamRole.Team == Team.CLASSD)
                        {
                            alive++;
                            lastalive = player;
                        }
                    });
                    if (alive == 1)
                    {
                        winner = lastalive;
                    }
                    else if (alive == 0 && winner == null)
                    {
                        EventManager.RoundLocked = false;
                        EventManager.DisableRespawns = false;
                        EventManager.ActiveEvent = "";
                        EventManager.BlackOut = false;
                        plugin.Server.Map.Broadcast(10, "(EventManager)Nikt nie wygrał.", false);
                    }
                }
                if (EventManager.ActiveEvent == "Apo")
                {
                    plugin.Server.GetPlayers().ForEach(player => {
                        if(player.TeamRole.Role == Role.SPECTATOR)
                        {
                            player.ChangeRole(Role.SCP_049_2);
                            player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_173));
                        }
                    });
                }
                if (EventManager.ActiveEvent == "Apo")
                {
                    int alive = 0;
                    Player lastalive = null;
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.TeamRole.Team == Team.CLASSD)
                        {
                            alive++;
                            lastalive = player;
                        }
                    });
                    if (alive == 1)
                    {
                        winner = lastalive;
                    }
                    else if (alive == 0 && winner == null)
                    {
                        EventManager.RoundLocked = false;
                        EventManager.DisableRespawns = false;
                        EventManager.ActiveEvent = "";
                        plugin.Server.Map.Broadcast(10, "(EventManager)Zombie wygrały.", false);
                    }
                }
                if (EventManager.ActiveEvent == "DBBR")
                {
                    int alive = 0;
                    Player lastalive = null;
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.TeamRole.Team == Team.CLASSD)
                        {
                            alive++;
                            lastalive = player;
                        }
                    });
                    if (alive == 1)
                    {
                        winner = lastalive;
                        DBBR_D = false;
                    }
                    else if (alive == 0 && winner == null)
                    {
                        EventManager.RoundLocked = false;
                        EventManager.DisableRespawns = false;
                        EventManager.ActiveEvent = "";
                        EventManager.BlackOut = false;
                        DBBR_D = false;
                        plugin.Server.Map.Broadcast(10, "(EventManager)Nikt nie wygrał.", false);
                    }
                }
                if (EventManager.ActiveEvent == "DM")
                {
                    #region Old
                    /*int CIc = 0;
                    int MTFc = 0;
                    plugin.Server.GetPlayers().ForEach(playerC =>
                    {
                        if (playerC.TeamRole.Team == Team.CHAOS_INSURGENCY)
                        {
                            CIc++;
                        }
                        else if (playerC.TeamRole.Team == Team.NINETAILFOX)
                        {
                            MTFc++;
                        }
                    });
                    if (CIc == 0 && MTFc == 0 && DM_players.Count != 0)
                    {
                        plugin.Server.Map.Broadcast(10, "Nikt nie wygrywa.", false);
                        EventManager.RoundLocked = false;
                        EventManager.DisableRespawns = false;
                        plugin.Round.EndRound();
                        EventManager.ActiveEvent = "";
                    }
                    else if (CIc == 0 && DM_players.Count != 0)
                    {
                        plugin.Server.Map.Broadcast(10, "MTF wygrywa!", false);
                        EventManager.RoundLocked = false;
                        EventManager.DisableRespawns = false;
                        plugin.Round.EndRound();
                        EventManager.ActiveEvent = "";
                    }
                    else if (MTFc == 0 && DM_players.Count != 0)
                    {
                        plugin.Server.Map.Broadcast(10, "CI wygrywa!", false);
                        EventManager.RoundLocked = false;
                        EventManager.DisableRespawns = false;
                        plugin.Round.EndRound();
                        EventManager.ActiveEvent = "";
                    }
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        bool done = false;
                        if (player.TeamRole.Role == Role.SPECTATOR && DM_players.Contains(player.SteamId))
                        {
                            for (int i = 0; i < DM_players.Count; i++)
                            {
                                if (DM_players[i] == player.SteamId)
                                {
                                    done = true;
                                    if (DM_PRole[i] == "CI")
                                    {
                                        player.ChangeRole(Role.NTF_LIEUTENANT);
                                        DM_PRole[i] = "MTF";
                                        player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_096));
                                    }
                                    else
                                    {
                                        player.ChangeRole(Role.CHAOS_INSURGENCY);
                                        DM_PRole[i] = "CI";
                                        for (int j = 0; j < plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA).Length; j++)
                                        {
                                            Room room = plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA)[j];
                                            if (room.RoomType == RoomType.GATE_A)
                                            {
                                                player.Teleport(new Vector(room.Position.x, room.Position.y + 2, room.Position.z));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (!done)
                        {
                            DM_players.Add(player.SteamId);
                            if (CIc >= MTFc)
                            {
                                DM_PRole.Add("CI");
                                player.ChangeRole(Role.CHAOS_INSURGENCY);
                                for (int j = 0; j < plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA).Length; j++)
                                {
                                    Room room = plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA)[j];
                                    if (room.RoomType == RoomType.GATE_A)
                                    {
                                        player.Teleport(new Vector(room.Position.x, room.Position.y + 2, room.Position.z));
                                    }
                                }
                            }
                            else
                            {
                                DM_PRole.Add("MTF");
                                player.ChangeRole(Role.NTF_LIEUTENANT);
                                player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_096));
                            }
                        }
                    });*/
                    #endregion
                    if(plugin.Server.GetPlayers(Role.CHAOS_INSURGENCY).Count == 0)
                    {
                        EventManager.ActiveEvent = "";
                        EventManager.DisableRespawns = false;
                        EventManager.RoundLocked = false;
                        plugin.Server.Map.Broadcast(10, "MTF wygrało", false);
                        plugin.Server.Map.AnnounceCustomMessage("Attention all personnel . Spotted only M T F");
                        plugin.Round.EndRound();
                    }
                    if(plugin.Server.GetPlayers(Role.NTF_LIEUTENANT).Count == 0)
                    {
                        EventManager.ActiveEvent = "";
                        EventManager.DisableRespawns = false;
                        EventManager.RoundLocked = false;
                        plugin.Server.Map.Broadcast(10, "CI wygrało", false);
                        plugin.Server.Map.AnnounceCustomMessage("Attention all personnel . Spotted only C I");
                        plugin.Round.EndRound();
                    }
                }
                if (EventManager.ActiveEvent == "TSL")
                {
                    if(TSL_T.Count == 0)
                    {
                        EventManager.DisableRespawns = false;
                        EventManager.RoundLocked = false;
                        plugin.Round.EndRound();
                        plugin.Server.Map.Broadcast(10,"Niewinni wygrali.",false);
                        EventManager.ActiveEvent = "";
                    }
                    else if(TSL_I.Count == 0 && TSL_D.Count == 0)
                    {
                        EventManager.DisableRespawns = false;
                        EventManager.RoundLocked = false;
                        plugin.Round.EndRound();
                        plugin.Server.Map.Broadcast(10, "Zdrajcy wygrali.", false);
                        EventManager.ActiveEvent = "";
                    }
                }
                if (EventManager.ActiveEvent == "689")
                {
                    plugin.Server.GetPlayers().ForEach(player => {
                        if(player.TeamRole.Team == Team.SCP)
                        {
                            if (Vector.Distance(SCP689.GetPosition(), player.GetPosition()) <= 10)
                            {
                                SCP689_Last = player;
                            }
                        }
                    });
                }
                if (EventManager.ActiveEvent == "Hunt")
                {
                    plugin.Server.GetPlayers().ForEach(player => {
                        if(player.TeamRole.Role == Role.SPECTATOR)
                        {
                            player.ChangeRole(Role.NTF_LIEUTENANT);
                            player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_939_53));
                        }
                    });
                }
                if (winner != null)
                {
                    plugin.Server.Map.Broadcast(10, winner.Name + " wygrywa!", false);
                    EventManager.RoundLocked = false;
                    EventManager.DisableRespawns = false;
                    plugin.Round.EndRound();
                    EventManager.ActiveEvent = "";
                    EventManager.BlackOut = false;
                    winner = null;
                }
                #region hidden
                /*if (EventManager.BlackOut)
                {
                    foreach (Room room in plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA))
                    {
                        if (room.ZoneType == ZoneType.HCZ || room.ZoneType == ZoneType.LCZ || room.RoomType == RoomType.NUKE || room.RoomType == RoomType.SCP_049 || room.RoomType == RoomType.ENTRANCE_CHECKPOINT) room.FlickerLights();
                    }
                }*/
                #endregion
                if (DBBR_D && false)
                {
                    ZoneType td = ZoneType.UNDEFINED;
                    ZoneType td2 = ZoneType.UNDEFINED;
                    if (DBBR_TD == "HCZ")
                    {
                        td = ZoneType.HCZ;
                    }
                    else if (DBBR_TD == "LCZ")
                    {
                        td = ZoneType.LCZ;
                    }
                    else if (DBBR_TD == "EZ")
                    {
                        td = ZoneType.ENTRANCE;
                    }
                    if (DBBR_TD2 == "HCZ")
                    {
                        td2 = ZoneType.HCZ;
                    }
                    else if (DBBR_TD2 == "LCZ")
                    {
                        td2 = ZoneType.LCZ;
                    }
                    else if (DBBR_TD2 == "EZ")
                    {
                        td2 = ZoneType.ENTRANCE;
                    }


                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        Room nearest = null;
                        float distance = 0;
                        if (player.TeamRole.Role == Role.CLASSD)
                        {
                            for (int i = 0; i < plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA).Length; i++)
                            {
                                Room room = plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA)[i];
                                if (nearest == null)
                                {
                                    nearest = room;
                                    distance = Vector.Distance(player.GetPosition(), room.Position);

                                }
                                else
                                {
                                    if (Vector.Distance(player.GetPosition(), room.Position) < distance)
                                    {
                                        nearest = room;
                                    }
                                }
                            }
                            ZoneType tmp1 = nearest.ZoneType;
                            if (nearest.RoomType == RoomType.NUKE || nearest.RoomType == RoomType.SCP_049)
                            {
                                tmp1 = ZoneType.HCZ;
                            }
                            if (tmp1 == td || tmp1 == td2)
                            {
                                player.Kill(DamageType.DECONT);
                            }
                            else
                            {
                                plugin.Server.Map.Broadcast(10, nearest.RoomType.ToString() + "||" + tmp1, false);
                            }
                        }
                    });
                    DBBR_D = false;
                }
                if(EventManager.spectator_role != Role.UNASSIGNED)
                {
                    plugin.Server.GetPlayers(Role.SPECTATOR).ForEach(player => {
                        player.ChangeRole(EventManager.spectator_role);
                    });
                }
            }          
        }

        public void CheckVotes()
        {
            if (votes > plugin.Server.NumPlayers * (plugin.GetConfigInt("AutoEventVoteEnd") / 100) && EventManager.ActiveEvent != "")
            {
                EventManager.ActiveEvent = "";
                EventManager.RoundLocked = false;
                plugin.Server.Round.EndRound();
                plugin.Server.Map.Broadcast(10, "(EventManager)Wymuszono zakończenie eventu przez głowowanie", false);
            }
        }

        public void OnRoundRestart(RoundRestartEvent ev)
        {
            winner = null;
            EventManager.RoundLocked = false;
            EventManager.ActiveEvent = "";
            EventManager.DisableRespawns = false;
            EventManager.BlackOut = false;
            votes = 0;
        }

        void CheckEventEnd()
        {
            bool aalive = false;
            plugin.Server.GetPlayers().ForEach(player =>
            {
                if (player.TeamRole.Role != Role.UNASSIGNED && player.TeamRole.Role != Role.SPECTATOR && (player.TeamRole.Team != Team.SCP)) aalive = true;
            });
            if(!aalive)
            {
                EventManager.RoundLocked = false;
                plugin.Server.Map.Broadcast(10,"(EventManager)No event winner.",false);
                EventManager.BlackOut = false;
            }
        }

        public void OnTeamRespawn(TeamRespawnEvent ev)
        {
            if(EventManager.DisableRespawns == true)
            {
                ev.PlayerList = new System.Collections.Generic.List<Player>();
            }
        }

        public void OnElevatorUse(PlayerElevatorUseEvent ev)
        {
            if(EventManager.ActiveEvent == "BDeath" && ev.Elevator.Lockable == true && false)
            {
                ev.AllowUse = false;
            }
            if(ev.Player.IsHandcuffed())
            {
                ev.AllowUse = false;
            }
        }

        public void OnGeneratorFinish(GeneratorFinishEvent ev)
        {
            if (EventManager.ActiveEvent == "BDeath")
            {
                ev.Generator.Open = false;
                ev.Generator.HasTablet = false;
                BDeath_GeneratorsDone++;
            }
            else if (EventManager.ActiveEvent == "Morbus")
            {
                bool allDone = false;
                for (int i = 0; i < plugin.Server.Map.GetGenerators().Length; i++)
                {
                    allDone = plugin.Server.Map.GetGenerators()[i].Engaged;
                }
                if(allDone)
                {
                    EventManager.T1 = DateTime.Now.AddMinutes(1);
                    EventManager.T1W = "Morbus_BOFF";
                    EventManager.TB1 = true;
                }
            }
        }

        public void OnGeneratorEjectTablet(PlayerGeneratorEjectTabletEvent ev)
        {
            if (EventManager.ActiveEvent == "BDeath")
            {
                ev.Allow = false;
                ev.SpawnTablet = false;
            }
        }

        public void OnPocketDimensionEnter(PlayerPocketDimensionEnterEvent ev)
        {
            if (EventManager.ActiveEvent == "BDeath")
            {
                ev.Player.Kill(DamageType.RAGDOLLLESS);
            }
            if (EventManager.ActiveEvent == "Cameleon")
            {
                if (ev.Player.SteamId == Cam_SCP.SteamId)
                {
                    ev.Player.Teleport(ev.LastPosition);
                }   
            }
        }

        public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
        {
            if(EventManager.BlackOut)
            {
                ev.Triggerable = false;
            }
            if(EventManager.ActiveEvent == "372")
            {
                if(ev.Player.GetGhostMode())
                {
                    ev.Triggerable = false;
                }
            }
        }

        public void OnGeneratorAccess(PlayerGeneratorAccessEvent ev)
        {
            if (EventManager.ActiveEvent == "BDeath" && (ev.Generator.HasTablet == true || ev.Generator.Engaged == true))
            {
                ev.Allow = false;
            }
        }

        public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
        {
            if (EventManager.ActiveEvent == "BDeath")
            {
                ev.Generator.Open = false;
                ev.Generator.TimeLeft = 40+plugin.Server.NumPlayers * 10;
            }
            else if (EventManager.ActiveEvent == "Cameleon" && ev.Player.SteamId == Cam_SCP.SteamId)
            {
                ev.Allow = false;
            }
            else if (EventManager.ActiveEvent == "372")
            {
                if (ev.Player.SteamId == SCP372.SteamId)
                {
                    ev.Allow = false;
                }
            }
            else if (EventManager.ActiveEvent == "Morbus")
            {
                ev.Generator.TimeLeft = 180;
            }
        }

        public void OnPlayerDie(PlayerDeathEvent ev)
        {
            if(EventManager.ActiveEvent == "Apo")
            {
                ev.SpawnRagdoll = false;
            }
            else if(EventManager.ActiveEvent == "TSL")
            {
                bool foundP = false;
                bool foundK = false;
                string KillerRole = "";
                string PlayerRole = "";
                if (ev.Player == null || ev.Killer == null) return;
                TSL_D.ForEach(pos => {
                    if(pos == ev.Player.SteamId)
                    {
                        PlayerRole = "D";
                        TSL_D.Remove(pos);
                        foundP = true;
                    }
                });

                TSL_T.ForEach(pos => {
                    if (pos == ev.Player.SteamId)
                    {
                        PlayerRole = "T";
                        TSL_T.Remove(pos);
                        foundP = true;
                    }
                });

                TSL_I.ForEach(pos => {
                    if (pos == ev.Player.SteamId)
                    {
                        PlayerRole = "I";
                        TSL_I.Remove(pos);
                        foundP = true;
                    }
                });
                TSL_D.ForEach(pos => {
                    if (pos == ev.Killer.SteamId)
                    {
                        KillerRole = "D";
                        foundK = true;
                    }
                });

                TSL_T.ForEach(pos => {
                    if (pos == ev.Killer.SteamId)
                    {
                        KillerRole = "T";
                        foundK = true;
                    }
                });

                TSL_I.ForEach(pos => {
                    if (pos == ev.Killer.SteamId)
                    {
                        KillerRole = "I";
                        foundK = true;
                    }
                });
                if(foundP == false)
                {
                    plugin.Info("(EventManager.TSL)Error:Player not found!");
                }
                if (foundK == false)
                {
                    plugin.Info("(EventManager.TSL)Error:Killer not found!");
                }
                if(KillerRole == "D")
                {
                    if(PlayerRole == "T")
                    {
                        plugin.Server.Map.Broadcast(5,"Detektyw("+ev.Killer.Name+ ") zabił Zdrajce(" + ev.Player.Name+ ")", false);
                        TSL_IKC++;
                    } else if(PlayerRole == "I")
                    {
                        plugin.Server.Map.Broadcast(5, "Detektyw(" + ev.Killer.Name + ") zabił Niewinneg(" + ev.Player.Name + ")", false);
                        TSL_ITK++;
                    } else if(PlayerRole == "D")
                    {
                        plugin.Server.Map.Broadcast(5, "Detektyw(" + ev.Killer.Name + ") zabił Detektywa(" + ev.Player.Name + ")", false);
                        TSL_ITK++;
                    } else
                    {
                        plugin.Info("(EventManager.TSL)Error:Unknown player role!");
                    }
                    ev.Killer.PersonalBroadcast(10, " Pozostało " + TSL_T.Count + " Zdrajców", false);
                }
                else if (KillerRole == "I")
                {
                    if (PlayerRole == "T")
                    {
                        plugin.Server.Map.Broadcast(5, "Niewinny(" + ev.Killer.Name + ") zabił Zdrajce(" + ev.Player.Name + ")", false);
                        TSL_IKC++;
                    }
                    else if (PlayerRole == "I")
                    {
                        plugin.Server.Map.Broadcast(5, "Niewinny(" + ev.Killer.Name + ") zabił Niewinneg(" + ev.Player.Name + ")", false);
                        TSL_ITK++;
                    }
                    else if (PlayerRole == "D")
                    {
                        plugin.Server.Map.Broadcast(5, "Niewinny(" + ev.Killer.Name + ") zabił Detektywa(" + ev.Player.Name + ")", false);
                        TSL_ITK++;
                    }
                    else
                    {
                        plugin.Info("(EventManager.TSL)Error:Unknown player role!");
                    }
                }
                else if (KillerRole == "T")
                {
                    if (PlayerRole == "T")
                    {
                        plugin.Server.Map.Broadcast(5, "Zdrajca(" + ev.Killer.Name + ") zabił Zdrajce(" + ev.Player.Name + ")", false);
                        TSL_TTK++;
                    }
                    else if (PlayerRole == "I")
                    {
                        TSL_TKC++;
                    }
                    else if (PlayerRole == "D")
                    {
                        TSL_TKC++;
                    }
                    else
                    {
                        plugin.Info("(EventManager.TSL)Error:Unknown player role!");
                    }
                    ev.Killer.PersonalBroadcast(10, " Pozostało " + TSL_T.Count + " niewinnych",false);
                } else
                {
                    plugin.Info("(EventManager.TSL)Error:Unknown killer role!");
                }
            }
            else if(EventManager.ActiveEvent == "Camelon")
            {
                if(ev.Player.SteamId == Cam_SCP.SteamId)
                {
                    plugin.Server.Map.AnnounceCustomMessage("Secret SCP containedsuccessfully");
                    Cam_SCP = null;
                    EventManager.ActiveEvent = "";
                }
            }
            else if(EventManager.ActiveEvent == "Plaga")
            {
                bool found = false;
                ev.Player.ChangeRole(Role.SPECTATOR);
                plugin.Server.GetPlayers().ForEach(player => {
                    if(player.TeamRole.Team != Team.SCP)
                    {
                        found = true;
                    }
                });
                if(!found)
                {
                    EventManager.ActiveEvent = "";
                    EventManager.DisableRespawns = false;
                    EventManager.RoundLocked = false;
                    plugin.Round.EndRound();
                    plugin.Server.Map.Broadcast(10, "SCP wygrało!", false);
                }
            }
        }

        public void OnCallCommand(PlayerCallCommandEvent ev)
        {
            if(ev.Command.ToLower() == "voteend" && EventManager.ActiveEvent != "")
            {
                votes++;
                plugin.Server.Map.Broadcast(5, ev.Player.Name+" zagłosował na zakończenie eventu. "+votes+"/"+(plugin.Server.NumPlayers * (plugin.GetConfigInt("AutoEventVoteEnd") / 100)), false);
                ev.ReturnMessage = "Zrobione";
                CheckVotes();
            }
            else if (ev.Command.ToLower() == "voteend" && EventManager.ActiveEvent == "")
            {
                ev.ReturnMessage = "Nie możesz głosować na zakończenie eventu gdy nie ma żadnego.";
            }
            else if(ev.Command.ToLower() == "check")
            {
                Room nearest = null;
                float distance = 0;
                for (int i = 0; i < plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA).Length; i++)
                {
                    Room room = plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA)[i];
                    if (nearest == null)
                    {
                        nearest = room;
                        distance = Vector.Distance(ev.Player.GetPosition(), room.Position);

                    }
                    else
                    {
                        if (Vector.Distance(ev.Player.GetPosition(), room.Position) < distance)
                        {
                            nearest = room;
                        }
                    }
                }
                ZoneType tmp1 = nearest.ZoneType;
                if ((nearest.Position.y > -700 && nearest.Position.y < -500) || (nearest.Position.y < -700 && nearest.Position.y > -800))
                {
                    tmp1 = ZoneType.HCZ;
                }
                ev.ReturnMessage = nearest.RoomType.ToString() + "||" + tmp1;
            }
            else if((ev.Command.ToLower() == "zmien" || ev.Command.ToLower() == "z") && EventManager.ActiveEvent == "Morbus")
            {
                if(Morbus_SCP_hidden.Contains(ev.Player.SteamId) || Morbus_Mother.SteamId == ev.Player.SteamId)
                {
                    if(ev.Player.TeamRole.Team == Team.SCP)
                    {
                        int hp = ev.Player.GetHealth();
                        ev.Player.ChangeRole(Role.CLASSD, false, false, false, false);
                        ev.Player.SetHealth(hp);
                        ev.ReturnMessage = "Zrobione";
                    } else
                    {
                        ev.ReturnMessage = "Nie możesz się zmienić z klasy D w 939. Wyrzuć kubek z ekwipunku by to zrobić.";
                    }
                }
            }
        }

        public void OnPlayerJoin(PlayerJoinEvent ev)
        {
            if(EventManager.RoundStarted == false && ev.Player.TeamRole.Role == Role.UNASSIGNED)
            {
                //ev.Player.Teleport(new Vector(0, 1005, -70));
                ev.Player.Teleport(new Vector(39, 989, -38));
            }
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            EventManager.RoundStarted = false;
            plugin.CommandManager.CallCommand(null, "nuke", new string[] { "on" });
            EventManager.InGhostMode_pid.Clear();
        }

        public void OnPlayerHurt(PlayerHurtEvent ev)
        {
            if(ev.DamageType == DamageType.USP && EventManager.ActiveEvent == "TSL")
            {
                if(TSL_T.Contains(ev.Player.SteamId) && !TSL_T.Contains(ev.Attacker.SteamId))
                {
                    ev.Damage = 10000;
                } else if(!TSL_T.Contains(ev.Player.SteamId) && TSL_T.Contains(ev.Attacker.SteamId))
                {
                    ev.Damage = 10000;
                } else if(TSL_T.Contains(ev.Player.SteamId) && TSL_T.Contains(ev.Attacker.SteamId))
                {
                    ev.Damage = 0;
                    ev.Attacker.Kill(DamageType.USP);
                } else if(!TSL_T.Contains(ev.Player.SteamId) && !TSL_T.Contains(ev.Attacker.SteamId))
                {
                    ev.Damage = 0;
                    ev.Attacker.Kill(DamageType.USP);
                }
                ev.Attacker.GetInventory().ForEach(item => {
                    if(item.ItemType == ItemType.USP)
                    {
                        item.Remove();
                    }
                });
            }
            if (EventManager.ActiveEvent == "Cameleon" && ev.Player.SteamId == Cam_SCP.SteamId)
            {
                if (ev.DamageType == DamageType.POCKET || ev.Attacker.TeamRole.Team == Team.SCP)
                {
                    ev.Damage = 0;
                }
            }
            else if (EventManager.ActiveEvent == "Cameleon" && ev.Attacker.SteamId == Cam_SCP.SteamId)
            {
                if (ev.Attacker.TeamRole.Role != Role.TUTORIAL)
                {
                    ev.Damage = 0;
                    ev.Attacker.PersonalBroadcast(5, "Nie możesz zadawać obrażeń kiedy jesteś przemieniony!", false);
                }
                else
                {
                    if (ev.Player.TeamRole.Team == Team.SCP)
                    {
                        ev.Damage = 0;
                    }
                }
            }
            else if (EventManager.ActiveEvent == "Morbus")
            {
                if (Morbus_Mother.SteamId == ev.Player.SteamId || Morbus_SCP_hidden.Contains(ev.Player.SteamId) || Morbus_SCP_939.Contains(ev.Player.SteamId))
                {
                    if (Morbus_Mother.SteamId == ev.Attacker.SteamId || Morbus_SCP_hidden.Contains(ev.Attacker.SteamId) || Morbus_SCP_939.Contains(ev.Attacker.SteamId))
                    {
                        if (ev.Player.SteamId != ev.Attacker.SteamId)
                        {
                            ev.Damage = 0;
                            ev.Attacker.PersonalBroadcast(10, "Nie możesz zadawać obrażeń żadnemu SCP 939!(ukryty 939 lub 939 matka to też 939)", false);
                        }
                    }
                }
                if (Morbus_Mother.SteamId == ev.Player.SteamId)
                {
                    if (ev.Player.GetHealth() - ev.Damage <= 0)
                    {
                        ev.Player.ChangeRole(Role.SPECTATOR);
                        plugin.Server.Map.AnnounceCustomMessage("Main SCP 9 3 9 containedsuccessfully .Initiating TERMINATION of all SCP 9 3 9 objects in T minus 1 minute");
                        EventManager.BlackOut = false;
                        EventManager.T1 = DateTime.Now.AddMinutes(1);
                        EventManager.T1W = "MorbusEnd";
                        EventManager.TB1 = true;
                        Morbus_Respawn = false;
                    }
                }
                if (Morbus_Respawn && ev.Player.GetHealth() - ev.Damage <= 0)
                {
                    if (!Morbus_SCP_hidden.Contains(ev.Player.SteamId))
                    {
                        ev.Damage = 0;
                        Morbus_SCP_hidden.Add(ev.Player.SteamId);
                        ev.Player.ChangeRole(Role.CLASSD);
                        ev.Player.SetHealth(500);
                        ev.Player.GiveItem(ItemType.CUP);
                        ev.Player.PersonalBroadcast(10, "Jesteś ukrytym SCP 939. Twoje zadanie to pomóc 'matce' zabić wszystkie klasy D. Jeśli wyrzucisz kubek stajesz się 939. By powrócic do poprzedniej formy wpisz .z w konsoli pod ~", false);
                    }
                    else if (!Morbus_SCP_939.Contains(ev.Player.SteamId))
                    {
                        ev.Damage = 0;
                        Morbus_SCP_939.Add(ev.Player.SteamId);
                        ev.Player.ChangeRole(Role.SCP_939_89);
                        ev.Player.SetHealth(400);
                        ev.Player.PersonalBroadcast(10, "Jesteś SCP 939. Twoje zadanie to zabić wszystkie klasy D", false);
                    }
                }
            }
            else if (EventManager.ActiveEvent == "Piniata" && Pin_pre)
            {
                ev.Attacker.ChangeRole(ev.Attacker.TeamRole.Role);
                ev.Attacker.SetGodmode(false);
                if (ev.Attacker.TeamRole.Team == Team.CLASSD)
                {
                    ev.Player.GetInventory().ForEach(item =>
                    {
                        Pin_Ditems.Add(item.ItemType);
                    });
                }
                else
                {
                    ev.Player.GetInventory().ForEach(item =>
                    {
                        Pin_Sitems.Add(item.ItemType);
                    });
                }
            }
            else if (EventManager.ActiveEvent == "372")
            {
                if (ev.Player.SteamId == SCP372.SteamId)
                {
                    if (ev.Player.GetHealth() - ev.Damage <= 0)
                    {
                        ev.Player.ChangeRole(Role.SPECTATOR);
                        plugin.Server.Map.AnnounceScpKill("3 7 2", ev.Attacker);
                        SCP372 = null;
                        EventManager.ActiveEvent = "";
                        EventManager.RoundLocked = false;
                    }
                }
            }
            else if (EventManager.ActiveEvent == "689")
            {
                if (ev.Attacker.SteamId == SCP689.SteamId && ev.DamageType == DamageType.SCP_049)
                {
                    ev.Damage = 0;
                }
                if (ev.Player.SteamId == SCP689.SteamId && ev.Player.TeamRole.Role == Role.SCP_049)
                {
                    if (ev.Player.GetHealth() - ev.Damage <= 0)
                    {
                        ev.Player.ChangeRole(Role.SPECTATOR);
                        plugin.Server.Map.AnnounceScpKill("6 8 9", ev.Attacker);
                        SCP689 = null;
                        EventManager.ActiveEvent = "";
                    }
                }
            }
            else if (EventManager.ActiveEvent == "Hunt")
            {
                if (ev.Player.GetHealth() - ev.Damage <= 0)
                {
                    if (ev.Player.TeamRole.Role == Role.SCP_173)
                    {
                        if (Hunt_scpsc < 10 && ev.Attacker != null)
                        {
                            ev.Attacker.ChangeRole(Role.SCP_173);
                            ev.Attacker.SetHealth(1500);
                            ev.Attacker.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_096));
                            Hunt_scpsc++;
                        }
                        else
                        {
                            EventManager.ActiveEvent = "";
                            EventManager.DisableRespawns = false;
                            EventManager.RoundLocked = false;
                            plugin.Server.Map.Broadcast(10, "Event zakończył się.", false);
                            plugin.Round.EndRound();
                        }

                    }
                }
            }
            else if (EventManager.ActiveEvent == "Plaga")
            {
                if (ev.Player.GetHealth() - ev.Damage <= 0)
                {
                    ev.Player.ChangeRole(Role.SCP_939_89);
                    ev.Player.SetHealth(800);
                    ev.Player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.CLASSD));
                    ev.Player.PersonalBroadcast(10, "Jesteś SCP. Twoje zadanie to zabić wszystkich MTF przed dekontaminacją LCZ.", false);
                }
            }
            else if(EventManager.ActiveEvent == "DM")
            {
                if (ev.Player.GetHealth() - ev.Damage <= 0)
                {
                    if(ev.Player.TeamRole.Team == Team.CHAOS_INSURGENCY)
                    {
                        ev.Player.ChangeRole(Role.NTF_LIEUTENANT);
                        ev.Player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_096));
                    } else if(ev.Player.TeamRole.Team == Team.NINETAILFOX)
                    {
                        ev.Player.ChangeRole(Role.CHAOS_INSURGENCY);
                        ev.Player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.FACILITY_GUARD));
                    } else
                    {
                        int rand = new Random().Next(0, 1);
                        if (rand == 0)
                        {
                            ev.Player.ChangeRole(Role.NTF_LIEUTENANT);
                            ev.Player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_096));
                        }
                        else if (rand == 1)
                        {
                            ev.Player.ChangeRole(Role.CHAOS_INSURGENCY);
                            ev.Player.Teleport(plugin.Server.Map.GetRandomSpawnPoint(Role.FACILITY_GUARD));
                        }
                    }
                }
            }
        }

        public void OnThrowGrenade(PlayerThrowGrenadeEvent ev)
        {
            if(ev.GrenadeType == ItemType.FLASHBANG && EventManager.ActiveEvent == "BDeath")
            {
                plugin.Server.GetPlayers().ForEach(player => {
                    if(player.TeamRole.Role == Role.SCP_106)
                    {
                        if(Vector.Distance(player.GetPosition(),ev.Player.GetPosition()) <= 5)
                        {
                            Vector pos = player.Get106Portal();
                            if (pos == null)
                            {
                                pos = plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_106);
                            }
                            player.Teleport(new Vector(pos.x, pos.y+2, pos.z));
                        }
                    }
                });
            }
            if(EventManager.ActiveEvent == "372")
            {
                if(ev.Player.SteamId == SCP372.SteamId)
                {
                    ev.Player.SetGhostMode(false);
                    EventManager.T1 = DateTime.Now.AddSeconds(5);
                    EventManager.T1W = "372Hide";
                    EventManager.TB1 = true;
                }
            }
            if (EventManager.InGhostMode_pid.Contains(ev.Player.PlayerId))
            {
                ev.Player.PersonalBroadcast(10, "Nie rzucaj granatów w trybie ducha!", false);
            }
        }

        public void OnSpawn(PlayerSpawnEvent ev)
        {
            if (EventManager.ActiveEvent == "ODay")
            {
                if (ev.Player.TeamRole.Role == Role.NTF_CADET)
                {
                    ev.SpawnPos = plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_173);
                }
                else if (ev.Player.TeamRole.Role == Role.NTF_LIEUTENANT)
                {
                    ev.SpawnPos = plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_096);
                }
                else if (ev.Player.TeamRole.Role == Role.NTF_COMMANDER)
                {
                    for (int i = 0; i < plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.SPEAKER).Length; i++)
                    {
                        Room room = plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.SPEAKER)[i];
                        if (room.RoomType == RoomType.SCP_106)
                        {
                            ev.SpawnPos = new Vector(room.Position.x, room.Position.y + 2, room.Position.z);
                        }
                    }
                }
                else if (ev.Player.TeamRole.Role == Role.CHAOS_INSURGENCY)
                {
                    ev.SpawnPos = plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_049);
                }
                else if (ev.Player.TeamRole.Role == Role.CLASSD)
                {
                    ev.SpawnPos = plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_049);
                }
                else if (ev.Player.TeamRole.Role == Role.SCIENTIST || ev.Player.TeamRole.Role == Role.NTF_SCIENTIST)
                {
                    ev.SpawnPos = plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_939_89);
                }
                else if (ev.Player.TeamRole.Role == Role.FACILITY_GUARD)
                {
                    plugin.Server.Map.GetDoors().ForEach(door => {
                        if (door.Name == "079_FIRST")
                        {
                            ev.SpawnPos = new Vector(door.Position.x, door.Position.y+2, door.Position.z);
                        }
                    });
                }
                else if (ev.Player.TeamRole.Role == Role.SCP_939_53 || ev.Player.TeamRole.Role == Role.SCP_939_89)
                {
                    ev.SpawnPos = plugin.Server.Map.GetRandomSpawnPoint(Role.SCIENTIST);
                }
                else if (ev.Player.TeamRole.Role == Role.SCP_173)
                {
                    ev.SpawnPos = plugin.Server.Map.GetRandomSpawnPoint(Role.NTF_CADET);
                }
                else if (ev.Player.TeamRole.Role == Role.SCP_049)
                {
                    ev.SpawnPos = plugin.Server.Map.GetRandomSpawnPoint(Role.CHAOS_INSURGENCY);
                }
                else if (ev.Player.TeamRole.Role == Role.SCP_106)
                {
                    ev.SpawnPos = plugin.Server.Map.GetRandomSpawnPoint(Role.NTF_COMMANDER);
                }
                else if (ev.Player.TeamRole.Role == Role.SCP_096)
                {
                    ev.SpawnPos = plugin.Server.Map.GetRandomSpawnPoint(Role.NTF_LIEUTENANT);
                }
                else if (ev.Player.TeamRole.Role == Role.SCP_049_2)
                {
                    ev.SpawnPos = plugin.Server.Map.GetRandomSpawnPoint(Role.CLASSD);
                }
            }
        }

        public void OnPlayerDropItem(PlayerDropItemEvent ev)
        {
            if(EventManager.ActiveEvent == "Cameleon")
            {
                if (ev.Player.SteamId == Cam_SCP.SteamId)
                {
                    if (ev.Item.ItemType == ItemType.COIN)
                    {
                        ev.Allow = false;
                        if (ev.Player.TeamRole.Role != Role.CLASSD)
                        {
                            int hp = ev.Player.GetHealth();
                            int current = ev.Player.GetCurrentItemIndex();
                            ev.Player.ChangeRole(Role.CLASSD, false, false, false, false);
                            ev.Player.SetHealth(hp);
                            ev.Player.SetCurrentItemIndex(current);
                        }
                    }
                    else if (ev.Item.ItemType == ItemType.JANITOR_KEYCARD)
                    {
                        ev.Allow = false;
                        if (ev.Player.TeamRole.Role != Role.SCIENTIST)
                        {
                            int current = ev.Player.GetCurrentItemIndex();
                            int hp = ev.Player.GetHealth();
                            ev.Player.ChangeRole(Role.SCIENTIST, false, false, false, false);
                            ev.Player.SetHealth(hp);
                            ev.Player.SetCurrentItemIndex(current);
                        }
                    }
                    else if (ev.Item.ItemType == ItemType.WEAPON_MANAGER_TABLET)
                    {
                        ev.Allow = false;
                        if (ev.Player.TeamRole.Role != Role.TUTORIAL)
                        {
                            int current = ev.Player.GetCurrentItemIndex();
                            int hp = ev.Player.GetHealth();
                            ev.Player.ChangeRole(Role.TUTORIAL, false, false, false, false);
                            ev.Player.SetHealth(hp);
                            ev.Player.SetCurrentItemIndex(current);
                        }
                    }
                }
            }
            else if (EventManager.ActiveEvent == "Morbus")
            {
                if(ev.Player.SteamId == Morbus_Mother.SteamId)
                {
                    if (ev.Item.ItemType == ItemType.CUP)
                    {
                        ev.Allow = false;
                        if (ev.Player.TeamRole.Role == Role.CLASSD)
                        {
                            int hp = ev.Player.GetHealth();
                            ev.Player.ChangeRole(Role.SCP_939_53, false, false, false, false);
                            ev.Player.SetHealth(hp);
                        }
                        else
                        {
                            int hp = ev.Player.GetHealth();
                            ev.Player.ChangeRole(Role.CLASSD, false, false, false, false);
                            ev.Player.SetHealth(hp);
                        }
                    } 
                }
                else if(Morbus_SCP_hidden.Contains(ev.Player.SteamId))
                {
                    if (ev.Item.ItemType == ItemType.CUP)
                    {
                        ev.Allow = false;
                        if (ev.Player.TeamRole.Role == Role.CLASSD)
                        {
                            int hp = ev.Player.GetHealth();
                            ev.Player.ChangeRole(Role.SCP_939_53, false, false, false, false);
                            ev.Player.SetHealth(hp);
                        }
                        else
                        {
                            int hp = ev.Player.GetHealth();
                            ev.Player.ChangeRole(Role.CLASSD, false, false, false, false);
                            ev.Player.SetHealth(hp);
                        }
                    }
                }
            }
            else if (EventManager.ActiveEvent == "372")
            {
                if (ev.Player.SteamId == SCP372.SteamId)
                {
                    ev.Player.SetGhostMode(false);
                    EventManager.T1 = DateTime.Now.AddSeconds(5);
                    EventManager.T1W = "372Hide";
                    EventManager.TB1 = true;
                }
            }
        }

        public void OnMedkitUse(PlayerMedkitUseEvent ev)
        {
            if (EventManager.ActiveEvent == "372")
            {
                if (ev.Player.SteamId == SCP372.SteamId)
                {
                    ev.Player.SetGhostMode(false);
                    EventManager.T1 = DateTime.Now.AddSeconds(5);
                    EventManager.T1W = "372Hide";
                    EventManager.TB1 = true;
                }
            }
        }

        public void OnShoot(PlayerShootEvent ev)
        {
            if (EventManager.ActiveEvent == "372")
            {
                if (ev.Player.SteamId == SCP372.SteamId)
                {
                    ev.Player.SetGhostMode(false);
                    EventManager.T1 = DateTime.Now.AddSeconds(5);
                    EventManager.T1W = "372Hide";
                    EventManager.TB1 = true;
                }
            }
            if (EventManager.InGhostMode_pid.Contains(ev.Player.PlayerId))
            {
                ev.WeaponSound = null;
                ev.Direction = new Smod2.API.Vector(0,1,0);
                ev.Player.PersonalBroadcast(10,"Jesteś w trybie ducha! Nie możesz strzelać!",false);
            }
        }

        public void OnReload(PlayerReloadEvent ev)
        {
            if (EventManager.ActiveEvent == "372")
            {
                if (ev.Player.SteamId == SCP372.SteamId)
                {
                    ev.Player.SetGhostMode(false);
                    EventManager.T1 = DateTime.Now.AddSeconds(5);
                    EventManager.T1W = "372Hide";
                    EventManager.TB1 = true;
                }
            }
        }

        public void OnSCP914ChangeKnob(PlayerSCP914ChangeKnobEvent ev)
        {
            if (EventManager.ActiveEvent == "372")
            {
                if (ev.Player.SteamId == SCP372.SteamId)
                {
                    ev.Player.SetGhostMode(false);
                    EventManager.T1 = DateTime.Now.AddSeconds(5);
                    EventManager.T1W = "372Hide";
                    EventManager.TB1 = true;
                }
            }
        }

        public void OnPlayerPickupItemLate(PlayerPickupItemLateEvent ev)
        {
            if (EventManager.ActiveEvent == "372")
            {
                if (ev.Player.SteamId == SCP372.SteamId)
                {
                    ev.Player.SetGhostMode(false);
                    EventManager.T1 = DateTime.Now.AddSeconds(5);
                    EventManager.T1W = "372Hide";
                    EventManager.TB1 = true;
                }
            }
            else if (EventManager.ActiveEvent == "343")
            {
                if(ev.Player.SteamId == SCP343.SteamId)
                {
                    if(ev.Item.ItemType == ItemType.COM15)
                    {
                        ev.Item.Remove();
                        ev.Player.GiveItem(ItemType.COIN);
                    } else if (ev.Item.ItemType == ItemType.JANITOR_KEYCARD)
                    {
                        ev.Item.Remove();
                        ev.Player.GiveItem(ItemType.O5_LEVEL_KEYCARD);
                    }
                    else if (ev.Item.ItemType == ItemType.USP)
                    {
                        ev.Item.Remove();
                        ev.Player.GiveItem(ItemType.COIN);
                    }
                    else if (ev.Item.ItemType == ItemType.LOGICER)
                    {
                        ev.Item.Remove();
                        ev.Player.GiveItem(ItemType.COIN);
                    }
                    else if (ev.Item.ItemType == ItemType.MICROHID)
                    {
                        ev.Item.Remove();
                        ev.Player.GiveItem(ItemType.MEDKIT);
                    }
                    else if (ev.Item.ItemType == ItemType.FLASHBANG)
                    {
                        ev.Item.Remove();
                        ev.Player.GiveItem(ItemType.COIN);
                    }
                    else if (ev.Item.ItemType == ItemType.FRAG_GRENADE)
                    {
                        ev.Item.Remove();
                        ev.Player.GiveItem(ItemType.COIN);
                    }
                    else if (ev.Item.ItemType == ItemType.E11_STANDARD_RIFLE)
                    {
                        ev.Item.Remove();
                        ev.Player.GiveItem(ItemType.COIN);
                    }
                    else if (ev.Item.ItemType == ItemType.P90)
                    {
                        ev.Item.Remove();
                        ev.Player.GiveItem(ItemType.COIN);
                    }
                    else if (ev.Item.ItemType == ItemType.MP4)
                    {
                        ev.Item.Remove();
                        ev.Player.GiveItem(ItemType.COIN);
                    }
                    else if (ev.Item.ItemType == ItemType.WEAPON_MANAGER_TABLET)
                    {
                        ev.Item.Remove();
                        ev.Player.GiveItem(ItemType.JANITOR_KEYCARD);
                    }
                    else if (ev.Item.ItemType == ItemType.SCIENTIST_KEYCARD)
                    {
                        ev.Item.Remove();
                        ev.Player.GiveItem(ItemType.RADIO);
                    }
                    else if (ev.Item.ItemType == ItemType.DISARMER)
                    {
                        ev.Item.Remove();
                        ev.Player.GiveItem(ItemType.CUP);
                    }
                }
            }
        }

        public void OnPlayerPickupItem(PlayerPickupItemEvent ev)
        {
            if(EventManager.InGhostMode_pid.Contains(ev.Player.PlayerId))
            {
                ev.Allow = false;
                ev.Player.PersonalBroadcast(10, "Nie możesz podnosić przedmiotów gdy jesteś w trybie ducha!", false);
            }
        }

        public void OnDoorAccess(PlayerDoorAccessEvent ev)
        {
            if(ev.Player.IsHandcuffed())
            {
                ev.Allow = false;
            }
            if(EventManager.ActiveEvent == "343")
            {
                if(ev.Player.SteamId == SCP343.SteamId)
                {
                    ev.Allow = true;
                }
            }
        }

        public void OnHandcuffed(PlayerHandcuffedEvent ev)
        {
            if (EventManager.InGhostMode_pid.Contains(ev.Owner.PlayerId))
            {
                ev.Handcuffed = false;
                ev.Owner.PersonalBroadcast(10, "Nie skuwaj gdy jesteś w trybie ducha!", false);
            }
        }

        public void OnDecontaminate()
        {
            if(EventManager.ActiveEvent == "Plaga")
            {
                plugin.Server.GetPlayers().ForEach(player => {
                    if(player.TeamRole.Team != Team.SCP)
                    {
                        EventManager.ActiveEvent = "";
                        EventManager.DisableRespawns = false;
                        EventManager.RoundLocked = false;
                        plugin.Round.EndRound();
                        plugin.Server.Map.Broadcast(10, "MTF wygrało!", false);
                    }
                });
            }
        }
    }
}


