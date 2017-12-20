using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoWPF.ViewModel
{
    public class GameField : ViewModelBase
    {

        public Int32 FieldValue { get; set; }
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public Boolean Signed { get; set; }
        public DelegateCommand SignCommand { get; set; }



    }
}
