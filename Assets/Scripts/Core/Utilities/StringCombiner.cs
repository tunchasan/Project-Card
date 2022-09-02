using System.Text;

namespace ProjectCard.Core.Utilities
{
    public static class StringCombiner
    {
        private static readonly StringBuilder StringBuilder = new(200);
        
        public static string Combine(string s1, string s2, string s3)
        {
            Clear();
            return StringBuilder.Append(s1).Append(s2).Append(s3).ToString();
        }

        public static string Combine(string s1, string s2, string s3, string s4)
        {
            Clear();
            return StringBuilder.Append(s1).Append(s2).Append(s3).Append(s4).ToString();
        }
        
        public static string Combine(string s1, string s2, string s3, string s4, string s5)
        {
            Clear();
            return StringBuilder.Append(s1).Append(s2).Append(s3).Append(s4).Append(s5).ToString();
        }
        
        public static string Combine(string s1, string s2, string s3, string s4, string s5, string s6)
        {
            Clear();
            return StringBuilder.Append(s1).Append(s2).Append(s3).Append(s4).Append(s5).Append(s6).ToString();
        }

        private static void Clear()
        {
            StringBuilder.Length = 0;
        }
    }
}