/*
 * Program:         ScrabbleLibrary.dll
 * Interface:       IBags.cs
 * Author:          Ruben Dario  Mejia Cardona
 * Date:            January , 2022
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleLibrary
{
    public interface IBag
    {
        public string About { get; }

        public int TilesRemaining { get; }

        public IRack NewRack();

        public string ToString();
    }
}
