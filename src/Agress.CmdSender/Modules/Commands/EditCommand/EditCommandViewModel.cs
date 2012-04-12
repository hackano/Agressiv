using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Agress.CmdSender.Class;
using Agress.CmdSender.Messages;
using Agress.Messages.Commands;
using MassTransit;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Agress.CmdSender.Modules.Commands.EditCommand
{
	public class EditCommandViewModel : INotifyPropertyChanged
	{
		private readonly Dictionary<Type, object> _defaultFor = ConstructDefaultsDictionary();

		static Dictionary<Type, object> ConstructDefaultsDictionary()
		{
			return new Dictionary<Type, object>
				{
					{typeof (RegisterKnowledgeActivityExpense), new RKAE()},
					{ typeof(RegisterAccessoryExpense), new RAE() },
					//{ typeof()}
				};
		}

		private IServiceBus _bus;
		string _cmdEditor;
		string _imageFile;

		public EditCommandViewModel(IServiceBus bus)
		{
			if (bus == null) throw new ArgumentNullException("bus");
			_bus = bus;

			AddInitialData();
			AssignCommands();
		}

		public ICommand AddNewHeaderCommand { get; set; }
		public ICommand DeleteHeaderCommand { get; set; }
		public ICommand SendCommand { get; set; }
		public ICommand TypesSelectedCommand { get; set; }
		public ObservableCollection<Header> Headers { get; set; }
		public Dictionary<string, string> CmdTypes { get; set; }
		public string CmdType { get; set; }


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

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		void AssignCommands()
		{
			AddNewHeaderCommand = new DelegateCommand(AddNewHeader, _ => true);
			DeleteHeaderCommand = new DelegateCommand(DeleteHeader, _ => true);
			SendCommand = new DelegateCommand(Send, _ => true);
			TypesSelectedCommand = new DelegateCommand(CommandTypeSelect, _ => true);
		}

		void AddInitialData()
		{
			Headers = new ObservableCollection<Header>
				{
					new Header("AGRESSO_USERNAME", ""), 
					new Header("AGRESSO_PASSWORD", "")
				};

			CmdTypes = typeof (RegisterKnowledgeActivityExpense)
				.Assembly
				.GetTypes()
				.Where(t => t.Namespace != null && t.Namespace.Contains("Command"))
				.ToDictionary(t => t.AssemblyQualifiedName, t => t.FullName);
		}

		void CommandTypeSelect(object obj)
		{
			Type t = Type.GetType(obj.ToString());
			CmdEditor = JsonConvert.SerializeObject(_defaultFor[t]);
		}

		void DeleteHeader(object obj)
		{
			Headers.Remove((Header) obj);
		}

		void AddNewHeader(object obj)
		{
			Headers.Add(new Header("", ""));
		}

		public void GetImage()
		{
			var dlg = new OpenFileDialog {Filter = "Image Files (*.png, *.jpg)|*.png;*.jpg"};
			bool? result = dlg.ShowDialog();

			if (result == true)
				ImageFile = dlg.FileName;
		}

		public void Send(object obj)
		{
			var msg = ConstructCmd(Type.GetType(CmdType));
			_bus.Publish<RegisterKnowledgeActivityExpense>(msg,
			    context =>
			    {
			        foreach (var header in Headers)
			            context.SetHeader(header.Key, header.Value);
			    });
		}

		object ConstructCmd(Type cmdType)
		{
			return JsonConvert.DeserializeObject(CmdEditor, cmdType);
		}
	}
}