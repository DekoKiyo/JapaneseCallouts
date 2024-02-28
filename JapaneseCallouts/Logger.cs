namespace JapaneseCallouts;

internal enum LogType
{
    Info,
    Warn,
    Error,
}

internal static class Logger
{
    internal static void Info(string text, string tag = "") => Log(text, LogType.Info, tag);
    internal static void Warn(string text, string tag = "") => Log(text, LogType.Warn, tag);
    internal static void Error(string text, string tag = "") => Log(text, LogType.Error, tag);

    private static void Log(string text, LogType type = LogType.Info, string tag = "")
    {
        if (!string.IsNullOrEmpty(tag))
        {
            Game.Console.Print($"[{Main.PLUGIN_NAME} - {tag}] {text}");
        }
        else
        {
            Game.Console.Print($"[{Main.PLUGIN_NAME}] {text}");
        }
    }
}