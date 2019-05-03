using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;
using System.Linq;
using Smod2.API;
using System.Net;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using RemoteAdmin;

namespace EventManager
{
    class CommandHandler : ICommandHandler
    {
        private EventManager plugin;
        public CommandHandler(EventManager plugin)
        {
            this.plugin = plugin;
        }

        public string GetCommandDescription()
        {
            return "Controll Event Manager";
        }

        public string GetUsage()
        {
            return "EventManager (force (event name))/(list)/(lockround true/false)/(disresp true/false)/(blackout true/false)/(ghost true/false)/(warp [warp])/(pos)/(tpc [x] [y] [z])/(config)/(specrole [role])(controll079 [args])";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            Player admin = null;
            plugin.Server.GetPlayers().ForEach(player =>
            {
                if (player.ToString() == sender.ToString())
                {
                    admin = player;
                }
            });
            if (args.Length == 0)
            {
                return new string[] { GetUsage() };
            }
            bool allow = false;
            plugin.Server.GetPlayers().ForEach(player => {
                if (player.ToString() == sender.ToString())
                {
                    EventManager.AllowedRoles.ForEach(role => {
                        if (player.GetRankName() == role)
                        {
                            allow = true;
                        }
                    });
                }
            });
            if (EventManager.AllowToEveryone) allow = true;
            if (args[0].ToLower() == "list" || args[0].ToLower() == "l")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "list", args2.ToArray());
                List<string> output = new List<string>() { "Events list:" };
                /*EventManager.EventsList.ForEach(ev => {
                    output.Add("("+ev.Command+")"+ev.Name + ":" + ev.Descripction);
                });*/
                EventManager.EventsDescripcion.ForEach(eventOBJ =>
                {
                    output.Add(eventOBJ);
                });

