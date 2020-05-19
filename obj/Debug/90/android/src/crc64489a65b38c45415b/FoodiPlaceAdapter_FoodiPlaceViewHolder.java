package crc64489a65b38c45415b;


public class FoodiPlaceAdapter_FoodiPlaceViewHolder
	extends com.telerik.widget.list.ListViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Foodi.Adapter.FoodiPlaceAdapter+FoodiPlaceViewHolder, Foodi", FoodiPlaceAdapter_FoodiPlaceViewHolder.class, __md_methods);
	}


	public FoodiPlaceAdapter_FoodiPlaceViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == FoodiPlaceAdapter_FoodiPlaceViewHolder.class)
			mono.android.TypeManager.Activate ("Foodi.Adapter.FoodiPlaceAdapter+FoodiPlaceViewHolder, Foodi", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
