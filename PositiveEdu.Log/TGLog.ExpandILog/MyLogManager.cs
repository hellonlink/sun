using log4net.Core;
using System;
using System.Reflection;

namespace TGLog.ExpandILog
{
	public class MyLogManager
	{
		private static readonly WrapperMap s_wrapperMap = new WrapperMap(new WrapperCreationHandler(MyLogManager.WrapperCreationHandler));

		private MyLogManager()
		{
		}

		public static IMyLog Exists(string name)
		{
			return MyLogManager.Exists(Assembly.GetCallingAssembly(), name);
		}

		public static IMyLog Exists(string domain, string name)
		{
			return MyLogManager.WrapLogger(LoggerManager.Exists(domain, name));
		}

		public static IMyLog Exists(Assembly assembly, string name)
		{
			return MyLogManager.WrapLogger(LoggerManager.Exists(assembly, name));
		}

		public static IMyLog[] GetCurrentLoggers()
		{
			return MyLogManager.GetCurrentLoggers(Assembly.GetCallingAssembly());
		}

		public static IMyLog[] GetCurrentLoggers(string domain)
		{
			return MyLogManager.WrapLoggers(LoggerManager.GetCurrentLoggers(domain));
		}

		public static IMyLog[] GetCurrentLoggers(Assembly assembly)
		{
			return MyLogManager.WrapLoggers(LoggerManager.GetCurrentLoggers(assembly));
		}

		public static IMyLog GetLogger(string name)
		{
			return MyLogManager.GetLogger(Assembly.GetCallingAssembly(), name);
		}

		public static IMyLog GetLogger(string domain, string name)
		{
			return MyLogManager.WrapLogger(LoggerManager.GetLogger(domain, name));
		}

		public static IMyLog GetLogger(Assembly assembly, string name)
		{
			return MyLogManager.WrapLogger(LoggerManager.GetLogger(assembly, name));
		}

		public static IMyLog GetLogger(Type type)
		{
			return MyLogManager.GetLogger(Assembly.GetCallingAssembly(), type.FullName);
		}

		public static IMyLog GetLogger(string domain, Type type)
		{
			return MyLogManager.WrapLogger(LoggerManager.GetLogger(domain, type));
		}

		public static IMyLog GetLogger(Assembly assembly, Type type)
		{
			return MyLogManager.WrapLogger(LoggerManager.GetLogger(assembly, type));
		}

		private static IMyLog WrapLogger(ILogger logger)
		{
			return (IMyLog)MyLogManager.s_wrapperMap.GetWrapper(logger);
		}

		private static IMyLog[] WrapLoggers(ILogger[] loggers)
		{
			IMyLog[] results = new IMyLog[loggers.Length];
			for (int i = 0; i < loggers.Length; i++)
			{
				results[i] = MyLogManager.WrapLogger(loggers[i]);
			}
			return results;
		}

		private static ILoggerWrapper WrapperCreationHandler(ILogger logger)
		{
			return new MyLogImpl(logger);
		}
	}
}
