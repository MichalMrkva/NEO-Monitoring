using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Widget;
using AndroidX.Core.Content;
using NEOMonitoring.CustomComponents;
using Java.Util;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using NEO_Monitoring.Droid;

[assembly: ExportRenderer(typeof(TextEntry), typeof(TextEntryRender))]

namespace NEO_Monitoring.Droid
{
    class TextEntryRender : EntryRenderer
    {
        public TextEntryRender(Context context): base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
                return;
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                Control.SetBackgroundDrawable(gd);
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                {
                    Control.SetTextCursorDrawable(Resource.Drawable.BlackEntryCursor);
                }
                else
                {
                    IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                    IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                    JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.BlackEntryCursor);
                }
            }
        }
    }
}