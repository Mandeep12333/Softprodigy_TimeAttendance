package md5e7903713153764fcbb99b9cb54d9c946;


public class ExtendedScrollViewRenderer
	extends md5b60ffeb829f638581ab2bb9b1a7f4f3f.ViewRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_addView:(Landroid/view/View;)V:GetAddView_Landroid_view_View_Handler\n" +
			"n_computeHorizontalScrollOffset:()I:GetComputeHorizontalScrollOffsetHandler\n" +
			"n_computeHorizontalScrollRange:()I:GetComputeHorizontalScrollRangeHandler\n" +
			"n_computeVerticalScrollOffset:()I:GetComputeVerticalScrollOffsetHandler\n" +
			"n_computeVerticalScrollRange:()I:GetComputeVerticalScrollRangeHandler\n" +
			"n_computeScroll:()V:GetComputeScrollHandler\n" +
			"n_onAttachedToWindow:()V:GetOnAttachedToWindowHandler\n" +
			"n_onDetachedFromWindow:()V:GetOnDetachedFromWindowHandler\n" +
			"n_onInterceptTouchEvent:(Landroid/view/MotionEvent;)Z:GetOnInterceptTouchEvent_Landroid_view_MotionEvent_Handler\n" +
			"n_onTouchEvent:(Landroid/view/MotionEvent;)Z:GetOnTouchEvent_Landroid_view_MotionEvent_Handler\n" +
			"n_onOverScrolled:(IIZZ)V:GetOnOverScrolled_IIZZHandler\n" +
			"n_onScrollChanged:(IIII)V:GetOnScrollChanged_IIIIHandler\n" +
			"n_scrollTo:(II)V:GetScrollTo_IIHandler\n" +
			"";
		mono.android.Runtime.register ("Syncfusion.SfDataGrid.XForms.Droid.ExtendedScrollViewRenderer, Syncfusion.SfDataGrid.XForms.Android, Version=14.2451.0.26, Culture=neutral, PublicKeyToken=null", ExtendedScrollViewRenderer.class, __md_methods);
	}


	public ExtendedScrollViewRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == ExtendedScrollViewRenderer.class)
			mono.android.TypeManager.Activate ("Syncfusion.SfDataGrid.XForms.Droid.ExtendedScrollViewRenderer, Syncfusion.SfDataGrid.XForms.Android, Version=14.2451.0.26, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public ExtendedScrollViewRenderer (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == ExtendedScrollViewRenderer.class)
			mono.android.TypeManager.Activate ("Syncfusion.SfDataGrid.XForms.Droid.ExtendedScrollViewRenderer, Syncfusion.SfDataGrid.XForms.Android, Version=14.2451.0.26, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public ExtendedScrollViewRenderer (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == ExtendedScrollViewRenderer.class)
			mono.android.TypeManager.Activate ("Syncfusion.SfDataGrid.XForms.Droid.ExtendedScrollViewRenderer, Syncfusion.SfDataGrid.XForms.Android, Version=14.2451.0.26, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void addView (android.view.View p0)
	{
		n_addView (p0);
	}

	private native void n_addView (android.view.View p0);


	public int computeHorizontalScrollOffset ()
	{
		return n_computeHorizontalScrollOffset ();
	}

	private native int n_computeHorizontalScrollOffset ();


	public int computeHorizontalScrollRange ()
	{
		return n_computeHorizontalScrollRange ();
	}

	private native int n_computeHorizontalScrollRange ();


	public int computeVerticalScrollOffset ()
	{
		return n_computeVerticalScrollOffset ();
	}

	private native int n_computeVerticalScrollOffset ();


	public int computeVerticalScrollRange ()
	{
		return n_computeVerticalScrollRange ();
	}

	private native int n_computeVerticalScrollRange ();


	public void computeScroll ()
	{
		n_computeScroll ();
	}

	private native void n_computeScroll ();


	public void onAttachedToWindow ()
	{
		n_onAttachedToWindow ();
	}

	private native void n_onAttachedToWindow ();


	public void onDetachedFromWindow ()
	{
		n_onDetachedFromWindow ();
	}

	private native void n_onDetachedFromWindow ();


	public boolean onInterceptTouchEvent (android.view.MotionEvent p0)
	{
		return n_onInterceptTouchEvent (p0);
	}

	private native boolean n_onInterceptTouchEvent (android.view.MotionEvent p0);


	public boolean onTouchEvent (android.view.MotionEvent p0)
	{
		return n_onTouchEvent (p0);
	}

	private native boolean n_onTouchEvent (android.view.MotionEvent p0);


	public void onOverScrolled (int p0, int p1, boolean p2, boolean p3)
	{
		n_onOverScrolled (p0, p1, p2, p3);
	}

	private native void n_onOverScrolled (int p0, int p1, boolean p2, boolean p3);


	public void onScrollChanged (int p0, int p1, int p2, int p3)
	{
		n_onScrollChanged (p0, p1, p2, p3);
	}

	private native void n_onScrollChanged (int p0, int p1, int p2, int p3);


	public void scrollTo (int p0, int p1)
	{
		n_scrollTo (p0, p1);
	}

	private native void n_scrollTo (int p0, int p1);

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
