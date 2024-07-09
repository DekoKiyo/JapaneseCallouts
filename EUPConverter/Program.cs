namespace EUPConverter;

internal enum Gender
{
    Male,
    Female
}

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new Main());
    }
}

internal class Preset
{
    internal string[] Hat { get; set; } = ["0", "0"];
    internal string[] Glasses { get; set; } = ["0", "0"];
    internal string[] Ear { get; set; } = ["0", "0"];
    internal string[] Watch { get; set; } = ["0", "0"];
    internal string[] Mask { get; set; } = ["0", "0"];
    internal string[] Top { get; set; } = ["0", "0"];
    internal string[] UpperSkin { get; set; } = ["0", "0"];
    internal string[] Decal { get; set; } = ["0", "0"];
    internal string[] UnderCoat { get; set; } = ["0", "0"];
    internal string[] Pants { get; set; } = ["0", "0"];
    internal string[] Shoes { get; set; } = ["0", "0"];
    internal string[] Accessories { get; set; } = ["0", "0"];
    internal string[] Armor { get; set; } = ["0", "0"];
    internal string[] Parachute { get; set; } = ["0", "0"];

    internal bool isSunny = true;
    internal bool isRainy = false;
    internal bool isSnowy = false;
    internal int pedHealth = 200;
    internal int pedArmor = 200;
}