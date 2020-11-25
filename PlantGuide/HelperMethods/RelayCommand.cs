using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlantGuide.HelperMethods
{
    class RelayCommand : ICommand
    {
        private Action _action;

        private Action<object> _actionP1;

        public RelayCommand(Action action)
        {
            _action = action;
        }

        public RelayCommand(Action<object> action)
        {
            _actionP1 = action;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                _actionP1(parameter);
            }
            else
            {
                _action();
            }
        }

        #endregion
    }
}
