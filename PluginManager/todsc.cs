using Smod2.Commands;
using Smod2.Events;
using Smod2;
using Smod2.EventHandlers;
using System;
using System.Net;
using System.Collections.Specialized;
using System.Text;

namespace EventManager
{
    class todsc : IEventHandler
    {
        static internal EventManager plugin;
        public todsc(EventManager plugint)
        {
            plugin = plugint;
        }

        public void Denied(Smod2.API.Player admin,string command)
        {
            if (EventManager.OfflineMode) return;
            using (var client = new WebClient())
            {
                string s_type = "";
                if(plugin.Server.Port == 7777)
                {
                    s_type = "PL";
                } else if (plugin.Server.Port == 7778)
                {
                    s_type = "PL RP";
                }
                else if (plugin.Server.Port == 7779)
                {
                    s_type = "EN";
                }
                else if (plugin.Server.Port == 7780)
                {
                    s_type = "EN RP";
                }
                else
                {
                    s_type = "unknown";
                }
                string url = "http://localhost/Mistaken/api/em.php";
                var response = client.UploadValues(url+ "?key=fx7S4SaQtnQ9eE6rVHtbJSx6a5XcaP4thwSYGAwdGFwzG3zTkJ&mode=denied&s_ip=" + plugin.Server.IpAddress+":"+plugin.Server.Port+"&s_type="+s_type+"&a_sid="+admin.SteamId+"&a_name="+admin.Name+"&a_role="+admin.GetRankName()+"&cmd="+command, new NameValueCollection());
                var responseString = Encoding.Default.GetString(response);
                plugin.Debug(responseString);
            }
        }

        public void CommandCalled(Smod2.API.Player admin,Smod2.API.Player player,string mode,string[] args)
        {
            if (EventManager.OfflineMode) return;
            using (var client = new WebClient())
            {
                string s_type = "";
                if (plugin.Server.Port == 7777)
                {
                    s_type = "PL";
                }
                else if (plugin.Server.Port == 7778)
                {
                    s_type = "PL RP";
                }
                else if (plugin.Server.Port == 7779)
                {
                    s_type = "EN";
                }
                else if (plugin.Server.Port == 7780)
                {
                    s_type = "EN RP";
                }
                else
                {
                    s_type = "unknown";
                }
                string url = "http://localhost/Mistaken/api/em.php";
                var response = client.UploadValues(url + "?key=fx7S4SaQtnQ9eE6rVHtbJSx6a5XcaP4thwSYGAwdGFwzG3zTkJ&mode=cmd&s_ip=" + plugin.Server.IpAddress + ":" + plugin.Server.Port + "&s_type=" + s_type + "&a_sid=" + admin.SteamId + "&a_name=" + admin.Name + "&p_sid=" + player.SteamId + "&p_name=" + player.Name + "&args=" + string.Join(" ",args)+"&amode="+mode, new NameValueCollection());
                var responseString = Encoding.Default.GetString(response);
                plugin.Debug(responseString);
            }
        }

        public void Initate(Smod2.API.Player admin, string event_name, bool forced)
        {
            if (EventManager.OfflineMode) return;
            using (var client = new WebClient())
            {
                string s_type = "";
                if (plugin.Server.Port == 7777)
                {
                    s_type = "PL";
                }
                else if (plugin.Server.Port == 7778)
                {
                    s_type = "PL RP";
                }
                else if (plugin.Server.Port == 7779)
                {
                    s_type = "EN";
                }
                else if (plugin.Server.Port == 7780)
                {
                    s_type = "EN RP";
                }
                else
                {
                    s_type = "unknown";
                }
                string url = "http://localhost/Mistaken/api/em.php";
                if(forced)
                {
                    var response = client.UploadValues(url + "?key=fx7S4SaQtnQ9eE6rVHtbJSx6a5XcaP4thwSYGAwdGFwzG3zTkJ&mode=initiate&s_ip=" + plugin.Server.IpAddress + ":" + plugin.Server.Port + "&s_type=" + s_type + "&a_sid=" + admin.SteamId + "&a_name=" + admin.Name + "&e_name=" + event_name + "&e_forced=" + "true", new NameValueCollection());
                    var responseString = Encoding.Default.GetString(response);
                    plugin.Debug(responseString);
                }
                else
                {
                    var response = client.UploadValues(url + "?key=fx7S4SaQtnQ9eE6rVHtbJSx6a5XcaP4thwSYGAwdGFwzG3zTkJ&mode=initiate&s_ip=" + plugin.Server.IpAddress + ":" + plugin.Server.Port + "&s_type=" + s_type + "&a_sid=" + "undefined" + "&a_name=" + "Server" + "&e_name=" + event_name + "&e_forced=" + "false", new NameValueCollection());

                    var responseString = Encoding.Default.GetString(response);
                    plugin.Debug(responseString);
                }
            }
        }

