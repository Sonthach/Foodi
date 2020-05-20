package crc64b1e4d0552e072eb1;


public class FoodiPlacesActivity_MyClickableSpan
	extends android.text.style.ClickableSpan
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler\n" +
			"";
		mono.android.Runtime.register ("Foodi.FoodiPlacesActivity+MyClickableSpan, Foodi", FoodiPlacesActivity_MyClickableSpan.class, __md_methods);
	}


	public FoodiPlacesActivity_MyClickableSpan ()
	{
		super ();
		if (getClass () == FoodiPlacesActivity_MyClickableSpan.class)
			mono.android.TypeManager.Activate ("Foodi.FoodiPlacesActivity+MyClickableSpan, Foodi", "", this, new java.lang.Object[] {  });
	}


	public void onClick (android.view.View p0)
	{
		n_onClick (p0);
	}

	private native void n_onClick (android.view.View p0);

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
