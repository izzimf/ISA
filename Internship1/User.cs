using System;
using System.Collections.Generic;

public class Program
{
    public static int FindLongestSubstringLength(string s)
    {
        int maxLength = 0;
        int left = 0;
        Dictionary<char, int> lastSeen = new Dictionary<char, int>();

        for (int right = 0; right < s.Length; right++)
        {
            char currentChar = s[right];

            if (lastSeen.ContainsKey(currentChar) && lastSeen[currentChar] >= left)
            {
                left = lastSeen[currentChar] + 1;
            }

            lastSeen[currentChar] = right;
            maxLength = Math.Max(maxLength, right - left + 1);
        }

        return maxLength;
    }

    public static void Main()
    {
        string s = "abcabcbb";
        int result = FindLongestSubstringLength(s);
        Console.WriteLine($"Ответ: {result}");
    }
}