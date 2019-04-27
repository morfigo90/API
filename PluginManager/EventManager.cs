using Smod2;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Linq;
using System;
using System.Collections.Generic;

namespace EventManager
{
    [PluginDetails(
        author = "Gamer",
        name = "EventManager",
        description = "Event Manager . Loaded 19 events",
        id = "Gamer.EM.BETA",
        version = "1.7.1",
        SmodMajor = 3,
        SmodMinor = 4,
        SmodRevision = 0
        )]
    class EventManager : Plugin
    {
        static internal List<string> AllowedRoles = new List<string>() {
            "owner",
            "coowner",
            "headadmin",
            "eventmanager"
        };
        static internal List<string> Events = new List<string>() {
            "WIN",
            "WHR",
            "Chowany",
            "Achtung",
            "BDeath",
            "VIP",
            "Fight173",
            "Blackout",
            "Run123",
            "Search",
            "Apo",
            "DM",
            "TSL",
            "ODay",
            "Cameleon",
            "Morbus",
            "Piniata",
            "Spy",
            "372",
            "343",
            "689",
            "1499",
            "Hunt",
            "Plaga"
        };
        static internal List<string> EventsDescripcion = new List<string>() {
            "(WIN)W imię nauki-Opis na discord",
            "(WHR)WarHead Run-Głowica jest uruchomiana po 1 minucie od startu a zadaniem klas D jest uczieczka. Pierwszy w ucieczce wygrywa.",
            "(Chowany)Chowany-Opis na discord",
            "(Achtung)Achtung-Opis na discord",
            "(BDeath)Czarna Śmierć-Opis na discord",
            "(VIP)V.I.P.-Opis na discord",
            "(Fight173)Bitka z 173-Opis na discord",
            "(Blackout)Blackout-Zwykła runda ale z zgaszonymi światłami",
            "(Run123)1 2 3 Idziemy gdy nie patrzysz ty-Opis na discord",
            "(Search)Poszukiwania-Opis na discord",
            "(Apo)Apokalipsa-Opis na discord",
            "(DM)Deathmatch tag-Opis na discord(WIP)",
            "(TSL)Trouble in Secret Labolatory-zasady TTT",
            "(ODay)Oposite Day-Zwykła runda ale odwrotnie",
            "(Cameleon)SCP-Camelon może zmieniać się pomiędzy Naukowcem/Klasą D/Tutorialem. Jego zadanie to zabicie wszystkich poza SCP. Może zadawać obrażenia tylko jak jest Tutorialem.",
            "(Morbus)Grę zaczynają klasy D a jedna z nich staje się SCP 9 3 9 'matka'.Klasy D muszą przeżyć a zadaniem SCP 9 3 9 jest zabicie ich. Śmierć matki=przegrana SCP",
            "(Piniata)(WIP)",
            "(Spy)(PlaceHolder)",
            "(372)SCP 372 jest nie widzialny i ma za zadanie zabić każdego poza SCP. Podmiot jest widoczny jeśli strzela, przeładowywuje,leczy się i inne.",
            "(343)SCP 343 jest nieśmertelny oraz może otwierać wszystkie drzwi. Każda broń i nie tylko którą podniesie staje się monetą.",
            "(689)Podmiot SCP 689 przenosi się do gracza który jako ostatni przeszedł obok niego.(WIP)",
            "(1499)(PlaceHolder)",
            "(Hunt)",
            "(Plaga)"
        };
        #region Vars
        static internal string ActiveEvent = "";
        static internal bool RoundLocked = false;
        static internal bool DisableRespawns = false;
        static internal bool BlackOut = false;
        static internal List<int> InGhostMode_pid = new List<int>();
        static internal Smod2.API.Role spectator_role = Smod2.API.Role.UNASSIGNED;
        static internal bool RoundStarted = false;
        static internal bool TB1 = false;
        static internal DateTime T1;
        static internal string T1W;
        static internal bool TB2 = false;
        static internal DateTime T2;
        static internal string T2W;
        static internal DateTime T_BO;
        static internal todsc ToDSC = null;
        static internal bool OfflineMode = true;
        static internal bool AllowToEveryone = true;
        static internal bool DNPN = true;
        #endregion
        public override void OnDisable(){}
        public override void OnEnable()
        {
            ToDSC = new todsc(this);
            this.Info("Event Manager loaded");
        }

        public override void Register()
        {
            this.AddEventHandlers(new RoundEventHandler(this));

            this.AddCommand("EventManager", new FEvent(this));
            this.AddCommand("EM", new FEvent(this));

            this.AddConfig(new Smod2.Config.ConfigSetting("AutoEvent", false, true, "AutoEvent enabled?"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AutoEventRoundCount", 4, true, "Number of round needed to initiate next event"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AutoEventVoteEnd", 75, true, "Number of votes needed to force event end."));
            this.AddConfig(new Smod2.Config.ConfigSetting("HandcuffedLock", false, true, "If player is handcuffed he can't use elevators and doors"));
        }
    }
}
