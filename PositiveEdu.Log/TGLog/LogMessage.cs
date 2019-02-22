using System;
using System.Collections;

namespace TGLog
{
	public class LogMessage
	{
		public int Operator
		{
			get;
			set;
		}

		public string Operand
		{
			get;
			set;
		}

		public string IP
		{
			get;
			set;
		}

		public string Browser
		{
			get;
			set;
		}

		public string MachineName
		{
			get;
			set;
		}

		public string Message
		{
			get;
			set;
		}

		public int ActionType
		{
			get;
			set;
		}

		public LogMessage(string message)
		{
			this.ActionType = 0;
			this.Operator = 0;
			this.Message = message;
			this.Operand = "";
			this.IP = "";
			this.Browser = "";
			this.MachineName = "";
		}

		public LogMessage(int operatorID, int ActionType, string message)
		{
			this.ActionType = ActionType;
			this.Operator = operatorID;
			this.Message = message;
			this.Operand = "";
			this.IP = "";
			this.Browser = "";
			this.MachineName = "";
		}

		public LogMessage(int operatorID, string operand, int ActionType, string message)
		{
			this.ActionType = ActionType;
			this.Operator = operatorID;
			this.Message = message;
			this.Operand = operand;
			this.IP = "";
			this.Browser = "";
			this.MachineName = "";
		}

		public LogMessage(int operatorID, string operand, int ActionType, string message, string ip, string machineName, string browser)
		{
			this.ActionType = ActionType;
			this.Operator = operatorID;
			this.Message = message;
			this.Operand = operand;
			this.IP = ip;
			this.Browser = browser;
			this.MachineName = machineName;
		}

		public Hashtable ToHashtable()
		{
			return new Hashtable
			{
				{
					"Operator",
					this.Operator
				},
				{
					"Message",
					this.Message
				},
				{
					"ActionType",
					this.ActionType
				},
				{
					"Operand",
					this.Operand
				},
				{
					"IP",
					this.IP
				},
				{
					"MachineName",
					this.MachineName
				},
				{
					"Browser",
					this.Browser
				}
			};
		}
	}
}
