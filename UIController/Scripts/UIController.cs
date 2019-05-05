using Assets.Prefab.ArduinoReader.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Prefab.Definitions.Enums;

namespace Assets.Prefab.UIController.Scripts
{
    public class BreathingInfo
    {
        public BreathingInfo()
        { }

        public BreathingInfo(HapticoModelInput hapticoModel)
        {
            Force = hapticoModel.RespiracionPresionAire;
            HeadTilt = hapticoModel.InclinacionCabeza;
        }

        public int Force { get; set; }
        public int HeadTilt { get; set; }
    }

    public class RCPCycle
    {
        public RCPCycle()
        {
            Contractions = new BindingList<int>();
            Breathings = new BindingList<BreathingInfo>();
        }

        public BindingList<int> Contractions { get; set; }
        public BindingList<BreathingInfo> Breathings { get; set; }
    }

    public class UIController : MonoBehaviour
    {
        public CreateMarkerController createMarkerController;
        public InterceptMarkerController interceptMarkerController;
        public RCPController RCPController;
        public Text TextCyclesCount;
        public ArduinoController ArduinoReader;

        private GameState _gameState;
        public GameState GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
                OnGameStateChange();
            }
        }

        private BindingList<RCPCycle> Cycles { get; set; }
        private int LimitCountCycles => 3;
        public RCPCycle CurrentCycle => Cycles.LastOrDefault();

        private void OnGameStateChange()
        {
            if (GameState == GameState.RestartCycle)
            { Cycles.Add(new RCPCycle()); }

            createMarkerController.ChangeState(GameState);
            interceptMarkerController.ChangeState(GameState);
            RCPController.ChangeState(GameState);

            //ArduinoReader.WriteSerialPort(GameState.ToString());

            if (GameState == GameState.RestartCycle)
            {
                TextCyclesCount.text = $"Ciclo: {Cycles.Count}/{LimitCountCycles}";

                if (Cycles.Count() > LimitCountCycles)
                { OnFinallyGame(); }
                else
                { GameState = GameState.WaitForStart; }
            }
        }

        private void OnFinallyGame()
        {
            // Aqui se calificara todo el pedo

            Cycles.Clear();
            Cycles.Add(new RCPCycle());
        }

        void Start()
        {
            Cycles = new BindingList<RCPCycle>();
            GameState = GameState.RestartCycle;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
