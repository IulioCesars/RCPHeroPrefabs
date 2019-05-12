using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Prefab.Definitions
{
    public static class Extensions
    {
        public static bool IsOverOf(this Vector thisVector, Vector otherVector)
        {
            float radioValue = 0.1f;

            bool BetweenIn(float value, float minValue, float maxValue)
            { return value >= minValue && value <= maxValue; }

            return BetweenIn(otherVector.x, thisVector.x - radioValue, thisVector.x + radioValue)
                && BetweenIn(otherVector.y, thisVector.y - radioValue, thisVector.y + radioValue)
                && BetweenIn(otherVector.z, thisVector.z - radioValue, thisVector.z + radioValue);
        }
    }
}
