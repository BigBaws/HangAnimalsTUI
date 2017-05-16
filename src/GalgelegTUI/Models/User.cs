using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalgelegTUI.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public string Userid { get; set; }
        public double Currency { get; set; }
        public string Study { get; set; }
        public string Animal { get; set; }
        public string AnimalColor { get; set; }
        public string Singleplayer { get; set; }
        public string Multiplayer { get; set; }

        override
        public string ToString()
        {
            return "Name: " + Name + "\nToken: " + Token + "\nUserid: " + Userid + "\nCurrency: " + Currency
                + "\nStudy: " + Study + "\nAnimal: " + Animal + "\nAnimalColor: " + AnimalColor;
        }
    }
}
