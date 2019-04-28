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

namespace EventManager
{
    class FEvent : ICommandHandler
    {
        private EventManager plugin;
        public FEvent(EventManager plugin)
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
            string Event = "";
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
                EventManager.EventsDescripcion.ForEach(eventOBJ =>
                {
                    output.Add(eventOBJ);
                });

                return output.ToArray();
            }
            else if (args[0].ToLower() == "force" || args[0].ToLower() == "f")
            {
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
                    EventManager.Events.ForEach(eventOBJ =>
                    {
                        if (args[1].ToLower() == eventOBJ.ToLower())
                        {
                            Event = eventOBJ;
                        }
                    });
                    
                    switch (Event)
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
                        case "Chowany":
                            {
                                new ChowanyEvent(plugin, admin, true);
                                return new string[] { "Starting event. Chowany" };
                            }
                        case "Achtung":
                            {
                                new AchtungEvent(plugin, admin, true);
                                return new string[] { "Starting event. Achtung" };
                            }
                        case "BDeath":
                            {
                                new BDeathEvent(plugin, admin, true);
                                return new string[] { "Starting event. Black Death" };
                            }
                        case "VIP":
                            {
                                if(args.Length == 2)
                                {
                                    return new string[] { "This event is Work In Progress.","Use experimental mode to activate it." };
                                } else
                                {
                                    if(args[2] == "-e")
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
                        case "Fight173":
                            {
                                new Fight173Event(plugin, admin, true);
                                return new string[] { "Starting event. Fight with 173" };
                            }
                        case "Blackout":
                            {
                                new DarkGameEvent(plugin, admin, true);
                                return new string[] { "Starting event. Blackout" };
                            }
                        case "Run123":
                            {
                                new Run123Event(plugin, admin, true);
                                return new string[] { "Starting event. Run 1 2 3" };
                            }
                        case "Search":
                            {
                                new SearchEvent(plugin, admin, true);
                                return new string[] { "Starting event. Search" };
                            }
                        case "Apo":
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
                        case "ODay":
                            {
                                new ODayEvent(plugin, admin, true);
                                return new string[] { "Starting event. Oposite day" };
                            }
                        case "Cameleon":
                            {
                                new CameleonEvent(plugin, admin, true);
                                return new string[] { "Starting event. SCP-Cameleon" };
                            }
                        case "Morbus":
                            {
                                new MorbusEvent(plugin, admin, true);
                                return new string[] { "Starting event. Morbus" };
                            }
                        case "Spy":
                            {
                                new SpyEvent(plugin, admin, true);
                                return new string[] { "Starting event. Spy" };
                            }
                        case "Piniata":
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
                        case "Hunt":
                            {
                                new PolowanieEvent(plugin, admin, true);
                                return new string[] { "Starting event. Hunt" };
                            }
                        case "Plaga":
                            {
                                new PlagaEvent(plugin, admin, true);
                                return new string[] { "Starting event. Plaga" };
                            }
                        default:
                            {
                                return new string[] { "Unknown event! ", "Type ForceEvent list to see list of events" };
                            }
                    }
                    //return new string[] { "" };
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
                                    EventManager.InGhostMode_pid.Add(player.PlayerId);
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
                                if (EventManager.InGhostMode_pid.Contains(player.PlayerId))
                                {
                                    toReturn = new string[] { "Player already have Ghost mode off" };
                                    return;
                                }
                                else
                                {
                                    EventManager.InGhostMode_pid.Remove(player.PlayerId);
                                    player.SetGhostMode(false);
                                    EventManager.ToDSC.Info("Ghost Mode disabled for "+player.Name);
                                    toReturn = new string[] { "Done" };
                                }
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
                switch(args[2])
                {
                    case "lvl":
                        {
                            if (args.Length < 3) return new string[] { "Unknown args!", "em controll079 lvl/xp/max_ap/ap/regen_ap [number]" };
                            plugin.Server.GetPlayers(Role.SCP_079).ForEach(player => {
                                player.Scp079Data.Level = Convert.ToInt16(args[3]);
                            });
                            return new string[] { "Done" };
                        }
                    case "xp":
                        {
                            if (args.Length < 3) return new string[] { "Unknown args!", "em controll079 lvl/xp/max_ap/ap/regen_ap [number]" };
                            plugin.Server.GetPlayers(Role.SCP_079).ForEach(player => {
                                player.Scp079Data.Exp = Convert.ToInt16(args[3]);
                            });
                            return new string[] { "Done" };
                        }
                    case "max_ap":
                        {
                            if (args.Length < 3) return new string[] { "Unknown args!", "em controll079 lvl/xp/max_ap/ap/regen_ap [number]" };
                            plugin.Server.GetPlayers(Role.SCP_079).ForEach(player => {
                                player.Scp079Data.MaxAP = Convert.ToInt16(args[3]);
                            });
                            return new string[] { "Done" };
                        }
                    case "ap":
                        {
                            if (args.Length < 3) return new string[] { "Unknown args!", "em controll079 lvl/xp/max_ap/ap/regen_ap [number]" };
                            plugin.Server.GetPlayers(Role.SCP_079).ForEach(player => {
                                player.Scp079Data.AP = Convert.ToInt16(args[3]);
                            });
                            return new string[] { "Done" };
                        }
                    case "regen_ap":
                        {
                            if (args.Length < 3) return new string[] { "Unknown args!", "em controll079 lvl/xp/max_ap/ap/regen_ap [number]" };
                            plugin.Server.GetPlayers(Role.SCP_079).ForEach(player => {
                                player.Scp079Data.APPerSecond = Convert.ToInt16(args[3]);
                            });
                            return new string[] { "Done" };
                        }
                    default:
                        {
                            return new string[] { "Unknown args!", "em controll079 lvl/xp/max_ap/ap/regen_ap [number]" };
                        }
                }
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
            else
            {
                return new string[] { "Unknown Command", GetUsage() };
            }
        }
    }
}
