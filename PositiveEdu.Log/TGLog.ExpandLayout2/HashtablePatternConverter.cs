using log4net.Core;
using log4net.Layout.Pattern;
using log4net.Util;
using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace TGLog.ExpandLayout2
{
	public class HashtablePatternConverter : PatternLayoutConverter
	{
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (this.Option != null)
			{
				PatternConverter.WriteObject(writer, loggingEvent.Repository, this.LookupProperty(this.Option, loggingEvent));
			}
			else
			{
				PatternConverter.WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
			}
		}

		private object LookupProperty(string property, LoggingEvent loggingEvent)
		{
			object propertyValue = string.Empty;
			Hashtable ht = new Hashtable();
			if (loggingEvent.MessageObject.GetType().Equals(ht.GetType()))
			{
				propertyValue = ((Hashtable)loggingEvent.MessageObject)[property];
			}
			else
			{
				PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
				if (propertyInfo != null)
				{
					propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);
				}
			}
			return propertyValue;
		}
	}
}
