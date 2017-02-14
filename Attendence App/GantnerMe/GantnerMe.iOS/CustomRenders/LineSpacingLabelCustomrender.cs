using Foundation;
using GantnerMe;
using GantnerMe.iOS.CustomRenders;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LineSpacingLabel), typeof(LineSpacingLabelCustomrender))]
namespace GantnerMe.iOS.CustomRenders
{
    public class LineSpacingLabelCustomrender : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                base.OnElementChanged(e);

                // Make sure control is not null
                var label = (LineSpacingLabel)Element;
                if (label == null || Control == null)
                {
                    return;
                }
                if (string.IsNullOrEmpty(label.Text))
                {

                }
                else
                {
                    var labelString = new NSMutableAttributedString(label.Text);
                    var paragraphStyle = new NSMutableParagraphStyle { LineSpacing = (nfloat)label.LineSpacing };
                    var style = UIStringAttributeKey.ParagraphStyle;
                    var range = new NSRange(0, labelString.Length);
                    labelString.AddAttribute(style, paragraphStyle, range);
                    Control.AttributedText = labelString;
                }
            }
        }

    }
}
