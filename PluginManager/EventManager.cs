using Smod2;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Linq;
using Smod2.Config;
using Smod2.Lang;
using System;
using System.Collections.Generic;
using Harmony;
using Smod2.API;

namespace EventManager
{
    [PluginDetails(
        author = "Gamer",
        name = "EventManager",
        description = "Event Manager.",
        id = "Gamer.EM",
        version = "0.11.1",
        configPrefix = "em",
        langFile = "EventManager",
        SmodMajor = 3,
        SmodMinor = 4,
        SmodRevision = 0
        )]


    public class EventManager : Plugin
    {
        static internal List<Event> EventsList = new List<Event>();
        static internal readonly List<string> AllowedRoles = new List<string>() {
            "owner",
            "coowner",
            "headadmin",
            "eventmanager"
        };
        static internal readonly List<string> Events = new List<string>() {
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
            "Plaga",
            "GBreach",
            "DBBR",
            "NeedLuck",
            "963"
        };
        static internal readonly List<string> EventsDescripcion = new List<string>() {
            "(WIN)W imię nauki-Opis na discord",
            "(WHR)WarHead Run-Głowica jest uruchomiana po 1 minucie od startu a zadaniem klas D jest uczieczka. Pierwszy w ucieczce wygrywa.",
            "(Chowany)Chowany-Opis na discord",
            "(Achtung)Achtung-Opis na discord",
            "(BDeath)Czarna Śmierć-Opis na discord",
            "(VIP)V.I.P.-Opis na discord(WIP)",
            "(Fight173)Bitka z 173-Opis na discord",
            "(Blackout)Blackout-Zwykła runda ale z zgaszonymi światłami",
            "(Run123)1 2 3 Idziemy gdy nie patrzysz ty-Opis na discord",
            "(Search)Poszukiwania-Opis na discord",
            "(Apo)Apokalipsa-Opis na discord",
            "(DM)Deathmatch tag-Opis na discord",
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
            "(Plaga)",
            "(GBReach)",
            "(DBBR)",
            "(NeedLuck)"
        };


        static internal readonly bool OfflineMode = true;
        static internal readonly bool AllowToEveryone = true;
        static public readonly bool DNPN = false;


        #region Vars
        static public string NextEvent = "";
        static public Smod2.API.Player NextEvent_Forcer;
        static public string ActiveEvent = "";
        static public bool RoundLocked = false;
        static public bool DisableRespawns = false;
        //static public bool BlackOut = false;
        static public bool ATTK = false;
        static public BlackoutType Blackout_type = BlackoutType.NONE;
        static internal List<int> InGhostMode_pid = new List<int>();
        static internal Smod2.API.Role spectator_role = Smod2.API.Role.UNASSIGNED;
        static public bool RoundStarted = false;
        static public bool TB1 = false;
        static public DateTime T1;
        static public string T1W;
        static public bool TB2 = false;
        static public DateTime T2;
        static public string T2W;
        static internal DateTime T_BO;
        static internal DateTime T_DD;
        static internal todsc ToDSC = null;

        static internal List<string> mlock = new List<string>();
        static internal List<string> munlock = new List<string>();
        static internal List<string> mopen = new List<string>();
        static internal List<string> mclose = new List<string>();
        static internal List<string> mdestroy = new List<string>();
        static public bool TranslationsEnabled = false;

        static public List<Vision> visions = new List<Vision>();

        static public Smod2.API.Player SCP372 = null;

        static internal readonly string EMRed = "(<color=red><b>Event Manager</b></color>)";

        static public Event CurrentEvent;

        #endregion
        public override void OnDisable(){}
        public override void OnEnable()
        {
            ToDSC = new todsc(this);
            this.Info("Event Manager loaded");
        }
        #region Config
        [ConfigOption]
        public static bool enabled = true;

        [ConfigOption]
        public static bool AutoEvent = false;

        [ConfigOption]
        public static int AutoEventRoundCount = 4;

        [ConfigOption]
        public static int AutoEventVoteEnd = 75;

        [ConfigOption]
        public static bool HandcuffedLock = false;

        [ConfigOption]
        public static bool decontaminate_classd = false;

        //[ConfigOption]
        public static bool cc_mtf_medic = false;

        //[ConfigOption]
        public static bool cc_mtf_tech = false;

        //[ConfigOption]
        public static bool cc_fg_sfg = false;

        [ConfigOption]
        public static bool translations_enabled = true;

