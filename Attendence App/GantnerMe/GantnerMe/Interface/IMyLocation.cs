using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.Interface
{
    public interface IMyLocation
    {
        void ObtainMyLocation();
        event EventHandler<ILocationEventArgs> locationObtained;
    }

    public interface ILocationEventArgs
    {
        double lat { get; set; }
        double lng { get; set; }
    }
}
