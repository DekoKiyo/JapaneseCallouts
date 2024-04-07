#region System
global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.Diagnostics;
global using System.Drawing;
global using System.Globalization;
global using System.IO;
global using System.IO.Compression;
global using System.IO.MemoryMappedFiles;
global using System.Linq;
global using System.Linq.Expressions;
global using System.Net;
global using System.Net.Http;
global using System.Net.Http.Headers;
global using System.Media;
global using System.Reflection;
global using System.Reflection.Emit;
global using System.Runtime.CompilerServices;
global using System.Runtime.InteropServices;
global using System.Security.Cryptography;
global using System.Text;
global using System.Text.RegularExpressions;
global using System.Threading;
global using System.Threading.Tasks;
global using Timer = System.Timers.Timer;
global using Task = System.Threading.Tasks.Task;
global using System.Windows.Forms;
global using System.Xml;
global using System.Xml.Linq;
global using System.Xml.Serialization;
global using Debug = System.Diagnostics.Debug;
global using Font = System.Drawing.Font;
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
global using RAGENativeUI.Internals;
global using RAGENativeUI.PauseMenu;
global using Sprite = RAGENativeUI.Elements.Sprite;
#endregion
#region Japanese Callouts
global using JapaneseCallouts;
global using JapaneseCallouts.API;
global using JapaneseCallouts.Callouts;
global using JapaneseCallouts.Helpers;
global using JapaneseCallouts.Modules;
#endregion
#region Rage
global using Rage;
global using Rage.Attributes;
global using Rage.ConsoleCommands;
global using Rage.Euphoria;
global using Rage.Exceptions;
global using Rage.Forms;
global using Rage.Native;
global using static Rage.Native.NativeFunction;
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

namespace JapaneseCallouts;

internal class Main : Plugin
{
    internal const string PLUGIN_NAME = "Japanese Callouts";
    internal const string DEVELOPER_NAME = "DekoKiyo";
    internal const string PLUGIN_VERSION = "0.1.0";
    internal const string VERSION_PREFIX = "Beta.";
    internal const string PLUGIN_INFO = $"~b~{PLUGIN_NAME}~s~ {PLUGIN_VERSION_DATA}";
    internal const string PLUGIN_VERSION_DATA = $"Version.{VERSION_PREFIX}{PLUGIN_VERSION}";
    internal const string LSPDFR_DIRECTORY = @"plugins/LSPDFR";
    internal const string PLUGIN_DIRECTORY = @$"{LSPDFR_DIRECTORY}/JapaneseCallouts";
    internal const string PLUGIN_AUDIO_DIRECTORY = @"Audio";
    internal const string PLUGIN_LANGUAGE_DIRECTORY = @"Languages";
    internal const string SETTINGS_INI_FILE = @"JapaneseCallouts.ini";

    internal const string CALLOUT_INTERFACE_API_DLL = "CalloutInterfaceAPI.dll";

    internal const string ERROR_CALLOUT_INTERFACE_API = "CalloutInterfaceAPI.dll was not found. Some functions automatically disabled.";

    internal static bool IsCalloutInterfaceAPIExist { get; } = Plugins.IsCalloutInterfaceAPIExist();

    internal static MersenneTwister MersenneTwister = new((int)DateTime.Now.Ticks);

    internal static Ped Player
    {
        get => Game.LocalPlayer.Character;
    }

    public override void Initialize()
    {
        Functions.OnOnDutyStateChanged += OnDutyStateChanged;
        Logger.Info($"{PLUGIN_NAME} was loaded.");
    }

    public override void Finally()
    {
        HudHelpers.DisplayNotification(Localization.GetString("PluginUnloaded", PLUGIN_NAME), PLUGIN_NAME, PLUGIN_VERSION_DATA);
        Logger.Info($"{PLUGIN_NAME} was unloaded.");
    }

    private static void OnDutyStateChanged(bool OnDuty)
    {
        if (OnDuty)
        {
            Logger.Info($"Loading {PLUGIN_NAME}, Version.{VERSION_PREFIX}{PLUGIN_VERSION}");
            CheckLibrary();
            CalloutManager.RegisterAllCallouts();
            Logger.Info($"{PLUGIN_NAME} Version.{VERSION_PREFIX}{PLUGIN_VERSION} was loaded.");
            HudHelpers.DisplayNotification(Localization.GetString("PluginLoaded", PLUGIN_NAME, DEVELOPER_NAME), PLUGIN_NAME, PLUGIN_VERSION_DATA);
        }
        else
        {
        }
    }

    private static void CheckLibrary()
    {
        if (!IsCalloutInterfaceAPIExist)
        {
            Logger.Info(ERROR_CALLOUT_INTERFACE_API);
        }
    }
}

[Obsolete("Use \"object\"(System.Object) or \"RObject\"(Rage.Object)")]
internal static class Object { }