        [ConfigOption]
        public static bool testmode = false;
        #endregion
        #region Lang
        [LangOption]
        public static string event_ini = "Uruchomiono event";
        [LangOption]
        public static string event_ld = "Uwaga! Mogą wystąpić chwilowe lagi!";
        [LangOption]
        public static string event_nep = "Niewystarczająca ilość graczy by uruchomić event.";
        [LangOption]
        public static string event_ph = "Ten event jest tylko place holderem!";
        [LangOption]
        public static string event_nl_m = "Jesteś klasą D. Twoje zadanie to zabić wszystkie inne klasy D. Każdy dosostał losową ilość losowych itemów. Punkty HP też są losowe";
        #endregion

        public override void Register()
        {
            this.AddEventHandlers(new RoundEventHandler(this));
            this.AddEventHandlers(new SCP372EventHandler(this));

            this.AddCommand("EventManager", new CommandHandler(this));
            this.AddCommand("EM", new CommandHandler(this));

            //Patch.Light.PatchMethodsUsingHarmony();
            #region Config
            /*this.AddConfig(new Smod2.Config.ConfigSetting("AutoEvent", false, true, "AutoEvent enabled?"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AutoEventRoundCount", 4, true, "Number of round needed to initiate next event"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AutoEventVoteEnd", 75, true, "Number of votes needed to force event end."));
            this.AddConfig(new Smod2.Config.ConfigSetting("HandcuffedLock", false, true, "If player is handcuffed he can't use elevators and doors"));
            this.AddConfig(new Smod2.Config.ConfigSetting("decontaminate_classd", false, true, "After 5 minutes of game class D cells are being decontaminated"));
            this.AddConfig(new Smod2.Config.ConfigSetting("cc_mtf_medic", false, true, "new MTF class"));
            this.AddConfig(new Smod2.Config.ConfigSetting("cc_mtf_tech", false, true, "new MTF class"));
            this.AddConfig(new Smod2.Config.ConfigSetting("cc_fg_sfg", false, true, "new guard class"));
            this.AddConfig(new Smod2.Config.ConfigSetting("translations_enabled", true, true, "Enable translations"));
            this.AddConfig(new Smod2.Config.ConfigSetting("testmode", false, true, "Enable testmode"));*/
            TranslationsEnabled = translations_enabled;
            #endregion
            #region Translations
            this.AddTranslation(new Smod2.Lang.LangSetting("event_372_scp", "Jesteś <color=red>SCP 372</color>. Jesteś niewidzialny dopóki nie strzelasz,przeładowywujesze,leczysz się,zmieniasz ustawienie 914,upuszczasz item", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_oday", "Transkrypcja:Alarm. Alarm. Doktor Bright wykryty w placówce. Alarm całej strefy uruchomiony. Kod czerwony. Skanowanie w poszukiwaniu uszkodzień placówki.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_win_scp", "Jesteś <color=red>SCP,/color>. Twoje zadanie to nie pozwolić naukowcom uciec.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_win_sci", "Jesteś <color=yellow>naukowcem</color>. Twoje zadanie to uciec z placówki.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_plag_scp", "<color=red>Jesteś SCP</color>. Twoje zadanie to zabić wszystkich <color=blue>MTF</color> przed dekontaminacją LCZ.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_plag_mtf", "<color=blue>Jesteś MTF</color>. Twoje zadanie to przerzyć do dekontaminacji LCZ. Każdy kto umrze staje się <color=red>SCP</color>", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_hunt_main", "Zadaniem <color=red>SCP</color> jest zabić <color=blue>MTF,/color> i odwrotnie. Jeśli <color=red>SCP</color> zginie jego zabójca staje się <color=red>SCP</color>. Powodzenia", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_689_scp", "Jesteś <color=red>SCP 689</color>", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_343_scp", "Jesteś <color=red>SCP 343</color>. Jesteś bogiem. Jesteś nieśmiertelny oraz możesz otwierać wszystkie drzwi. Każda broń którą podniesiesz staje się monetą lub czymś innym.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_morbus_d", "Jesteś <color=orange>klasą D</color>. Twoje zadanie to przeżyć. Uważaj na <color=red>SCP 939</color> 'Matka'. Uruchom wszystkie generatory by zabić <color=red>SCP 939</color> i wygrać.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_morbus_m_1", "Jesteś <color=red>SCP 939</color> 'Matka'. Za 2 minuty otrzymasz kubek. Jeśli go wyrzucisz staniesz się <color=red>scp 939</color>. Aby zamienić się spowrotem wpisz .z w konsoli pod ~.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_morbus_m_2", "Twoje zadanie to zabic <color=orange>klasy D</color>. Jeśli umrzesz to przegrywasz. Jeśli kogoś zabijesz to staje się on ukrytym <color=red>SCP 939</color>. Jeśli ukryty <color=red>SCP 939</color> umrze staje sie zwykłym <color=red>SCP 939</color>.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_morbus_m_3", "Jeśli wszystkie generatory zostaną uruchomione to podczas overchargu umrzesz.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_cameleon_m_1", "Jesteś <color=red>SCP-Kameleon</color>. Twoje zadanie to zabić wszystkich poza <color=red>SCP</color>. <color=red>SCP</color> nie mogą cię skrzywdzić(poza teslą).", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_cameleon_m_2", "W ekwipunku masz pistolet,kartę O5,monetę,kartę sprzątacza,tablet.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_cameleon_m_3", "Po wyrzuceniu monety przybierasz postać <color=orange>klasy D</color>, Karta sprzątacza - <color=yellow>Naukowiec</color>,Tablet - <color=green>Tutorial</color>,Masz 3000 hp. Nie możesz zadawać obrażeń w innej formie niż <color=green>tutorial</color>.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_cameleon_m_4", "Nie możesz ranić <color=red>SCP</color>. Powodzenia", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_tsl_i", "Jesteś <color=green>niewinny</color>. Twoje zadanie to pomóc <color=blue>detektywowi</color> zabić <color=red>zdrajców</color>.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_tsl_d", "Jesteś <color=blue>detektywem</color>. Twoje zadanie to zabić <color=red>zdrajców</color>.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_tsl_t", "Jesteś <color=red>zdrajcą</color>. Twoje zadanie to zabić <color=green>niewinnych</color> i <color=blue>detektywów</color>. Pod ~ możesz sprawdzić kto też jest <color=red>zdrajcą</color>.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_tsl_tm", "<color=red>Zdrajcy</color>", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_search_d", "Jesteś <color=orange>klasą D</color>. Twoje zadanie to znaleść Micro-HID i uciec z nim. Kto pierwszy ten lepszy. Możecie się zabijać.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_run123_d", "Jesteś <color=orange>klasą D</color> . Twoje zadanie to uciec", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_run123_scp", "Jesteś <color=red>SCP 173</color>. Twoje zadanie to zabić <color=orange>klasy D</color> zanim uciekną", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_bo", "Jest to zwykła runda. Prawie.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_fight173_d", "Jesteś <color=orange>klasą D</color>. Twoje zadanie to z przeżyć.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_fight173_scp", "Jesteś <color=red>SCP 173</color>. Twoje zadanie to zabić <color=orange>klasy D</color>. Za 30 sekund zostaniesz przeniesiony", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_vip_ci", "Jesteś <color=darkgreen>Rebelią Chaosu</color>. Twoje zadanie to nie pozwolić zabic <color=yellow>naukowca</color>.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_vip_scp", "Jesteś podmiotem <color=red>SCP</color>. Twoje zadanie to nie pozwolić zabic <color=yellow>naukowca</color>.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_vip_vip", "Jesteś VIP-em twoje zadanie to przeżyć i uciec z pomocą <color=blue>MTF</color>.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_vip_l", "Jesteś <color=blue>Porucznikiem MTF</color>. Twoje zadanie to eskortować <color=yellow>naukowca</color>", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_vip_c", "Jesteś <color=red>Dowódcą MTF</color>. Twoje zadanie to eskortować <color=yellow>naukowca</color>", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_bd_scp", "Jesteś <color=black>Czarną Śmiercią</color>. Twoje zadanie to zabić <color=orange>klasy D</color> zanim uruchomią wszystkie generatory.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_bd_d", "Jesteś <color=orange>klasą D</color>. Twoje zadanie to uruchomić wszystkie generatory by zabić <color=black>Czarną Śmierć</color>.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_ach_d", "Twoje zadanie to przeżyć. Granaty respią się co 30 sekund a każdy następny sekundę mniej.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_hs_scp", "Za 30 sekund zostaniesz przeniesiony. Twoje zadanie to zabić wszystkie <color=orange>klasy D</color>.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_hs_d", "Jesteś <color=orange>klasą D</color>. Twoje zadanie to przeżyć.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_whr_d", "Jesteś <color=orange>klasą D</color>. Twoje zadanie to uciec z placówki!", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_apo_d", "Jesteś <color=orange>klasą D</color>. Twoje zadanie uciec. Jeśli umrzesz zostaniesz <color=red>zombie</color>", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_apo_scp", "Jesteś <color=red>zombie</color>. Twoje zadanie to zabić <color=red>klasy D</color> i nie pozwolić im uciec.", "EventManager"));

            #endregion

            //EventSystemHandler.LoadEvents(this);
        }
        public struct Event
        {
            public Plugin Plugin { get; set; }
            public string Name { get; set; }
            public string ID { get; set; }
            public string Descripction { get; set; }
            public Version EventManagerVersion { get; set; }
            public Version Version { get; set; }
            public bool Experimental { get; set; }
            public bool CanLagOnStart { get; set; }
            public bool CanLaginRound { get; set; }
            public string Command { get; set; }
        }

