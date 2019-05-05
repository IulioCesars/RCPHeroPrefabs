using Assets.Prefab.ArduinoReader.Scripts;
using Assets.Prefab.Definitions;
using Assets.Prefab.Marker.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Prefab.UIController.Scripts
{
    public class InterceptMarkerController : MonoBehaviour, INotifyChangeState
    {
        public Text TextScore;

        private UIController ParentController => GetComponentInParent<UIController>();
        private BindingList<int> Contractions => ParentController?.CurrentCycle?.Contractions;
        private MarkerController IntercepterMarker = null;

        void Start()
        {
            HapticoEvents.Subscribe(GetType(), CheckContraction);
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            { CheckContraction(new HapticoModelInput() { FuerzaContraccion = 40 }); }
        }

        private void CheckContraction(HapticoModelInput hapticoModel)
        {
            var ValidContraction = Configuration.FuerzaContraccionValida(hapticoModel.FuerzaContraccion);

            if (ValidContraction)
            {
                if (ParentController.GameState == Enums.GameState.WaitForStart)
                {
                    ParentController.GameState = Enums.GameState.Contractions;
                    ParentController.createMarkerController.CreateInitialContraction();
                    Contractions.Add(hapticoModel.FuerzaContraccion);
                }
                else if (IntercepterMarker?.MarkerState == Enums.MarkerState.Normal)
                {
                    IntercepterMarker.MarkerState = Enums.MarkerState.Success;
                    Contractions.Add(hapticoModel.FuerzaContraccion);
                }
            }
        }

        void OnTriggerStay(Collider other)
        { IntercepterMarker = other.GetComponent<MarkerController>(); }

        public void ChangeState(Enums.GameState gameState)
        {
            if (gameState == Enums.GameState.RestartCycle)
            {
                Contractions.ListChanged += Contractions_ListChanged;
                Contractions_ListChanged(null, null);
            }
            else if (gameState == Enums.GameState.RCP)
            { gameObject.SetActive(false); }
            else
            { gameObject.SetActive(true); }
        }

        private void Contractions_ListChanged(object sender, ListChangedEventArgs e)
        {
            var _score = Contractions.Count;

            if (_score >= Configuration.LimitContractions)
            { ParentController.GameState = Enums.GameState.RCP; }

            TextScore.text = $"{Contractions.Count}/{Configuration.LimitContractions}";
        }
    }
}
