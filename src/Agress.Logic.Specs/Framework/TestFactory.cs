using System.IO;

namespace Agress.Logic.Specs.Framework
{
	internal static class TestFactory
	{
		public static Credentials GetCredentialsFromFileOrEnv()
		{
			if (File.Exists("credentials.txt"))
			{
				var rows = File.ReadAllLines("credentials.txt");
				return new EnvironmentCredentials(rows[0], rows[1]);
			}
			return new EnvironmentCredentials();
		}
	}
}