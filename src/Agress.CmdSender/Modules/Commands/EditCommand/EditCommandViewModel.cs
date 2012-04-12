using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Agress.CmdSender.Messages;
using Agress.Messages.Commands;
using MassTransit;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Agress.CmdSender.Modules.Commands.EditCommand
{
	public class EditCommandViewModel : INotifyPropertyChanged
	{
		private readonly IServiceBus _bus;
		private readonly Dictionary<Type, object> _defaultFor = ConstructDefaultsDictionary();
		string _cmdEditor;
		string _imageFile;

		public EditCommandViewModel(IServiceBus bus)
		{
			if (bus == null) throw new ArgumentNullException("bus");
			_bus = bus;
			AddInitialData();
		}


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

		static Dictionary<Type, object> ConstructDefaultsDictionary()
		{
			return new Dictionary<Type, object>
				{
					{typeof (RegisterKnowledgeActivityExpense), new RKAE()},
					{typeof (RegisterAccessoryExpense), new RAE()},
					//{ typeof()}
				};
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

		public void CommandTypeSelected(object obj)
		{
			Type t = Type.GetType(obj.ToString());
			CmdEditor = JsonConvert.SerializeObject(_defaultFor[t]);
		}

		public void DeleteHeader(object obj)
		{
			Headers.Remove((Header) obj);
		}

		public void AddNewHeader()
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

		public void Send()
		{

			if (CmdType == null)
				return;

			object msg = ConstructCmd(Type.GetType(CmdType));

			_bus.Publish<RegisterKnowledgeActivityExpense>(msg,
			                                               context =>
			                                               	{
			                                               		foreach (Header header in Headers)
			                                               			context.SetHeader(header.Key, header.Value);
			                                               	});
		}

		object ConstructCmd(Type cmdType)
		{
			return JsonConvert.DeserializeObject(CmdEditor, cmdType);
		}
	}
}