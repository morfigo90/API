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
        description = "Event Manager.",
        id = "Gamer.EM.BETA",
        version = "0.9.2",
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
        static internal DateTime T_DD;
        static internal todsc ToDSC = null;
        static internal bool OfflineMode = true;
        static internal bool AllowToEveryone = false;
        static internal bool DNPN = false;
        static internal List<string> mlock = new List<string>();
        static internal List<string> munlock = new List<string>();
        static internal List<string> mopen = new List<string>();
        static internal List<string> mclose = new List<string>();
        static internal List<string> mdestroy = new List<string>();
        static internal bool TranslationsEnabled = false;
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

            #region Config
            this.AddConfig(new Smod2.Config.ConfigSetting("AutoEvent", false, true, "AutoEvent enabled?"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AutoEventRoundCount", 4, true, "Number of round needed to initiate next event"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AutoEventVoteEnd", 75, true, "Number of votes needed to force event end."));
            this.AddConfig(new Smod2.Config.ConfigSetting("HandcuffedLock", false, true, "If player is handcuffed he can't use elevators and doors"));
            this.AddConfig(new Smod2.Config.ConfigSetting("decontaminate_classd", false, true, "After 5 minutes of game class D cells are being decontaminated"));
            this.AddConfig(new Smod2.Config.ConfigSetting("cc_mtf_medic", false, true, "new MTF class"));
            this.AddConfig(new Smod2.Config.ConfigSetting("cc_mtf_tech", false, true, "new MTF class"));
            this.AddConfig(new Smod2.Config.ConfigSetting("cc_fg_sfg", false, true, "new guard class"));
            this.AddConfig(new Smod2.Config.ConfigSetting("ts_enabled", true, true, "Enable translations"));
            TranslationsEnabled = this.GetConfigBool("ts_enabled");
            #endregion
            #region Translations

            this.AddTranslation(new Smod2.Lang.LangSetting("event_ini", "Uruchomiono event", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_nep", "Niewystarczająca ilość graczy by uruchomić event.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_ph", "Ten event jest tylko place holderem!", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_372_scp", "Jesteś SCP 372. Jesteś niewidzialny dopóki nie strzelasz,przeładowywujesze,leczysz się,zmieniasz ustawienie 914,podnosisz item", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_oday", "Transkrypcja:Alarm. Alarm. Doktor Bright wykryty w placówce. Alarm całej strefy uruchomiony. Kod czerwony. Skanowanie w poszukiwaniu uszkodzień placówki.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_win_scp", "Jesteś SCP. Twoje zadanie to nie pozwolić naukowcom uciec.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_win_sci", "Jesteś naukowcem. Twoje zadanie to uciec z placówki.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_plag_scp", "Jesteś SCP. Twoje zadanie to zabić wszystkich MTF przed dekontaminacją LCZ.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_plag_mtf", "Jesteś MTF. Twoje zadanie to przerzyć do dekontaminacji LCZ. Każdy kto umrze staje się SCP", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_hunt_main", "Zadaniem SCP jest zabić MTF i odwrotnie. Jeśli SCP zginie jego zabójca staje się SCP. Powodzenia", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_689_scp", "Jesteś SCP 689", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_343_scp", "Jesteś SCP 343. Jesteś bogiem. Jesteś nieśmiertelny oraz możesz otwierać wszystkie drzwi. Każda broń którą podniesiesz staje się monetą lub czymś innym.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_morbus_d", "Jesteś klasą D. Twoje zadanie to przeżyć. Uważaj na SCP 939 'Matka'. Uruchom wszystkie generatory by zabić SCP 939 i wygrać.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_morbus_m_1", "Jesteś SCP 939 'Matka'. Za 2 minuty otrzymasz kubek. Jeśli go wyrzucisz staniesz się scp 939. Aby zamienić się spowrotem wpisz .z w konsoli pod ~.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_morbus_m_2", "Twoje zadanie to zabic klasy D. Jeśli umrzesz to przegrywasz. Jeśli kogoś zabijesz to staje się on ukrytym 939. Jeśli ukryty 939 umrze staje sie zwykłym 939.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_morbus_m_3", "Jeśli wszystkie generatory zostaną uruchomione to podczas overchargu umrzesz.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_cameleon_m_1", "Jesteś SCP-Kameleon. Twoje zadanie to zabić wszystkich poza SCP. SCP nie mogą cię skrzywdzić(poza teslą).", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_cameleon_m_2", "W ekwipunku masz pistolet,kartę O5,monetę,kartę sprzątacza,tablet.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_cameleon_m_3", "Po wyrzuceniu monety przybierasz postać klasy D, Karta sprzątacza - Naukowiec,Tablet - Tutorial,Masz 3000 hp. Nie możesz zadawać obrażeń w innej formie niż tutorial.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_cameleon_m_4", "Nie możesz ranić SCP. Powodzenia", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_tsl_i", "Jesteś niewinny. Twoje zadanie to pomóc detektywowi zabić zdrajców.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_tsl_d", "Jesteś detektywem. Twoje zadanie to zabić zdrajców.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_tsl_t", "Jesteś zdrajcą. Twoje zadanie to zabić niewinnych i detektywów. Pod ~ możesz sprawdzić kto też jest zdrajcą.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_tsl_tm", "Traitorzy", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_search_d", "Jesteś klasą D. Twoje zadanie to znaleść Micro-HID i uciec z nim. Kto pierwszy ten lepszy. Możecie się zabijać.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_ld", "Uwaga! Mogą wystąpić chwilowe lagi!", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_run123_d", "Jesteś klasą D . Twoje zadanie to uciec", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_run123_scp", "Jesteś SCP 173. Twoje zadanie to zabić klasy D zanim uciekną", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_bo", "Jest to zwykła runda. Prawie.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_fight173_d", "Jesteś klasą D. Twoje zadanie to z przeżyć.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_fight173_scp", "Jesteś 173. Twoje zadanie to zabić klasy D. Za 30 sekund zostaniesz przeniesiony", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_vip_ci", "Jesteś Rebelią Chaosu. Twoje zadanie to nie pozwolić zabic naukowca.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_vip_scp", "Jesteś podmiotem SCP. Twoje zadanie to nie pozwolić zabic naukowca.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_vip_vip", "Jesteś VIP-em twoje zadanie to przeżyć i uciec z pomocą MTF.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_vip_l", "Jesteś Porucznikiem. Twoje zadanie to eskortować naukowca", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_vip_c", "Jesteś Dowódcą. Twoje zadanie to eskortować naukowca", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_bd_scp", "Jesteś Czarną Śmiercią. Twoje zadanie to zabić klasy D zanim uruchomią wszystkie generatory.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_bd_d", "Jesteś klasą D. Twoje zadanie to uruchomić wszystkie generatory by zabić Czarną Śmierć.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_ach_d", "Twoje zadanie to przeżyć. Granaty respią się co 30 sekund a każdy następny sekundę mniej.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_hs_scp", "Za 30 sekund zostaniesz przeniesiony. Twoje zadanie to zabić wszystkie klasy D.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_hs_d", "Jesteś klasą D. Twoje zadanie to przeżyć.", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_whr_d", "Jesteś klasą D. Twoje zadanie to uciec z placówki!", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_apo_d", "Jesteś klasą D. Twoje zadanie uciec. Jeśli umrzesz zostaniesz zombie", "EventManager"));
            this.AddTranslation(new Smod2.Lang.LangSetting("event_apo_scp", "Jesteś zombie. Twoje zadanie to zabić klasy D i nie pozwolić im uciec.", "EventManager"));

            #endregion
        }
    }
}
