package crc64f26adfbfa5b555f3;


public class DistrictItem
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Foodi.Model.DistrictItem, Foodi", DistrictItem.class, __md_methods);
	}


	public DistrictItem ()
	{
		super ();
		if (getClass () == DistrictItem.class)
			mono.android.TypeManager.Activate ("Foodi.Model.DistrictItem, Foodi", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
