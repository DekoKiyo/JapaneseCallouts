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

namespace JapaneseCallouts.Helpers;

internal static class HudHelpers
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
        NativeFunction.Natives.BEGIN_TEXT_COMMAND_PRINT("CELL_EMAIL_BCON");
        PushLongString(message);
        NativeFunction.Natives.END_TEXT_COMMAND_PRINT(duration, true);
    }

    internal static void DisplayHelp(string message) => DisplayHelp(message, 5000, true);
    internal static void DisplayHelp(string message, bool sound) => DisplayHelp(message, 5000, sound);
    internal static void DisplayHelp(string message, int duration) => DisplayHelp(message, duration, true);
    internal static void DisplayHelp(string message, int duration, bool sound)
    {
        NativeFunction.Natives.BEGIN_TEXT_COMMAND_DISPLAY_HELP("CELL_EMAIL_BCON");
        PushLongString(message);
        NativeFunction.Natives.END_TEXT_COMMAND_DISPLAY_HELP(0, false, sound, duration);
    }

    private static void PostTickerWithShortMessageWithTitle(string title, string subTitle, string text, string textureDic, string textureName, bool isImportant = false, bool cacheMessage = true)
    {
        NativeFunction.Natives.BEGIN_TEXT_COMMAND_THEFEED_POST("STRING");
        NativeFunction.Natives.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
        NativeFunction.Natives.END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT(textureDic, textureName, false, 0, title, subTitle);
        NativeFunction.Natives.END_TEXT_COMMAND_THEFEED_POST_TICKER(isImportant, cacheMessage);
    }

    private static void PostTickerWithLongMessageWithTitle(string title, string subTitle, string text, string textureDic, string textureName, bool isImportant, bool cacheMessage = true)
    {
        // For RPH version, I had to make this custom method because TextExtensions.DisplayNotification doesn't consider non-ASCII characters and it carelessly uses string.Length and not parsing UTF-8 strings at all before splitting into chunks
        NativeFunction.Natives.BEGIN_TEXT_COMMAND_THEFEED_POST("CELL_EMAIL_BCON");
        PushLongString(text);
        NativeFunction.Natives.END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT(textureDic, textureName, false, 0, title, subTitle);
        NativeFunction.Natives.END_TEXT_COMMAND_THEFEED_POST_TICKER(isImportant, cacheMessage);
    }

    private static void PostTickerWithShortMessage(string text, bool isImportant, bool cacheMessage = true)
    {
        NativeFunction.Natives.BEGIN_TEXT_COMMAND_THEFEED_POST("STRING");
        NativeFunction.Natives.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
        NativeFunction.Natives.END_TEXT_COMMAND_THEFEED_POST_TICKER(isImportant, cacheMessage);
    }

    private static void PostTickerWithLongMessage(string text, bool isImportant, bool cacheMessage = true)
    {
        // For RPH version, I had to make this custom method because TextExtensions.DisplayNotification doesn't consider non-ASCII characters and it carelessly uses string.Length and not parsing UTF-8 strings at all before splitting into chunks
        NativeFunction.Natives.BEGIN_TEXT_COMMAND_THEFEED_POST("CELL_EMAIL_BCON");
        PushLongString(text);
        NativeFunction.Natives.END_TEXT_COMMAND_THEFEED_POST_TICKER(isImportant, cacheMessage);
    }

    internal static void PushLongString(string str, int maxLengthUtf8 = 99) => PushLongString(str, PushStringInternal, maxLengthUtf8);

    private static void PushStringInternal(string str)
    {
        NativeFunction.Natives.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(str);
    }

    private static void PushLongString(string str, Action<string> action, int maxLengthUtf8 = 99)
    {
        var startPos = 0;
        var currentPos = 0;
        var currentUtf8StrLength = 0;

        while (currentPos < str.Length)
        {
            var codePointSize = 0;

            // Calculate the UTF-8 code point size of the current character
            var chr = str[currentPos];
            if (chr < 0x80)
            {
                codePointSize = 1;
            }
            else if (chr < 0x800)
            {
                codePointSize = 2;
            }
            else if (chr < 0x10000)
            {
                codePointSize = 3;
            }
            else
            {
                #region Surrogate check
                const int LowSurrogateStart = 0xD800;
                const int HighSurrogateStart = 0xD800;

                var temp1 = chr - HighSurrogateStart;
                if (temp1 >= 0 && temp1 <= 0x7ff)
                {
                    // Found a high surrogate
                    if (currentPos < str.Length - 1)
                    {
                        var temp2 = str[currentPos + 1] - LowSurrogateStart;
                        if (temp2 >= 0 && temp2 <= 0x3ff)
                        {
                            // Found a low surrogate
                            codePointSize = 4;
                        }
                    }
                }
                #endregion
            }

            if (currentUtf8StrLength + codePointSize > maxLengthUtf8)
            {
                action(str.Substring(startPos, currentPos - startPos));

                startPos = currentPos;
                currentUtf8StrLength = 0;
            }
            else
            {
                currentPos++;
                currentUtf8StrLength += codePointSize;
            }

            // Additional increment is needed for surrogate
            if (codePointSize is 4)
            {
                currentPos++;
            }
        }

        if (startPos == 0)
        {
            action(str);
        }
        else
        {
            action(str.Substring(startPos, str.Length - startPos));
        }
    }
}