/*
 * Library:         ScrabbleLibrary.dll
 * Module:          Bags.cs
 * Author:          Ruben Dario  Mejia Cardona
 * Date:            January , 2022
 * Description:     Exposed class that implements IBag
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleLibrary
{
    public class Bag : IBag
    {
        /*------------------------ Member Variables ------------------------*/
        private Dictionary<char, int> Tiles;

        /*-------------------------- Constructor ---------------------------*/
        public Bag()
        {
            Tiles = new Dictionary<char, int>();
            PopulateBag();
        }

        /*------------------ Public properties and methods -----------------*/

       /*
        * Method: PopulateBag()
        * Description: populate the Bag with Tails
        */
        private void PopulateBag()
        {
            for (char c = 'a'; c <= 'z'; c++)
                Tiles.Add(c, LetterCount(c));
        }

       /*
        * Method: About
        * Description: display programmer name 
        */
        public string About
        {
            get { return "R Mejia"; }
        }



        public int TilesRemaining
        {
            get
            {
                int sum = 0;
                foreach (KeyValuePair<char, int> tile in Tiles)
                {
                    sum += tile.Value;
                }
                return sum;
            }
        }

       /*
        * Method: ToString
        * Description: returns the current bag as a string
        */
        public override string ToString()
        {
            int cnt = 0;
            string bagSet = "";

            Dictionary<char, int> RemainedTales = new Dictionary<char, int>();

            foreach (KeyValuePair<char, int> items in Tiles)
            {
                if (items.Value != 0)
                {
                    RemainedTales.Add(items.Key, items.Value);
                }
            }

            foreach (KeyValuePair<char, int> kvp in RemainedTales)
            {
                cnt++;
                bagSet += kvp.Key + "(" + kvp.Value + ")\t";
                if (cnt == 13) 
                    bagSet += "\n";
            }
            return bagSet;
        }


       /*------------------------- Helper methods -------------------------*/

       /*
        * Method: LetterCount
        * Description: set of letters to build the Bag
        */
        private int LetterCount(char c)
        {
            Dictionary<char, int> charBag = new Dictionary<char, int>()
            {
            { 'j', 1 },{ 'k', 1 },{ 'q', 1 },{ 'x', 1 },{ 'z', 1 },
            { 'b', 2 },{ 'c', 2 },{ 'f', 2 },{ 'h', 2 },{ 'm', 2 },{ 'p', 2 },{ 'v', 2 },{ 'w', 2 },{ 'y', 2 },
            { 'g', 3 },
            { 'd', 4 },{ 'l', 4 },{ 's', 4 },{ 'u', 4 },
            { 'n', 6 },{ 'r', 6 },{ 't', 6 },
            { 'o', 8 },
            { 'a', 9 },{ 'i', 9 },
            { 'e', 12 }
            };
            return charBag[c];
        }

       /*
        * Method: GetTile
        * Description: returns tile if it exists in bag
        */
        public char GetTile()
        {
            Random rng = new Random();
            int index = rng.Next(Tiles.Count);

            KeyValuePair<char, int> pair = Tiles.ElementAt(index);

            if (Tiles.ElementAt(index).Value == 0)
            {
                return ' ';
            }

            Tiles[pair.Key] -= 1;
            return pair.Key;
        }

       /*
        * Method: NewRack
        * Description: returns a new Rack
        */
        public IRack NewRack()
        {
            return new Rack(this);
        }
    }
}
