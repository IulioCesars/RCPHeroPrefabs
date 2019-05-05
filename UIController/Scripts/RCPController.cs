using Assets.Prefab.ArduinoReader.Scripts;
using Assets.Prefab.Definitions;
using Assets.Prefab.UIController.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class RCPController : MonoBehaviour, INotifyChangeState
{
    private Text ScoreText => GetComponentInChildren<Text>();
    private UIController ParentController => GetComponentInParent<UIController>();
    private BindingList<BreathingInfo> Breathings => ParentController?.CurrentCycle?.Breathings;

    void Start()
    { HapticoEvents.Subscribe(GetType(), CheckRespiration); }

    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        { CheckRespiration(new HapticoModelInput() { RespiracionPresionAire = 31 }); }
    }

    private void CheckRespiration(HapticoModelInput hapticoModel)
    {
        if (Configuration.FuerzaRespiracionValida(hapticoModel.RespiracionPresionAire))
        { Breathings.Add(new BreathingInfo(hapticoModel)); }
    }

    public void ChangeState(Enums.GameState gameState)
    {
        if (gameState == Enums.GameState.RestartCycle)
        {
            Breathings.ListChanged += Breathings_ListChanged;
            Breathings_ListChanged(null, null);
        }
        else if (gameState == Enums.GameState.RCP)
        {
            gameObject.SetActive(true);
            Breathings_ListChanged(null, null);
        }
        else
        { gameObject.SetActive(false); }
    }

    private void Breathings_ListChanged(object sender, ListChangedEventArgs e)
    {
        var respirationScore = Breathings.Count;

        if (respirationScore >= Configuration.LimitBreathing)
        { ParentController.GameState = Enums.GameState.RestartCycle; }

        ScoreText.text = $"{respirationScore}/{Configuration.LimitBreathing}";
    }
}
