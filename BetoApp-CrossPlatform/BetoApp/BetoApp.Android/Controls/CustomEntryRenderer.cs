using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.Views;

[assembly: ExportRenderer(typeof(BetoApp.Controls.CustomEntry), typeof(BetoApp.Controls.CustomEntryRenderer))]
namespace BetoApp.Controls
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                this.Control.TextAlignment = Android.Views.TextAlignment.Center;
                this.Control.Gravity = GravityFlags.CenterVertical;
            }
        }
    }
}
