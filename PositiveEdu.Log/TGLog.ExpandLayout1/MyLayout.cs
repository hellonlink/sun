using log4net.Layout;
using System;
using TGLog.Layout;

namespace TGLog.ExpandLayout1
{
	public class MyLayout : PatternLayout
	{
		public MyLayout()
		{
			base.AddConverter("Operator", typeof(OperatorPatternConverter));
			base.AddConverter("Message", typeof(MessagePatternConverter));
			base.AddConverter("Operand", typeof(OperandPatternConverter));
			base.AddConverter("ActionType", typeof(ActionTypePatternConverter));
			base.AddConverter("IP", typeof(IPPatternConverter));
			base.AddConverter("MachineName", typeof(MachineNamePatternConverter));
			base.AddConverter("Browser", typeof(BrowserPatternConverter));
		}
	}
}
