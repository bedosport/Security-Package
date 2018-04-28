using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        public string Encrypt(string plainText, int key)
        {
            char[] arr = {'a' ,'b' ,'c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
            string s = "";
            int indx = 0;
            for (int i=0;i<plainText.Length;i++)
            {
                indx = char.ToLower(plainText[i]) - 'a';
                indx += key;
                indx %= 26;
                s += arr[indx];
            }
            return s;
        }

        public string Decrypt(string cipherText, int key)
        {
            char[] arr = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            string s = "";
            int indx = 0;
            for(int i=0;i<cipherText.Length;i++)
            {
                indx = char.ToLower(cipherText[i]) - 'a';
                indx -= key;
                while(indx<0)
                {
                    indx += 26;
                }
                indx %= 26;
                s += arr[indx];
            }
            return s;
        }

        public int Analyse(string plainText, string cipherText)
        {
            int sz1 = plainText.Length;
            int sz2 = cipherText.Length;
            int indx1, indx2;
            for(int key=1;key<26;key++)
            {
                bool b = true;
               for(int i=0;i<Math.Min(sz1,sz2);i++)
               {
                indx1 = char.ToLower(plainText[i])-'a';
                indx2 = char.ToLower(cipherText[i])-'a';
                if((indx1+key)%26 !=indx2)
                    {
                        b = false;
                        break;
                    }
               }
               if(b)
                {
                    return key;
                }
            }
            return 0;
        }
    }
}
