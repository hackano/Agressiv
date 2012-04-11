using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Agress.CmdSender.Class;
using Agress.Messages.Commands;
using MassTransit;
using Newtonsoft.Json;

namespace Agress.CmdSender.Modules.EditCommand
{
	public class EditCommandViewModel : INotifyPropertyChanged
	{
		readonly IServiceBus _bus;
		string _cmdEditor;
		string _imageFile;

		public EditCommandViewModel(IServiceBus bus)
		{
			if (bus == null) throw new ArgumentNullException("bus");
			_bus = bus;

			AddInitialData();
			AddNewHeaderCommand = new DelegateCommand(AddNewHeader, _ => true);
			DeleteHeaderCommand = new DelegateCommand(DeleteHeader, _ => true);
			SendCommand = new DelegateCommand(Send, _ => true);
		}

		public ICommand AddNewHeaderCommand { get; set; }
		public ICommand DeleteHeaderCommand { get; set; }
		public ICommand SendCommand { get; set; }


		public ObservableCollection<Header> Headers { get; set; }
		public Dictionary<string, string> CmdTypes { get; set; }


		public string ImageFile
		{
			get { return _imageFile; }
			set
			{
				_imageFile = value;
				PropertyChanged(this, new PropertyChangedEventArgs("ImageFile"));
			}
		}

		public string CmdEditor
		{
			get { return _cmdEditor; }
			set
			{
				_cmdEditor = value;
				PropertyChanged(this, new PropertyChangedEventArgs("CmdEditor"));
			}
		}

		public string CmdType { get; set; }

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		void DeleteHeader(object obj)
		{
			Headers.Remove((Header) obj);
		}

		void AddNewHeader(object obj)
		{
			Headers.Add(new Header("", ""));
		}

		void AddInitialData()
		{
			Headers = new ObservableCollection<Header>();
			Headers.Add(new Header("AGRESSO_USERNAME", ""));
			Headers.Add(new Header("AGRESSO_PASSWORD", ""));

			CmdTypes = typeof (RegisterKnowledgeActivityExpense)
				.Assembly
				.GetTypes()
				.Where(t => t.Namespace.Contains("Command"))
				.ToDictionary(t => t.AssemblyQualifiedName, t => t.FullName);
		}

		public void Send(object obj)
		{
			object msg = ConstructCmd(Type.GetType(CmdType));
			_bus.Publish<RegisterKnowledgeActivityExpense>(msg,
			                                               context =>
			                                               	{
			                                               		foreach (Header header in Headers)
			                                               			context.SetHeader(header.Key, header.Value);
			                                               	});
		}

		private object ConstructCmd(Type t)
		{
			return JsonConvert.DeserializeObject(CmdEditor, t);
		}
	}
}