        public void Info(string message)
        {
            if (EventManager.OfflineMode) return;
            using (var client = new WebClient())
            {
                string s_type = "";
                if (plugin.Server.Port == 7777)
                {
                    s_type = "PL";
                }
                else if (plugin.Server.Port == 7778)
                {
                    s_type = "PL RP";
                }
                else if (plugin.Server.Port == 7779)
                {
                    s_type = "EN";
                }
                else if (plugin.Server.Port == 7780)
                {
                    s_type = "EN RP";
                }
                else
                {
                    s_type = "unknown";
                }
                string url = "http://localhost/Mistaken/api/em.php";
                var response = client.UploadValues(url + "?key=fx7S4SaQtnQ9eE6rVHtbJSx6a5XcaP4thwSYGAwdGFwzG3zTkJ&mode=info&s_ip=" + plugin.Server.IpAddress + ":" + plugin.Server.Port + "&s_type=" + s_type + "&message=" + message, new NameValueCollection());
                var responseString = Encoding.Default.GetString(response);
                plugin.Debug(responseString);
            }
        }

        public void Warn(string message)
        {
            if (EventManager.OfflineMode) return;
            using (var client = new WebClient())
            {
                string s_type = "";
                if (plugin.Server.Port == 7777)
                {
                    s_type = "PL";
                }
                else if (plugin.Server.Port == 7778)
                {
                    s_type = "PL RP";
                }
                else if (plugin.Server.Port == 7779)
                {
                    s_type = "EN";
                }
                else if (plugin.Server.Port == 7780)
                {
                    s_type = "EN RP";
                }
                else
                {
                    s_type = "unknown";
                }
                string url = "http://localhost/Mistaken/api/em.php";
                var response = client.UploadValues(url + "?key=fx7S4SaQtnQ9eE6rVHtbJSx6a5XcaP4thwSYGAwdGFwzG3zTkJ&mode=warn&s_ip=" + plugin.Server.IpAddress + ":" + plugin.Server.Port + "&s_type=" + s_type + "&message=" + message, new NameValueCollection());
                var responseString = Encoding.Default.GetString(response);
                plugin.Debug(responseString);
                return;
            }
        }

        public void Error(string message)
        {
            if (EventManager.OfflineMode) return;
            using (var client = new WebClient())
            {
                string s_type = "";
                if (plugin.Server.Port == 7777)
                {
                    s_type = "PL";
                }
                else if (plugin.Server.Port == 7778)
                {
                    s_type = "PL RP";
                }
                else if (plugin.Server.Port == 7779)
                {
                    s_type = "EN";
                }
                else if (plugin.Server.Port == 7780)
                {
                    s_type = "EN RP";
                }
                else
                {
                    s_type = "unknown";
                }
                string url = "http://localhost/Mistaken/api/em.php";
                var response = client.UploadValues(url + "?key=fx7S4SaQtnQ9eE6rVHtbJSx6a5XcaP4thwSYGAwdGFwzG3zTkJ&mode=error&s_ip=" + plugin.Server.IpAddress + ":" + plugin.Server.Port + "&s_type=" + s_type + "&message=" + message, new NameValueCollection());
                var responseString = Encoding.Default.GetString(response);
                plugin.Debug(responseString);
            }
        }
    }
}
