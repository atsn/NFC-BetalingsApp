using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tilbudsapp;

namespace NFC_BetalingsApp
{
    public class KøbsHistorik : IWebUri
    {
        public int Id { get; set; }

        public int Amount { get; set; }

       
        public string Fk_Chipid { get; set; }

        public string ResourceUri { get; }
        public string VerboseName { get; }

        public KøbsHistorik()
        {
            ResourceUri = "KøbsHistorik";
            VerboseName = "KøbsHistorik";
        }
    }
}
