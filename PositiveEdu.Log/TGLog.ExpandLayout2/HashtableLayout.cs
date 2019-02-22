using log4net.Layout;
using System;

namespace TGLog.ExpandLayout2
{
	public class HashtableLayout : PatternLayout
	{
		public HashtableLayout()
		{
			base.AddConverter("property", typeof(HashtablePatternConverter));
		}
	}
}
