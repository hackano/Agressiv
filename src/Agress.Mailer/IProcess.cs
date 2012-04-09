using System;

namespace Agress.Mailer
{
	public interface IProcess
		: IDisposable
	{
		void WaitForExit();
	}
}