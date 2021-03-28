using Android.Graphics.Drawables;
using Paypro_Mobile.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace Paypro_Mobile.Droid.CustomRenderers
{
    [System.Obsolete]
    class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.White);
                gd.SetCornerRadius(15);
                gd.SetStroke(3, Android.Graphics.Color.DodgerBlue);
                Control.SetBackgroundDrawable(gd);
                /*Control.SetShadowLayer(10, 10, 10, Android.Graphics.Color.Red);*/
            }
        }
    }
}