using BepInEx;
using System;
using UnityEngine;

namespace iiMenu
{
    [System.ComponentModel.Description(PluginInfo.Description)]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")] // Make sure to add Utilla 1.5.0 as a dependency!
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [ModdedGamemode] // Enable callbacks in default modded gamemodes
    public class Plugin : BaseUnityPlugin
    {
        bool inAllowedRoom = false;

        private void Update()
        {
            if (inAllowedRoom)
            {
                Console.Title = "ii's Stupid Menu // Build " + PluginInfo.Version;
                iiMenu.Patches.Menu.ApplyHarmonyPatches();
                GameObject Loading = new GameObject("ii");
                Loading.AddComponent<iiMenu.UI.Main>();
                Loading.AddComponent<iiMenu.Notifications.NotifiLib>();
                Loading.AddComponent<iiMenu.Classes.CoroutineManager>();
                UnityEngine.Object.DontDestroyOnLoad(Loading);
            }
        }

        [ModdedGamemodeJoin]
        private void RoomJoined(string gamemode)
        {
            // The room is modded. Enable mod stuff.
            inAllowedRoom = true;
        }

        [ModdedGamemodeLeave]
        private void RoomLeft(string gamemode)
        {
            // The room was left. Disable mod stuff.
            inAllowedRoom = false;
        }
    }
}
