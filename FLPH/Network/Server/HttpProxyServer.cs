using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using FLPH.Util;
using Newtonsoft.Json.Linq;
using AppContext = FLPH.Core.AppContext;

namespace FLPH.Network.Server
{
    internal sealed class HttpProxyServer : IDisposable
    {
        public HttpProxyServer()
        {
            Listener = new HttpListener();
            Listener.Prefixes.Add($"http://*:{AppContext.Instance.Configuration.WebProxyPort}/gate/");
            Listener.Prefixes.Add($"http://*:{AppContext.Instance.Configuration.WebProxyPort}/auth/");
            Listener.Prefixes.Add($"http://*:{AppContext.Instance.Configuration.WebProxyPort}/game/");

            GenuineClient = new HttpClient();
        }
        
        public void Start()
        {
            Listener.Start();

            Listener.BeginGetContext(new AsyncCallback(GetContextCallback), Listener);
        }

        private async void GetContextCallback(IAsyncResult ar)
        {
            var listener = ar.AsyncState as HttpListener;

            try
            {
                var context = listener.EndGetContext(ar);

                var splitter = context.Request.RawUrl.Split('/');
                if (splitter[1] == "gate")
                {
                    using (var reader = new StreamReader(context.Request.InputStream))
                    using (var writer = new StreamWriter(context.Response.OutputStream))
                    {
                        var requestContent = reader.ReadToEnd();
                        var Command = JObject.Parse(requestContent)["cmd"].ToObject<string>();
                        //Console.WriteLine($"Handling: {Command}");
                        requestContent = AppContext.Instance.HookManager.CallClientToGateHooks(Command, requestContent);
                        var genuineResponse = await GenuineClient.PostAsync(AppContext.Instance.Configuration.GenuineGateServerAddress, new StringContent(requestContent));
                        var genuineResponseContent = await genuineResponse.Content.ReadAsStringAsync();
                        genuineResponseContent = AppContext.Instance.HookManager.CallGateToClientHooks(Command, genuineResponseContent);
                        writer.Write(genuineResponseContent);
                    }
                }

                if (splitter[1] == "auth")
                {
                    using (var reader = new StreamReader(context.Request.InputStream))
                    using (var writer = new StreamWriter(context.Response.OutputStream))
                    {
                        var requestContent = reader.ReadToEnd();
                        requestContent = WPDUtil.Transform(requestContent, "D");
                        var Command = JObject.Parse(requestContent)["cmd"].ToObject<string>();
                        //Console.WriteLine($"Handling: {Command}");
                        var requestContentPatched = AppContext.Instance.HookManager.CallClientToAuthHooks(Command, requestContent);
                        requestContentPatched = WPDUtil.Transform(requestContentPatched, "E");
                        var genuineResponse = await GenuineClient.PostAsync(AppContext.Instance.Configuration.GenuineAuthServerAddress, new StringContent(requestContentPatched));
                        var genuineResponseContent = await genuineResponse.Content.ReadAsStringAsync();
                        if (EncryptedResponses.Any(r => r == Command))
                        {
                            var genuineResponseContentDecrypted = WPDUtil.Transform(genuineResponseContent, "D");
                            var genuineResponseContentPatched = AppContext.Instance.HookManager.CallAuthToClientHooks(Command, genuineResponseContentDecrypted);
                            genuineResponseContent = WPDUtil.Transform(genuineResponseContentPatched, "E");
                        }
                        else
                            genuineResponseContent = AppContext.Instance.HookManager.CallAuthToClientHooks(Command, genuineResponseContent);

                        writer.Write(genuineResponseContent);
                    }
                }

                if (splitter[1] == "game")
                {
                    using (var reader = new StreamReader(context.Request.InputStream))
                    using (var writer = new StreamWriter(context.Response.OutputStream))
                    {
                        var requestContent = reader.ReadToEnd();
                        var Command = JObject.Parse(requestContent)["cmd"].ToObject<string>();
                        //Console.WriteLine($"Handling: {Command}");
                        requestContent = AppContext.Instance.HookManager.CallClientToGameHooks(Command, requestContent);
                        var genuineResponse = await GenuineClient.PostAsync(AppContext.Instance.Configuration.GenuineGameServerAddress, new StringContent(requestContent));
                        var genuineResponseContent = await genuineResponse.Content.ReadAsStringAsync();
                        genuineResponseContent = AppContext.Instance.HookManager.CallGameToClientHooks(Command, genuineResponseContent);

                        writer.Write(genuineResponseContent);
                    }
                }

                listener.BeginGetContext(new AsyncCallback(GetContextCallback), listener);
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private HttpListener Listener;
        private HttpClient GenuineClient;

        private readonly string[] EncryptedResponses = new string[]
        {
            "CreateGuestUser",
            "CreateFacebookUser",
            "CreateGoogleUser",
            "CreateEntermateUser",
            "Login",
            "LinkingGoogleUser",
            "LinkingFacebookUser"
        };
    }
}