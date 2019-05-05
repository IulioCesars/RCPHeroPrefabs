using Assets.Prefab.Definitions;
using Assets.Prefab.Marker.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Prefab.UIController.Scripts
{
    public class CreateMarkerController : MonoBehaviour, INotifyChangeState
    {
        public GameObject MarkerObject;

        private float Speed => 12;//120.0f;
        private float InitialMarkerPoint { get; set; }
        private float DeleteMarkerPoint { get; set; }
        private List<GameObject> Markers { get; set; }
        private Timer TimerCreateMarkets;
        private bool AllowCreateMarker { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            if (!(transform is RectTransform rt))
            { throw new Exception("Este script solo funciona en RectTransform"); }

            Markers = new List<GameObject>();

            InitialMarkerPoint = rt.offsetMin.x;
            DeleteMarkerPoint = rt.offsetMax.x;

            //TimerCreateMarkets = new Timer(600) { AutoReset = true };
            TimerCreateMarkets = new Timer(6000) { AutoReset = true };
            TimerCreateMarkets.Elapsed += TimerCreateMarkets_Elapsed;
        }

        void Update()
        {
            if (AllowCreateMarker)
            {
                CreateNewMarker();
                AllowCreateMarker = false;
            }

            var deltaSpeed = Speed * Time.deltaTime;

            foreach (var m in Markers.ToList())
            {
                if (m == null)
                {
                    Markers.Remove(m);
                    continue;
                }

                m.transform.localPosition = new Vector3(m.transform.localPosition.x + deltaSpeed, 0);

                if (m.transform.localPosition.x >= DeleteMarkerPoint)
                {
                    Markers.Remove(m);
                    Destroy(m);
                }
            }
        }

        void OnDestroy()
        {
            TimerCreateMarkets.Stop();
            TimerCreateMarkets.Dispose();
        }

        private void TimerCreateMarkets_Elapsed(object sender, ElapsedEventArgs e)
        { AllowCreateMarker = true; }

        private void CreateNewMarker()
        {
            var marker = Instantiate(MarkerObject, transform, false);
            marker.transform.localPosition = new Vector3(InitialMarkerPoint, 0, 0);

            Markers.Add(marker);
        }

        public void CreateInitialContraction()
        {
            var marker = Instantiate(MarkerObject, transform, false);
            marker.transform.localPosition = MarkerObject.transform.position;

            var markerController = marker.GetComponent<MarkerController>();
            markerController.MarkerState = Enums.MarkerState.Success;

            Markers.Add(markerController.gameObject);
        }

        public void ChangeState(Enums.GameState gameState)
        {
            switch (gameState)
            {
                case Enums.GameState.Contractions:
                    {
                        AllowCreateMarker = true;
                        TimerCreateMarkets.Start();
                    } break;
                case Enums.GameState.WaitForStart:
                case Enums.GameState.RCP:
                    {
                        TimerCreateMarkets.Stop();
                        Markers.ForEach(it => Destroy(it));
                        Markers.Clear();
                    }
                    break;
            }

            gameObject.SetActive(!(gameState == Enums.GameState.RCP));
        }
    }
}
