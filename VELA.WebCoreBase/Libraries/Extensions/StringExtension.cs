using System.Globalization;
using System.Text;

namespace VELA.WebCoreBase.Libraries.Extensions;

public static class StringExtension
{
    private static readonly Random StaticRandom = new();

    public static string RandomDigitsLength(int length = 1)
    {
        StringBuilder strBuilder = new();
        for (int i = 0; i < length; i++)
        {
            strBuilder.Append(StaticRandom.Next(0, 10)); // append a random digit from 0 to 9
        }

        return strBuilder.ToString();
    }

    public static bool ContainsAny(this string source, params string[] @params)
    {
        if (string.IsNullOrEmpty(source))
        {
            return false;
        }

        return source.Split(',', '-', '-', '|').Any(@params.Contains);
    }

    public static string Capitalize(this string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return s;
        }

        return s[0].ToString().ToUpper() + s.Substring(1);
    }

    public static string Right(this string str, int length)
    {
        return str.Substring(str.Length - length, length);
    }
    public static string Left(this string str, int length)
    {
        return str.Substring(0, length);
    }

    public static string RemoveVietnamese(string input)
    {
        string normalizedString = input.Normalize(NormalizationForm.FormD).ToUpper();
        StringBuilder stringBuilder = new();

        foreach (char c in normalizedString)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }

        return stringBuilder.ToString();
    }

    public static string ValidBillingCycle(string first)//1-1
    {
        int first1 = int.Parse(first.Split('-')[0]);
        int first2 = int.Parse(first.Split('-')[1]);

        //if (first1 <= 0 || first1 >= first2 || first2 > 31)
        //{
        //    return first;
        //}

        if (first1 == first2)
        {
            return first;
        }

        if (first1 > 31 || first2 > 31 || first1 == 0 || first2 == 0)
        {
            return first;
        }

        return string.Empty;
    }

    public static string ValidBillingCycle_bk(string first, string second)//1-4, 2-3
    {
        int first1 = int.Parse(first.Split('-')[0]);
        int first2 = int.Parse(first.Split('-')[1]);

        int second1 = int.Parse(second.Split('-')[0]);
        int second2 = int.Parse(second.Split('-')[1]);

        if (first1 <= 0 || first1 >= first2 || first2 > 31)
        {
            return first;
        }
        if (second1 >= second2 || second2 > 31)
        {
            return second;
        }

        if (second1 <= first2 && second2 >= first1)
        {
            return second;
        }

        return string.Empty;
    }

    public static string ValidBillingCycle(string first, string second)//1-4, 2-3,10-25, 26-09
    {
        int first1 = int.Parse(first.Split('-')[0]);
        int first2 = int.Parse(first.Split('-')[1]);

        int second1 = int.Parse(second.Split('-')[0]);
        int second2 = int.Parse(second.Split('-')[1]);

        if (first1 == first2)
        {
            return first;
        }
        if (second1 == second2)
        {
            return second;
        }
        if (first1 > 31 || first2 > 31)
        {
            return first;
        }
        if (second1 > 31 || second2 > 31)
        {
            return second;
        }
        if (second1 == first2)
        {
            return second;
        }
        if (second1 > first2 && second1 < first1)//[31-20,21-30]
        {
            if (second2 <= first2)
            {
                return second;
            }
            if (second2 > first2 && second2 >= first1)
            {
                return second;
            }
        }
        if (second1 < first1 && second1 < first2 && first1 > first2)//[26-20,1-19]
        {
            if (second2 <= first2)
            {
                return second;
            }
        }
        if (second1 > first1 && second1 > first2)
        {
            if (second2 < second1 && (second2 >= first1 || second2 >= first2))
            {
                return second;
            }
        }
        if (first1 > first2)
        {
            if (second1 <= first2)
            {
                return second;
            }
        }
        if (first1 < first2)
        {
            if (second1 < second2 && second1 < first2)
            {
                if (second2 >= first1)
                {
                    return second;
                }
            }
        }
        else
        {
            string checkSecound = SecoundOutFirst(first, second);
            if (!string.IsNullOrEmpty(checkSecound))
            {
                return checkSecound;
            }
        }
        return string.Empty;
    }

    private static string SecoundOutFirst(string first, string second)
    {
        int first1 = int.Parse(first.Split('-')[0]);
        int first2 = int.Parse(first.Split('-')[1]);

        int second1 = int.Parse(second.Split('-')[0]);
        int second2 = int.Parse(second.Split('-')[1]);

        if (Enumerable.Range(first1, first2).Contains(second1) ||
                  Enumerable.Range(first1, first2).Contains(second2))
        {
            return second;
        }

        return "";
    }

    public static string GetFileName(this string objectKey)
    {
        if (string.IsNullOrEmpty(objectKey))
        {
            return string.Empty;
        }
        string[] parts = objectKey.Split('/');

        return parts[parts.Length - 1];
    }
}