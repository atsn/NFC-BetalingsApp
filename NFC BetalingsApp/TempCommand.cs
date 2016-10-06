using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NFC_BetalingsApp
{
    class TempCommand : ICommand
    {
        private Action excecute;

        public TempCommand(Action excecute)
        {
            this.excecute = excecute;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            excecute();
        }

        public event EventHandler CanExecuteChanged;
    }
}
