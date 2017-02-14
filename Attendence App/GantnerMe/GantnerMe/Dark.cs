using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GantnerMe
{
    public class Dark : DataGridStyle
    {
        public Dark()
        {
        }
        public override Color GetHeaderBackgroundColor()
        {
            return Color.FromRgb(87, 131, 155);//FromHex("#27ae60"); //Black//
        }

        public override Color GetHeaderForegroundColor()
        {
            return Color.White;//(255, 255, 255); // White//
        }

        public override Color GetRecordBackgroundColor()
        {
            return Color.FromHex("#CDDBDE"); // Black//
        }

        public override Color GetRecordForegroundColor()
        {
            return Color.FromHex("#1D6386");//FromRgb(255, 255, 255);
        }

        //public override Color GetSelectionBackgroundColor()
        //{
        //    return Color.FromRgb(42, 159, 214);
        //}

        //public override Color GetSelectionForegroundColor()
        //{
        //    return Color.FromRgb(255, 255, 255);
        //}

        public override Color GetCaptionSummaryRowBackgroundColor()
        {
            return Color.FromRgb(02, 02, 02);
        }

        public override Color GetCaptionSummaryRowForeGroundColor()
        {
            return Color.FromRgb(255, 255, 255);
        }

        public override Color GetBordercolor()
        {
            return Color.FromRgb(81, 83, 82);
        }

        public override Color GetLoadMoreViewBackgroundColor()
        {
            return Color.FromRgb(242, 242, 242);
        }

        public override Color GetLoadMoreViewForegroundColor()
        {
            return Color.FromRgb(34, 31, 31);
        }

        //public override Color GetAlternatingRowBackgroundColor()
        //{
        //    return Color.FromRgb(133, 193, 233);
        //}
    }
}
