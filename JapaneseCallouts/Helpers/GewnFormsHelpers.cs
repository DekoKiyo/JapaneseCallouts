namespace JapaneseCallouts.Helpers;

internal static class GwenFormsHelpers
{
    internal static bool IsOpen(this GwenForm form)
    {
        try
        {
            return form.Window.IsVisible;
        }
        catch
        {
            return false;
        }
    }
}