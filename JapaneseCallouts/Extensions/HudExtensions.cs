/*
MIT License

Copyright (c) 2023 kagikn

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace JapaneseCallouts.Extensions;

internal static class HudExtensions
{
    internal static void DisplayNotification(string text, bool isImportant = false, bool cacheMessage = true)
    {
        var textByteLength = Encoding.UTF8.GetByteCount(text);
        if (textByteLength > 99)
        {
            PostTickerWithLongMessage(text, isImportant, cacheMessage);
        }
        else
        {
            PostTickerWithShortMessage(text, isImportant, cacheMessage);
        }
    }

    internal static void DisplayNotification(string text, string title, string subTitle, string textureDic = "web_lossantospolicedept", string textureName = "web_lossantospolicedept", bool isImportant = false, bool cacheMessage = true)
    {
        var textByteLength = Encoding.UTF8.GetByteCount(text);
        if (textByteLength > 99)
        {
            PostTickerWithLongMessageWithTitle(title, subTitle, text, textureDic, textureName, isImportant, cacheMessage);
        }
        else
        {
            PostTickerWithShortMessageWithTitle(title, subTitle, text, textureDic, textureName, isImportant, cacheMessage);
        }
    }

    internal static void DisplaySubtitle(string message) => DisplaySubtitle(message, 2500);
    internal static void DisplaySubtitle(string message, int duration)
    {
        NT.HUD.BEGIN_TEXT_COMMAND_PRINT("CELL_EMAIL_BCON");
        UTF8Extensions.PushLongString(message);
        NT.HUD.END_TEXT_COMMAND_PRINT(duration, true);
    }

    internal static void DisplayHelp(string message) => DisplayHelp(message, 5000, true);
    internal static void DisplayHelp(string message, bool sound) => DisplayHelp(message, 5000, sound);
    internal static void DisplayHelp(string message, int duration) => DisplayHelp(message, duration, true);
    internal static void DisplayHelp(string message, int duration, bool sound)
    {
        NT.HUD.BEGIN_TEXT_COMMAND_DISPLAY_HELP("CELL_EMAIL_BCON");
        UTF8Extensions.PushLongString(message);
        NT.HUD.END_TEXT_COMMAND_DISPLAY_HELP(0, false, sound, duration);
    }

    private static void PostTickerWithShortMessageWithTitle(string title, string subTitle, string text, string textureDic, string textureName, bool isImportant = false, bool cacheMessage = true)
    {
        NT.HUD.BEGIN_TEXT_COMMAND_THEFEED_POST("STRING");
        NT.HUD.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
        NT.HUD.END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT(textureDic, textureName, false, 0, title, subTitle);
        NT.HUD.END_TEXT_COMMAND_THEFEED_POST_TICKER(isImportant, cacheMessage);
    }

    private static void PostTickerWithLongMessageWithTitle(string title, string subTitle, string text, string textureDic, string textureName, bool isImportant, bool cacheMessage = true)
    {
        // For RPH version, I had to make this custom method because TextExtensions.DisplayNotification doesn't consider non-ASCII characters and it carelessly uses string.Length and not parsing UTF-8 strings at all before splitting into chunks
        NT.HUD.BEGIN_TEXT_COMMAND_THEFEED_POST("CELL_EMAIL_BCON");
        UTF8Extensions.PushLongString(text);
        NT.HUD.END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT(textureDic, textureName, false, 0, title, subTitle);
        NT.HUD.END_TEXT_COMMAND_THEFEED_POST_TICKER(isImportant, cacheMessage);
    }

    private static void PostTickerWithShortMessage(string text, bool isImportant, bool cacheMessage = true)
    {
        NT.HUD.BEGIN_TEXT_COMMAND_THEFEED_POST("STRING");
        NT.HUD.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
        NT.HUD.END_TEXT_COMMAND_THEFEED_POST_TICKER(isImportant, cacheMessage);
    }

    private static void PostTickerWithLongMessage(string text, bool isImportant, bool cacheMessage = true)
    {
        // For RPH version, I had to make this custom method because TextExtensions.DisplayNotification doesn't consider non-ASCII characters and it carelessly uses string.Length and not parsing UTF-8 strings at all before splitting into chunks
        NT.HUD.BEGIN_TEXT_COMMAND_THEFEED_POST("CELL_EMAIL_BCON");
        UTF8Extensions.PushLongString(text);
        NT.HUD.END_TEXT_COMMAND_THEFEED_POST_TICKER(isImportant, cacheMessage);
    }
}