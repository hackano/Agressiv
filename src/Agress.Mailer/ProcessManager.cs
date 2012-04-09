using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting;
using MassTransit.Util;

namespace Agress.Mailer
{
	public interface ProcessManager
	{
		IProcess Start(string processPath, params string[] args);
	}

	public class SystemProcessManager
		: ProcessManager
	{
		public IProcess Start(string processPath, params string[] args)
		{
			var startInfo = new ProcessStartInfo(processPath, string.Join(" ", args))
				{
					CreateNoWindow = true
				};

			return new ProcWrapper(Process.Start(startInfo));
		}

		class ProcWrapper : IProcess
		{
			readonly Process _process;

			public ProcWrapper([NotNull] Process process)
			{
				if (process == null) throw new ArgumentNullException("process");
				_process = process;
			}

			public object GetLifetimeService()
			{
				return _process.GetLifetimeService();
			}

			public object InitializeLifetimeService()
			{
				return _process.InitializeLifetimeService();
			}

			public ObjRef CreateObjRef(Type requestedType)
			{
				return _process.CreateObjRef(requestedType);
			}

			public void Dispose()
			{
				_process.Dispose();
			}

			public ISite Site
			{
				get { return _process.Site; }
				set { _process.Site = value; }
			}

			public IContainer Container
			{
				get { return _process.Container; }
			}

			public event EventHandler Disposed
			{
				add { _process.Disposed += value; }
				remove { _process.Disposed -= value; }
			}

			public bool CloseMainWindow()
			{
				return _process.CloseMainWindow();
			}

			public void Close()
			{
				_process.Close();
			}

			public void Refresh()
			{
				_process.Refresh();
			}

			public bool Start()
			{
				return _process.Start();
			}

			public void Kill()
			{
				_process.Kill();
			}

			public bool WaitForExit(int milliseconds)
			{
				return _process.WaitForExit(milliseconds);
			}

			public void WaitForExit()
			{
				_process.WaitForExit();
			}

			public bool WaitForInputIdle(int milliseconds)
			{
				return _process.WaitForInputIdle(milliseconds);
			}

			public bool WaitForInputIdle()
			{
				return _process.WaitForInputIdle();
			}

			public void BeginOutputReadLine()
			{
				_process.BeginOutputReadLine();
			}

			public void BeginErrorReadLine()
			{
				_process.BeginErrorReadLine();
			}

			public void CancelOutputRead()
			{
				_process.CancelOutputRead();
			}

			public void CancelErrorRead()
			{
				_process.CancelErrorRead();
			}

			public int BasePriority
			{
				get { return _process.BasePriority; }
			}

			public int ExitCode
			{
				get { return _process.ExitCode; }
			}

			public bool HasExited
			{
				get { return _process.HasExited; }
			}

			public DateTime ExitTime
			{
				get { return _process.ExitTime; }
			}

			public IntPtr Handle
			{
				get { return _process.Handle; }
			}

			public int HandleCount
			{
				get { return _process.HandleCount; }
			}

			public int Id
			{
				get { return _process.Id; }
			}

			public string MachineName
			{
				get { return _process.MachineName; }
			}

			public IntPtr MainWindowHandle
			{
				get { return _process.MainWindowHandle; }
			}

			public string MainWindowTitle
			{
				get { return _process.MainWindowTitle; }
			}

			public ProcessModule MainModule
			{
				get { return _process.MainModule; }
			}

			public IntPtr MaxWorkingSet
			{
				get { return _process.MaxWorkingSet; }
				set { _process.MaxWorkingSet = value; }
			}

			public IntPtr MinWorkingSet
			{
				get { return _process.MinWorkingSet; }
				set { _process.MinWorkingSet = value; }
			}

			public ProcessModuleCollection Modules
			{
				get { return _process.Modules; }
			}

			public int NonpagedSystemMemorySize
			{
				get { return _process.NonpagedSystemMemorySize; }
			}

			public long NonpagedSystemMemorySize64
			{
				get { return _process.NonpagedSystemMemorySize64; }
			}

			public int PagedMemorySize
			{
				get { return _process.PagedMemorySize; }
			}

			public long PagedMemorySize64
			{
				get { return _process.PagedMemorySize64; }
			}

			public int PagedSystemMemorySize
			{
				get { return _process.PagedSystemMemorySize; }
			}

			public long PagedSystemMemorySize64
			{
				get { return _process.PagedSystemMemorySize64; }
			}

			public int PeakPagedMemorySize
			{
				get { return _process.PeakPagedMemorySize; }
			}

			public long PeakPagedMemorySize64
			{
				get { return _process.PeakPagedMemorySize64; }
			}

			public int PeakWorkingSet
			{
				get { return _process.PeakWorkingSet; }
			}

			public long PeakWorkingSet64
			{
				get { return _process.PeakWorkingSet64; }
			}

			public int PeakVirtualMemorySize
			{
				get { return _process.PeakVirtualMemorySize; }
			}

			public long PeakVirtualMemorySize64
			{
				get { return _process.PeakVirtualMemorySize64; }
			}

			public bool PriorityBoostEnabled
			{
				get { return _process.PriorityBoostEnabled; }
				set { _process.PriorityBoostEnabled = value; }
			}

			public ProcessPriorityClass PriorityClass
			{
				get { return _process.PriorityClass; }
				set { _process.PriorityClass = value; }
			}

			public int PrivateMemorySize
			{
				get { return _process.PrivateMemorySize; }
			}

			public long PrivateMemorySize64
			{
				get { return _process.PrivateMemorySize64; }
			}

			public TimeSpan PrivilegedProcessorTime
			{
				get { return _process.PrivilegedProcessorTime; }
			}

			public string ProcessName
			{
				get { return _process.ProcessName; }
			}

			public IntPtr ProcessorAffinity
			{
				get { return _process.ProcessorAffinity; }
				set { _process.ProcessorAffinity = value; }
			}

			public bool Responding
			{
				get { return _process.Responding; }
			}

			public int SessionId
			{
				get { return _process.SessionId; }
			}

			public ProcessStartInfo StartInfo
			{
				get { return _process.StartInfo; }
				set { _process.StartInfo = value; }
			}

			public DateTime StartTime
			{
				get { return _process.StartTime; }
			}

			public ISynchronizeInvoke SynchronizingObject
			{
				get { return _process.SynchronizingObject; }
				set { _process.SynchronizingObject = value; }
			}

			public ProcessThreadCollection Threads
			{
				get { return _process.Threads; }
			}

			public TimeSpan TotalProcessorTime
			{
				get { return _process.TotalProcessorTime; }
			}

			public TimeSpan UserProcessorTime
			{
				get { return _process.UserProcessorTime; }
			}

			public int VirtualMemorySize
			{
				get { return _process.VirtualMemorySize; }
			}

			public long VirtualMemorySize64
			{
				get { return _process.VirtualMemorySize64; }
			}

			public bool EnableRaisingEvents
			{
				get { return _process.EnableRaisingEvents; }
				set { _process.EnableRaisingEvents = value; }
			}

			public StreamWriter StandardInput
			{
				get { return _process.StandardInput; }
			}

			public StreamReader StandardOutput
			{
				get { return _process.StandardOutput; }
			}

			public StreamReader StandardError
			{
				get { return _process.StandardError; }
			}

			public int WorkingSet
			{
				get { return _process.WorkingSet; }
			}

			public long WorkingSet64
			{
				get { return _process.WorkingSet64; }
			}

			public event DataReceivedEventHandler OutputDataReceived
			{
				add { _process.OutputDataReceived += value; }
				remove { _process.OutputDataReceived -= value; }
			}

			public event DataReceivedEventHandler ErrorDataReceived
			{
				add { _process.ErrorDataReceived += value; }
				remove { _process.ErrorDataReceived -= value; }
			}

			public event EventHandler Exited
			{
				add { _process.Exited += value; }
				remove { _process.Exited -= value; }
			}
		}
	}
}