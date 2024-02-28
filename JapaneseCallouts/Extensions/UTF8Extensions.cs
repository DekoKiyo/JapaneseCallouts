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

internal static class UTF8Extensions
{
    internal static void PushLongString(string str, int maxLengthUtf8 = 99) => PushLongString(str, PushStringInternal, maxLengthUtf8);

    private static void PushStringInternal(string str)
    {
        NT.HUD.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(str);
    }

    private static void PushLongString(string str, Action<string> action, int maxLengthUtf8 = 99)
    {
        int startPos = 0;
        int currentPos = 0;
        int currentUtf8StrLength = 0;

        while (currentPos < str.Length)
        {
            int codePointSize = 0;

            // Calculate the UTF-8 code point size of the current character
            var chr = str[currentPos];
            if (chr < 0x80) codePointSize = 1;
            else if (chr < 0x800) codePointSize = 2;
            else if (chr < 0x10000) codePointSize = 3;
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
            if (codePointSize is 4) currentPos++;
        }

        if (startPos == 0) action(str);
        else action(str.Substring(startPos, str.Length - startPos));
    }
}