package crc64489a65b38c45415b;


public class CollapseListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.list.CollapsibleGroupsBehavior.CollapseGroupListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onGroupCollapsed:(Ljava/lang/Object;)V:GetOnGroupCollapsed_Ljava_lang_Object_Handler:Com.Telerik.Widget.List.CollapsibleGroupsBehavior/ICollapseGroupListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"n_onGroupExpanded:(Ljava/lang/Object;)V:GetOnGroupExpanded_Ljava_lang_Object_Handler:Com.Telerik.Widget.List.CollapsibleGroupsBehavior/ICollapseGroupListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"";
		mono.android.Runtime.register ("Foodi.Adapter.CollapseListener, Foodi", CollapseListener.class, __md_methods);
	}


	public CollapseListener ()
	{
		super ();
		if (getClass () == CollapseListener.class)
			mono.android.TypeManager.Activate ("Foodi.Adapter.CollapseListener, Foodi", "", this, new java.lang.Object[] {  });
	}

	public CollapseListener (android.content.Context p0)
	{
		super ();
		if (getClass () == CollapseListener.class)
			mono.android.TypeManager.Activate ("Foodi.Adapter.CollapseListener, Foodi", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onGroupCollapsed (java.lang.Object p0)
	{
		n_onGroupCollapsed (p0);
	}

	private native void n_onGroupCollapsed (java.lang.Object p0);


	public void onGroupExpanded (java.lang.Object p0)
	{
		n_onGroupExpanded (p0);
	}

	private native void n_onGroupExpanded (java.lang.Object p0);

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
