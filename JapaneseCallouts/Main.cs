#region System
global using System;
global using System.Collections.Generic;
global using System.Drawing;
global using System.IO;
global using System.Linq;
global using System.Net;
global using System.Net.Http;
global using System.Media;
global using System.Reflection;
global using System.Runtime.InteropServices;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Threading.Tasks;
global using Timer = System.Timers.Timer;
global using System.Windows.Forms;
global using Math = System.Math;
#endregion
#region IniParser
global using IniParser;
global using IniParser.Model;
global using IniParser.Parser;
#endregion
#region LSPDFR
global using LSPD_First_Response;
global using LSPD_First_Response.Engine.Scripting;
global using LSPD_First_Response.Engine.Scripting.Entities;
global using LSPD_First_Response.Mod.API;
global using Functions = LSPD_First_Response.Mod.API.Functions;
global using LSPD_First_Response.Mod.Callouts;
global using LSPD_First_Response.Mod.Menus;
#endregion
#region Newtonsoft.Json
global using Newtonsoft.Json;
global using Newtonsoft.Json.Linq;
#endregion
#region RAGENativeUI
global using RAGENativeUI;
global using RAGENativeUI.Elements;
global using RAGENativeUI.PauseMenu;
global using Sprite = RAGENativeUI.Elements.Sprite;
#endregion
#region Japanese Callouts
global using BaseLib;
global using JapaneseCallouts;
global using JapaneseCallouts.API;
global using JapaneseCallouts.Callouts;
global using JapaneseCallouts.Configurations;
global using JapaneseCallouts.Debug;
global using JapaneseCallouts.Helpers;
global using JapaneseCallouts.Modules;
#endregion
#region Rage
global using Rage;
global using Rage.Attributes;
global using Rage.ConsoleCommands;
global using Rage.ConsoleCommands.AutoCompleters;
global using Rage.Euphoria;
global using Rage.Exceptions;
global using Rage.Forms;
global using Rage.Native;
global using RageTask = Rage.Task;
global using RObject = Rage.Object;
#endregion
#region NAudio
global using NAudio;
global using NAudio.Wave;
#endregion
#region IPT.Common
global using IPT.Common;
global using IPT.Common.API;
global using IPTFunctions = IPT.Common.API.Functions;
#endregion

global using RequiredPath = (string path, bool isError);

namespace JapaneseCallouts;

internal class Main : Plugin
{
    // Change here if you want to change the version.
    internal const string VERSION = "1.1.0";

    private static readonly RequiredPath[] REQUIRED_DLL_FILES = [
        (@"CalloutInterfaceAPI.dll", true),
        (@"BaseLib.dll", true),
        (@"RawCanvasUI.dll", true),
        (@"RAGENativeUI.dll", true),
        (@"IPT.Common.dll", true)
    ];

    private static readonly RequiredPath[] REQUIRED_FILES_PATH = [
        .. ConfigurationManager.REQUIRED_FILES_PATH.Values,
        ..REQUIRED_DLL_FILES,
        ($"{LSPDFR_DIRECTORY}/{SETTINGS_INI_FILE}", false),
        ($"{PLUGIN_DIRECTORY}/{PLUGIN_AUDIO_DIRECTORY}/{ALARM_SOUND_FILE_NAME}", true),
        ($"{PLUGIN_DIRECTORY}/{PLUGIN_AUDIO_DIRECTORY}/{Conversations.PHONE_CALLING_SOUND}", true),
        ($"{PLUGIN_DIRECTORY}/{PLUGIN_AUDIO_DIRECTORY}/{Conversations.PHONE_BUSY_SOUND}", true),
    ];

    internal const string PLUGIN_NAME = "Japanese Callouts";
    internal const string PLUGIN_NAME_NO_SPACE = "JapaneseCallouts";
    internal const string DEVELOPER_NAME = "DekoKiyo";
    internal const string VERSION_PREFIX = "";

    internal const string LSPDFR_DIRECTORY = @"plugins/LSPDFR";
    internal const string PLUGIN_DIRECTORY = @$"{LSPDFR_DIRECTORY}/{PLUGIN_NAME_NO_SPACE}"; // plugins/LSPDFR/JapaneseCallouts/

    internal const string PLUGIN_AUDIO_DIRECTORY = @"Audio";
    internal const string PLUGIN_LOCALIZATION_DIRECTORY = @"Localization";
    internal const string PLUGIN_JSON_DIRECTORY = @"Json";

    internal const string SETTINGS_INI_FILE = @$"{PLUGIN_NAME_NO_SPACE}.ini";
    internal const string ALARM_SOUND_FILE_NAME = "BankHeistAlarm.wav";

