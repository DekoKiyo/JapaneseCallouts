using CalloutInterfaceAPI;
using CalloutInterfaceAPI.Records;
using CIFunctions = CalloutInterfaceAPI.Functions;
using CIEvents = CalloutInterfaceAPI.Events;

namespace JapaneseCallouts.API;

internal static class CalloutInterfaceAPIFunctions
{
    internal static bool IsCalloutInterfaceAvailable
    => CIFunctions.IsCalloutInterfaceAvailable;

    internal static void SendMessage(Callout callout, string message)
    => CIFunctions.SendMessage(callout, message);

    internal static void SendVehicle(Vehicle vehicle)
    => CIFunctions.SendVehicle(vehicle);

    internal static string GetColorName(Color color)
    => CIFunctions.GetColorName(color);

    internal static DateTime GetDateTime()
    => CIFunctions.GetDateTime();
}