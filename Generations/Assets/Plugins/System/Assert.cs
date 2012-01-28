using UnityEngine;

public class Assert
{
	[System.Diagnostics.Conditional("ASSERT")]
	public static void Test(object comparaison, string message)
	{
		Test(comparaison != null, message);
	}
	
	[System.Diagnostics.Conditional("ASSERT")]
	public static void Test(bool comparaison, string message)
	{
		if (!comparaison)
		{
			Logger.Assert(message);
#if ASSERT_BREAK
#	if UNITY_EDITOR
			Debug.Break();
#	else
			
#	endif
#endif
		}
	}
	
	[System.Diagnostics.Conditional("ASSERT")]
	public static void Test(object comparaison)
	{
		Test(comparaison != null);
	}
		
	[System.Diagnostics.Conditional("ASSERT")]
	public static void Test(bool comparaison)
	{
		if (!comparaison)
		{
			Logger.Assert("Unsatisfied assertion");
#if ASSERT_BREAK
#	if UNITY_EDITOR
			Debug.Break();
#	else
			
#	endif
#endif
		}
	}
}
