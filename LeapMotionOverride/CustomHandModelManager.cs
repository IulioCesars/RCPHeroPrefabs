using Leap;
using Leap.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Prefab.LeapMotionOverride
{
    public class CustomHandModelManager : HandModelManager
    {
        protected override void UpdateHandRepresentations(Dictionary<int, HandRepresentation> all_hand_reps, ModelType modelType, Frame frame)
        {
            //base.UpdateHandRepresentations(all_hand_reps, modelType, frame);

            var messageBuilder = new StringBuilder();

            messageBuilder.AppendLine($"Hands: {frame.Hands.Count}");

            for (int i = 0; i < frame.Hands.Count; i++)
            {
                var curHand = frame.Hands[i];
                HandRepresentation rep;

                messageBuilder.AppendLine($"#{i}, HandId: {curHand.Id}");

                if (!all_hand_reps.TryGetValue(curHand.Id, out rep))
                {
                    rep = MakeHandRepresentation(curHand, modelType);
                    all_hand_reps.Add(curHand.Id, rep);
                }
                if (rep != null)
                {
                    rep.IsMarked = true;
                    rep.UpdateRepresentation(curHand);
                    rep.LastUpdatedTime = (int)frame.Timestamp;
                }
            }

            if (frame.Hands.Count > 0)
            { Debug.Log(messageBuilder.ToString()); }

            /** Mark-and-sweep to finish unused HandRepresentations */
            HandRepresentation toBeDeleted = null;
            for (var it = all_hand_reps.GetEnumerator(); it.MoveNext();)
            {
                var r = it.Current;
                if (r.Value != null)
                {
                    if (r.Value.IsMarked)
                    {
                        r.Value.IsMarked = false;
                    }
                    else
                    {
                        /** Initialize toBeDeleted with a value to be deleted */
                        //Debug.Log("Finishing");
                        toBeDeleted = r.Value;
                    }
                }
            }
            /**Inform the representation that we will no longer be giving it any hand updates 
             * because the corresponding hand has gone away */
            //if (toBeDeleted != null)
            //{
            //    all_hand_reps.Remove(toBeDeleted.HandID);
            //    toBeDeleted.Finish();
            //}
        }
    }
}
