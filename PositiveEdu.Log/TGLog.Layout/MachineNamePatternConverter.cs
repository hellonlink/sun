using log4net.Core;
using log4net.Layout.Pattern;
using System;
using System.IO;

namespace TGLog.Layout
{
	internal sealed class MachineNamePatternConverter : PatternLayoutConverter
	{
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			LogMessage logMessage = loggingEvent.MessageObject as LogMessage;
			if (logMessage != null)
			{
				writer.Write(logMessage.MachineName);
			}
		}
	}
}