        public struct Version
        {
            public int Major;
            public int Minior;
            public int Patch;
        }

        /*public struct WarheadStatus
        {
            public bool CountingDown { get; set; }
            public bool Resumed { get; set; }
            public float TimeLeft { get; set; }
            public bool LeverLock { get; set; }
            public bool StopLock { get; set; }
            public bool StartLock { get; set; }
            public bool ButtonOpen { get; set; }
            public bool ButtonLock { get; set; }
            public bool Detonated { get; set; }
        }
        */

        public struct Vision
        {
            public Smod2.API.Player Player { get; set; }
            public List<Smod2.API.Player> Invisible { get; set; }
        }

        public enum BlackoutType
        {
            HCZ,
            LCZ,
            BOTH,
            NONE
        }
    
        public enum Action
        {
            NONE = -1,
            EnableBlackout = 0,
            DisableBlackout = 1,
            EnableRespawns = 2,
            DisableRespawns = 3,
            EnableRoundLock = 4,
            DisableRoundLock = 5,
            EnableATTK = 6,
            DisableATTK = 7
        }
    }
    /* public abstract class EventManager_Public
    {
        public static void RegiserEvent(Plugin eventobj, bool experimental, bool CanLagOnStart, bool CanLagInRound,string command)
        {
            EventSystemHandler.RegiserEvent(eventobj, experimental, CanLagOnStart, CanLagInRound,command);
        }
    }
    */

