using Android.Graphics.Drawables;
using Paypro_Mobile.Droid.CustomRenderers;
using Paypro_Mobile.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]
namespace Paypro_Mobile.Droid.CustomRenderers
{
    [System.Obsolete]
    class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.White);
                gd.SetCornerRadius(15);
                gd.SetStroke(3, Android.Graphics.Color.DodgerBlue);
                Control.SetBackgroundDrawable(gd);
                Control.Text = LangRS.PickerPlaceHolderText;
            }
        }

    }
}