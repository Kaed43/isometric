using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric
{
    enum EMoveType: byte
    {
        Foot = 0,
        Tire,
        Tread,
        Crawler, //crawler?
        Amph, //goddamit
        Hover,
        Structure,
        Air,
        SmallVessel, // Fishing boat
        LargeVessel, // Not fishing boat
        Lander,
        Sub // fish
    }
}
