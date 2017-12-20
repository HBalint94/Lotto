using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoWPF.Model
{
    public class RandomEventArgs : EventArgs
    {

        public Int32 Number { get; set; }
        public Boolean Bingo { get; set; }
        public RandomEventArgs(Int32 n,Boolean b)
        {
            Number = n;
            Bingo = b;
        }
    }
}