                return output.ToArray();
            }
            else if (args[0].ToLower() == "force" || args[0].ToLower() == "f")
            {
                EventManager.Event Event = new EventManager.Event() { Plugin = null };
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "force", args2.ToArray());
                if (!allow)
                {
                    plugin.Server.GetPlayers().ForEach(player =>
                    {
                        if (player.ToString() == sender.ToString())
                        {
                            EventManager.ToDSC.Denied(player, "EventManager " + string.Join(" ", args));
                        }
                    });

                    return new string[] { "You're not allowed to controll Event Manager!" };
                }
                else
                {
                    switch (args[1].ToUpper())
                    {
                        case "WIN":
                            {
                                new WINaukiEvent(plugin, admin, true);
                                return new string[] { "Starting event. WINauki" };
                            }
                        case "WHR":
                            {
                                new WarHeadRunEvent(plugin, admin, true);
                                return new string[] { "Starting event. WarHeadRun" };
                            }
                        case "CHOWANY":
                            {
                                new ChowanyEvent(plugin, admin, true);
                                return new string[] { "Starting event. Chowany" };
                            }
                        case "ACHTUNG":
                            {
                                new AchtungEvent(plugin, admin, true);
                                return new string[] { "Starting event. Achtung" };
                            }
                        case "BDEATH":
                            {
                                new BDeathEvent(plugin, admin, true);
                                return new string[] { "Starting event. Black Death" };
                            }
                        case "VIP":
                            {
                                if (args.Length == 2)
                                {
                                    return new string[] { "This event is Work In Progress.", "Use experimental mode to activate it." };
                                }
                                else
                                {
                                    if (args[2] == "-e")
                                    {
                                        new VIPEvent(plugin, admin, true);
                                        return new string[] { "Starting event. V.I.P" };
                                    }
                                    else
                                    {
                                        return new string[] { "This event is Work In Progress.", "Use experimental mode to activate it." };
                                    }
                                }

                            }
                        case "FIGHT173":
                            {
                                new Fight173Event(plugin, admin, true);
                                return new string[] { "Starting event. Fight with 173" };
                            }
                        case "BLACKOUT":
                            {
                                new DarkGameEvent(plugin, admin, true);
                                return new string[] { "Starting event. Blackout" };
                            }
                        case "RUN123":
                            {
                                new Run123Event(plugin, admin, true);
                                return new string[] { "Starting event. Run 1 2 3" };
                            }
                        case "SEARCH":
                            {
                                new SearchEvent(plugin, admin, true);
                                return new string[] { "Starting event. Search" };
                            }
                        case "APO":
                            {
                                new ApoEvent(plugin, admin, true);
                                return new string[] { "Starting event. Apokalipse" };
                            }
                        case "DBBR":
                            {
                                new DBBREvent(plugin, admin, true);
                                return new string[] { "Starting event. D-Boi Battle Royale" };
                            }
                        case "DM":
                            {
                                new DMEvent(plugin, admin, true);
                                return new string[] { "Starting event. DeathMatch" };
                            }
                        case "TSL":
                            {
                                new TTTEvent(plugin, admin, true);
                                return new string[] { "Starting event. Trouble in Secret Labolatory" };
                            }
                        case "ODAY":
                            {
                                new ODayEvent(plugin, admin, true);
                                return new string[] { "Starting event. Oposite day" };
                            }
                        case "CAMELEON":
                            {
                                new CameleonEvent(plugin, admin, true);
                                return new string[] { "Starting event. SCP-Cameleon" };
                            }
                        case "MORBUS":
                            {
                                new MorbusEvent(plugin, admin, true);
                                return new string[] { "Starting event. Morbus" };
                            }
                        case "SPY":
                            {
                                new SpyEvent(plugin, admin, true);
                                return new string[] { "Starting event. Spy" };
                            }
                        case "PINIATA":
                            {
                                if (args.Length == 2)
                                {
                                    return new string[] { "This event is Work In Progress.", "Use experimental mode to activate it." };
                                }
                                else
                                {
                                    if (args[2] == "-e")
                                    {
                                        new PiniataEvent(plugin, admin, true);
                                        return new string[] { "Starting event. Piniata(WIP)" };
                                    }
                                    else
                                    {
                                        return new string[] { "This event is Work In Progress.", "Use experimental mode to activate it." };
                                    }
                                }

                            }
                        case "372":
                            {
                                new Breakout372Event(plugin, admin, true);
                                return new string[] { "Starting event. Breakout of SCP 372" };
                            }
                        case "343":
                            {
                                new SCP343Event(plugin, admin, true);
                                return new string[] { "Starting event. Breakout of SCP 343" };
                            }
                        case "689":
                            {
                                if (args.Length == 2)
                                {
                                    return new string[] { "This event is Work In Progress.", "Use experimental mode to activate it." };
                                }
                                else
                                {
                                    if (args[2] == "-e")
                                    {
                                        new SCP689Event(plugin, admin, true);
                                        return new string[] { "Starting event. Breakout of SCP 689(WIP)" };
                                    }
                                    else
                                    {
                                        return new string[] { "This event is Work In Progress.", "Use experimental mode to activate it." };
                                    }
                                }

                            }
                        case "1499":
                            {
                                new SCP1499Event(plugin, admin, true);
                                return new string[] { "Starting event. Breakout of SCP 1499" };
                            }
                        case "HUNT":
                            {
                                new PolowanieEvent(plugin, admin, true);
                                return new string[] { "Starting event. Hunt" };
                            }
                        case "PLAGA":
                            {
                                new PlagaEvent(plugin, admin, true);
                                return new string[] { "Starting event. Plaga" };
                            }
                        case "GBREACH":
                            {
                                new GlobalBreachEvent(plugin, admin, true);
                                return new string[] { "Starting event. Global Breach" };
                            }
                        default:
                            {
                                return new string[] { "Unkown event", "Use em list to check event list" };
                            }
                    }
                    #region diabled
                    EventManager.EventsList.ForEach(ev =>
                    {
                        if (args[1].ToLower() == ev.Command.ToLower())
                        {
                            Event = ev;
                        }
                    });
                    if(Event.Plugin == null)
                    {
                        return new string[] { "Unkown event", "Use em list to check event list" };
                    }
                    else
                    {
                        if (Event.Experimental)
                        {
                            if (args.Length == 2)
                            {
                                return new string[] { "This event is Work In Progress.", "Use experimental mode to activate it." };
                            }
                            else
                            {
                                if (args[2] == "-e")
                                {
                                    if (Event.CanLagOnStart)
                                    {
                                        plugin.Server.Map.Broadcast(10, "(<color=red>EventManager</color>)" + EventManager.event_ini + ":" + Event.Name + " " + EventManager.event_ld, false);
                                    }
                                    else
                                    {
                                        plugin.Server.Map.Broadcast(10, "(<color=red>EventManager</color>)" + EventManager.event_ini + ":" + Event.Name, false);
                                    }

                                    EventManager.ActiveEvent = Event.Command;
                                    return new string[] { "Starting event. " + Event.Name };
                                }
                                else
                                {
                                    return new string[] { "This event is Work In Progress.", "Use experimental mode to activate it." };
                                }
                            }
                        }
                        else
                        {
                            if (Event.CanLagOnStart)
                            {
                                plugin.Server.Map.Broadcast(10, "(<color=red>EventManager</color>)" + EventManager.event_ini + ":" + Event.Name + " " + EventManager.event_ld, false);
                            }
                            else
                            {
                                plugin.Server.Map.Broadcast(10, "(<color=red>EventManager</color>)" + EventManager.event_ini + ":" + Event.Name, false);
                            }
                            EventManager.ActiveEvent = Event.Command;
                            return new string[] { "Starting event. " + Event.Name };
                        }
                    }
                    #endregion
                }
            }
            else if (args[0].ToLower() == "config" || args[0].ToLower() == "c")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "config", args2.ToArray());
                return new string[] { "Config", "AutoEvent:" + plugin.GetConfigBool("AutoEvent"), "AutoEventRoundCount:" + plugin.GetConfigInt("AutoEventRoundCount"), "AutoEventVoteEnd:" + plugin.GetConfigInt("AutoEventVoteEnd") };
            }
            else if (args[0].ToLower() == "lockround")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "lockround", args2.ToArray());
                if (EventManager.ActiveEvent != "") return new string[] { "You can't override roundlock when event is active." };
                if (args.Length <= 1 || args[1] == "")
                {
                    return new string[] { "Unknown Command", "EventManager lockround true/false" };
                }
                else
                {
                    if (args[1].ToLower() == "true")
                    {
                        EventManager.RoundLocked = true;
                        return new string[] { "Done", "Round is now locked" };
                    }
                    else if (args[1].ToLower() == "false")
                    {
                        EventManager.RoundLocked = false;
                        return new string[] { "Done", "Round is now unlocked" };
                    }
                    else
                    {
                        return new string[] { "Unknown Command", "EventManager lockround true/false" };
                    }
                }
            }
            else if (args[0].ToLower() == "disresp")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "disresp", args2.ToArray());
                if (EventManager.ActiveEvent != "") return new string[] { "You can't override RespawnLock when event is active." };
                if (args.Length <= 1 || args[1] == "")
                {
                    return new string[] { "Unknown Command", "EventManager disresp true/false" };
                }
                else
                {
                    if (args[1].ToLower() == "true")
                    {
                        EventManager.DisableRespawns = true;
                        return new string[] { "Done", "Respawns are now disabled" };
                    }
                    else if (args[1].ToLower() == "false")
                    {
                        EventManager.DisableRespawns = false;
                        return new string[] { "Done", "Respawns are now enabled" };
                    }
                    else
                    {
                        return new string[] { "Unknown Command", "EventManager disresp true/false" };
                    }
                }
            }
            else if (args[0].ToLower() == "blackout")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "blackout", args2.ToArray());
                if (EventManager.ActiveEvent != "") return new string[] { "You can't override Blackout when event is active." };
                if (args.Length <= 1 || args[1] == "")
                {
                    return new string[] { "Unknown Command", "EventManager blackout true/false" };
                }
                else
                {
                    if (args[1].ToLower() == "true")
                    {
                        EventManager.BlackOut = true;
                        EventManager.T_BO = DateTime.Now.AddSeconds(1);
                        return new string[] { "Done", "Blackout is now enabled" };
                    }
                    else if (args[1].ToLower() == "false")
                    {
                        EventManager.BlackOut = false;
                        return new string[] { "Done", "Blackout is now disabled" };
                    }
                    else
                    {
                        return new string[] { "Unknown Command", "EventManager blackout true/false" };
                    }
                }
            }
            else if (args[0].ToLower() == "c106")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "c106", args2.ToArray());
                if (args.Length == 1) return new string[] { "Wrong arguments" };
                if (args[1].ToLower() == "true")
                {
                    plugin.Server.Map.FemurBreaker(true);
                    return new string[] { "done" };
                }
                else if (args[1].ToLower() == "false")
                {
                    plugin.Server.Map.FemurBreaker(false);
                    return new string[] { "done" };
                }
                else
                {
                    return new string[] { "Wrong arguments" };
                }
            }
            else if (args[0].ToLower() == "warp")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "warp", args2.ToArray());
                if (args.Length == 1) return new string[] { "Unknown warp. Warp list:", "heli", "car", "jail", "gatea_up", "gatea_bottom", "warhead_bottom", "gateb_up", "escape_up", "079" };
                switch (args[1].ToLower())
                {
                    case "heli":
                        {
                            plugin.Server.GetPlayers().ForEach(player =>
                            {
                                if (sender.ToString() == player.ToString())
                                {
                                    player.Teleport(new Smod2.API.Vector(295, 980, -62));//295 980 - 62
                                    }
                            });
                            return new string[] { "Done" };
                        }
                    case "car":
                        {
                            plugin.Server.GetPlayers().ForEach(player =>
                            {
                                if (sender.ToString() == player.ToString())
                                {
                                    player.Teleport(new Smod2.API.Vector(-96, 988, -60));//-96 988 -60
                                    }
                            });
                            return new string[] { "Done" };
                        }
                    case "jail":
                        {
                            plugin.Server.GetPlayers().ForEach(player =>
                            {
                                if (sender.ToString() == player.ToString())
                                {
                                    player.Teleport(new Smod2.API.Vector(55, 1020, -43));//55 1020 -43
                                    }
                            });
                            return new string[] { "Done" };
                        }
                    case "gatea_up":
                        {
                            plugin.Server.GetPlayers().ForEach(player =>
                            {
                                if (sender.ToString() == player.ToString())
                                {
                                    player.Teleport(new Smod2.API.Vector(10, 1010, -7));//10 1010 -7
                                    }
                            });
                            return new string[] { "Done" };
                        }
                    case "gatea_bottom":
                        {
                            plugin.Server.GetPlayers().ForEach(player =>
                            {
                                if (sender.ToString() == player.ToString())
                                {
                                    player.Teleport(new Smod2.API.Vector(5, 995, -10));//5 995 -10
                                    }
                            });
                            return new string[] { "Done" };
                        }
                    case "warhead_bottom":
                        {
                            plugin.Server.GetPlayers().ForEach(player =>
                            {
                                if (sender.ToString() == player.ToString())
                                {
                                    foreach (Room room in plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA))
                                    {
                                        if (room.RoomType == RoomType.NUKE)
                                        {
                                            player.Teleport(new Smod2.API.Vector(room.Position.x + 5, room.Position.y + 404, room.Position.z - 1));//127 -594 75 || tpc 123 -998 76 == +5,404,-1
                                                break;
                                        }
                                    }

                                }
                            });
                            return new string[] { "Done" };
                        }
                    case "079":
                        {
                            plugin.Server.GetPlayers().ForEach(player =>
                            {
                                if (sender.ToString() == player.ToString())
                                {
                                    Vector pos = plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_079);
                                    player.Teleport(new Smod2.API.Vector(pos.x, pos.y + 2, pos.z));//127 -594 75 || tpc 123 -998 76 == +5,404,-1

                                    }
                            });
                            return new string[] { "Done" };
                        }
                    case "gateb_up":
                        {
                            plugin.Server.GetPlayers().ForEach(player =>
                            {
                                if (sender.ToString() == player.ToString())
                                {
                                    player.Teleport(new Smod2.API.Vector(147, 1010, -45));//147 1010 -45
                                    }
                            });
                            return new string[] { "Done" };
                        }
                    case "escape_up":
                        {
                            plugin.Server.GetPlayers().ForEach(player =>
                            {
                                if (sender.ToString() == player.ToString())
                                {
                                    player.Teleport(new Smod2.API.Vector(210, 1008, -10));//210 1008 -10
                                    }
                            });
                            return new string[] { "Done" };
                        }
                    default:
                        {
                            return new string[] { "Unknown warp. Warp list:", "heli", "car", "jail", "gatea_up", "gatea_bottom", "warhead_bottom", "gateb_up", "escape_up", "079" };
                        }
                }
            }
            else if (args[0].ToLower() == "pos")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "pos", args2.ToArray());
                Player player = null;
                plugin.Server.GetPlayers().ForEach(playert =>
                {
                    if (sender.ToString() == playert.ToString())
                    {
                        player = playert;
                    }
                });
                return new string[] { "Your pos:", "x:" + player.GetPosition().x, "y:" + player.GetPosition().y, "z:" + player.GetPosition().z };
            }
            else if (args[0].ToLower() == "tpc")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "tpc", args2.ToArray());
                if (args.Length < 4)
                {
                    return new string[] { "Wrong arguments em tpc [x] [y] [z]" };
                }
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if (sender.ToString() == player.ToString())
                    {
                        player.Teleport(new Vector(Convert.ToInt16(args[1]), Convert.ToInt16(args[2]), Convert.ToInt16(args[3])));
                    }
                });
                return new string[] { "Done" };
            }
            else if (args[0].ToLower() == "ghost")
            {
                if (!plugin.ConfigManager.Config.GetBoolValue("sm_enable_ghostmode", false)) return new string[] { "Ghost mode is disabled in server configs!" };
                string[] toReturn = new string[] { "" };
                if (args.Length == 1) return new string[] { "Unknown arguments!", "em ghost [playerid] true/false (visibleTospec?(default:true)) (visibleWhenTalking?(default:true))" };
                Player player_tmp = null;
                plugin.Server.GetPlayers().ForEach(player =>
                {
                    if (player.PlayerId.ToString() == args[1])
                    {
                        player_tmp = player;
                    }
                });
                List<string> args2 = new List<string>();
                for (int i = 2; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, player_tmp, "ghost", args2.ToArray());
                if (args.Length == 2)
                {
                    return new string[] { EventManager.InGhostMode_pid.Contains(Convert.ToInt16(args[1])).ToString() };
                } else
                {
                    if (args[2] == "true")
                    {
                        plugin.Server.GetPlayers().ForEach(player => {
                            if (player.PlayerId.ToString() == args[1])
                            {
                                if (EventManager.InGhostMode_pid.Contains(player.PlayerId))
                                {
                                    toReturn = new string[] {"Player already have Ghost mode on"};
                                    return;
                                } else
                                {
                                    bool vtspec = true;
                                    bool vwt = true;
                                    if(args.Length > 3)
                                    {
                                        if (args[3] == "true")
                                        {
                                            vtspec = true;
                                        }
                                        else if (args[3] == "false")
                                        {
                                            vtspec = false;
                                        }
                                        else
                                        {
                                            toReturn = new string[] { "Unknown arguments!", "em ghost [playerid] true/false (visibleTospec?(default:true)) (visibleWhenTalking?(default:true))" };
                                            return;
                                        }
                                    }
                                    if (args.Length > 4)
                                    {
                                        if (args[4] == "true")
                                        {
                                            vwt = true;
                                        }
                                        else if (args[4] == "false")
                                        {
                                            vwt = false;
                                        }
                                        else
                                        {
                                            toReturn = new string[] { "Unknown arguments!", "em ghost [playerid] true/false (visibleTospec?(default:true)) (visibleWhenTalking?(default:true))" };
                                            return;
                                        }
                                    }
                                    player.SetGhostMode(true,vtspec,vwt);
                                    EventManager.ToDSC.Info("Ghost Mode enabled for " + player.Name);
                                    EventManager.InGhostMode_pid.Add(player.PlayerId);
                                    toReturn = new string[] { "Done" };
                                }
                            }
                        });
                        return toReturn;
                    }
                    else if (args[2] == "false")
                    {
                        plugin.Server.GetPlayers().ForEach(player => {
                            if (player.PlayerId.ToString() == args[1])
                            {
                                EventManager.InGhostMode_pid.Remove(player.PlayerId);
                                player.SetGhostMode(false);
                                EventManager.ToDSC.Info("Ghost Mode disabled for " + player.Name);
                                toReturn = new string[] { "Done" };
                            }
                        });
                        return toReturn;
                    }
                    else
                    {
                        return new string[] { "Unknown arguments!", "em ghost [playerid] true/false (visibleTospec?(default:true)) (visibleWhenTalking?(default:true))" };
                    }
                }
            }
            else if (args[0].ToLower() == "specrole")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "specrole", args2.ToArray());
                if (args.Length == 1) return new string[] { "Wrong arguments!","Unknown Role. Role list:","MTF_COMM","MTF_L","MTF_C","MTF_S","GUARD", "SCIENTIST","CLASSD","TUT","CI","106","173","096","049","079","939","049-2", "ZOMBIE" ,"NONE"};
                switch(args[1].ToUpper())
                {
                    case "MTF_COMM":
                        {
                            EventManager.spectator_role = Role.NTF_COMMANDER;
                            return new string[] { "Done" };
                        }
                    case "MTF_L":
                        {
                            EventManager.spectator_role = Role.NTF_LIEUTENANT;
                            return new string[] { "Done" };
                        }
                    case "MTF_C":
                        {
                            EventManager.spectator_role = Role.NTF_CADET;
                            return new string[] { "Done" };
                        }
                    case "MTF_S":
                        {
                            EventManager.spectator_role = Role.NTF_SCIENTIST;
                            return new string[] { "Done" };
                        }
                    case "GUARD":
                        {
                            EventManager.spectator_role = Role.FACILITY_GUARD;
                            return new string[] { "Done" };
                        }
                    case "SCIENTIST":
                        {
                            EventManager.spectator_role = Role.SCIENTIST;
                            return new string[] { "Done" };
                        }
                    case "CLASSD":
                        {
                            EventManager.spectator_role = Role.CLASSD;
                            return new string[] { "Done" };
                        }
                    case "CI":
                        {
                            EventManager.spectator_role = Role.CHAOS_INSURGENCY;
                            return new string[] { "Done" };
                        }
                    case "TUT":
                        {
                            EventManager.spectator_role = Role.TUTORIAL;
                            return new string[] { "Done" };
                        }
                    case "ZOMBIE":
                        {
                            EventManager.spectator_role = Role.ZOMBIE;
                            return new string[] { "Done" };
                        }
                    case "106":
                        {
                            EventManager.spectator_role = Role.SCP_106;
                            return new string[] { "Done" };
                        }
                    case "173":
                        {
                            EventManager.spectator_role = Role.SCP_173;
                            return new string[] { "Done" };
                        }
                    case "096":
                        {
                            EventManager.spectator_role = Role.SCP_096;
                            return new string[] { "Done" };
                        }
                    case "049":
                        {
                            EventManager.spectator_role = Role.SCP_049;
                            return new string[] { "Done" };
                        }
                    case "079":
                        {
                            EventManager.spectator_role = Role.SCP_079;
                            return new string[] { "Done" };
                        }
                    case "939":
                        {
                            EventManager.spectator_role = Role.SCP_939_53;
                            return new string[] { "Done" };
                        }
                    case "049-2":
                        {
                            EventManager.spectator_role = Role.SCP_049_2;
                            return new string[] { "Done" };
                        }
                    case "NONE":
                        {
                            EventManager.spectator_role = Role.UNASSIGNED;
                            return new string[] { "Done" };
                        }
                    default:
                        {
                            return new string[] { "Wrong arguments!", "Unknown Role. Role list:", "MTF_COMM", "MTF_L", "MTF_C", "MTF_S", "GUARD", "scientist".ToUpper(), "CLASSD", "TUT", "CI", "106", "173", "096", "049", "079", "939", "049-2" };
                        }
                }
            }
            else if (args[0].ToLower() == "controll079")
            {
                if (args.Length < 2) return new string[] { "Unknown args!","em controll079 lvl/xp/max_ap/ap/regen_ap [number]" };
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "controll079", args2.ToArray());
                switch(args[1])
                {
                    case "lvl":
                        {
                            if (args.Length < 3) return new string[] { "Unknown args!", "em controll079 lvl/xp/max_ap/ap/regen_ap [number]" };
                            plugin.Server.GetPlayers(Role.SCP_079).ForEach(player => {
                                player.Scp079Data.Level = Convert.ToInt16(args[2]);
                            });
                            return new string[] { "Done" };
                        }
                    case "xp":
                        {
                            if (args.Length < 3) return new string[] { "Unknown args!", "em controll079 lvl/xp/max_ap/ap/regen_ap [number]" };
                            plugin.Server.GetPlayers(Role.SCP_079).ForEach(player => {
                                player.Scp079Data.Exp = Convert.ToInt16(args[2]);
                            });
                            return new string[] { "Done" };
                        }
                    case "max_ap":
                        {
                            if (args.Length < 3) return new string[] { "Unknown args!", "em controll079 lvl/xp/max_ap/ap/regen_ap [number]" };
                            plugin.Server.GetPlayers(Role.SCP_079).ForEach(player => {
                                player.Scp079Data.MaxAP = Convert.ToInt16(args[2]);
                            });
                            return new string[] { "Done" };
                        }
                    case "ap":
                        {
                            if (args.Length < 3) return new string[] { "Unknown args!", "em controll079 lvl/xp/max_ap/ap/regen_ap [number]" };
                            plugin.Server.GetPlayers(Role.SCP_079).ForEach(player => {
                                player.Scp079Data.AP = Convert.ToInt16(args[2]);
                            });
                            return new string[] { "Done" };
                        }
                    case "regen_ap":
                        {
                            if (args.Length < 3) return new string[] { "Unknown args!", "em controll079 lvl/xp/max_ap/ap/regen_ap [number]" };
                            plugin.Server.GetPlayers(Role.SCP_079).ForEach(player => {
                                player.Scp079Data.APPerSecond = Convert.ToInt16(args[2]);
                            });
                            return new string[] { "Done" };
                        }
                    default:
                        {
                            return new string[] { "Unknown args!", "em controll079 lvl/xp/max_ap/ap/regen_ap [number]" };
                        }
                }
            }
            else if (args[0].ToLower() == "mlock")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "mlock", args2.ToArray());
                plugin.Server.GetPlayers().ForEach(player => {
                    if(player.ToString() == sender.ToString())
                    {
                        EventManager.mlock.Add(player.SteamId);
                    }
                });
                return new string[] { "Done" };
            }
            else if (args[0].ToLower() == "munlock")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "munlock", args2.ToArray());
                plugin.Server.GetPlayers().ForEach(player => {
                    if (player.ToString() == sender.ToString())
                    {
                        EventManager.munlock.Add(player.SteamId);
                    }
                });
                return new string[] { "Done" };
            }
            else if (args[0].ToLower() == "mopen")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "mopen", args2.ToArray());
                plugin.Server.GetPlayers().ForEach(player => {
                    if (player.ToString() == sender.ToString())
                    {
                        EventManager.mopen.Add(player.SteamId);
                    }
                });
                return new string[] { "Done" };
            }
            else if (args[0].ToLower() == "mclose")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "mclose", args2.ToArray());
                plugin.Server.GetPlayers().ForEach(player => {
                    if (player.ToString() == sender.ToString())
                    {
                        EventManager.mclose.Add(player.SteamId);
                    }
                });
                return new string[] { "Done" };
            }
            else if (args[0].ToLower() == "mdestroy")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "mdestroy", args2.ToArray());
                plugin.Server.GetPlayers().ForEach(player => {
                    if (player.ToString() == sender.ToString())
                    {
                        EventManager.mdestroy.Add(player.SteamId);
                    }
                });
                return new string[] { "Done" };
            }
            else if (args[0].ToLower() == "giverank")
            {
                string[] toreturn = new string[] { "Player not found." };
                if (args.Length < 3) return new string[] { "Wrong arguments","Use:EventManager giverank [playerid] [rankname] (rank-color)" };
                plugin.Server.GetPlayers().ForEach(player => {
                    if(player.PlayerId.ToString() == args[1])
                    {
                        if (args.Length > 3)
                        {
                            player.SetRank(args[3], args[2], player.GetUserGroup().Name);
                            toreturn = new string[] { "Done", "Rank " + args[2] + "with color " + args[3] + " granted to " + player.Name };
                        }
                        else
                        {
                            player.SetRank(null, args[2], player.GetUserGroup().Name);
                            toreturn = new string[] { "Done", "Rank " + args[2] + "without color granted to " + player.Name };
                        }
                    }
                });
                return toreturn;
            }
            else if (args[0].ToLower() == "info")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "info", args2.ToArray());
                if (args.Length == 1) return new string[] { "Wrong args em info [message]" };
                EventManager.ToDSC.Info(args[1]);
                return new string[] { "Done" };
            }
            else if (args[0].ToLower() == "warn")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "warn", args2.ToArray());
                if (args.Length == 1) return new string[] { "Wrong args em warn [message]" };
                EventManager.ToDSC.Warn(args[1]);
                return new string[] { "Done" };
            }
            else if (args[0].ToLower() == "error")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "error", args2.ToArray());
                if (args.Length == 1) return new string[] { "Wrong args em error [message]" };
                EventManager.ToDSC.Error(args[1]);
                return new string[] { "Done" };
            }
            else if (args[0].ToLower() == "dnask")
            {
                List<string> args2 = new List<string>();
                for (int i = 1; i < args.Length; i++)
                {
                    args2.Add(args[i]);
                }
                EventManager.ToDSC.CommandCalled(admin, admin, "dnask", args2.ToArray());
                int maxi = 10;
                int mini = 0;
                if (args.Length != 1) maxi = Convert.ToInt16(args[1]);
                if (args.Length != 2 && args.Length != 1) mini = Convert.ToInt16(args[2]);
                string output = "";
                for (int i = mini; i < maxi; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if(j != 0 || i != 0)
                        {
                            output += "PITCH_" + i + "," + j + " " + (i * 10 + j) + " CLASSD r r r ";
                        }
                    }
                }
                plugin.Server.Map.AnnounceCustomMessage(output); ;
                return new string[] { ":)" };
            }
            else if (args[0].ToLower() == "say")
            {
                if (args.Length <= 3) return new string[] { "Wrong arguments" , "em say [playerid]/all [color] [message]" };
                string message = "";
                for (int i = 3; i < args.Length; i++)
                {
                    message = message + " " + args[i];
                }
                if(args[1].ToLower() == "all")
                {
                    plugin.Server.GetPlayers().ForEach(player => {
                        player.SendConsoleMessage(message,args[2]);
                    });
                }
                else
                {
                    plugin.Server.GetPlayers().ForEach(player => {
                        if(player.PlayerId.ToString() == args[1])
                        {
                            player.SendConsoleMessage(message, args[2]);
                        }
                    });
                }
                return new string[] { "Done" };
            }
            #region Disabled
            /* 
            else if (args[0].ToLower() == "warhead")
            {
                if(args.Length == 1) return new string[] { "Wrong arguments", "em warhead start/stop/on/off/lockstart/lockstop/lockbutton/locklever" };
                switch(args[1])
                {
                    case "start":
                        {
                            plugin.CommandManager.CallCommand(sender, "nuke", new string[] { "start" });
                            EventManager.Warhead.CountingDown = true;
                            return new string[] { "Alpha Warhead initated" };
                        }
                    case "stop":
                        {
                            plugin.CommandManager.CallCommand(sender, "nuke", new string[] { "stop" });
                            EventManager.Warhead.CountingDown = false;
                            return new string[] { "Alpha Warhead cancled" };
                        }
                    case "on":
                        {
                            plugin.CommandManager.CallCommand(sender,"nuke", new string[] { "on" });
                            return new string[] { "Alpha Warhead turned on" };
                        }
                    case "off":
                        {
                            plugin.CommandManager.CallCommand(sender, "nuke", new string[] { "off" });
                            return new string[] { "Alpha Warhead turned off" };
                        }
                    case "lockstart":
                        {
                            if(args.Length == 2) return new string[] { "Wrong arguments", "em warhead lockstart true/false" };
                            if(args[2] == "true")
                            {
                                EventManager.Warhead.StartLock = true;
                                return new string[] { "Alpha Warhead start lock turned on" };
                            }
                            else if(args[2] == "false")
                            {
                                EventManager.Warhead.StartLock = false;
                                return new string[] { "Alpha Warhead start lock turned off" };
                            }
                            else
                            {
                                return new string[] { "Wrong arguments", "em warhead lockstart true/false" };
                            }
                        }
                    case "lockstop":
                        {
                            if (args.Length == 2) return new string[] { "Wrong arguments", "em warhead lockstop true/false" };
                            if (args[2] == "true")
                            {
                                EventManager.Warhead.StopLock = true;
                                plugin.CommandManager.CallCommand(sender, "nuke", new string[] { "lock" });
                                return new string[] { "Alpha Warhead stop lock turned on" };
                            }
                            else if (args[2] == "false")
                            {
                                EventManager.Warhead.StopLock = false;
                                plugin.CommandManager.CallCommand(sender, "nuke", new string[] { "unlock" });
                                return new string[] { "Alpha Warhead stop lock turned off" };
                            }
                            else
                            {
                                return new string[] { "Wrong arguments", "em warhead lockstop true/false" };
                            }
                        }
                    case "lockbutton":
                        {
                            if (args.Length == 2) return new string[] { "Wrong arguments", "em warhead lockbutton true/false" };
                            if (args[2] == "true")
                            {
                                EventManager.Warhead.ButtonLock = true;
                                return new string[] { "Alpha Warhead button lock turned on" };
                            }
                            else if (args[2] == "false")
                            {
                                EventManager.Warhead.ButtonLock = false;
                                return new string[] { "Alpha Warhead button lock turned off" };
                            }
                            else
                            {
                                return new string[] { "Wrong arguments", "em warhead lockbutton true/false" };
                            }
                        }
                    case "locklever":
                        {
                            if (args.Length == 2) return new string[] { "Wrong arguments", "em warhead locklever true/false" };
                            if (args[2] == "true")
                            {
                                EventManager.Warhead.LeverLock = true;
                                return new string[] { "Alpha Warhead level locked" };
                            }
                            else if (args[2] == "false")
                            {
                                EventManager.Warhead.LeverLock = false;
                                return new string[] { "Alpha Warhead level locked" };
                            }
                            else
                            {
                                return new string[] { "Wrong arguments", "em warhead locklever true/false" };
                            }
                        }
                    case "resume":
                        {
                            if (args.Length == 2) return new string[] { "Wrong arguments", "em warhead resume true/false" };
                            if (args[2] == "true")
                            {
                                EventManager.Warhead.Resumed = true;
                                return new string[] { "Alpha Warhead will be resumed" };
                            }
                            else if (args[2] == "false")
                            {
                                EventManager.Warhead.Resumed = false;
                                return new string[] { "Alpha Warhead won't be resumed" };
                            }
                            else
                            {
                                return new string[] { "Wrong arguments", "em warhead resume true/false" };
                            }
                        }
                    default:
                        {
                            return new string[] { "Wrong arguments", "em warhead start/stop/on/off/lockstart/lockstop/lockbutton/locklever" };
                        }
                }
            }
            */
            #endregion
            else if (args[0].ToLower() == "test939" && plugin.GetConfigBool("testmode"))
            {
                string[] toreturn = new string[] { "" };
                List<GameObject> players = PlayerManager.singleton.players.ToList();
                foreach (GameObject gameObject in players)
                {
                    Scp939_VisionController component = gameObject.GetComponent<Scp939_VisionController>();
                    
                    players.ForEach(player => {
                        if (player.GetComponent<CharacterClassManager>() == null) toreturn = new string[] { "OOF" }; else
                        {
                            if (player.GetComponent<CharacterClassManager>().curClass == 14)
                            {
                                if(player.GetComponent<Scp939PlayerScript>())
                                {
                                    plugin.Server.GetPlayers().ForEach(playert => {
                                        if(player.GetComponent<CharacterClassManager>().SteamId == playert.SteamId)
                                        {
                                            plugin.Debug("Adding player " + playert.Name);
                                        }
                                    });
                                    component.seeingSCPs.Add(new Scp939_VisionController.Scp939_Vision
                                    {
                                        remainingTime = 10000000f,
                                        scp = player.GetComponent<Scp939PlayerScript>()

                                    });
                                    toreturn = new string[] { ":)" };
                                } else
                                {
                                    toreturn = new string[] { ":)/2" };
                                }
                                
                            }
                            else
                            {
                                toreturn = new string[] { "Not TUT" };
                            }
                        }
                        
                    });
                    if (toreturn != new string[] { "" }) return toreturn;
                    return new string[] { ":):(" };
                }
                return new string[] { "Unknown Command2", GetUsage() };
            }
            else if(args[0] == "test9392" && EventManager.testmode)
            {
                string[] toreturn = new string[] { "" };
                List<GameObject> players = PlayerManager.singleton.players.ToList();
                foreach (GameObject gameObject in players)
                {
                    Scp939_VisionController component = gameObject.GetComponent<Scp939_VisionController>();
                    players.ForEach(player =>
                    {
                        Player fs = null;
                        Player fp = null;
                        plugin.Server.GetPlayers().ForEach(playert => {
                            if (player.GetComponent<CharacterClassManager>().SteamId == playert.SteamId)
                            {
                                fp = playert;
                                
                            }
                            if (gameObject.GetComponent<CharacterClassManager>().SteamId == playert.SteamId)
                            {
                                fs = playert;
                            }
                        });
                        plugin.Debug(fp.Name + "|" + component.CanSee(player.GetComponent<Scp939PlayerScript>()).ToString() + "|" +fs.Name);

                    });
                }
                return new string[] { "well" };
            }
            else if(args[0] == "test" && EventManager.testmode)
            {
                EventSystemHandler.ReloadEvents(plugin);
                return new string[] { "Processing" };
            }
            else if(args[0].ToLower() == "el" && EventManager.testmode)
            {
                return EventSystemHandler.GetList();
            }
            else if(args[0] == "oof2" && EventManager.testmode)
            {
                EventManager.visions.Add(new EventManager.Vision() {player = admin,invisible = plugin.Server.GetPlayers(Role.CLASSD) });
                return new string[] { "Processing" };
            }
            else if (args[0] == "oof1" && EventManager.testmode)
            {
                EventManager.visions.Clear();
                return new string[] { "Processing" };
            }
            else
            {
                return new string[] { "Unknown Command", GetUsage() };
            }
        }
    }
}
