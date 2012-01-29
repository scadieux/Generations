using System.Reflection;
using System.Text;
using UnityEngine;

public class Logger
{
	public enum Level { Error = 0, Assert, Warning, Info, Trace }
	internal static readonly string[] levelStrings = { "ERROR", "ASSERT", "WARNING", "INFO", "TRACE" };
		
	[System.Diagnostics.Conditional("LOGGING_ERROR")]
	public static void Error(GameObject context) { Log(Level.Error, context, ""); }
	[System.Diagnostics.Conditional("LOGGING_ERROR")]
	public static void Error(string format, params object[] args) { Log(Level.Error, null, format, args); }
	[System.Diagnostics.Conditional("LOGGING_ERROR")]
	public static void Error(GameObject context, object message) { Log(Level.Error, context, message != null ? message.ToString() : "(null)"); }
	[System.Diagnostics.Conditional("LOGGING_ERROR")]
	public static void Error(GameObject context, string format, params object[] args) { Log(Level.Error, context, format, args); }
	
	[System.Diagnostics.Conditional("LOGGING_ASSERT")]
	public static void Assert(GameObject context) { Log(Level.Assert, context, ""); }
	[System.Diagnostics.Conditional("LOGGING_ASSERT")]
	public static void Assert(string format, params object[] args) { Log(Level.Assert, null, format, args); }
	[System.Diagnostics.Conditional("LOGGING_ASSERT")]
	public static void Assert(GameObject context, object message) { Log(Level.Assert, context, message != null ? message.ToString() : "(null)"); }
	[System.Diagnostics.Conditional("LOGGING_ASSERT")]
	public static void Assert(GameObject context, string format, params object[] args) { Log(Level.Assert, context, format, args); }
	
	[System.Diagnostics.Conditional("LOGGING_WARNING")]
	public static void Warn(GameObject context) { Log(Level.Warning, context, ""); }
	[System.Diagnostics.Conditional("LOGGING_WARNING")]
	public static void Warn(string format, params object[] args) { Log(Level.Warning, null, format, args); }
	[System.Diagnostics.Conditional("LOGGING_WARNING")]
	public static void Warn(GameObject context, object message) { Log(Level.Warning, context, message != null ? message.ToString() : "(null)"); }
	[System.Diagnostics.Conditional("LOGGING_WARNING")]
	public static void Warn(GameObject context, string format, params object[] args) { Log(Level.Warning, context, format, args); }

	
	[System.Diagnostics.Conditional("LOGGING_INFO")]
	public static void Info(GameObject context) { Log(Level.Info, context, ""); }
	[System.Diagnostics.Conditional("LOGGING_INFO")]
	public static void Info(string format, params object[] args) { Log(Level.Info, null, format, args); }
	[System.Diagnostics.Conditional("LOGGING_INFO")]
	public static void Info(GameObject context, object message) { Log(Level.Info, context, message != null ? message.ToString() : "(null)"); }
	[System.Diagnostics.Conditional("LOGGING_INFO")]
	public static void Info(GameObject context, string format, params object[] args) { Log(Level.Info, context, format, args); }


	[System.Diagnostics.Conditional("LOGGING_TRACE")]
	public static void Trace() { Log(Level.Trace, null, ""); }
	[System.Diagnostics.Conditional("LOGGING_TRACE")]
	public static void Trace(GameObject context) { Log(Level.Trace, context, ""); }
	[System.Diagnostics.Conditional("LOGGING_TRACE")]
	public static void Trace(string format, params object[] args) { Log(Level.Trace, null, format, args); }
	[System.Diagnostics.Conditional("LOGGING_TRACE")]
	public static void Trace(GameObject context, object message) { Log(Level.Trace, context, message != null ? message.ToString() : "(null)"); }
	[System.Diagnostics.Conditional("LOGGING_TRACE")]
	public static void Trace(GameObject context, string format, params object[] args) { Log(Level.Trace, context, format, args); }

	
	[System.Diagnostics.Conditional("LOGGING")]
	private static void Log(Level level, GameObject context, string format, params object[] args)
	{
		Log(level, context, string.Format(format, args));
	}

	[System.Diagnostics.Conditional("LOGGING")]
	private static void Log(Level level, GameObject context, string message)
	{
		switch (level)
		{
		case Level.Error:
		case Level.Assert:
			Debug.LogError(message, context);
			break;
		case Level.Warning:
			Debug.LogWarning(message, context);
			break;
		case Level.Info:
		case Level.Trace:
			Debug.Log(message, context);
			break;
		}
	}
}
