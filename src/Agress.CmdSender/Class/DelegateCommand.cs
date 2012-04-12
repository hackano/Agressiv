using System;
using System.Windows.Input;

namespace Agress.CmdSender.Class
{
	internal class DelegateCommand : ICommand


	{
		private readonly Func<object, bool> canExecute;
		private readonly Action<object> executeAction;


		private bool canExecuteCache;

		public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute)
		{
			this.executeAction = executeAction;
			this.canExecute = canExecute;
		}

		#region ICommand Members

		public bool CanExecute(object parameter)
		{
			bool temp = canExecute(parameter);
			if (canExecuteCache != temp)
			{
				canExecuteCache = temp;
				if (CanExecuteChanged != null)
				{
					CanExecuteChanged(this, new EventArgs());
				}
			}
			return canExecuteCache;
		}


		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			executeAction(parameter);
		}

		#endregion
	}
}