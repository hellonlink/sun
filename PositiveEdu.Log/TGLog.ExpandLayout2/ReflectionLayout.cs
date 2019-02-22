using log4net.Layout;
using System;

namespace TGLog.ExpandLayout2
{
	public class ReflectionLayout : PatternLayout
	{
		public ReflectionLayout()
		{
			base.AddConverter("property", typeof(ReflectionPatternConverter));
		}
	}
}
