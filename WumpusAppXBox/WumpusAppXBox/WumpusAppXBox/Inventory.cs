using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    class Inventory
    {
        int gold;
        int arrows;
        List<Secret> secrets = new List<Secret>();
        
        public Inventory (int startGold, int startArrows)
        {
            gold = startGold;
            arrows = startArrows;
        }

        public Secret [] getSecrets()
        {
            return secrets.ToArray();
        }

        public void addSecret(Secret s)
        {
            int n = 0;
            while (n < secrets.Count && secrets[n].secretNum < s.secretNum)
            {
                n++;
            }
            
            secrets.Insert(n, s);
            Console.WriteLine(secrets.Count);
        }

        public int getGold ()
        {
            return gold;
        }

        public int getArrows()
        {
            return arrows;
        }

        public void changeGold(int deltaGold)
        {
            gold += deltaGold;
        }

        public void changeArrows(int deltaArrows)
        {
            arrows += deltaArrows;
        }

        public void setGold (int gold)
        {
            this.gold = gold;
        }

        public void setArrows(int arrows)
        {
            this.arrows = arrows;
        }

        public string getSecretsAsString()
        {
            string secretsString = "";
            for (int i = 0; i < secrets.Count; i++)
            {
                secretsString += "Secret #" + secrets[i].secretNum + ": " + secrets[i].secret + " \r\n";
            }
            return secretsString;
        }
    }
}
