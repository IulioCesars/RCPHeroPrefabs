using Assets.Prefab.Definitions;
using Leap;
using Leap.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Prefab.LeapMotionOverride
{
    public class HandState
    {
        public HandState(Hand hand, HandRepresentation representation)
        {
            Hand = hand;
            Representation = representation;
        }

        public Hand Hand { get; }
        public HandRepresentation Representation { get; }

        public void UpdateRepresentation(Hand hand)
        {
            try
            { Representation.UpdateRepresentation(hand); }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Vector GetPalmPosition()
        { return Hand.PalmPosition; }

        public bool IsBackOf(Hand otherHand)
        { return otherHand != null && otherHand.PalmPosition.IsOverOf(GetPalmPosition()); }
    }
}
