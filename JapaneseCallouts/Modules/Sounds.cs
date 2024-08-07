namespace JapaneseCallouts.Modules;

internal static class Sounds
{
    internal const string PHONE_CALLING_SOUND = "PhoneCalling.wav";
    internal const string PHONE_BUSY_SOUND = "PhoneBusy.wav";

    internal static void PlayPhoneCallingSound(int count)
    {
        PlaySoundLoop($"{Main.PLUGIN_DIRECTORY}/{Main.PLUGIN_AUDIO_DIRECTORY}/{PHONE_CALLING_SOUND}", count);
    }

    internal static void PlayPhoneBusySound(int count)
    {
        PlaySoundLoop($"{Main.PLUGIN_DIRECTORY}/{Main.PLUGIN_AUDIO_DIRECTORY}/{PHONE_BUSY_SOUND}", count);
    }

    private static void PlaySoundLoop(string path, int count)
    {
        GameFiber.StartNew(() =>
        {
            using var file = new AudioFileReader(path);
            using var output = new WaveOutEvent();
            output.Init(file);
            for (int i = 0; i < count; i++)
            {
                output.Play();
                while (output.PlaybackState is PlaybackState.Playing)
                {
                    GameFiber.Yield();
                }
                file.Position = 0;
            }
        });
    }
}