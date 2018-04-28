using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            Dictionary<char, char> mop = new Dictionary<char, char>();
            bool[] visit = new bool[26];
            int sz = plainText.Length;
            for(int i=0;i<sz;i++)
            {
                if(!mop.ContainsKey(char.ToLower(plainText[i])))
                {
                    mop.Add(char.ToLower(plainText[i]), char.ToLower(cipherText[i]));
                    visit[char.ToLower(cipherText[i]) - 'a'] = true;
                }
            }
            for(char c='a';c<='z';c++)
            {
                if(!mop.ContainsKey(c))
                {
                    for (char c1 = 'a'; c1 <= 'z'; c1++)
                    {
                        if(!visit[c1-'a'])
                        {
                            visit[c1 - 'a'] = true;
                            mop.Add(c, c1);
                            break;
                        }
                    }
                }
            }
            string s = "";
            for(char c='a';c<='z';c++)
            {
                s += mop[c];
            }
            return s;
        }

        public string Decrypt(string cipherText, string key)
        {
            Dictionary<char, int> mop = new Dictionary<char, int>();
            int sz = key.Length;
            for (int i=0;i<sz;i++)
            {
                mop.Add(char.ToLower(key[i]), i);
            }
            sz = cipherText.Length;
            string s = "";
            for (int i=0;i<sz;i++)
            {
                char tmp = Convert.ToChar('a' + mop[char.ToLower(cipherText[i])]);
                s += tmp;
            }
            return s;
        }

        public string Encrypt(string plainText, string key)
        {
            int sz = plainText.Length;
            int indx;
            string s = "";
            for (int i=0;i<sz;i++)
            {
                indx = char.ToLower(plainText[i]) - 'a';
                s += key[indx];
            }
            return s;
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
           SortedList<Double, char> Sl = new SortedList<double, char>();
           Sl.Add(12.51, 'E');
           Sl.Add(9.25 , 'T');
           Sl.Add(8.04 , 'A');
           Sl.Add(7.60 , 'O');
           Sl.Add(7.26 , 'I');
           Sl.Add(7.09 , 'N');
           Sl.Add(6.54 , 'S');
           Sl.Add(6.12 , 'R');
           Sl.Add(5.49 , 'H');
           Sl.Add(4.14 , 'L');
           Sl.Add(3.99 , 'D');
           Sl.Add(3.06 , 'C');
           Sl.Add(2.71 , 'U');
           Sl.Add(2.53 , 'M');
           Sl.Add(2.30 , 'F');
           Sl.Add(2.00 , 'P');
           Sl.Add(1.96 , 'G');
           Sl.Add(1.92 , 'W');
           Sl.Add(1.73 , 'Y');
           Sl.Add(1.54 , 'B');
           Sl.Add(0.99 , 'V');
           Sl.Add(0.67 , 'K');
           Sl.Add(0.19 , 'X');
           Sl.Add(0.16 , 'J');
           Sl.Add(0.11 , 'Q');
           Sl.Add(0.09 , 'Z');
            int sz = cipher.Length;
            cipher = cipher.ToLower();
            int[] count = new int[26];
            for (int i=0;i<sz;i++)
            {
                count[cipher[i] - 'a']++;
            }
            List<KeyValuePair<int, char>> Sl1 = new List<KeyValuePair<int, char>>();
            char c = 'a';
            for(int i=0;i<26;i++,c++)
            {
                Sl1.Add(new KeyValuePair<int,char>(count[i], c));
            }
            Sl1.Sort((x, y) => x.Key.CompareTo(y.Key));
            Dictionary<char, char> mop = new Dictionary<char, char>();
            for(int i=0;i<26;i++)
            {
                mop.Add(Sl1.ElementAt(i).Value,char.ToLower(Sl.ElementAt(i).Value));
            }
            string s2 = "";
            for(int i=0;i<sz;i++)
            {
                s2 += mop[cipher[i]];
            }
            return s2;
        }
    }
}
