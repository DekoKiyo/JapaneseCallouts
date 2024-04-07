namespace JapaneseCallouts.Helpers;

internal static class CalloutHelpers
{
    internal static void DisplayTranslatedNotification(string callName, string location)
    {
        var startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalMilliseconds < 100)
        {
            GameFiber.Yield();
            NativeFunction.Natives.THEFEED_FLUSH_QUEUE();
            NativeFunction.Natives.THEFEED_SET_SNAP_FEED_ITEM_POSITIONS(true);
            NativeFunction.Natives.THEFEED_SET_SNAP_FEED_ITEM_POSITIONS(false);
        }
        HudHelpers.DisplayNotification(Localization.GetString("CalloutNotificationText", callName, location), Localization.GetString("Dispatch"), Localization.GetString("CalloutNotificationSubtitle"), "CHAR_CALL911", "CHAR_CALL911");
    }
}