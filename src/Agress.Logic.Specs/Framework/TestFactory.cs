using System.IO;
using System.Text;

namespace Agress.Logic.Specs.Framework
{
	internal static class TestFactory
	{
		public static Credentials GetCredentialsFromFileOrEnv()
		{
			if (File.Exists("credentials.txt"))
			{
				var rows = File.ReadAllLines("credentials.txt", Encoding.UTF8);
				return new EnvironmentCredentials(rows[0], rows[1]);
			}
			return new EnvironmentCredentials();
		}
	}
}