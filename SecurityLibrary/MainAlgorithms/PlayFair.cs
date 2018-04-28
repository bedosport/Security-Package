using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    
    public class PlayFair : ICryptographicTechnique<string, string>
    {
        private char[,] Grid;
        private Dictionary<char, KeyValuePair<int, int>> indexies;
        private void Fill_matrix(string key)
        {
            bool[] visit = new bool[26];
            int cur_pos = 0, key_size = key.Length;
            char c = 'a';
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (cur_pos < key_size)
                    {
                        if (!visit[key[cur_pos] - 'a'] && key[cur_pos] != 'i' && key[cur_pos] != 'j')
                        {
                            Grid[i, j] = key[cur_pos];
                            indexies[key[cur_pos]] = new KeyValuePair<int, int>(i, j);
                            visit[key[cur_pos] - 'a'] = true;
                        }
                        else if (key[cur_pos] == 'i' || key[cur_pos] == 'j')
                        {
                            if (!visit['i' - 'a'])
                            {
                                Grid[i, j] = 'i';
                                indexies['i'] = new KeyValuePair<int, int>(i, j);
                                indexies['j'] = new KeyValuePair<int, int>(i, j);
                                visit['i' - 'a'] = true;
                            }
                        }
                        else
                        {
                            j--;
                        }
                        cur_pos++;
                    }
                    else
                    {
                        if (!visit[c - 'a'] && c != 'i' && c != 'j')
                        {
                            Grid[i, j] = c;
                            indexies[c] = new KeyValuePair<int, int>(i, j);
                            visit[c - 'a'] = true;
                        }
                        else if (c == 'i' || c == 'j')
                        {
                            if (!visit['i' - 'a'])
                            {
                                Grid[i, j] = 'i';
                                indexies['i'] = new KeyValuePair<int, int>(i, j);
                                indexies['j'] = new KeyValuePair<int, int>(i, j);
                                visit['i' - 'a'] = true;
                            }
                            else
                            {
                                j--;
                            }
                        }
                        else
                        {
                            j--;
                        }
                        c++;
                    }
                }
            }
        }
        public string Decrypt(string cipherText, string key)
        {
            Grid = new char[5, 5];
            indexies = new Dictionary<char, KeyValuePair<int, int>>();
            key = key.ToLower();
            cipherText = cipherText.ToLower();
            Fill_matrix(key);
            string plainText = "";
            int col1, col2, row1, row2;
            int sz = cipherText.Length;
            for (int i = 0; i + 1 < sz; i += 2)
            {
                row1 = indexies[cipherText[i]].Key;
                col1 = indexies[cipherText[i]].Value;
                row2 = indexies[cipherText[i + 1]].Key;
                col2 = indexies[cipherText[i + 1]].Value;
                if (row1 == row2)
                {
                    plainText += Grid[row1, (col1 - 1 + 5) % 5];
                    plainText += Grid[row2, (col2 - 1 + 5) % 5];
                }
                else if (col1 == col2)
                {
                    plainText += Grid[(row1 - 1 + 5 ) % 5, col1];
                    plainText += Grid[(row2 - 1 + 5 ) % 5, col2];
                }
                else
                {
                    plainText += Grid[row1, col2];
                    plainText += Grid[row2, col1];
                }
            }
            string new_pt = "";
            for (int i = 0; i < sz; i++)
            {
                if (plainText[i] == 'x' && i%2 ==1)
                {
                    if (i - 1 >= 0 && i + 1 < sz && plainText[i - 1] == plainText[i + 1] && i%2==1)
                    {
                        continue;
                    }
                    else
                    {
                        new_pt += plainText[i];
                    }
                }
                else
                {
                    new_pt += plainText[i];
                }
            }
            if (new_pt[new_pt.Length-1]=='x')
            {
                new_pt = new_pt.Remove(new_pt.Length- 1);
            }
            return new_pt;
        }
        public string Encrypt(string plainText, string key)
        {
            Grid = new char[5, 5];
            indexies = new Dictionary<char, KeyValuePair<int, int>>();
            key = key.ToLower();
            plainText = plainText.ToLower();
            Fill_matrix(key);
            int sz = plainText.Length;
            for (int i=0;i+1<sz;i+=2)
            {
                if(plainText[i]==plainText[i+1])
                {
                    plainText=plainText.Insert(i + 1, "x");
                    sz++;
                }
            }
            if(sz%2==1)
            {
                plainText += 'x';
                sz++;
            }
            int col1, col2, row1, row2;
            string cipherText = "";
            for(int i=0;i+1<sz;i+=2)
            {
                row1 = indexies[plainText[i]].Key;
                col1 = indexies[plainText[i]].Value;
                row2 = indexies[plainText[i+1]].Key;
                col2 = indexies[plainText[i+1]].Value;
                if(row1==row2)
                {
                    cipherText += Grid[row1, (col1 + 1) % 5];
                    cipherText += Grid[row2, (col2 + 1) % 5];
                }
                else if(col1==col2)
                {
                    cipherText += Grid[(row1 + 1) % 5, col1];
                    cipherText += Grid[(row2 + 1) % 5, col2];
                }
                else
                {
                    cipherText += Grid[row1 , col2];
                    cipherText += Grid[row2 , col1];
                }
            }
            return cipherText;
        }
        public string Analyse(string plainText, string cipherText)
        {
            throw new NotImplementedException();
        }
    }
}
