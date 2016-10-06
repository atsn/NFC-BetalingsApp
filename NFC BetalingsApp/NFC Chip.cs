using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tilbudsapp;

namespace NFC_BetalingsApp
{
    public class NFC_Chip : IWebUri
    {
        public int Chipid { get; set; }
        public int Konto { get; set; }
        public string Name { get; set; }

        public NFC_Chip(int chipid, string name) : this()
        {
            Konto = 0;
            Chipid = chipid;
            Name = name;
        }

        public NFC_Chip()
        {
            ResourceUri = "NFC_Chip";
            VerboseName = "NFC_Chip";
        }

        public string ResourceUri { get; }
        public string VerboseName { get; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Konto)}: {Konto}, {nameof(Chipid)}: {Chipid}";
        }
    }
}
