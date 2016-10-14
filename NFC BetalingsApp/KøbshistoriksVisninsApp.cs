using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFC_BetalingsApp
{
    public class KøbshistoriksVisninsApp
    {
        public int Id { get; set; }

        public int Amount { get; set; }
        
        public string Fk_Chipid { get; set; }
        public string Name { get; set; }


        public KøbshistoriksVisninsApp(int id, int amount, string fkChipid, string name)
        {
            Id = id;
            Amount = amount;
            Fk_Chipid = fkChipid;
            Name = name;
        }
        public KøbshistoriksVisninsApp()
        {
            
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Fk_Chipid)}: {Fk_Chipid}, {nameof(Name)}: {Name}, {nameof(Amount)}: {Amount}";
        }
    }
}
