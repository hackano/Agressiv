using System;
using System.Collections.Generic;
using System.Windows.Input;
using Agress.CmdSender.Class;
using Agress.Messages.Commands;
using Magnum.Reflection;
using MassTransit;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Agress.CmdSender.Modules.EditCommand
{
	public class EditCommandViewModel
	{
		readonly IServiceBus _bus;

		public ICommand AddNewHeaderCommand { get; set; }


		public EditCommandViewModel(IServiceBus bus)
		{
			if (bus == null) throw new ArgumentNullException("bus");
			_bus = bus;

			AddInitialData();
			AddNewHeaderCommand = new DelegateCommand(AddNewHeader, _ => true);

		}

		void AddNewHeader(object obj)
		{
			Headers.Add(new Header("",""));
		}

		void AddInitialData()
		{
			Headers = new ObservableCollection<Header>();
			Headers.Add(new Header("AGRESSO_USERNAME", ""));
			Headers.Add(new Header("AGRESSO_PASSWORD", ""));
		}

		public ObservableCollection<Header> Headers { get; set; }

		public string CmdEditor { get; set; }
		public string CmdType { get; set; }

		public void Send()
		{
			var msg = ConstructCmd(Type.GetType(CmdType));
			_bus.Publish<RegisterKnowledgeActivityExpense>(msg,
				context =>
					{
						foreach (var header in Headers)
							context.SetHeader(header.Key, header.Value);
					});
		}

		private object ConstructCmd(Type t)
		{
			return JsonConvert.DeserializeObject(CmdEditor, t);
		}
	}
}