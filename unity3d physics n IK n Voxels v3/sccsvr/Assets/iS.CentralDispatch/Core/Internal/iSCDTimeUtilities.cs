using System;

public class iSCDTimeUtilities {

	private static DateTime JanFirst1970 = new DateTime(1970, 1, 1);

	public static long MillisecondsSinceJanFirst1970()
	{
		return (long)((DateTime.Now.ToUniversalTime() - JanFirst1970).TotalMilliseconds);
	}
}
