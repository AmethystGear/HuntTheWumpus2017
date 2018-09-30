using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    class Secret
    {
        public string secret { get; }
        public int secretNum { get; }
       
        public Secret(int key, string SecretsFile)
        {
            string lineText = File.ReadLines(SecretsFile).ElementAtOrDefault(key - 1);
            string[] parsedText = lineText.Split(':');
            secret = parsedText[6];
            secretNum = key;
        }
        
    }
}
