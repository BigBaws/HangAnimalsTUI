using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalgelegTUI.Models
{
   

    public class SingleplayerModel
    {
        public string GameID { get; set; }
        public bool Combo_active { get; set; }
        public int Combo { get; set; }
        public string[] Usedletters { get; set; }
        public string Userid { get; set; }
        public string Word { get; set; }
        public int GameScore { get; set; }
    }
}
