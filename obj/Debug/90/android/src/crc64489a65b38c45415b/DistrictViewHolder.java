package crc64489a65b38c45415b;


public class DistrictViewHolder
	extends com.telerik.widget.list.ListViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Foodi.Adapter.DistrictViewHolder, Foodi", DistrictViewHolder.class, __md_methods);
	}


	public DistrictViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == DistrictViewHolder.class)
			mono.android.TypeManager.Activate ("Foodi.Adapter.DistrictViewHolder, Foodi", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
