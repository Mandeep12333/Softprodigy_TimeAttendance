using CoreAnimation;
using CoreGraphics;
using GantnerMe.iOS.CustomRenders;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(MyEntryRenderer))]
namespace GantnerMe.iOS.CustomRenders
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var toolbar = new UIToolbar(new CGRect(0.0f, 0.0f, Control.Frame.Size.Width, 44.0f));

                toolbar.Items = new[]
                {
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate { Control.ResignFirstResponder(); })
            };
                this.Control.InputAccessoryView = toolbar;
                
                //this.Control.InputAccessoryView = toolbar;
                //UITextField textField = (UITextField)Control;
                //// Most commonly customized for font and no border
                //// do whatever you want to the UITextField here!
                //// textField.BackgroundColor = UIColor.FromRGB(236, 240, 241);
                //textField.BorderStyle = UITextBorderStyle.Line;
                //textField.Font = UIFont.FromName("Ubuntu-light", 13);
                //textField.SetValueForKeyPath(UIColor.Black, new NSString("_placeholderLabel.textColor"));
                //textField.Layer.SublayerTransform = CATransform3D.MakeTranslation(10, 0, 0);
                //Control.BorderStyle = UITextBorderStyle.None;
                //// textField.TintColor = UIColor.Black;

                //// Use 'Done' on keyboard
                //textField.ReturnKeyType = UIReturnKeyType.Done;
                //textField.EnablesReturnKeyAutomatically = true;

                //// No auto-correct
                //textField.AutocorrectionType = UITextAutocorrectionType.No;
                //textField.SpellCheckingType = UITextSpellCheckingType.No;
                //textField.AutocapitalizationType = UITextAutocapitalizationType.Words;

                //// Misc.
                //textField.ClearButtonMode = UITextFieldViewMode.WhileEditing;
                //textField.ClearsOnBeginEditing = false;
                //textField.ClearsOnInsertion = false;
                //textField.AdjustsFontSizeToFitWidth = false;
                //textField.KeyboardAppearance = UIKeyboardAppearance.Default;
            }
        }
    }
}
