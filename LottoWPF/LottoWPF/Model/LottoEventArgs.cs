using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoWPF.Model
{
    public class LottoEventArgs:EventArgs
    {
        public Int32 X { get; set; }
        public Int32 Y { get; set; }

        public LottoEventArgs(Int32 x,Int32 y)
        {
            X = x;
            Y = y;
        }
    }
}