    public class Functions
    {
        public static ItemType GetRandomItem()
        {
            int rand = new Random().Next(0, 30);
            switch (rand)
            {
                case 0:
                    {
                        return ItemType.JANITOR_KEYCARD;
                    }
                case 1:
                    {
                        return ItemType.SCIENTIST_KEYCARD;
                    }
                case 2:
                    {
                        return ItemType.MAJOR_SCIENTIST_KEYCARD;
                    }
                case 3:
                    {
                        return ItemType.ZONE_MANAGER_KEYCARD;
                    }
                case 4:
                    {
                        return ItemType.GUARD_KEYCARD;
                    }
                case 5:
                    {
                        return ItemType.SENIOR_GUARD_KEYCARD;
                    }
                case 6:
                    {
                        return ItemType.CONTAINMENT_ENGINEER_KEYCARD;
                    }
                case 7:
                    {
                        return ItemType.MTF_LIEUTENANT_KEYCARD;

                    }
                case 8:
                    {
                        return ItemType.MTF_COMMANDER_KEYCARD;
                    }
                case 9:
                    {
                        return ItemType.FACILITY_MANAGER_KEYCARD;
                    }
                case 10:
                    {
                        return ItemType.CHAOS_INSURGENCY_DEVICE;
                    }
                case 11:
                    {
                        return ItemType.O5_LEVEL_KEYCARD;
                    }
                case 12:
                    {
                        return ItemType.RADIO;
                    }
                case 13:
                    {
                        return ItemType.COM15;
                    }
                case 14:
                    {
                        return ItemType.MEDKIT;
                    }
                case 15:
                    {
                        return ItemType.FLASHLIGHT;
                    }
                case 16:
                    {
                        return ItemType.MICROHID;
                    }
                case 17:
                    {
                        return ItemType.COIN;
                    }
                case 18:
                    {
                        return ItemType.CUP;
                    }
                case 19:
                    {
                        return ItemType.WEAPON_MANAGER_TABLET;
                    }
                case 20:
                    {
                        return ItemType.E11_STANDARD_RIFLE;
                    }
                case 21:
                    {
                        return ItemType.P90;
                    }
                case 22:
                    {
                        return ItemType.DROPPED_5;
                    }
                case 23:
                    {
                        return ItemType.MP4;
                    }
                case 24:
                    {
                        return ItemType.LOGICER;
                    }
                case 25:
                    {
                        return ItemType.FRAG_GRENADE;
                    }
                case 26:
                    {
                        return ItemType.FLASHBANG;
                    }
                case 27:
                    {
                        return ItemType.DISARMER;
                    }
                case 28:
                    {
                        return ItemType.DROPPED_7;
                    }
                case 29:
                    {
                        return ItemType.DROPPED_9;
                    }
                case 30:
                    {
                        return ItemType.USP;
                    }
                default:
                    {
                        return ItemType.NULL;
                    }
            }

        }

        public static bool ExecuteAction(EventManager.Action action)
        {
            if(action == EventManager.Action.NONE)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
    }
}
