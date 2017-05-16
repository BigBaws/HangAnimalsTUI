using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalgelegTUI.Models
{
    public class Guess
    {

        private string Letters = "";
        public int Score { get; set; }
        public string[] Used { get; set; }
        public string Word { get; set; }
        public int Wrongs { get; set; }


        public String PrintUsed()
        {
            foreach(string used in Used)
            {
                Letters += used + ", ";
            }
            return Letters;
        }
    }
}
