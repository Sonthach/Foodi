package crc64489a65b38c45415b;


public class FoodiPlaceAdapter
	extends com.telerik.widget.list.ListViewAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateViewHolder:(Landroid/view/ViewGroup;I)Lcom/telerik/widget/list/ListViewHolder;:GetOnCreateViewHolder_Landroid_view_ViewGroup_IHandler\n" +
			"n_onBindViewHolder:(Lcom/telerik/widget/list/ListViewHolder;I)V:GetOnBindListViewHolder_Lcom_telerik_widget_list_ListViewHolder_IHandler\n" +
			"";
		mono.android.Runtime.register ("Foodi.Adapter.FoodiPlaceAdapter, Foodi", FoodiPlaceAdapter.class, __md_methods);
	}


	public FoodiPlaceAdapter (java.util.List p0)
	{
		super (p0);
		if (getClass () == FoodiPlaceAdapter.class)
			mono.android.TypeManager.Activate ("Foodi.Adapter.FoodiPlaceAdapter, Foodi", "System.Collections.IList, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public com.telerik.widget.list.ListViewHolder onCreateViewHolder (android.view.ViewGroup p0, int p1)
	{
		return n_onCreateViewHolder (p0, p1);
	}

	private native com.telerik.widget.list.ListViewHolder n_onCreateViewHolder (android.view.ViewGroup p0, int p1);


	public void onBindViewHolder (com.telerik.widget.list.ListViewHolder p0, int p1)
	{
		n_onBindViewHolder (p0, p1);
	}

	private native void n_onBindViewHolder (com.telerik.widget.list.ListViewHolder p0, int p1);

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
