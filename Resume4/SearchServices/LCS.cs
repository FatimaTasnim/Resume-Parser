using System;
using System.Collections.Generic;
using System.Text;


namespace Resume4.SearchServices
{
    public class LCS
    {
        static int[,] c;

        static int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        static int LongestCommonSubsequence(string s1, string s2)
        {
            for (int i = 1; i <= s1.Length; i++)
                c[i, 0] = 0;
            for (int i = 1; i <= s2.Length; i++)
                c[0, i] = 0;

            for (int i = 1; i <= s1.Length; i++)
                for (int j = 1; j <= s2.Length; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                        c[i, j] = c[i - 1, j - 1] + 1;
                    else
                    {
                        c[i, j] = max(c[i - 1, j], c[i, j - 1]);

                    }

                }

            return c[s1.Length, s2.Length];

        }


        static SortedSet<string> backtrack1(string s1, string s2, int i, int j)
        {
            if (i == 0 || j == 0)
                return new SortedSet<string>() { "" };
            else if (s1[i - 1] == s2[j - 1])
            {
                SortedSet<string> temp = new SortedSet<string>();
                SortedSet<string> holder = backtrack1(s1, s2, i - 1, j - 1);
                if (holder.Count == 0)
                {
                    temp.Add(s1[i - 1].ToString());
                }
                foreach (string str in holder)

                    temp.Add(str + s1[i - 1]);


                return temp;
            }
            else
            {
                SortedSet<string> Result = new SortedSet<string>();
                if (c[i - 1, j] >= c[i, j - 1])
                {
                    SortedSet<string> holder = backtrack1(s1, s2, i - 1, j);

                    foreach (string s in holder)
                        Result.Add(s);
                }

                if (c[i, j - 1] >= c[i - 1, j])
                {
                    SortedSet<string> holder = backtrack1(s1, s2, i, j - 1);

                    foreach (string s in holder)
                        Result.Add(s);
                }

                return Result;
            }
        }

        static List<List<int>> backtrack(string s1, string s2, int i, int j)
        {
            if (i == 0 || j == 0)
                return new List<List<int>>();
            else if (s1[i - 1] == s2[j - 1])
            {
                List<List<int>> temp = new List<List<int>>();
                List<int> keep = new List<int>();
                List<List<int>> holder = backtrack(s1, s2, i - 1, j - 1);
                if (holder.Count == 0)
                {
                    keep.Add(i - 1);
                }
                foreach (List<int> LS in holder)
                {
                    keep = LS;
                    keep.Add(i - 1);
                }

                temp.Add(keep);
                return temp;
            }
            else
            {
                List<List<int>> Result = new List<List<int>>();
                if (c[i - 1, j] >= c[i, j - 1])
                {
                    List<List<int>> holder = backtrack(s1, s2, i - 1, j);

                    foreach (List<int> s in holder)
                        Result.Add(s);
                }

                if (c[i, j - 1] >= c[i - 1, j])
                {
                    List<List<int>> holder = backtrack(s1, s2, i, j - 1);

                    foreach (List<int> s in holder)
                        Result.Add(s);
                }

                return Result;
            }
        }

        static string AddMarker(string s, List<List<int>> LCSresult)
        {
            Dictionary<int, int> mark = new Dictionary<int, int>();
            int[] array = new int[1000006];
            int[] CountOcc = new int[1000006];
            int idx = 0;
            foreach (List<int> ls in LCSresult)
            {
                foreach (int element in ls)
                {
                    array[idx++] = element;
                }
            }
            int id = idx;
            Console.WriteLine("Your index here {0}", idx);
            for (idx = 0; idx < id; idx++)
            {
                CountOcc[array[idx]]++;
                Console.WriteLine("Element {0} , Count {1}", array[idx], CountOcc[array[idx]]);
            }
            Console.WriteLine(s.Length);
            idx = 0;
            StringBuilder str = new StringBuilder();
            foreach (char x in s)
            {
                if (CountOcc[idx] > 0)
                {
                    for (int i = 0; i < CountOcc[idx]; i++)
                    {
                        str.Append("<span style =\"background-color: orange \">");
                        Console.Write("Hi");
                    }
                    str.Append(x);
                    for (int i = 0; i < CountOcc[idx]; i++)
                    {
                        str.Append("</span>");
                        Console.WriteLine("Hello");
                    }
                }
                else str.Append(x);
                idx++;
            }
            return str.ToString();
        }

        public static string SearchFinalResult(string fileData, string Key)
        {
            c = new int[fileData.Length + 1, Key.Length + 1];

            int Length = LongestCommonSubsequence(fileData, Key);
            List<List<int>> st = backtrack(fileData, Key, fileData.Length, Key.Length);
            Console.WriteLine(st.Count);
            SortedSet<string> x = backtrack1(fileData, Key, fileData.Length, Key.Length);
            string result = AddMarker(fileData, st);
            return result;
        }

    }
}