    internal static readonly string PLUGIN_VERSION = Assembly.GetExecutingAssembly().GetName().Version.ToString();
    internal static readonly string PLUGIN_INFO = $"~b~{PLUGIN_NAME}~s~ {PLUGIN_VERSION_DATA}";
    internal static readonly string PLUGIN_VERSION_DATA = $"Version.{VERSION_PREFIX}{PLUGIN_VERSION}";

    internal static string RemoteLatestVersion = PLUGIN_VERSION;

    internal static bool IsSTPRunning = false;
    internal static bool IsUBRunning = false;

    internal static MersenneTwister MT = new((int)DateTime.Now.Ticks);
    internal static Logger Logger = new(PLUGIN_NAME);

    public override void Initialize()
    {
        Functions.OnOnDutyStateChanged += OnDutyStateChanged;
        Logger.Info($"{PLUGIN_NAME} {PLUGIN_VERSION_DATA} was loaded.");
    }

    public override void Finally()
    {
        Logger.Info($"{PLUGIN_NAME} {PLUGIN_VERSION_DATA} was unloaded.");
    }

    private static void OnDutyStateChanged(bool OnDuty)
    {
        if (OnDuty)
        {
            Logger.Info(@"       __                                          ______      ____            __");
            Logger.Info(@"      / /___ _____  ____ _____  ___  ________     / ____/___ _/ / /___  __  __/ /______");
            Logger.Info(@" __  / / __ `/ __ \/ __ `/ __ \/ _ \/ ___/ _ \   / /   / __ `/ / / __ \/ / / / __/ ___/");
            Logger.Info(@"/ /_/ / /_/ / /_/ / /_/ / / / /  __(__  )  __/  / /___/ /_/ / / / /_/ / /_/ / /_(__  )");
            Logger.Info(@"\____/\__,_/ .___/\__,_/_/ /_/\___/____/\___/   \____/\__,_/_/_/\____/\__,_/\__/____/");
            Logger.Info(@"          /_/");
            Logger.Info(string.Empty);
            Logger.Info($"Initializing {PLUGIN_NAME}, {PLUGIN_VERSION_DATA}");
            IsSTPRunning = Functions.GetAllUserPlugins().Any(x => x.GetName().Name == "StopThePed");
            IsUBRunning = Functions.GetAllUserPlugins().Any(x => x.GetName().Name == "UltimateBackup");
            var missing = FileCheck(out bool error);
            if (missing.Length is not 0)
            {
                Logger.Warn($"Some files are missing to load {PLUGIN_NAME}.");
                Logger.Warn("================== Missing Files List ==================");
                foreach (var file in missing) Logger.Warn(file);
                Logger.Warn("================== Missing Files List ==================");
                if (error)
                {
                    Logger.Warn("Plugin won't be loaded.");
                    throw new FileNotFoundException($"Some files that are necessary to load {PLUGIN_NAME} were not found.");
                }
            }
            Remote.Initialize();
            Localization.Initialize();
            // XmlManager.Initialize();
            ConfigurationManager.Initialize();
            BlipPlus.Initialize();
            Game.AddConsoleCommands();
            CalloutBase.RegisterAllCallouts();
            Hud.DisplayNotification(Localization.GetString("PluginLoaded", PLUGIN_NAME, DEVELOPER_NAME), PLUGIN_NAME, PLUGIN_VERSION_DATA);
#if DEBUG
            DebugManager.Initialize();
            Hud.DisplayNotification(Localization.GetString("PluginIsDebug", PLUGIN_NAME, $"{VERSION_PREFIX}{PLUGIN_VERSION}"), PLUGIN_NAME, "");
#endif
            Logger.Info($"{PLUGIN_NAME} {PLUGIN_VERSION_DATA} was successfully initialized.");

            GameFiber.StartNew(() =>
            {
                GameFiber.Yield();
                if (PluginUpdater.CheckUpdate())
                {
                    Logger.Info($"The latest update found. Latest Version: {RemoteLatestVersion}");

                    if (Settings.Instance.EnableAutoUpdate)
                    {
                        PluginUpdater.Update();
                    }
                    else
                    {
                        var answer = Conversations.DisplayQuestionPopup(Localization.GetString("PluginUpdateAvailable", PLUGIN_NAME), new() { { Localization.GetString("UpdateYes"), Keys.D1 }, { Localization.GetString("UpdateNo"), Keys.D2 } }, true);
                        if (answer is 0)
                        {
                            PluginUpdater.Update();
                        }
                        else
                        {
                            Hud.DisplayNotification(Localization.GetString("UpdateManual"), PLUGIN_NAME, "");
                        }
                    }
                }
            });
        }
    }

    private static string[] FileCheck(out bool error)
    {
        error = false;
        var missing = new List<string>();

        foreach (var (path, isError) in REQUIRED_FILES_PATH)
        {
            if (!File.Exists(path))
            {
                missing.Add(path);
                if (!error && isError)
                {
                    error = isError;
                }
            }
        }

        return [.. missing];
    }
}