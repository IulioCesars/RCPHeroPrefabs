using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Prefab.Definitions.Enums;

namespace Assets.Prefab.Marker.Scripts
{
    public class MarkerController : MonoBehaviour
    {
        public Texture TextureNormal;
        public Texture TextureSuccess;

        public MarkerState? _markerState;
        public MarkerState MarkerState
        {
            get => _markerState.Value;
            set
            {
                _markerState = value;

                switch (_markerState)
                {
                    case MarkerState.Success:
                        { MarkerImage.texture = TextureSuccess; }
                        break;
                    case MarkerState.Normal:
                        { MarkerImage.texture = TextureNormal; }
                        break;
                }
            }
        }

        private RawImage MarkerImage => GetComponentInChildren<RawImage>();

        void Start()
        {
            if (_markerState == null)
            { MarkerState = MarkerState.Normal; }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}