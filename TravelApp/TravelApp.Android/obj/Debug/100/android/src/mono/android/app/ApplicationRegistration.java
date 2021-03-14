package mono.android.app;

public class ApplicationRegistration {

	public static void registerApplications ()
	{
				// Application and Instrumentation ACWs must be registered first.
		mono.android.Runtime.register ("TravelApp.Droid.MainApplication, TravelApp.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", crc648097ddf7ab9d027d.MainApplication.class, crc648097ddf7ab9d027d.MainApplication.__md_methods);
		
	}
}
