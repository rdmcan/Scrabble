/*
 * Library:         ScrabbleLibrary.dll
 * Module:          Rack.cs
 * Author:          Ruben Dario  Mejia Cardona
 * Date:            January , 2022
 * Description:     Hidden class that implements IRack
 *                  Rack objects are constructed by the Bag’s NewRack() Method
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;


namespace ScrabbleLibrary
{
    // Internal type definition
    internal enum EPoints
    {
        a = 1, e = 1, i = 1, l = 1, n = 1, o = 1, r = 1, s = 1, t = 1, u = 1,
        d = 2, g = 2,
        b = 3, c = 3, m = 3, p = 3,
        f = 4, h = 4, v = 4, w = 4, y = 4,
        k = 5,
        j = 8, x = 8,
        q = 10, z = 10,
        NotAvailable
    }

    internal class Rack : IRack
    {
        /*------------------------ Member Variables ------------------------*/
        private Dictionary<char, int> rackTiles;
        private Application app;
        public Bag newBag;


        // Current rack content
        private string rackString;

        // Check candidate word and rack points
        public bool candidateChk = false;
        private int rackPoint;

        /*-------------------------- Constructor ---------------------------*/
        public Rack(Bag bag)
        {
            newBag = bag;

            if (newBag.TilesRemaining > 0)
            {
                // Initialize dictionary and TopUp the rack
                rackTiles = new Dictionary<char, int>();
                TopUp();
            }
        }

       /*------------------ Properties and Methods -----------------*/
       
       /*
        * Method: TotalPoints
        * Description: stores the total points score by word
        */
        int IRack.TotalPoints
        {
            get
            {
                int points = rackPoint;
                return points;
            }
        }

       /*
        * Method: GetPoints
        * Description: get the points from the input word by comparing it with the corresponding value
        */
        public int GetPoints(string candidate)
        {
            char[] charArray = candidate.ToCharArray();
            List<char> candidateList = charArray.ToList<char>();
            List<char> tilesRackTemp = rackTiles.Keys.ToList();

            // Letters of the candidate string are a subset of the letters in the rack
            bool flag = false;
            int acc = 0;
            foreach (var item in candidateList)
            {
                if (candidateList.Count > 7)
                {
                    tilesRackTemp.Clear();
                    break;
                }
                else if (tilesRackTemp.Contains(item))
                {
                    tilesRackTemp.Remove(item);
                    acc++;
                }
            }

            if (tilesRackTemp.Count + acc == rackTiles.Keys.Count)
                flag = true;
            else
                flag = false;

            // A valid word is tested using the Application class’s CheckSpelling() method
            if (flag && CheckSpelling(candidate))
            {
                Quit();
            }
            else
                Environment.Exit(0);

            // Potential point value
            char[] words = candidate.ToCharArray();
            int total = 0;
            foreach (var item in words)
            {
                total += (int)Enum.Parse(typeof(EPoints), item.ToString());
            }

            rackPoint = total;
            return total;
        }

       /*
        * Method: PlayWord
        * Description: if word is received by GetPoints it returns a new rack
        */
        public string PlayWord(string candidate)
        {
            int counter = 0;
            string str = rackString;
           
                foreach (char c in candidate)
                {
                    if (rackTiles.ContainsKey(c))
                    {
                        if (rackTiles[c] > 0)
                            rackTiles[c] -= 1;

                        counter++;
                    }
                    if (rackTiles[c] == 0)
                        rackTiles.Remove(c);
                }

                string tempString = "";
                foreach (KeyValuePair<char, int> kvp in rackTiles)
                {
                    if (kvp.Value > 1)
                    {
                        for (int i = 0; i < kvp.Value - 1; i++)
                        {
                            tempString += kvp.Key;
                        }
                    }
                    tempString += kvp.Key;
                }

                rackString = tempString;
                TopUp();           

            return this.ToString();
        }

       /*
        * Method: TopUp
        * Description: fill in the rack with new tiles from the bag
        */
        public string TopUp()
        {
            int num = 0;
            char value;

            if (rackTiles.Count != 7)
            {
                // total letters 
                int cnt = rackTiles.Skip(0).Sum(x => x.Value);
                
                while (cnt != 7)
                {
                    value = newBag.GetTile();

                    if (value == ' ')
                        continue;

                    //if the actual rack contains this letter
                    if (rackTiles.ContainsKey(value))
                    {
                        rackTiles[value] += 1;
                        cnt++;
                    }
                    else if (!rackTiles.ContainsKey(value))
                    {
                        cnt++;
                        num = 1;
                        rackTiles.Add(value, num);
                    }
                    rackString += value;
                }
            }
            else
                return rackString;

            return ToString();
        }

        /*
         * Method: CheckSpelling
         * Description: Microsoft Word Object Library
         */
        public bool CheckSpelling(string candidate)
        {
            app = new Application();
            bool check = false;
            try
            {
                if (app.CheckSpelling(candidate))
                {
                    return check = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return check;
        }

        /*
         * Method: CheckSpelling
         * Description: terminates correctly manage system resources Word Object
         */
        public void Quit()
        {
            app.Quit();
            //app = null;
        }

        /*
         * Method: ToString
         * Description: return the current content of the rack
         */
        public override string ToString()
        {
            return rackString;
        }
    }
}
