package crc64489a65b38c45415b;


public class FoodiPlaceAdapter_FoodiPlacesClickListenner
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.list.RadListView.ItemClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onItemClick:(ILandroid/view/MotionEvent;)V:GetOnItemClick_ILandroid_view_MotionEvent_Handler:Com.Telerik.Widget.List.RadListView/IItemClickListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"n_onItemLongClick:(ILandroid/view/MotionEvent;)V:GetOnItemLongClick_ILandroid_view_MotionEvent_Handler:Com.Telerik.Widget.List.RadListView/IItemClickListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"";
		mono.android.Runtime.register ("Foodi.Adapter.FoodiPlaceAdapter+FoodiPlacesClickListenner, Foodi", FoodiPlaceAdapter_FoodiPlacesClickListenner.class, __md_methods);
	}


	public FoodiPlaceAdapter_FoodiPlacesClickListenner ()
	{
		super ();
		if (getClass () == FoodiPlaceAdapter_FoodiPlacesClickListenner.class)
			mono.android.TypeManager.Activate ("Foodi.Adapter.FoodiPlaceAdapter+FoodiPlacesClickListenner, Foodi", "", this, new java.lang.Object[] {  });
	}

	public FoodiPlaceAdapter_FoodiPlacesClickListenner (crc64b1e4d0552e072eb1.FoodiPlacesActivity p0, com.telerik.widget.list.ListViewAdapter p1)
	{
		super ();
		if (getClass () == FoodiPlaceAdapter_FoodiPlacesClickListenner.class)
			mono.android.TypeManager.Activate ("Foodi.Adapter.FoodiPlaceAdapter+FoodiPlacesClickListenner, Foodi", "Foodi.FoodiPlacesActivity, Foodi:Com.Telerik.Widget.List.ListViewAdapter, Telerik.Xamarin.Android.List", this, new java.lang.Object[] { p0, p1 });
	}


	public void onItemClick (int p0, android.view.MotionEvent p1)
	{
		n_onItemClick (p0, p1);
	}

	private native void n_onItemClick (int p0, android.view.MotionEvent p1);


	public void onItemLongClick (int p0, android.view.MotionEvent p1)
	{
		n_onItemLongClick (p0, p1);
	}

	private native void n_onItemLongClick (int p0, android.view.MotionEvent p1);

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
