using System;
using System.Linq;
using System.Windows.Forms;
using Agress.Core.Commands;
using Agress.Logic;
using Agress.UI.Properties;
using MassTransit;

namespace Agress.UI
{
	public partial class MainForm : Form
	{
		private readonly IServiceBus _Bus;

		public MainForm(IServiceBus bus)
		{
			_Bus = bus;
			InitializeComponent();

			var settings = Settings.Default;
			p = new MainPresenter(_Bus,
			                      settings.Login_Username,
			                      settings.Login_Password,
			                      settings.Login_Client,
			                      settings.Login_Url);

			textBoxInfo.Text = p.GetSettingsString();
		}

		private readonly MainPresenter p;

		private void buttonLogin_Click(object sender, EventArgs e)
		{
			if (!p.IsLoggedIn)
			{
				var s = p.LogIn();

				if (!s)
				{
					textBoxInfo.AppendText("Login failed");
					textBoxInfo.AppendText(Environment.NewLine);
				}
			}
		}

		private void buttonSalarySuggestion_Click(object sender, EventArgs e)
		{
			p.SalarySuggestion();
		}

		private void buttonSalarySpec_Click(object sender, EventArgs e)
		{
			p.SalarySpec();
		}

		private void buttonReg1_Click(object sender, EventArgs e)
		{
			var reg1 = textBoxReg1.Text;

			var strings1 = reg1.Split(';');


			if (strings1.Length != 12)
				textBoxInfo.AppendText("Input must be ;-separated with 12 parts");
			else
				SendTimeReport(strings1);
		}

		private void buttonReg2_Click(object sender, EventArgs e)
		{
			var reg2 = textBoxReg2.Text;
			var reg1 = reg2.Split(';');

			if (reg1.Length != 12)
				textBoxInfo.AppendText("Input must be ;-separated with 12 parts");
			else
			{
				SendTimeReport(reg1);
			}
		}

		private void SendTimeReport(string[] strings)
		{
			// todo: send to specific endpoint instead
			_Bus.Publish(new ReportAWeekOfTimes(
			             	strings[3],
			             	strings.Skip(5).Take(7).Select(double.Parse).ToList(),
			             	new AccountingData(strings[0], strings[1], strings[2], int.Parse(strings[4]))
			             	));
		}

		private void buttonReg_Click(object sender, EventArgs e)
		{
			p.GotoTimeRegistration();
		}

		private void buttonPrev_Click(object sender, EventArgs e)
		{
			p.GotoPreviousPeriod();
		}

		private void buttonNext_Click(object sender, EventArgs e)
		{
			p.GotoNextPeriod();
		}

		private void buttonNow_Click(object sender, EventArgs e)
		{
			p.GotoThisPeriod();
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			var info = p.SaveTimeSheet();
			textBoxInfo.AppendText(string.Format("{0}{1}", info, Environment.NewLine));
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			p.Quit();
			Application.Exit();
		}

		private void buttonExpenses_Click(object sender, EventArgs e)
		{
			p.GotoExpenses();
		}

		private void buttonMiles_Click(object sender, EventArgs e)
		{
			p.Expenses1();
		}

		private void buttonBroBizz_Click(object sender, EventArgs e)
		{
			p.Expenses2();
		}

		private void buttonMassage_Click(object sender, EventArgs e)
		{
			p.Expenses3();
		}

		private void buttonSaveExpenses_Click(object sender, EventArgs e)
		{
			var info = p.SaveExpenses();
			textBoxInfo.AppendText(string.Format("{0}{1}", info, Environment.NewLine));
		}

		private void buttonVersion_Click(object sender, EventArgs e)
		{
			var ver = p.Version();
			textBoxInfo.AppendText(string.Format("Version: {0}{1}", ver, Environment.NewLine));
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			p.LogOut();
		}
	}
}