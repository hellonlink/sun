using log4net;
using log4net.Core;
using System;

namespace TGLog.ExpandILog
{
	public class MyLogImpl : LogImpl, IMyLog, ILog, ILoggerWrapper
	{
		private static readonly Type ThisDeclaringType = typeof(MyLogImpl);

		public MyLogImpl(ILogger logger) : base(logger)
		{
		}

		public void Debug(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName)
		{
			this.Debug(operatorID, operand, actionType, message, ip, browser, machineName, null);
		}

		public void Debug(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName, Exception t)
		{
			if (this.IsDebugEnabled)
			{
				LoggingEvent loggingEvent = new LoggingEvent(MyLogImpl.ThisDeclaringType, this.Logger.Repository, this.Logger.Name, Level.Info, message, t);
				loggingEvent.Properties["Operator"] = operatorID;
				loggingEvent.Properties["Operand"] = operand;
				loggingEvent.Properties["ActionType"] = actionType;
				loggingEvent.Properties["IP"] = ip;
				loggingEvent.Properties["Browser"] = browser;
				loggingEvent.Properties["MachineName"] = machineName;
				this.Logger.Log(loggingEvent);
			}
		}

		public void Info(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName)
		{
			this.Info(operatorID, operand, actionType, message, ip, browser, machineName, null);
		}

		public void Info(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName, Exception t)
		{
			if (this.IsInfoEnabled)
			{
				LoggingEvent loggingEvent = new LoggingEvent(MyLogImpl.ThisDeclaringType, this.Logger.Repository, this.Logger.Name, Level.Info, message, t);
				loggingEvent.Properties["Operator"] = operatorID;
				loggingEvent.Properties["Operand"] = operand;
				loggingEvent.Properties["ActionType"] = actionType;
				loggingEvent.Properties["IP"] = ip;
				loggingEvent.Properties["Browser"] = browser;
				loggingEvent.Properties["MachineName"] = machineName;
				this.Logger.Log(loggingEvent);
			}
		}

		public void Warn(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName)
		{
			this.Warn(operatorID, operand, actionType, message, ip, browser, machineName, null);
		}

		public void Warn(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName, Exception t)
		{
			if (this.IsWarnEnabled)
			{
				LoggingEvent loggingEvent = new LoggingEvent(MyLogImpl.ThisDeclaringType, this.Logger.Repository, this.Logger.Name, Level.Info, message, t);
				loggingEvent.Properties["Operator"] = operatorID;
				loggingEvent.Properties["Operand"] = operand;
				loggingEvent.Properties["ActionType"] = actionType;
				loggingEvent.Properties["IP"] = ip;
				loggingEvent.Properties["Browser"] = browser;
				loggingEvent.Properties["MachineName"] = machineName;
				this.Logger.Log(loggingEvent);
			}
		}

		public void Error(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName)
		{
			this.Error(operatorID, operand, actionType, message, ip, browser, machineName, null);
		}

		public void Error(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName, Exception t)
		{
			if (this.IsErrorEnabled)
			{
				LoggingEvent loggingEvent = new LoggingEvent(MyLogImpl.ThisDeclaringType, this.Logger.Repository, this.Logger.Name, Level.Info, message, t);
				loggingEvent.Properties["Operator"] = operatorID;
				loggingEvent.Properties["Operand"] = operand;
				loggingEvent.Properties["ActionType"] = actionType;
				loggingEvent.Properties["IP"] = ip;
				loggingEvent.Properties["Browser"] = browser;
				loggingEvent.Properties["MachineName"] = machineName;
				this.Logger.Log(loggingEvent);
			}
		}

		public void Fatal(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName)
		{
			this.Fatal(operatorID, operand, actionType, message, ip, browser, machineName, null);
		}

		public void Fatal(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName, Exception t)
		{
			if (this.IsFatalEnabled)
			{
				LoggingEvent loggingEvent = new LoggingEvent(MyLogImpl.ThisDeclaringType, this.Logger.Repository, this.Logger.Name, Level.Info, message, t);
				loggingEvent.Properties["Operator"] = operatorID;
				loggingEvent.Properties["Operand"] = operand;
				loggingEvent.Properties["ActionType"] = actionType;
				loggingEvent.Properties["IP"] = ip;
				loggingEvent.Properties["Browser"] = browser;
				loggingEvent.Properties["MachineName"] = machineName;
				this.Logger.Log(loggingEvent);
			}
		}
	}
}
