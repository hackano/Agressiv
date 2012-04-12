// Copyright 2012 Henrik Feldt, Håkan Hedenström
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Agress.CmdSender.Messages;
using Agress.Messages.Commands;
using Caliburn.Micro;
using MassTransit;
using MassTransit.Util;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Agress.CmdSender.Modules.Commands.EditCommand
{
	public class EditCommandViewModel : PropertyChangedBase
	{
		readonly IServiceBus _bus;
		readonly Credentials _creds;
		Type _selectedCmd;

		readonly Dictionary<Type, object> _defaultFor = ConstructDefaultsDictionary();

		static Dictionary<Type, object> ConstructDefaultsDictionary()
		{
			return new Dictionary<Type, object>
				{
					{typeof (RegisterKnowledgeActivityExpense), new RKAE()},
					{typeof (RegisterAccessoryExpense), new RAE()},
					//{ typeof()}
				};
		}

		public EditCommandViewModel([NotNull] IServiceBus bus, [NotNull] Credentials creds)
		{
			if (bus == null) throw new ArgumentNullException("bus");
			if (creds == null) throw new ArgumentNullException("creds");
			_bus = bus;
			_creds = creds;
			AddInitialData();
		}

		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		public ObservableCollection<Header> Headers { get; set; }

		string _imageFile;

		public string ImageFile
		{
			get { return _imageFile; }
			set
			{
				_imageFile = ToUri(value);
				NotifyOfPropertyChange(() => ImageFile);
			}
		}

		static string ToUri(string value)
		{
			return new Uri("file:///" + Uri.EscapeUriString(value.Replace("\\", "/"))).ToString();
		}

		string _cmdEditor;
		IDictionary<string, string> _cmdTypes;

		public string CmdEditor
		{
			get { return _cmdEditor; }
			set
			{
				_cmdEditor = value;
				NotifyOfPropertyChange(() => CmdEditor);
			}
		}

		public IDictionary<string, string> CmdTypes
		{
			get { return _cmdTypes; }
			set
			{
				_cmdTypes = value;
				NotifyOfPropertyChange(() => CmdTypes);
			}
		}

		void AddInitialData()
		{
			Headers = new ObservableCollection<Header>
				{
					new Header("AGRESSO_USERNAME", _creds.UserName),
					new Header("AGRESSO_PASSWORD", _creds.Password)
				};

			CmdTypes = typeof (RegisterKnowledgeActivityExpense)
				.Assembly
				.GetTypes()
				.Where(t => t.Namespace != null && t.Namespace.Contains("Commands"))
				.ToDictionary(t => t.AssemblyQualifiedName, t => t.FullName);
		}

		public void CommandTypeSelected(object obj)
		{
			var t = Type.GetType(obj.ToString());
			CmdEditor = JsonConvert.SerializeObject(_defaultFor[t], new UriConverter());
			_selectedCmd = t;
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
			var result = dlg.ShowDialog();

			if (result == true)
				ImageFile = dlg.FileName;
		}

		public void Send()
		{
			if (_selectedCmd == null)
				return;

			object msg;
			try
			{
				msg = ConstructCmd(_defaultFor[_selectedCmd].GetType());
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
				return;
			}

			_bus.Publish<RegisterKnowledgeActivityExpense>(msg, context =>
				{
					foreach (var header in Headers)
						context.SetHeader(header.Key, header.Value);
				});
		}

		object ConstructCmd(Type cmdType)
		{
			return JsonConvert.DeserializeObject(CmdEditor, cmdType, new UriConverter());
		}
	}
}