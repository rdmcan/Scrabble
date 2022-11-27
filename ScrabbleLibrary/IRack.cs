/*
 * Program:         ScrabbleLibrary.dll
 * Interface:       IRack.cs
 * Author:          Ruben Dario  Mejia Cardona
 * Date:            January , 2022
 * Description:     
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleLibrary
{
    public interface IRack
    {
        public int TotalPoints { get; }

        public int GetPoints(string candidate);

        public string PlayWord(string candidate);

        public string TopUp();

        public string ToString();
    }
}
