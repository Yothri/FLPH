using System;
using System.Collections.Generic;
using System.IO;
using CSScriptLib;
using Ether.Network.Photon.Common;

namespace FLPH.Core.Hook
{
    internal class HookManager
    {
        private const string SCRIPTS_DIR = "Hooks";

        public HookManager()
        {
            _hooks = new List<FLPH.Hook>();
        }

        public void LoadHooks()
        {
            _hooks.Clear();

            if (!Directory.Exists(SCRIPTS_DIR))
                Directory.CreateDirectory(SCRIPTS_DIR);
            
            var files = Directory.GetFiles(SCRIPTS_DIR, "*.cs", SearchOption.AllDirectories);

            foreach(var file in files)
            {
                try
                {
                    _hooks.Add(CSScript.Evaluator
                        .ReferenceDomainAssemblies(DomainAssemblies.All)
                        .LoadCode<FLPH.Hook>(File.ReadAllText(file)));
                    Console.WriteLine($"[HookManager] Hook {Path.GetFileNameWithoutExtension(file)} has been loaded.");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            Console.WriteLine($"[HookManager] Loaded {_hooks.Count} hook(s).");
        }

        #region "PDGameServer"
        public byte[] CallClientToGameServerHooks(byte[] packet)
        {
            var result = packet;
            foreach (var hook in Hooks)
                result = hook.ClientToGameServerOverride(result);
            return result;
        }

        public byte[] CallGameServerToClientHooks(byte[] packet)
        {
            var result = packet;
            foreach (var hook in Hooks)
                result = hook.GameServerToClientOverride(result);
            return result;
        }
        #endregion
        #region "WPDGate"
        public string CallClientToGateHooks(string cmd, string data)
        {
            foreach (var hook in Hooks)
                data = hook.ClientToGateOverride(cmd, data);
            return data;
        }

        public string CallGateToClientHooks(string cmd, string data)
        {
            foreach (var hook in Hooks)
                data = hook.GateToClientOverride(cmd, data);
            return data;
        }
        #endregion
        #region "WPDAuth"
        public string CallClientToAuthHooks(string cmd, string data)
        {
            foreach (var hook in Hooks)
                data = hook.ClientToAuthOverride(cmd, data);
            return data;
        }

        public string CallAuthToClientHooks(string cmd, string data)
        {
            foreach (var hook in Hooks)
                data = hook.AuthToClientOverride(cmd, data);
            return data;
        }
        #endregion
        #region "WPDGame"
        public string CallClientToGameHooks(string cmd, string data)
        {
            foreach (var hook in Hooks)
                data = hook.ClientToGameOverride(cmd, data);
            return data;
        }

        public string CallGameToClientHooks(string cmd, string data)
        {
            foreach (var hook in Hooks)
                data = hook.GameToClientOverride(cmd, data);
            return data;
        }
        #endregion

        private readonly List<FLPH.Hook> _hooks;
        public IEnumerable<FLPH.Hook> Hooks => _hooks;
    }
}