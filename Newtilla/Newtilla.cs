using BepInEx;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Newtilla
{
    [BepInPlugin("Lofiat.Newtilla", "Newtilla", "1.0.1")]
    public class Newtilla : BaseUnityPlugin
    {
        internal static Dictionary<string, Action> JoinActions = new Dictionary<string, Action>();
        internal static Dictionary<string, Action> LeaveActions = new Dictionary<string, Action>();
        internal static string currentMode;
        /// <summary>
        /// Subscribe mod initialization to this event
        /// </summary>
        public static event Action<string> OnJoinModded = delegate { };
        /// <summary>
        /// Subscribe mod deinitialization to this event
        /// </summary>
        public static event Action<string> OnLeaveModded = delegate { };
        internal static ModeSelectButtonInfoData PageButtonInfo = new ModeSelectButtonInfoData();
        internal static List<ModeSelectButtonInfoData> GameModes = new List<ModeSelectButtonInfoData>();
        internal static List<KeyValuePair<int, ModeSelectButtonInfoData>> GameModePages = new List<KeyValuePair<int, ModeSelectButtonInfoData>>();
        internal static ModeSelectButtonInfoData[] DisplayedSelectorModes = new ModeSelectButtonInfoData[0];
        internal static int pageLength = 4;
        internal static int maxPage = 0;
        internal static int currentPage = 0;

        void Start()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), Info.Metadata.GUID);
            GorillaTagger.OnPlayerSpawned(OnPlayerSpawned);
            StartCoroutine(WaitForGameModeSelector());
        }

        void OnPlayerSpawned()
        {
            try
            {
                Utilla.Events.TriggerGameInitialized();
            }
            catch
            {

            }
        }

        IEnumerator WaitForGameModeSelector()
        {
            yield return new WaitUntil(() => FindObjectOfType<GameModeSelectorButtonLayout>() != null);
            OnGameModeSelectorInit();
        }

        internal static void TriggerOnJoin(string mode)
        {
            OnJoinModded(mode);
        }

        internal static void TriggerOnLeave(string mode)
        {
            OnLeaveModded(mode);
        }

        void OnGameModeSelectorInit()
        {
            this.AddComponent<RoomManagement>();
            GameModeSelectorButtonLayoutData initialData = FindObjectOfType<GameModeSelectorButtonLayout>().data;
            pageLength = initialData.info.Length;
            foreach (ModeSelectButtonInfoData data in initialData.info)
            {
                GameModes.Insert(initialData.info.ToList().IndexOf(data), data);
            }
            foreach (ModeSelectButtonInfoData mode in initialData.info)
            {
                GameModes.Insert(initialData.info.ToList().IndexOf(mode) + pageLength, new ModeSelectButtonInfoData { Mode = "MODDED_" + mode.Mode, ModeTitle = "MODDED " + mode.ModeTitle.Substring(0, 4), NewMode = mode.NewMode, CountdownTo = mode.CountdownTo });
            }
            foreach (ModeSelectButtonInfoData mode in GameModes)
            {
                GameModePages.Add(new KeyValuePair<int, ModeSelectButtonInfoData>(GameModes.IndexOf(mode) / pageLength, mode));
            }
            maxPage = (GameModes.Count - 1) / pageLength;

            UpdateDisplayedGameModes();
        }

        /// <summary>
        /// Adds a game mode to the game mode selector.
        /// </summary>
        /// <param name="Name">The name that will be displayed on the selector.</param>
        /// <param name="ID">The internal ID, you should perferably keep this simple, for advanced users this is what will show in custom properties</param>
        /// <param name="baseGamemode">The base gamemode for this custom mode, you can alternatively use a string but this is not recommended unless you know what you're doing.</param>
        /// <param name="EnablesMods">Whether or not mods should be enabled in this game mode.</param>
        /// <param name="OnJoin">The method that triggers when this mode is joined.</param>
        /// <param name="OnLeave">The method that triggers when this mode is left.</param>
        public static void AddGameMode(string Name, string ID, BaseGamemode baseGamemode, bool EnablesMods, Action OnJoin = null, Action OnLeave = null)
        {
            GameModes.Add(new ModeSelectButtonInfoData { Mode = EnablesMods ? "MODDED_" : "CUSTOM_" + ID + baseGamemode, ModeTitle = Name});
            if (OnJoin != null)
                JoinActions.Add(EnablesMods ? "MODDED_" : "CUSTOM_" + ID + baseGamemode, OnJoin);
            if (OnLeave != null)
                LeaveActions.Add(EnablesMods ? "MODDED_" : "CUSTOM_" + ID + baseGamemode, OnLeave);
        }

        /// <summary>
        /// Adds a game mode to the game mode selector.
        /// </summary>
        /// <param name="Name">The name that will be displayed on the selector.</param>
        /// <param name="ID">The internal ID, you should perferably keep this simple, for advanced users this is what will show in custom properties</param>
        /// <param name="baseGamemode">The base gamemode for this custom mode, NOTE: YOU SHOULD USE THE "BaseGamemode" ENUM UNLESS YOU KNOW WHAT YOU'RE DOING!</param>
        /// <param name="EnablesMods">Whether or not mods should be enabled in this game mode.</param>
        /// <param name="OnJoin">The method that triggers when this mode is joined.</param>
        /// <param name="OnLeave">The method that triggers when this mode is left.</param>
        public static void AddGameMode(string Name, string ID, string baseGamemode, bool EnablesMods, Action OnJoin = null, Action OnLeave = null)
        {
            GameModes.Add(new ModeSelectButtonInfoData { Mode = EnablesMods ? "MODDED_" : "CUSTOM_" + ID + baseGamemode, ModeTitle = Name });
            if (OnJoin != null)
                JoinActions.Add(EnablesMods ? "MODDED_" : "CUSTOM_" + ID + baseGamemode, OnJoin);
            if (OnLeave != null)
                LeaveActions.Add(EnablesMods ? "MODDED_" : "CUSTOM_" + ID + baseGamemode, OnLeave);
        }

        internal static void UpdateDisplayedGameModes()
        {
            DisplayedSelectorModes = GameModePages.Where(x => x.Key == currentPage).Select(x => x.Value).ToArray();
            foreach (GameModeSelectorButtonLayout gm in FindObjectsOfType<GameModeSelectorButtonLayout>())
            {
                gm.data.info = DisplayedSelectorModes;
                gm.Start();
            }
        }
    }

    public enum BaseGamemode
    {
        CASUAL,
        INFECTION,
        HUNT,
        PAINTBRAWL
    }
}
