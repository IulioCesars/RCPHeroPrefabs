using Assets.Prefab.Definitions;
using Leap;
using Leap.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Prefab.LeapMotionOverride
{
    public class CustomHandModelManager : HandModelManager
    {
        public Text TextDetailsLeft;
        public Text TextDetailsRigth;
        public Text TextEstatus;

        public CustomHandModelManager()
        { }

        private HandState LastStateLeftHand { get; set; }
        private HandState LastStateRigthHand { get; set; }

        //private Hand LastStateLeftHand { get; set; }
        //private Hand LastStateRigthHand { get; set; }

        protected override void UpdateHandRepresentations(Dictionary<int, HandRepresentation> all_hand_reps, ModelType modelType, Frame frame)
        {
            string logMessage = string.Empty;

            if (frame.Hands.Count == 0)
            { return; }

            var leftHand = frame.Hands.FirstOrDefault(it => it.IsLeft);
            var rigthHand = frame.Hands.FirstOrDefault(it => it.IsRight);

            TextDetailsLeft.text = LastStateLeftHand != null ? LastStateLeftHand.GetPalmPosition().ToString() : "NULL";
            TextDetailsRigth.text = LastStateRigthHand != null ? LastStateRigthHand.GetPalmPosition().ToString() : "NULL";

            if (leftHand != null && all_hand_reps.TryGetValue(leftHand.Id, out var leftRepresentation))
            { LastStateLeftHand = new HandState(leftHand, leftRepresentation); }
            else if (LastStateLeftHand?.IsBackOf(rigthHand) ?? false)
            {
                var countHands = LastStateLeftHand.Representation.handModels.Count;
                logMessage = $"Mano derecha sobre mano izquierda, Manos: {countHands}";
            }

            if (rigthHand != null && all_hand_reps.TryGetValue(rigthHand.Id, out var rigthRepresentation))
            { LastStateRigthHand = new HandState(rigthHand, rigthRepresentation); }
            else if (LastStateRigthHand?.IsBackOf(leftHand) ?? false)
            {
                var countHands = LastStateRigthHand.Representation.handModels.Count;
                logMessage = $"Mano izquierda sobre mano derecha, Manos: {countHands}";
            }

            //TextDetailsLeft.text = LastStateLeftHand != null ? LastStateLeftHand.PalmPosition.ToString() : "NULL";
            //TextDetailsRigth.text = LastStateRigthHand != null ? LastStateRigthHand.PalmPosition.ToString() : "NULL";

            //if (leftHand != null)
            //{
            //    LastStateLeftHand = leftHand;
            //}
            //else if (rigthHand != null 
            //    && LastStateLeftHand != null 
            //    && rigthHand.PalmPosition.IsOverOf(LastStateLeftHand.PalmPosition))
            //{
            //    logMessage = "Mano derecha sobre mano izquierda";


            //}

            //if (rigthHand != null)
            //{ LastStateRigthHand = rigthHand; }
            //else if (leftHand != null 
            //    && LastStateRigthHand != null
            //    && leftHand.PalmPosition.IsOverOf(LastStateRigthHand.PalmPosition))
            //{
            //    logMessage = "Mano izquierda sobre mano derecha";
            //}

            base.UpdateHandRepresentations(all_hand_reps, modelType, frame);
            Log(logMessage);
        }

        private void Log(string value)
        {
            TextEstatus.text = value;
        }
    }
}
